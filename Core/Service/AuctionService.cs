using DistLab2.Core.Interfaces;
using DistLab2.Persistence;
using AutoMapper;
using Persistence.Repository;
using DistLab2.Persistence.Error;
using DistLab2.Core.Error;
using Microsoft.EntityFrameworkCore.Storage;



namespace DistLab2.Core
{
    public class AuctionService : IAuctionService
    {
        private readonly IRepository<AuctionDb> _auctionRepository;
        private readonly IRepository<UserDb> _userRepository;


        private readonly IRepository<BidDb> _bidRepository;

        private readonly IMapper _mapper;

        public AuctionService(IRepository<AuctionDb> auctionRepository, IRepository<UserDb> userRepository, IRepository<BidDb> bidReposetory, IMapper mapper)
        {
            _bidRepository = bidReposetory;
            _auctionRepository = auctionRepository;
            _mapper = mapper;
            _userRepository = userRepository;

        }

        public void CreateAuction(Auction auction)
        {
            try
            {
                AuctionDb auctionDb = _mapper.Map<AuctionDb>(auction);
                _auctionRepository.Insert(auctionDb);
            }
            catch (Exception ex)
            {
                System.Console.WriteLine(ex.Message);
                System.Console.WriteLine(ex.StackTrace);

            }


        }

        public IEnumerable<Auction> GetAll()
        {
            try
            {
                IEnumerable<AuctionDb> auctionDBs = _auctionRepository.GetAll();

                IEnumerable<Auction> auctions = _mapper.Map<IEnumerable<Auction>>(auctionDBs);
                Console.WriteLine("Printing the list of auctions:");

                foreach (var auction in auctions)
                {
                    Console.WriteLine($"Auction Id: {auction.Id}, Name: {auction.Name}, Description: {auction.Description}, " +
                                       $"Starting Price: {auction.StartingPrice}, Created Date: {auction.CreatedDate}, " +
                                       $"End Date: {auction.EndDate}, User Id: {auction.UserId},");
                    if (auction.Bids != null)
                    {
                        Console.WriteLine("Bids found get all in service:");
                        foreach (var bid in auction.Bids)
                        {
                            Console.WriteLine("Bid from Service: " + bid.Id);
                        }
                    }
                    else
                    {
                        Console.WriteLine("No bids found for this auction.");
                    }
                }

                return auctions;
            }
            catch (DatabaseException ex)
            {
                throw new ServiceException("Error in get all auctions." + ex.Message, ex);
            }

        }


        public Auction GetById(int id) //todo test
        {
            try
            {


                AuctionDb auctionDb = _auctionRepository.GetById(id);

                if (auctionDb.Bids != null)
                {
                    Console.WriteLine("Bids found:");
                    foreach (var bid in auctionDb.Bids)
                    {
                        Console.WriteLine("Bid from Service: " + bid.Id);
                    }
                }
                else
                {
                    Console.WriteLine("No bids found for this auction.");
                }

                if (auctionDb != null)
                {
                    return _mapper.Map<Auction>(auctionDb);
                }

                return null; // Handle the case when the auction is not found.
            }
            catch (DatabaseException ex)
            {
                throw new ServiceException("Error in get auction by id." + ex.Message);
            }

        }

        public void PlaceBid(Bid bid)
        {
            try
            {
                AuctionDb auction = _auctionRepository.Find(a => a.Id == bid.AuctionId, a => a.Bids).SingleOrDefault();

                if (auction == null)
                {
                    return;
                }
                if (bid.Amount < auction.StartingPrice)
                {
                    return;
                }
                decimal highestBidAmount = auction.Bids.Max(b => (int?)b.Amount) ?? 0;
                if (bid.Amount <= highestBidAmount)
                {
                    return;
                }

                UserDb user = _userRepository.GetById(bid.UserId);
                BidDb newBid = _mapper.Map<BidDb>(bid);

                Console.WriteLine("Amount s " + newBid.Amount);
                Console.WriteLine("UserId s " + newBid.UserId);
                Console.WriteLine("a id  " + newBid.Id);
                Console.WriteLine("Amount s " + newBid.CreatedTime);

                _bidRepository.Insert(newBid);
                auction.Bids.Add(newBid);
                user.Bids.Add(newBid);
            }
            catch (DatabaseException ex)
            {
                throw new ServiceException("Error in get auction with bids." + ex.Message);
            }
        }

        public void UpdateDescription(string description, int id)
        {
            try
            {

                AuctionDb auctionDb = _auctionRepository.GetById(id);
                auctionDb.Description = description;
                _auctionRepository.Update(auctionDb);
            }
            catch (DatabaseException ex)
            {
                throw new ServiceException("Error in update auction." + ex.Message, ex);
            }
        }

        public void DeleteAuction(int id)
        {
            try
            {
                _auctionRepository.Delete(id);
            }
            catch (DatabaseException ex)
            {
                throw new ServiceException("Error in delete auction." + ex.Message, ex);
            }
        }

        public Auction GetAuctionWithBids(int id)
        {
            try
            {
                var dbAuction = _auctionRepository.Find(a => a.Id == id, a => a.Bids);

                List<Auction> auction = _mapper.Map<List<Auction>>(dbAuction);
                return auction[0];

            }
            catch (DatabaseException ex)
            {
                throw new ServiceException("Error in get auction with bids." + ex.Message);
            }
        }

        public IEnumerable<Auction> GetUserAuctions(string userId)
        {
            try
            {
                IEnumerable<AuctionDb> auctions = _auctionRepository.Find(p => p.UserId == userId);
                return _mapper.Map<IEnumerable<Auction>>(auctions);
            }
            catch (DatabaseException ex)
            {
                throw new ServiceException("Error in get  user auctions." + ex.Message, ex);
            }
        }

        public IEnumerable<Auction> GetUnownedAuctions(string userId)
        {
            try
            {
                IEnumerable<AuctionDb> auctions = _auctionRepository.Find(p => p.UserId != userId && DateTime.Now < p.EndDate);
                return _mapper.Map<IEnumerable<Auction>>(auctions);
            }
            catch (DatabaseException ex)
            {
                throw new ServiceException("Error in get unowned auctions." + ex.Message, ex);
            }
        }

        public IEnumerable<Auction> GetAuctionsUserBidIn(string userId)
        {
            try
            {
                var bids = _bidRepository
                    .Find(b => b.UserId == userId, b => b.Auction)
                    .Select(b => b.Auction)
                    .Where(b => b.EndDate > DateTime.Now)
                    .Distinct()
                    .ToList();
                return _mapper.Map<IEnumerable<Auction>>(bids);

            }
            catch (DatabaseException ex)
            {
                throw new ServiceException("Error in get auctions user bid in." + ex.Message);
            }
        }

        public IEnumerable<Auction> GetAuctionsUserWon(string userId)
        {
            try
            {
                var auction = _auctionRepository
                    .Find(a => a.EndDate < DateTime.Now, b => b.Bids)
                    .Where(a => a.Bids.OrderByDescending(b => b.Amount).FirstOrDefault()?.UserId == userId)
                    .ToList();
                return _mapper.Map<IEnumerable<Auction>>(auction);

            }
            catch (DatabaseException ex)
            {
                throw new ServiceException("Error in get auctions user won." + ex.Message);
            }
        }
    }
}
