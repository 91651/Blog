using App.Business.Services.Models;

namespace App.Business.Services
{
    public interface IUserService
    {
        List<UserModel> GetUsers();
    }
}