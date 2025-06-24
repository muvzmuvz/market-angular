using AutoMapper;
using marketplace_api.Models;
using marketplace_api.ModelsDto;

namespace marketplace_api.MappingProfile;

public class ShopProfile : Profile
{
  public ShopProfile()
  {

    CreateMap<Shop, ShopDto>()
            .ForMember(dest => dest.UserId, opt => opt.MapFrom(src => src.Owner.IdentityId))
            .ForMember(dest => dest.Sellers, opt => opt.MapFrom(src => src.Sellers));

    CreateMap<ShopDtoRequest, Shop>();
    CreateMap<Shop, ShopDtoRequest>();

    CreateMap<ShopDto, Shop>();

    CreateMap<ShopDtoRequest, Shop>();
    CreateMap<Shop, ShopDtoRequest>();
  }
}
