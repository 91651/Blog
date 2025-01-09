using System.Linq.Expressions;
using App.Business.Model;
using App.DbAccess.Entities.Identity;
using App.DbAccess.Infrastructure;
using App.DbAccess.Repositories;
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
            Expression<Func<User, bool>> ex = t => true;
            if (!string.IsNullOrWhiteSpace(model.Name))
            {
                ex = t => t.Id.Contains(model.Name);
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
