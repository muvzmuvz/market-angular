using AutoMapper;
using marketplace_api.Models;
using marketplace_api.ModelsDto;

namespace marketplace_api.MappingProfile;

public class ShopSellerProfile : Profile
{
  public ShopSellerProfile()
  {
    CreateMap<ShopSeller, ShopSellerDto>();

    CreateMap<ShopSellerDto, ShopSeller>();
  }

}
