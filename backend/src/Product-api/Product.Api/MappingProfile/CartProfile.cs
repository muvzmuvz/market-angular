using AutoMapper;
using Products.Api.Models;
using Products_Api.ModelsDto;

namespace Products_Api.MappingProfile;

public class CartProfile : Profile
{
  public CartProfile()
  {
    CreateMap<CartItem, CartItemDto>();
  }
}
