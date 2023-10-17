using Microsoft.AspNetCore.Mvc;

namespace DistLab2.Controllers
{
    public class AuthController : Controller
    {

        public IActionResult Login()
        {
            return View();
        }

        public IActionResult Register()
        {
            return View();
        }

        

    }
}