using App.Business.Model;

namespace App.Business.Services
{
    public interface IUserService
    {
        List<UserModel> GetUsers();
    }
}