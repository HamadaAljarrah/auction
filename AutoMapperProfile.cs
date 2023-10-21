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
            CreateMap<Auction, AuctionVM>().ReverseMap();

            CreateMap<RegisterVM, User>().ReverseMap();
            CreateMap<User, UserDb>().ReverseMap();

        }
    }
}
