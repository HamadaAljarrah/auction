

using AutoMapper;
using DistLab2.Core.Error;
using DistLab2.Core.Interfaces;
using DistLab2.Persistence;
using DistLab2.Persistence.Error;
using Persistence.Repository;

namespace DistLab2.Core
{
    public class UserService : IUserService
    {


        private readonly IRepository<UserDb> _userRepository;
        private readonly IMapper _mapper;
        public UserService(IRepository<UserDb> userRepository, IMapper mapper)
        {
            _userRepository = userRepository;
            _mapper = mapper;
        }


        public void CreateUser(User user)
        {
            try
            {
                UserDb userDb = _mapper.Map<UserDb>(user);
                _userRepository.Insert(userDb);
            }
            catch (DatabaseException ex)
            {
                throw new ServiceException("Error in create user.", ex);
            }

        }

        public User GetUsername(string email)
        {
            try
            {
                UserDb user = _userRepository.GetById(email);
                return _mapper.Map<User>(user);
            }
            catch (DatabaseException ex)
            {
                throw new ServiceException("Error in get username.", ex);
            }
        }
    }
}