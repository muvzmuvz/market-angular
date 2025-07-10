using Products_Api.ModelsDto;

namespace Products_Api.Interfaces;

public interface IProductHistoryService
{
  public  Task<ProductHistoryDto> GetProductHistoryByIdAsync(Guid productHistoryId);
  public Task<ProductHistoryDto> AddProductHistoryAsync(ProductHistoryRequest productHistory);
  public Task DeleteProductHistoryAsync(Guid productHistoryId);
  public Task<IEnumerable<ProductHistoryDto>> GetProductHistoriesByProductIdAsync(Guid productId);
  public Task<IEnumerable<ProductHistoryDto>> GetAllProductHistoriesAsync();
  public Task<IEnumerable<ProductHistoryDto>> GetProductHistoriesByUserId(Guid userId);
}
