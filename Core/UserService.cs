

using AutoMapper;
using DistLab2.Core.Interfaces;
using DistLab2.Persistence;

namespace DistLab2.Core
{
    public class UserService : IUserService
    {


          private readonly IReposetory<UserDb> _userRepository;
        private readonly IMapper  _mapper;
        public UserService(IReposetory<UserDb> userRepository, IMapper mapper) {
            _userRepository = userRepository;
            _mapper = mapper;
        }


        public void CreateUser(User user)
        {
            UserDb userDb = _mapper.Map<UserDb>(user);

            _userRepository.Add(userDb);
        }
    }
}