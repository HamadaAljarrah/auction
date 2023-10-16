using DistLab2.Controllers;
using DistLab2.Core;
using DistLab2.Core.Interfaces;
using DistLab2.ViewModels;
using Microsoft.AspNetCore.Mvc;


namespace Controllers
{
    public class AuctionController : Controller
    {


        private readonly IAuctionService _auctionService;
        public AuctionController(IAuctionService auctionService)
        {
            _auctionService = auctionService;
        }

        // GET: AuctionsController
        public IActionResult Index()
        {
            return View();

        }

        // POST: AuctionsController/Create
        [HttpPost]
        public ActionResult Create(IFormCollection formData)
        {
            Console.WriteLine(formData["name"]);
            Console.WriteLine(formData["endDate"]);

            DateTime.TryParse(formData["endDate"], out DateTime endDate);
      
            Console.WriteLine(formData["description"]);
            Console.WriteLine(formData["startPrice"]);

             _auctionService.CreateAuction(1, formData["name"], endDate, formData["description"]);
            return View();
        }

    }
}