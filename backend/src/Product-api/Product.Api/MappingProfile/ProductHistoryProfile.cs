using AutoMapper;
using Products.Api.Models;
using Products_Api.ModelsDto;

namespace Products_Api.MappingProfile;

public class ProductHistoryProfile : Profile
{
  public ProductHistoryProfile()
  {
    CreateMap<ProductHistory, ProductHistoryDto>();
  }
}
