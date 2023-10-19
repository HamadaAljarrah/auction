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

        private readonly List<AuctionVM> DUMMAY_ACTIONS = new()
            {
                new AuctionVM
                {
                    Name = "Monalisa",
                    Description = "Painting, the expression of ideas and emotions, with the creation of certain aesthetic qualities, in a two-dimensional visual language. The elements of this language, its shapes, lines, colours, tones, and texture are used in various ways to produce sensations of volume, space, movement, and light on",
                    CreatedDate = DateTime.Now,
                    StartingPrice = 300,
                    EndingDate = DateTime.Now,
                    Bids = new()
                },
                 new AuctionVM
                {
                    Name = "Africa",
                    Description = "Painting, the expression of ideas and emotions, with the creation of certain aesthetic qualities, in a two-dimensional visual language. The elements of this language, its shapes, lines, colours, tones, and texture are used in various ways to produce sensations of volume, space, movement, and light on",
                    CreatedDate = DateTime.Now,
                    StartingPrice = 500,
                    Bids = new() 
                },
            };

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
           
            return View(DUMMAY_ACTIONS);

        }

        // GET: AuctionsController/Create
        public ActionResult Create()
        {
            return View();
        }


        // GET: AuctionsController/Details/id
        public ActionResult Details(int id)
        {
            var action = DUMMAY_ACTIONS.Find(p => p.Id == id); 
            return View(action);
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
            return View();
        }


        // PUT: AuctionsController/Edit/id
        [HttpPut("{id}")]
        public ActionResult Edit(IFormCollection formData, int id)
        {
            string description = formData["description"];
            
            System.Console.WriteLine("-----------------------------");
            System.Console.WriteLine("Editing auction with ID: " + id);
            System.Console.WriteLine("New description: " + description);
            System.Console.WriteLine("-----------------------------");
            return View();
        }
    }
}