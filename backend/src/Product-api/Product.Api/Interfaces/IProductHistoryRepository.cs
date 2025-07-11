using Products.Api.Models;

namespace Products_Api.Interfaces;

public interface IProductHistoryRepository
{
  public Task<ProductHistory> GetProductHistoryByIdAsync(Guid productHistoryId);
  public Task<ProductHistory> AddProductHistoryAsync(ProductHistory productHistory);
  public Task DeleteProductHistoryAsync(Guid productHistoryId);
  public Task<IEnumerable<ProductHistory>> GetProductHistoriesByProductIdAsync(Guid productId);
  public Task<IEnumerable<ProductHistory>> GetAllProductHistoriesAsync();
  public Task<IEnumerable<ProductHistory>> GetProductHistoriesByUserId(Guid userId);
}
