using AutoMapper;
using DistLab2.Core;
using DistLab2.Core.Error;
using DistLab2.Core.Interfaces;
using DistLab2.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DistLab2.Controllers
{
    public class AuthController : Controller
    {



        private readonly IUserService _userService;
        private readonly IMapper _mapper;

        public AuthController(IUserService userService, IMapper mapper)
        {

            _userService = userService;
            _mapper = mapper;

        }


        public IActionResult Login()
        {
            if (_userService.IsAuthenticated())
            {
                return RedirectToAction("Index", "Home");
            }
            return View();

        }

        public IActionResult Register()
        {
            if (_userService.IsAuthenticated())
            {
                return RedirectToAction("Index", "Home");
            }
            return View();
        }


        [HttpPost]
        public IActionResult Register(RegisterVM model)
        {

            if (ModelState.IsValid)
            {
                try
                {
                    _userService.RegisterUser(_mapper.Map<User>(model));
                    return RedirectToAction("Index", "Home");
                }
                catch (ServiceException ex)
                {
                    Console.WriteLine(ex.StackTrace);
                    return View("Error", ex.Message);
                }
            }

            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginVM model)
        {
            if (ModelState.IsValid)
            {
                var result = await _userService.LoginUser(_mapper.Map<User>(model));
                if (result)
                {
                    return RedirectToAction("Index", "Home");
                }

                ModelState.AddModelError("", "Wrong email or password.");
            }
            return View();
        }


        [Authorize]
        public IActionResult Logout()
        {
            _userService.LogoutUser();
            return RedirectToAction("Index", "Home");
        }


    }
}