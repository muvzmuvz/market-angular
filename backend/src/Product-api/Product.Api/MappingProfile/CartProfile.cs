using AutoMapper;
using Products.Api.Models;
using Products_Api.ModelsDto;

namespace Products_Api.MappingProfile;

public class CartProfile : Profile
{
  public CartProfile()
  {
    CreateMap<CartItem, CartItemDto>();
    CreateMap<CartItemDto, CartItem>()
      .ForMember(dest => dest.Product, opt => opt.Ignore())
      .ForMember(dest => dest.Id, opt => opt.Ignore())
      .ForMember(dest => dest.CartId, opt => opt.Ignore());
  }
}
