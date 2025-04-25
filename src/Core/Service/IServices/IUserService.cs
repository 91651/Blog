using Blog.Model;
using EFCore.DynamicLinq;

namespace Blog.Service
{
    public interface IUserService
    {
        List<UserModel> GetUsers();

        Task<PageResult<List<UserModel>>> GetUsersAsync(UserQueryModel model);
    }
}