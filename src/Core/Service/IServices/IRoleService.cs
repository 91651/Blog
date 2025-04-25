using Blog.Model;
using EFCore.DynamicLinq;

namespace Blog.Service
{
    public interface IRoleService
    {
        Task<PageResult<List<RoleModel>>> GetRolesAsync(RoleQueryModel model);
    }
}