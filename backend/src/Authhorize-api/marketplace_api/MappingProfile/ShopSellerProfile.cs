using AutoMapper;
using marketplace_api.Models;
using marketplace_api.ModelsDto;

namespace marketplace_api.MappingProfile;

public class ShopSellerProfile : Profile
{
  public ShopSellerProfile()
  {
    CreateMap<ShopSeller, ShopSellerDto>()
        .ForMember(dest => dest.UserId, opt => opt.MapFrom(src => src.Seller.IdentityId));

    CreateMap<ShopSellerDto, ShopSeller>()
        .ForMember(dest => dest.SellerId, opt => opt.MapFrom(src => src.UserId));
  }

}
