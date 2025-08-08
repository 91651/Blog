using Blog.Model;
using EFCore.DynamicLinq;
using Refit;

namespace Blog.Admin.Data;

public interface IAdminApiProvider
{
    #region Auth Api

    [Get("/api/admin/auth/claims")]
    Task<IApiResponse<IDictionary<string, string>>> GetClaimsAsync();

    #endregion Auth Api

    #region Article Api

    [Get("/api/admin/article/{id}")]
    Task<ArticleModel> GetArticleAsync(string id);

    [Get("/api/admin/article")]
    Task<PageResult<List<ArticleListModel>>> GetArticlesAsync([Query] ArticleQueryModel query);

    [Post("/api/admin/article")]
    Task<string> AddArticleAsync(ArticleModel model);

    [Put("/api/admin/article/{id}")]
    Task<bool> UpdateArticleAsync(string id, ArticleModel model);

    [Delete("/api/admin/article/{id}")]
    Task<bool> DeleteArticleAsync(string id);

    #endregion Article Api

    #region Channel Api

    [Get("/api/admin/channel/{id}")]
    Task<ChannelModel> GetChannelAsync(string id);

    [Get("/api/admin/channel")]
    Task<List<ChannelModel>> GetChannelsAsync();

    [Post("/api/admin/channel")]
    Task<string> AddChannelAsync(ChannelModel model);

    [Put("/api/admin/channel/{id}")]
    Task<bool> UpdateChannelAsync(string id, ChannelModel model);

    [Delete("/api/admin/channel/{id}")]
    Task<bool> DeleteChannelAsync(string id);

    #endregion Channel Api

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
    Task<bool> DeleteRoleAsync(string id);

    #endregion Role Api

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
    Task<bool> DeleteUserAsync(string id);

    #endregion User Api
}