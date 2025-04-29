using System.Linq.Expressions;
using AutoMapper;
using Blog.Data;
using Blog.Data.Entities;
using Blog.Model;
using EFCore.DynamicLinq;
using Util;

namespace Blog.Service;

public class UrlRewriteRuleService : IUrlRewriteRuleService
{
    private readonly AppDbContext _db;
    private readonly IMapper _mapper;

    public UrlRewriteRuleService(AppDbContext db, IMapper mapper)
    {
        _db = db;
        _mapper = mapper;
    }

    public async Task<string> AddUrlRewriteRuleAsync(UrlRewriteRuleModel model)
    {
        var entity = _mapper.Map<UrlRewriteRule>(model);
        entity.Id = Guid.NewGuid().ToString(10);
        await _db.UrlRewriteRules.AddAsync(entity);
        await _db.SaveChangesAsync();
        return entity.Id;
    }

    public async Task<bool> DeleteUrlRewriteRuleAsync(string id)
    {
        var entity = await _db.UrlRewriteRules.FindAsync(id);
        entity.Status = 0;
        var rows = await _db.SaveChangesAsync();
        return rows > 0;
    }

    public async Task<bool> UpdateUrlRewriteRuleAsync(UrlRewriteRuleModel model)
    {
        var entity = _mapper.Map<UrlRewriteRule>(model);
        _db.UrlRewriteRules.Update(entity);
        var rows = await _db.SaveChangesAsync();
        return rows > 0;
    }

    public async Task<PageResult<List<UrlRewriteRuleModel>>> GetUrlRewriteRulesAsync(UrlRewriteRuleQueryModel model)
    {
        Expression<Func<UrlRewriteRule, bool>> ex = t => true;
        if (!string.IsNullOrWhiteSpace(model.Regex))
        {
            ex = t => t.Regex.Contains(model.Regex);
        }
        if (!string.IsNullOrWhiteSpace(model.Replacement))
        {
            ex = t => t.Replacement.Contains(model.Replacement);
        }
        if (model.StatusCode != null)
        {
            ex = t => t.StatusCode == model.StatusCode;
        }
        var data = await _db.UrlRewriteRules.Where(ex).ToDataSourceResultAsync(model);
        return new PageResult<List<UrlRewriteRuleModel>>
        {
            Data = _mapper.Map<List<UrlRewriteRuleModel>>(data.Data),
            Total = data.Total
        };
    }
}