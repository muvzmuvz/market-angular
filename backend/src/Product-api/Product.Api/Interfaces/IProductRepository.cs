using Products.Api.Models;
using Products.Api.ModelsDto;

namespace Products.Api.Interfaces;

public interface IProductRepository
{
  Task<IEnumerable<Product>> GetAllProductsAsync();
  Task<Product?> GetProductByIdAsync(Guid id);
  Task<Product> AddProductAsync(Product product);
  Task DeleteProductAsync(Guid id);
  Task<List<Product>> GetProductsOfShopAsync(Guid shopId);
  Task<Product> UpdateProductAsync(Guid id, UpdateProductDto updateProductDto);
  public Task<ICollection<Product>> GetByName(string name);
  public Task<ICollection<Product>> GetByTitle(string title);
  public Task<List<Product>> GetProductsByIdsAsyncForTopProduct(IEnumerable<Guid> productIds);
  public Task<List<Product>> GetTopProduct();
}
