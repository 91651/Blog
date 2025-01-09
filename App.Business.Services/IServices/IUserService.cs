using App.Business.Model;
using App.EFCore.DynamicLinq;

namespace App.Business.Services
{
    public interface IUserService
    {
        List<UserModel> GetUsers();
        Task<PageResult<List<UserModel>>> GetUsersAsync(UserQueryModel model);
    }
}