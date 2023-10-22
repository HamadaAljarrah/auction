using AutoMapper;
using Filter;
using DistLab2.Core;
using DistLab2.Core.Error;
using DistLab2.Core.Interfaces;
using DistLab2.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;


namespace DistLab2.Controllers
{
     [ServiceFilter(typeof(AuthFilter))]
    public class AuctionController : Controller
    {
        private readonly IMapper _mapper;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly IAuctionService _auctionService;

        public AuctionController(IAuctionService auctionService, IMapper mapper, UserManager<IdentityUser> userManager)
        {
            _auctionService = auctionService;
            _mapper = mapper;
            _userManager = userManager;
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
        public IActionResult MyAuctions()
        {
            try
            {
                var myacutions = _auctionService.GetUserAuctions(User.Identity.Name);
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


            IdentityUser currentUser = await _userManager.GetUserAsync(HttpContext.User);//todo g�r s� p� auction service f�r user
            AuctionVM auction = new AuctionVM()
            {
                Name = name,
                Description = description,
                StartingPrice = startPrice,
                CreatedDate = DateTime.Now,
                EndDate = endDate,
                UserId = currentUser.Email,
            };

            if (image != null && image.Length > 0)
            {
                using var stream = new MemoryStream();
                await image.CopyToAsync(stream);
                // Now you have the image data in stream.ToArray()
                auction.Image = stream.ToArray();
            }
            _auctionService.CreateAuction(_mapper.Map<Auction>(auction));
            return RedirectToAction("MyAuctions");
        }


        // POST: AuctionsController/Edit/id
        [HttpPost]
        public ActionResult Edit(string description, int id)
        {

            try
            {
                _auctionService.UpdateDescription(description, id);
                return RedirectToAction("MyAuctions");
            }
            catch (ServiceException ex)
            {
                Console.WriteLine(ex.StackTrace);
                return View("Error", ex.Message);
            }

        }

        // POST: AuctionsController/Delete/id
        [HttpPost]
        public ActionResult Delete(int id)
        {
             try
            {
                _auctionService.DeleteAuction(id);
                return RedirectToAction("MyAuctions");

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
        public IActionResult UserAuctions()
        {

            try
            {
                var myacutions = _auctionService.GetUserAuctions(User.Identity.Name);
                var viewAuction = _mapper.Map<IEnumerable<AuctionVM>>(myacutions);
                return View(viewAuction);
            }
            catch (ServiceException ex)
            {
                Console.WriteLine(ex.StackTrace);
                return View("Error", ex.Message);
            }

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