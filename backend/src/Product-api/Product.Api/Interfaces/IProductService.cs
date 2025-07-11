using Products.Api.Models;
using Products.Api.ModelsDto;

namespace Products.Api.Interfaces;

public interface IProductService
{
  Task<IEnumerable<Product>> GetAllProductsAsync();
  Task<ProductDto?> GetProductByIdAsync(Guid id);
  Task<ProductDto> CreateProductAsync(ProductCreateDto productDto, Guid userId);
  Task<bool> DeleteProductAsync(Guid shopId, Guid userId);
  Task<List<ProductDto>> GetProductsOfShopAsync(Guid shopId);
  Task<ProductDto> UpdateProductAsync(Guid id, UpdateProductDto updateProductDto);
  public Task<ICollection<ProductDto>> GetByName(string name);
  public Task<ICollection<ProductDto>> GetByTitle(string title);
  public Task<ICollection<ProductDto>> GetTopProduct(Guid userId);
}
