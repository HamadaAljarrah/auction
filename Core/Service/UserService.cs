

using System.Security.Claims;
using AutoMapper;
using DistLab2.Core.Error;
using DistLab2.Core.Interfaces;
using DistLab2.Persistence;
using DistLab2.Persistence.Error;
using Microsoft.AspNetCore.Identity;
using Persistence.Repository;

namespace DistLab2.Core
{
    public class UserService : IUserService
    {


        private readonly IMapper _mapper;
        private readonly IRepository<UserDb> _userRepository;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly SignInManager<IdentityUser> _signInManager;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public UserService(IHttpContextAccessor httpContextAccessor, IRepository<UserDb> userRepository, IMapper mapper, UserManager<IdentityUser> userManager, SignInManager<IdentityUser> signInManager)
        {
            _userRepository = userRepository;
            _mapper = mapper;
            _userManager = userManager;
            _signInManager = signInManager;
            _httpContextAccessor = httpContextAccessor;
        }

        private void CreateUser(User user)
        {
            try
            {
                UserDb adb = _mapper.Map<UserDb>(user);
                _userRepository.Insert(adb);

            }
            catch (DatabaseException ex)
            {
                throw new ServiceException("Error in create user.", ex);
            }
        }
        public async void RegisterUser(User user)
        {
            var newUser = new IdentityUser { UserName = user.Email, Email = user.Email };
            var result = await _userManager.CreateAsync(newUser, user.Password);

            if (result.Succeeded)
            {
                try
                {
                    CreateUser(user);
                }
                catch (ServiceException ex)
                {
                    Console.WriteLine(ex.StackTrace);
                    throw new ServiceException("Error in registering user." + ex.Message, ex);
                }
                await _signInManager.SignInAsync(newUser, isPersistent: false);
            }
        }

        public User GetCurrnetUser()
        {
            try
            {
                ClaimsPrincipal currentUser = _httpContextAccessor.HttpContext.User;
                string email = currentUser.Identity.Name;
                UserDb userDb = _userRepository.GetById(email);
                return _mapper.Map<User>(userDb);
            }
            catch (DatabaseException ex)
            {
                Console.WriteLine(ex.StackTrace);
                throw new ServiceException("Error in registering user." + ex.Message, ex);
            }

        }



        public async Task<bool> LoginUser(User user)
        {
            var result = await _signInManager.PasswordSignInAsync(user.Email, user.Password, false, false);
            return result.Succeeded;
        }


        public async void LogoutUser()
        {
            await _signInManager.SignOutAsync();
        }

        public bool IsAuthenticated()
        {
            ClaimsPrincipal currentUser = _httpContextAccessor.HttpContext.User;
            return currentUser.Identity.IsAuthenticated;
        }
    }
}