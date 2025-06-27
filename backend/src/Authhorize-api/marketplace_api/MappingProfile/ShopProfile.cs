using AutoMapper;
using marketplace_api.Models;
using marketplace_api.ModelsDto;

namespace marketplace_api.MappingProfile;

public class ShopProfile : Profile
{
  public ShopProfile()
  {

    CreateMap<Shop, ShopDto>();
    CreateMap<ShopDtoRequest, Shop>();
    CreateMap<Shop, ShopDtoRequest>();

    CreateMap<ShopDto, Shop>();

    CreateMap<ShopDtoRequest, Shop>();
    CreateMap<Shop, ShopDtoRequest>();

    CreateMap<Shop, ShopFullInformationDto>();
  }
}
