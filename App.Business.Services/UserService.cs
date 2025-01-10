using System.Linq.Expressions;
using App.Business.Model;
using App.DbAccess.Entities.Identity;
using App.DbAccess.Infrastructure;
using App.EFCore.DynamicLinq;
using AutoMapper;

namespace App.Business.Services
{
    public class UserService : IUserService
    {
        private readonly AppDbContext _db;
        private readonly IMapper _mapper;

        public UserService(AppDbContext db, IMapper mapper)
        {
            _db = db;
            _mapper = mapper;
        }

        public List<UserModel> GetUsers()
        {
            var users = _db.Users.ToList();
            return _mapper.Map<List<UserModel>>(users);
        }

        public async Task<PageResult<List<UserModel>>> GetUsersAsync(UserQueryModel model)
        {
            Expression<Func<User, bool>> ex = u => true;
            if (!string.IsNullOrWhiteSpace(model.UserName))
            {
                ex = u => u.Id.Contains(model.UserName);
            }
            if (!string.IsNullOrWhiteSpace(model.UserName))
            {
                ex = u => u.Id.Contains(model.UserName);
            }
            if (!string.IsNullOrWhiteSpace(model.PhoneNumber))
            {
                ex = u => u.Id.Contains(model.PhoneNumber);
            }
            if (!string.IsNullOrWhiteSpace(model.Email))
            {
                ex = u => u.Id.Contains(model.Email);
            }
            var data = await _db.Users.Where(ex).ToDataSourceResultAsync(model);
            return new PageResult<List<UserModel>>
            {
                Data = _mapper.Map<List<UserModel>>(data.Data),
                Total = data.Total
            };
        }
    }
}
