using AutoMapper;
using DistLab2.Core;
using DistLab2.Persistence;
using DistLab2.ViewModels;

namespace DistLab2
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<AuctionDb, Auction>().ReverseMap();
            CreateMap<AuctionVM, Auction>().ReverseMap();
            CreateMap<Auction, AuctionVM>().ReverseMap();//tod remove extra

            CreateMap<BidDb, Bid>().ReverseMap();
            CreateMap<BidVM, Bid>().ReverseMap();
            CreateMap<Bid, BidVM>().ReverseMap();//todo remove extra


            CreateMap<RegisterVM, User>().ReverseMap();
            CreateMap<User, UserDb>().ReverseMap();

        }
    }
}
