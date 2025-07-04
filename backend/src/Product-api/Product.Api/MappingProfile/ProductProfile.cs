using AutoMapper;
using Products.Api.Models;
using Products.Api.ModelsDto;

namespace Products.Api.MappingProfile;

public class ProductProfile : Profile
{
  public ProductProfile()
  {
    CreateMap<Product, ProductDto>();
    CreateMap<ProductCreateDto, Product>();
  }
}
