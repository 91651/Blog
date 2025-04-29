using System.Linq.Expressions;
using AutoMapper;
using Blog.Data;
using Blog.Data.Entities.Identity;
using Blog.Model;
using EFCore.DynamicLinq;

namespace Blog.Service;

public class RoleService : IRoleService
{
    private readonly AppDbContext _db;
    private readonly IMapper _mapper;

    public RoleService(AppDbContext db, IMapper mapper)
    {
        _db = db;
        _mapper = mapper;
    }

    public async Task<PageResult<List<RoleModel>>> GetRolesAsync(RoleQueryModel model)
    {
        Expression<Func<Role, bool>> ex = t => true;
        if (!string.IsNullOrWhiteSpace(model.Name))
        {
            ex = t => t.Name.Contains(model.Name);
        }
        var data = await _db.Roles.Where(ex).ToDataSourceResultAsync(model);
        return new PageResult<List<RoleModel>>
        {
            Data = _mapper.Map<List<RoleModel>>(data.Data),
            Total = data.Total
        };
    }
}