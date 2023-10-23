

using AutoMapper;
using DistLab2.Core;
using DistLab2.Core.Error;
using DistLab2.Core.Interfaces;
using DistLab2.ViewModels;
using Filter;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace DistLab2.Controllers
{
    [Authorize(Roles = "Admin")]
    public class AdminController : Controller
    {

        private readonly IMapper _mapper;
        private readonly IAuctionService _auctionService;
        private readonly IUserService _userService;


        public AdminController(IUserService userService, IAuctionService auctionService, IMapper mapper)
        {
            _auctionService = auctionService;
            _mapper = mapper;
            _userService = userService;
        }

        // GET: AdminController/
        public IActionResult Index()
        {
            try
            {
                var users = _userService.GetNoneAdminUsers();
                var viewUsers = _mapper.Map<IEnumerable<RegisterVM>>(users);
                return View(viewUsers);
            }
            catch (ServiceException ex)
            {
                return View("Error", ex.Message);
            }
        }


        // GET: AdminController/RegisterUser
        public IActionResult RegisterUser()
        {
            return View();
        }


        [HttpPost]
        public IActionResult DeleteUser(string email)
        {
            _userService.DeleteUser(email);
            return RedirectToAction("Index");
        }

        [HttpPost]
        public IActionResult ChangeUsername(string email, string username)
        {
            try
            {
                _userService.UpdateUsername(email, username);
                return RedirectToAction("Index");
            }
            catch (ServiceException ex)
            {
                return View("Error", ex.Message);
            }

        }

        public IActionResult ChangeUsername(string email)
        {
            try
            {
                var user = _userService.GetUserByEmail(email);
                var viewUser = _mapper.Map<RegisterVM>(user);
                return View(viewUser);
            }
            catch (ServiceException ex)
            {
                return View("Error", ex.Message);
            }
        }


        [HttpPost]
        public IActionResult AddUser(RegisterVM user)
        {
            try
            {
                _userService.RegisterUser(_mapper.Map<User>(user), false);
                return RedirectToAction("Index");
            }
            catch (ServiceException ex)
            {
                Console.WriteLine(ex.StackTrace);
                return View("Error", ex.Message);
            }
        }

         public IActionResult AddUser()
        {
           return View();
        }


    }
}