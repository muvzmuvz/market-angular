using Products_Api.ModelsDto;

namespace Products_Api.Interfaces;

public interface IProductFavourityService
{
  Task<ProductFavourityDto> AddProductToFavourity(Guid userId, ProductFavourityRequest productFavourity);
  Task RemoveProductFromFavourity(Guid userId, Guid productId);
  Task<List<ProductFavourityDto>> GetProductFavourities(Guid userId);
}
