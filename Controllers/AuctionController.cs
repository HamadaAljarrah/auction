using AutoMapper;
using Filter;
using DistLab2.Core;
using DistLab2.Core.Error;
using DistLab2.Core.Interfaces;
using DistLab2.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;


namespace DistLab2.Controllers
{
    [ServiceFilter(typeof(AuthFilter))]
    public class AuctionController : Controller
    {
        private readonly IMapper _mapper;
        private readonly IAuctionService _auctionService;
        private readonly IUserService _userService;


        public AuctionController(IUserService userService, IAuctionService auctionService, IMapper mapper, UserManager<IdentityUser> userManager)
        {
            _auctionService = auctionService;
            _mapper = mapper;
            _userService = userService;
        }

        // GET: AuctionsController
        public IActionResult Index()
        {
            try
            {
                var auctions = _auctionService.GetUnownedAuctions(User.Identity.Name);
                var viewAuction = _mapper.Map<IEnumerable<AuctionVM>>(auctions);
                return View(viewAuction);
            }
            catch (ServiceException ex)
            {
                Console.WriteLine(ex.StackTrace);
                return View("Error", ex.Message);
            }

        }



        // GET: AuctionsController/Auction/MyAuctions
        public IActionResult MyAuctions(string email)
        {
            try
            {
                var myacutions = _auctionService.GetUserAuctions(email);
                var viewAuction = _mapper.Map<IEnumerable<AuctionVM>>(myacutions);
                return View(viewAuction);
            }
            catch (ServiceException ex)
            {
                Console.WriteLine(ex.StackTrace);
                return View("Error", ex.Message);
            }

        }

        // GET: AuctionsController/Create
        public ActionResult Create()
        {
            return View();
        }

        // GET: AuctionsController/Details/id
        public ActionResult Details(int id)
        {
            var auction = _auctionService.GetAuctionWithBids(id);
            Console.WriteLine("in details : id" + id);
            Console.WriteLine("in details : name " + auction.Name);
            if (auction.Bids != null)
            {
                Console.WriteLine("Bids found:");
                foreach (var bid in auction.Bids)
                {
                    Console.WriteLine("Bid from controller: " + bid.Id);
                }
            }
            else
            {
                Console.WriteLine("No bids found for this auction.");
            }
            return View(_mapper.Map<AuctionVM>(auction));
        }

        // GET: AuctionsController/Edit/id
        public ActionResult Edit(int id)
        {
            return View(id);
        }
        // POST: AuctionsController/Create
        [HttpPost]
        public async Task<ActionResult> Create(string name, string description, DateTime endDate, int startPrice, IFormFile image)
        {
            string email = _userService.GetCurrnetUser().Email;
            AuctionVM auction = new AuctionVM()
            {
                Name = name,
                Description = description,
                StartingPrice = startPrice,
                CreatedDate = DateTime.Now,
                EndDate = endDate,
                UserId = email,
            };

            if (image != null && image.Length > 0)
            {
                using var stream = new MemoryStream();
                await image.CopyToAsync(stream);
                // Now you have the image data in stream.ToArray()
                auction.Image = stream.ToArray();
            }
            _auctionService.CreateAuction(_mapper.Map<Auction>(auction));
            return RedirectToAction("MyAuctions", new { email });
        }


        // POST: AuctionsController/Edit/id
        [HttpPost]
        public async Task<ActionResult> Edit(string description, int id)
        {
             string email = _userService.GetCurrnetUser().Email;
            bool isOwn = _auctionService.GetById(id).UserId.CompareTo(email) != 0;
            bool isAdmin = await _userService.IsAdmin();

            if (!isAdmin || !isOwn)
            {
                return Forbid();
            }
            try
            {
                _auctionService.UpdateDescription(description, id);
                return RedirectToAction("MyAuctions", new{email});
            }
            catch (ServiceException ex)
            {
                Console.WriteLine(ex.StackTrace);
                return View("Error", ex.Message);
            }

        }

        // POST: AuctionsController/Delete/id
        [HttpPost]
        public async Task<ActionResult> Delete(int id)
        {
            string email = _userService.GetCurrnetUser().Email;
            bool isOwn = _auctionService.GetById(id).UserId.CompareTo(email) == 0;
            bool isAdmin = await _userService.IsAdmin();

            if (!isAdmin && !isOwn)
            {
                return Forbid();
            }
            try
            {
                _auctionService.DeleteAuction(id);
                return RedirectToAction("MyAuctions", new { email });

            }
            catch (ServiceException ex)
            {
                Console.WriteLine(ex.StackTrace);
                return View("Error", ex.Message);
            }

        }
        [HttpPost]
        public IActionResult PlaceBid(int Id, int Amount)
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
            BidVM bid = new() { Amount = Amount, AuctionId = Id, UserId = User.Identity.Name };
            _auctionService.PlaceBid(_mapper.Map<Bid>(bid));
            // Redirect to the auction details page
            return RedirectToAction("Details", new { Id });
        }


      
        // GET: AuctionsController/Auction/MyAuctions
        public IActionResult AuctionUserWon()
        {

            try
            {
                var myacutions = _auctionService.GetAuctionsUserWon(User.Identity.Name);
                var viewAuction = _mapper.Map<IEnumerable<AuctionVM>>(myacutions);
                return View("Index", viewAuction);
            }
            catch (ServiceException ex)
            {
                Console.WriteLine(ex.StackTrace);
                return View("Error", ex.Message);
            }

        }
        // GET: AuctionsController/Auction/MyAuctions
        public IActionResult AuctionUserBidIn()
        {

            try
            {
                var myacutions = _auctionService.GetAuctionsUserBidIn(User.Identity.Name);
                var viewAuction = _mapper.Map<IEnumerable<AuctionVM>>(myacutions);
                return View("Index", viewAuction);
            }
            catch (ServiceException ex)
            {
                Console.WriteLine(ex.StackTrace);
                return View("Error", ex.Message);
            }

        }

    }
}