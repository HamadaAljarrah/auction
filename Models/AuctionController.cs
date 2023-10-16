using Microsoft.AspNetCore.Mvc;


namespace Presentation.Controllers
{
    public class AuctionController : Controller
    {



        
        public IActionResult Index()
        {
            return View();
        }




         public IActionResult Create()
        {
            return View();
        }
        
    }
}