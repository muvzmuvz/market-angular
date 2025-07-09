using AutoMapper;
using Products.Api.Models;
using Products_Api.ModelsDto;

namespace Products_Api.MappingProfile;

public class ProductFavourityProfile : Profile
{
  public ProductFavourityProfile()
  {
    CreateMap<ProductFavourity, ProductFavourityDto>()
      .ForMember(dest => dest.ProductId, opt => opt.MapFrom(src => src.ProductId))
      .ForMember(dest => dest.UserId, opt => opt.MapFrom(src => src.UserId))
      .ForMember(dest => dest.Product, opt => opt.MapFrom(src => src.Product));
  }
}
