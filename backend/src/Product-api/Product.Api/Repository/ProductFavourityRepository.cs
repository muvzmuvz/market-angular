using Microsoft.EntityFrameworkCore;
using Products.Api.Data;
using Products.Api.Models;
using Products_Api.Interfaces;

namespace Products_Api.Repository;

public class ProductFavourityRepository : IProductFavourityRepository
{
  private readonly ProductDbContext _context;

  public ProductFavourityRepository(ProductDbContext context)
  {
    _context = context;
  }

  public async Task<ProductFavourity> AddProductToFavourity(Guid userId, ProductFavourity productFavourity)
  {
    var existingFavourity = await _context.ProductFavourities
      .FirstOrDefaultAsync(pf => pf.UserId == userId
      && pf.ProductId == productFavourity.ProductId);

    if (existingFavourity != null)
    {
      return existingFavourity;
    }

    await _context.AddAsync(productFavourity);

    return productFavourity;
  }

  public async Task<List<ProductFavourity>> productFavourities(Guid userId)
  {
    var favourities = await _context.ProductFavourities
      .Where(pf => pf.UserId == userId)
      .Include(pf => pf.Product)
      .ThenInclude(img => img.Images)
      .ToListAsync();

    return favourities;
  }

  public async Task RemoveProductFromFavourity(Guid userId, Guid productId)
  {
    var productFavourity = await _context.ProductFavourities
      .FirstOrDefaultAsync(pf => pf.UserId == userId && pf.ProductId == productId)
      ?? throw new Exception("Product favourity not found");

    _context.ProductFavourities.Remove(productFavourity);
  }
}
