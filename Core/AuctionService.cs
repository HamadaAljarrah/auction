using DistLab2.Core.Interfaces;
using DistLab2.Persistence;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using NuGet.Packaging.Signing;
using AutoMapper;

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
        public void CreateAuction(int auctionId, string name, DateTime endDate, string description)
        {
            Auction auction = new Auction(auctionId, name, endDate, description);
            AuctionDb auctionDb = _mapper.Map<AuctionDb>(auction);

            _auctionReposetory.Add(auctionDb);
        }
    

        public IEnumerable<Auction> GetAll()
        {
            //retunerar all auctions
            throw new NotImplementedException();
        }

        public void Remove(Auction auction)
        {
            throw new NotImplementedException();
        }
    }
}
