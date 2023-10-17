using DistLab2.Core.Interfaces;
using DistLab2.ViewModels;
using Microsoft.AspNetCore.Mvc;


namespace DistLab2.Controllers
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
            List<AuctionVM> data = new()
            {
                new AuctionVM
                {
                    Name = "Monalisa",
                    Description = "Painting, the expression of ideas and emotions, with the creation of certain aesthetic qualities, in a two-dimensional visual language. The elements of this language, its shapes, lines, colours, tones, and texture are used in various ways to produce sensations of volume, space, movement, and light on",
                    CreatedDate = DateTime.Now,
                    StartingPrice = 300,
                    Bids = new()
                },
                 new AuctionVM
                {
                    Name = "Aftica",
                    Description = "Painting, the expression of ideas and emotions, with the creation of certain aesthetic qualities, in a two-dimensional visual language. The elements of this language, its shapes, lines, colours, tones, and texture are used in various ways to produce sensations of volume, space, movement, and light on",
                    CreatedDate = DateTime.Now,
                    StartingPrice = 500,
                    Bids = new()
                },
            };

            return View(data);

        }

        // GET: AuctionsController/Create
        public ActionResult Create()
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



        // GET: AuctionsController/Details
        public ActionResult Details()
        {
            return View();
        }
    }
}