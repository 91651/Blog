using System.Linq.Expressions;
using App.Business.Model;
using App.DbAccess.Entities;
using App.DbAccess.Infrastructure;
using App.EFCore.DynamicLinq;
using App.Util;
using AutoMapper;
using Microsoft.EntityFrameworkCore;

namespace App.Business.Services
{
    public class ArticleService : IArticleService
    {
        private readonly AppDbContext _db;
        private readonly IMapper _mapper;

        public ArticleService(AppDbContext db, IMapper mapper)
        {
            _db = db;
            _mapper = mapper;
        }

        public async Task<string> AddArticleAsync(ArticleModel model)
        {
            model.Created = DateTime.Now;
            model.Updated = DateTime.Now;
            model.State = 1;
            var entity = _mapper.Map<Article>(model);
            entity.Id = Guid.NewGuid().ToString(10);
            await _db.Articles.AddAsync(entity);
            //包含的文件处理
            if (model.Files != null)
            {
                await _db.Files.Where(f => model.Files.Contains(f.Id)).ForEachAsync(f => f.OwnerId = entity.Id);
            }

            await _db.SaveChangesAsync();
            return entity.Id;
        }

        public async Task<bool> DeleteArticleAsync(string id)
        {
            var entity = await _db.Articles.FindAsync(id);
            entity.State = 0;
            var rows = await _db.SaveChangesAsync();
            return rows > 0;
        }
        public async Task<bool> UpdateArticleAsync(ArticleModel model)
        {
            model.Updated = DateTime.Now;
            var entity = _mapper.Map<Article>(model);
            _db.Articles.Update(entity);
            _db.Entry(entity).Property(nameof(entity.Created)).IsModified = false;
            _db.Entry(entity).Property(nameof(entity.UserId)).IsModified = false;
            //包含的文件处理
            if (model.Files != null)
            {
                await _db.Files.Where(f => model.Files.Contains(f.Id)).ForEachAsync(f => f.OwnerId = entity.Id);
            }

            var rows = await _db.SaveChangesAsync();
            return rows > 0;
        }

        public async Task<ArticleModel> GetArticleAsync(string id)
        {
            var query = _db.Articles.Where(a => a.Id.EndsWith(id));
            var model = await _mapper.ProjectTo<ArticleModel>(query).FirstOrDefaultAsync();
            return model;
        }
        public async Task<PageResult<List<ArticleListModel>>> GetArticlesAsync(ArticleQueryModel model)
        {
            model.Sort = new List<Sort> { new Sort { Field = "Created", Desc = true } };
            Expression<Func<Article, bool>> ex = t => true;
            ex = t => t.State != 0;
            if (!string.IsNullOrWhiteSpace(model.Id))
            {
                ex = t => t.Id.Contains(model.Id);
            }
            if (!string.IsNullOrWhiteSpace(model.CreatedDate))
            {
                DateTime.TryParse(model.CreatedDate, out var createdDate);
                ex = ex.And(t => t.Created.Date == createdDate);
            }
            if (!string.IsNullOrWhiteSpace(model.ChannelId))
            {
                ex = ex.And(t => t.ChannelId == model.ChannelId);
            }
            if (!string.IsNullOrWhiteSpace(model.Keyword))
            {
                ex = ex.And(t => t.Title.Contains(model.Keyword) || t.SubTitle.Contains(model.Keyword) || t.Summary.Contains(model.Keyword) || t.Content.Contains(model.Keyword));
            }
            var data = await _db.Articles.Include(i => i.Channel).Include(i => i.User).Include(i => i.Comments).Include(i => i.Files).Where(ex).ToDataSourceResultAsync(model);
            return new PageResult<List<ArticleListModel>>
            {
                Data = _mapper.Map<List<ArticleListModel>>(data.Data),
                Total = data.Total
            };
        }

        public async Task<ArticleModel> GetPrevArticleAsync(string id)
        {
            var article = await _db.Articles.FindAsync(id);
            var entity = await _db.Articles.Where(a => a.ChannelId == article.ChannelId && a.Updated < article.Updated).OrderByDescending(o => o.Updated).FirstOrDefaultAsync();
            var model = _mapper.Map<ArticleModel>(entity);
            return model;
        }
        public async Task<ArticleModel> GetNextArticleAsync(string id)
        {
            var article = await _db.Articles.FindAsync(id);
            var entity = await _db.Articles.Where(a => a.ChannelId == article.ChannelId && a.Updated > article.Updated).OrderBy(o => o.Updated).FirstOrDefaultAsync();
            var model = _mapper.Map<ArticleModel>(entity);
            return model;
        }
        public async Task<int> UpdateArticleViewedAsync(string id)
        {
            var article = await _db.Articles.FindAsync(id);
            article.Viewed++;
            await _db.SaveChangesAsync();
            return article.Viewed;
        }
    }
}
