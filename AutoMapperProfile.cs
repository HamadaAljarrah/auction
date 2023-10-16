using AutoMapper;
using DistLab2.Core;
using DistLab2.Persistence;

namespace DistLab2
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<AuctionDb, Auction>().ReverseMap();
            //CreateMap<AuctionView, Auction>().ReverseMap();
        }
    }
}
