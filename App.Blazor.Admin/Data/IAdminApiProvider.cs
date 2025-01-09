using App.Business.Model;
using App.EFCore.DynamicLinq;
using Refit;

namespace App.Blazor.Admin.Data
{
    public interface IAdminApiProvider
    {
        #region Role Api
        [Get("/api/admin/role/{id}")]
        Task<ArticleModel> GetRoleAsync(string id);
        [Get("/api/admin/role")]
        Task<PageResult<List<RoleModel>>> GetRolesAsync([Query] RoleQueryModel query);
        [Post("/api/admin/role")]
        Task<bool> AddRoleAsync(RoleModel model);
        [Put("/api/admin/role/{id}")]
        Task<bool> UpdateRoleAsync(string id, RoleModel model);
        [Delete("/api/admin/role/{id}")]
        Task<bool> DelRoleAsync(string id);
        #endregion

        #region User Api
        [Get("/api/admin/user/{id}")]
        Task<ArticleModel> GetUserAsync(string id);
        [Get("/api/admin/user")]
        Task<PageResult<List<UserModel>>> GetUsersAsync([Query] UserQueryModel query);
        [Post("/api/admin/user")]
        Task<bool> AddUserAsync(UserModel model);
        [Put("/api/admin/user/{id}")]
        Task<bool> UpdateUserAsync(string id, UserModel model);
        [Delete("/api/admin/user/{id}")]
        Task<bool> DelUserAsync(string id);
        #endregion

    }
}
