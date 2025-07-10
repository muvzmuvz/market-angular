using Microsoft.EntityFrameworkCore;
using Products.Api.Data;
using Products.Api.Models;
using Products_Api.Interfaces;

namespace Products_Api.Repository;

public class ProductHistoryRepository : IProductHistoryRepository
{
  private readonly ProductDbContext _context;

  public ProductHistoryRepository(ProductDbContext context)
  {
    _context = context;
  }

  public async Task<ProductHistory> AddProductHistoryAsync(ProductHistory productHistory)
  {
    await _context.AddAsync(productHistory);

    return productHistory;
  }

  public async Task DeleteProductHistoryAsync(Guid productHistoryId)
  {
    var productHistory = await _context.ProductHistories.FindAsync(productHistoryId)
      ?? throw new KeyNotFoundException($"ProductHistory with ID {productHistoryId} not found.");

    _context.ProductHistories.Remove(productHistory);
  }

  public async Task<IEnumerable<ProductHistory>> GetAllProductHistoriesAsync()
  {
    var productHistories = await _context.ProductHistories.ToListAsync();

    return productHistories;
  }

  public async Task<IEnumerable<ProductHistory>> GetProductHistoriesByProductIdAsync(Guid productId)
  {
    var productHistories = await _context.ProductHistories
      .Where(ph => ph.ProductId == productId)
      .ToListAsync();

    return productHistories;
  }

  public async Task<IEnumerable<ProductHistory>> GetProductHistoriesByUserId(Guid userId)
  {
    var productHistories = await _context.ProductHistories
      .Where(ph => ph.UserId == userId)
      .ToListAsync();

    return productHistories;
  }

  public async Task<ProductHistory> GetProductHistoryByIdAsync(Guid productHistoryId)
  {
    var productHistory = await _context.ProductHistories
      .FirstOrDefaultAsync(ph => ph.Id == productHistoryId)
      ?? throw new KeyNotFoundException($"ProductHistory with ID {productHistoryId} not found.");

    return productHistory;
  }
}
