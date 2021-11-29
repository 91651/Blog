using App.Business.Model;
using App.DbAccess.Entities.Identity;
using App.DbAccess.Repositories;
using AutoMapper;

namespace App.Business.Services
{
    public class UserService : IUserService
    {
        private readonly IMapper _mapper;
        private readonly IRepository<User> _userRepository;

        public UserService(IMapper mapper, IRepository<User> userRepository)
        {
            _mapper = mapper;
            _userRepository = userRepository;
        }

        public List<UserModel> GetUsers()
        {
            var users = _userRepository.GetAll().ToList();
            return _mapper.Map<List<UserModel>>(users);
        }
    }
}
