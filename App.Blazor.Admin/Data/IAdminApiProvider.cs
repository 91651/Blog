using App.Business.Model;
using App.EFCore.DynamicLinq;
using Refit;

namespace App.Blazor.Admin.Data
{
    public interface IAdminApiProvider
    {
        #region Role Api
        [Get("/api/role/{id}")]
        Task<ArticleModel> GetRoleAsync(string id);
        [Get("/api/role")]
        Task<PageResult<List<RoleModel>>> GetRolesAsync([Query] RoleQueryModel query);
        [Post("/api/role")]
        Task<bool> AddRoleAsync(RoleModel model);
        [Put("/api/role/{id}")]
        Task<bool> UpdateRoleAsync(string id, RoleModel model);
        [Delete("/api/role/{id}")]
        Task<bool> DelRoleAsync(string id);
        #endregion

    }
}
