using System.Linq.Expressions;
using App.Business.Model;
using App.DbAccess.Entities;
using App.DbAccess.Repositories;
using App.EFCore.DynamicLinq;
using App.Util;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using File = App.DbAccess.Entities.File;

namespace App.Business.Services
{
    public class ArticleService : IArticleService
    {
        private readonly IMapper _mapper;
        private readonly IRepository<Article> _articleRepository;
        private readonly IRepository<Channel> _channleRepository;
        private readonly IRepository<File> _fileRepository;

        public ArticleService(IMapper mapper, IRepository<Article> articleRepository, IRepository<Channel> channleRepository, IRepository<File> fileRepository)
        {
            _mapper = mapper;
            _articleRepository = articleRepository;
            _channleRepository = channleRepository;
            _fileRepository = fileRepository;
        }

        public async Task<string> AddArticleAsync(ArticleModel model)
        {
            model.Created = DateTime.Now;
            model.Updated = DateTime.Now;
            model.State = 1;
            var entity = _mapper.Map<Article>(model);
            entity.Id = Guid.NewGuid().ToString(10);
            await _articleRepository.AddAsync(entity);
            //包含的文件处理
            await _fileRepository.GetAll().Where(f => model.Files.Contains(f.Id)).ForEachAsync(f => f.OwnerId = entity.Id);

            await _articleRepository.SaveChangesAsync();
            return entity.Id;
        }

        public async Task<bool> DelArticleAsync(string id)
        {
            var entity = await _articleRepository.GetByIdAsync(id);
            entity.State = 0;
            var rows = await _articleRepository.SaveChangesAsync();
            return rows > 0;
        }
        public async Task<bool> UpdateArticleAsync(ArticleModel model)
        {
            model.Updated = DateTime.Now;
            var entity = _mapper.Map<Article>(model);
            _articleRepository.Update(entity);
            _articleRepository.Entry(entity).Property(nameof(entity.Created)).IsModified = false;
            _articleRepository.Entry(entity).Property(nameof(entity.UserId)).IsModified = false;
            //包含的文件处理
            await _fileRepository.GetAll().Where(f => model.Files.Contains(f.Id)).ForEachAsync(f => f.OwnerId = entity.Id);

            var rows = await _articleRepository.SaveChangesAsync();
            return rows > 0;
        }

        public async Task<ArticleModel> GetArticleAsync(string id)
        {
            var query = _articleRepository.GetAll().Where(a => a.Id.EndsWith(id));
            var model = await _mapper.ProjectTo<ArticleModel>(query).FirstOrDefaultAsync();
            return model;
        }
        public async Task<PageResult<List<ArticleListModel>>> GetArticlesAsync(ArticleQueryModel model)
        {
            model.Sort = new List<Sort> { new Sort { Field = "Created", Desc = true } };
            Expression<Func<Article, bool>> ex = t => true;
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
            var users = await _articleRepository.GetAll().Include(i => i.Channel).Include(i => i.User).Include(i => i.Comments).Include(i => i.Files).Where(ex).ToDataSourceResultAsync(model);
            return new PageResult<List<ArticleListModel>>
            {
                Data = _mapper.Map<List<ArticleListModel>>(users.Data),
                Total = users.Total
            };
        }
    }
}
