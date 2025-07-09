using Products.Api.Models;
using Products.Api.ModelsDto;

namespace Products_Api.Interfaces;

public interface IProductFavourityRepository
{
  public Task<ProductFavourity> AddProductToFavourity(Guid userId, ProductFavourity productFavourity);
  public Task RemoveProductFromFavourity(Guid userId, Guid productId);
  public Task<List<ProductFavourity>> productFavourities(Guid userId);
}
