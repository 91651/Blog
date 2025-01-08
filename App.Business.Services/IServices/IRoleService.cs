using App.Business.Model;
using App.EFCore.DynamicLinq;

namespace App.Business.Services
{
    public interface IRoleService
    {
        Task<PageResult<List<RoleModel>>> GetRolesAsync(RoleQueryModel model);
    }
}