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
        private readonly IMapper  _mapper;
        public AuctionService(IReposetory<AuctionDb> auctinReposetory, IMapper mapper) {
            _auctionReposetory = auctinReposetory;
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
    }
}
