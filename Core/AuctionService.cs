using DistLab2.Core.Interfaces;
using DistLab2.Persistence;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using NuGet.Packaging.Signing;
using AutoMapper;
using DistLab2.ViewModels;

namespace DistLab2.Core
{
    public class AuctionService : IAuctionService
    {
        private readonly IReposetory<AuctionDb> _auctionReposetory;
        private readonly IReposetory<BidDb> _bidReposetory;

        private readonly IMapper  _mapper;
        public AuctionService(IReposetory<AuctionDb> auctinReposetory, IReposetory<BidDb> bidReposetory ,IMapper mapper) {
            _auctionReposetory = auctinReposetory;
            _bidReposetory = bidReposetory;
            _mapper = mapper;
        }

        public void CreateAuction(Auction auction)
        {        
            AuctionDb auctionDb = _mapper.Map<AuctionDb>(auction);
            _auctionReposetory.Add(auctionDb);

        }

        public IEnumerable<Auction> GetAll()
        {
            IEnumerable<AuctionDb> auctionDBs = _auctionReposetory.GetAll();

            IEnumerable<Auction> auctions = _mapper.Map<IEnumerable<Auction>>(auctionDBs);
            Console.WriteLine("Printing the list of auctions:");

            foreach (var auction in auctions)
            {
                Console.WriteLine($"Auction Id: {auction.Id}, Name: {auction.Name}, Description: {auction.Description}, " +
                                   $"Starting Price: {auction.StartingPrice}, Created Date: {auction.CreatedDate}, " +
                                   $"End Date: {auction.EndDate}, User Id: {auction.UserId}");
            }

            return auctions;
        }

        public void Remove(Auction auction)
        {
            throw new NotImplementedException();
        }
        public Auction GetById(int id)
        {
            AuctionDb auctionDb = _auctionReposetory.GetById(id);
            Console.WriteLine("in sevice 1 : name " + auctionDb.Name);
            Auction auction = _mapper.Map<Auction>(auctionDb);
            Console.WriteLine("in service 2 : name " + auction.Name);
            return auction;
        }
        public void PlaceBid(int auctionId, int bidAmount)
        {
            AuctionDb auction = _auctionReposetory.GetById(auctionId);

            if (auction == null)
            {
       
                return;
            }
            decimal highestBidAmount = auction.Bids.Max(b => (int?)b.Amount) ?? 0;
            if (bidAmount <= highestBidAmount)
            {
                return;
            }

            BidDb newBid = new BidDb //todo läg dynamic id
            {
                Amount = bidAmount,
                UserId = "-1",
                CreatedTime = DateTime.Now
            };
            Console.WriteLine("Amount s " + newBid.Amount);
            Console.WriteLine("UserId s " + newBid.UserId);
            Console.WriteLine("Amount s " + newBid.CreatedTime);


            _bidReposetory.Add(newBid);


            // Save the changes to the database
            _bidReposetory.Update(newBid);
        }

    }
}
