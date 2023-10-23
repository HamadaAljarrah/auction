

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

        private void InsertUser(User user)
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
        public async void RegisterUser(User user, bool login)
        {
            user.Email = user.Email.ToLower();
            var newUser = new IdentityUser { UserName = user.Email, Email = user.Email };
            var result = await _userManager.CreateAsync(newUser, user.Password);
            if (result.Succeeded)
            {
                System.Console.WriteLine("Ininininin");

                if (user.Email.CompareTo("hamadasepost@gmail.com") == 0)
                {
                    user.Role = "Admin";
                    await _userManager.AddToRoleAsync(newUser, "Admin");

                }
                else
                {
                    user.Role = "User";
                    await _userManager.AddToRoleAsync(newUser, "User");

                }
                try
                {
                    InsertUser(user);
                }
                catch (ServiceException ex)
                {
                    Console.WriteLine(ex.StackTrace);
                    throw new ServiceException("Error in registering user." + ex.Message, ex);
                }
                if (login)
                {
                    await _signInManager.SignInAsync(newUser, isPersistent: false);
                }
            }
        }

        public User GetCurrnetUser()
        {
            try
            {
                ClaimsPrincipal currentUser = _httpContextAccessor.HttpContext.User;
                string email = currentUser.Identity.Name;
                UserDb userDb = null;
                if (email != null)
                {
                    userDb = _userRepository.GetById(email);

                }
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

        public async Task<bool> IsAdmin()
        {


            ClaimsPrincipal currentUser = _httpContextAccessor.HttpContext.User;
            var user = await _userManager.GetUserAsync(currentUser);
            bool isAdmin = false;
            if (user != null)
            {
                isAdmin = await _userManager.IsInRoleAsync(user, "Admin");
            }

            return isAdmin;


        }

        public IEnumerable<User> GetNoneAdminUsers()
        {
            try
            {
                var usersDb = _userRepository.Find(p => p.Role.CompareTo("User") == 0);
                return _mapper.Map<IEnumerable<User>>(usersDb);

            }
            catch (DatabaseException ex)
            {
                Console.WriteLine(ex.StackTrace);
                throw new ServiceException("Error in get none admin users." + ex.Message, ex);
            }
        }

        public async void DeleteUser(string email)
        {
            try
            {
                var user = await _userManager.FindByEmailAsync(email);
                await _userManager.DeleteAsync(user);
                _userRepository.Delete(email);
            }
            catch (DatabaseException ex)
            {
                throw new ServiceException("Error in delete user." + ex.Message, ex);
            }
        }

        public User GetUserByEmail(string email)
        {

            try
            {
                var usersDb = _userRepository.GetById(email);
                return _mapper.Map<User>(usersDb);

            }
            catch (DatabaseException ex)
            {
                Console.WriteLine(ex.StackTrace);
                throw new ServiceException("Error in get user by email." + ex.Message, ex);
            }
        }

        public void UpdateUsername(string email, string username)
        {
            try
            {
                UserDb auctionDb = _userRepository.GetById(email);
                auctionDb.Username = username;
                _userRepository.Update(auctionDb);
            }
            catch (DatabaseException ex)
            {
                throw new ServiceException("Error in update username." + ex.Message, ex);
            }
        }
    }
}