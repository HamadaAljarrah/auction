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
        [ValidateAntiForgeryToken]
        public ActionResult Create(int auctionId, string name, DateTime endTime, string description)
        {
            _auctionService.CreateAuction(1, name, endTime, description);
            return View();
        }

    }
}