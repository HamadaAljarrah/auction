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
        public void CreateAuction(int auctionId, string name, DateTime endDate, string description)
        {
            Console.WriteLine("in creat auction" + name);

            Auction auction = new Auction(auctionId, name, endDate, description);
            AuctionDb auctionDb = _mapper.Map<AuctionDb>(auction);

            _auctionReposetory.Add(auctionDb);
        }


        //public IEnumerable<AuctionVM> GetAll()
        //{
        //    Console.WriteLine("IN GET ALL");
        //    IEnumerable<AuctionDb> auctionDBs = _auctionReposetory.GetAll();

        //    IEnumerable<AuctionVM> auctions = _mapper.Map<IEnumerable<AuctionVM>>(auctionDBs);//todo ska det vara vms eller auctions

        //    return auctions;
        //}

        public IEnumerable<Auction> GetAll()
        {
            Console.WriteLine("IN GET ALL");
            IEnumerable<AuctionDb> auctionDBs = _auctionReposetory.GetAll();

            IEnumerable<Auction> auctions = _mapper.Map<IEnumerable<Auction>>(auctionDBs);

            return auctions;
        }

        public void Remove(Auction auction)
        {
            throw new NotImplementedException();
        }
    }
}
