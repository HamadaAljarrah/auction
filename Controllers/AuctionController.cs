using System.Globalization;
using AutoMapper;
using DistLab2.Core;
using DistLab2.Core.Interfaces;
using DistLab2.Persistence;
using DistLab2.ViewModels;
using Microsoft.AspNetCore.Mvc;


namespace DistLab2.Controllers
{
    public class AuctionController : Controller
    {
        private readonly IMapper _mapper;

     

        private readonly IAuctionService _auctionService;
        public AuctionController(IAuctionService auctionService, IMapper mapper)
        {
            _auctionService = auctionService;
            _mapper = mapper;
        }

        // GET: AuctionsController
        public IActionResult Index()
        {
            var auctions = _auctionService.GetAll();
      

            IEnumerable<AuctionVM> auctionVMs = _mapper.Map<IEnumerable<AuctionVM>>(auctions);

            return View(auctionVMs);

        }

        // GET: AuctionsController/Auction/MyAuctions
        public IActionResult MyAuctions()
        {
            var auctions = _auctionService.GetAll();

            IEnumerable<AuctionVM> auctionVMs = _mapper.Map<IEnumerable<AuctionVM>>(auctions);
            return View(auctionVMs);

        }

        // GET: AuctionsController/Create
        public ActionResult Create()
        {
            return View();
        }


        // GET: AuctionsController/Details/id
        public ActionResult Details(int id)
        {
            var auction = _auctionService.GetById(id);
            Console.WriteLine("in details : id"+id);
            Console.WriteLine("in details : name " + auction.Name);

            //var action = DUMMAY_ACTIONS.Find(p => p.Id == id);//dummy
            AuctionVM auctionVM = _mapper.Map<AuctionVM>(auction);
            return View(auctionVM);
        }

        // GET: AuctionsController/Edit/id
        public ActionResult Edit(int id)
        {
            return View(id);
        }



        // POST: AuctionsController/Create
        [HttpPost]
        public ActionResult Create(IFormCollection formData)
        {
            string name = formData["name"];
            string description = formData["description"];
            if (!DateTime.TryParse(formData["endDate"], out DateTime endDate))
            {
                // Return an error view
            }
            if (!double.TryParse(formData["startPrice"], NumberStyles.Float, CultureInfo.InvariantCulture, out double startPrice))
            {
                // Return an error view
            }
            System.Console.WriteLine("-----------------------------");
            System.Console.WriteLine("Createing auction with values: ");
            System.Console.WriteLine("Name: " + name);
            System.Console.WriteLine("Description: " + description);
            System.Console.WriteLine("End date: " + endDate);
            System.Console.WriteLine("Start price: " + startPrice);
            System.Console.WriteLine("Image: " + "TODO");
            System.Console.WriteLine("-----------------------------");


            Auction auction = new Auction
            {
                Name = name,
                Description = description,
                StartingPrice = (int)startPrice,
                CreatedDate = DateTime.Now,  
                EndDate = endDate
            };

            _auctionService.CreateAuction(auction);    
            return View();
        }


        // POST: AuctionsController/Edit/id
        [HttpPost]
        public ActionResult Edit(int id, IFormCollection formData)
        {
            string description = formData["description"];
            Console.WriteLine("Editing auction with ID: " + id+ "\nNew description: " + description);
            return View();
        }

         // POST: AuctionsController/Delete/id
        [HttpPost]
        public ActionResult Delete(int id)
        {
            Console.WriteLine("Deleting auction with ID: " + id);
            //return View("MyAuctions", DUMMAY_ACTIONS);
            return View();//send redirect

        }
        [HttpPost]
        public IActionResult PlaceBid(int Id,int Amount)
        {
            Console.WriteLine(Id);
            Console.WriteLine(Amount);
            Console.WriteLine("_____");
            // Get the auction by its ID
            var auction = _auctionService.GetById(Id);

            if (auction == null)
            {
                // Handle the case where the auction with the given ID does not exist
                return NotFound();
            }

            // Place the bid
            _auctionService.PlaceBid(auction.Id, Amount);

            // Redirect to the auction details page
            return RedirectToAction("Details", new { Id });
        }
    }
}