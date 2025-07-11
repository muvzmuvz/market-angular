using Microsoft.EntityFrameworkCore;
using Products.Api.Data;
using Products.Api.Interfaces;
using Products.Api.Models;
using Products.Api.ModelsDto;

namespace Products.Api.Repository;

public class ProductRepository : IProductRepository
{
  private readonly ProductDbContext _context;

  public ProductRepository(ProductDbContext context)
  {
    _context = context;
  }

  public async Task<IEnumerable<Product>> GetAllProductsAsync()
  {
    return await _context.Products.Include(img => img.Images).ToListAsync();
  }

  public async Task<Product> GetProductByIdAsync(Guid id)
  {
    var product = await _context.Products.FindAsync(id)
        ?? throw new Exception("данный продукт не существует");

    return product;
  }

  public async Task<Product> AddProductAsync(Product product)
  {
    await _context.Products.AddAsync(product);
    return product;
  }

  public async Task DeleteProductAsync(Guid id)
  {
    var product = await GetProductByIdAsync(id);

    product.productStatus = ProductStatus.Deleted;
  }

  public async Task<Product> UpdateProductAsync(Guid id, UpdateProductDto updateProductDto)
  {
    var product = await GetProductByIdAsync(id);

    product.UpdateProduct(product.Description,
      updateProductDto.Price,
      updateProductDto.Title,
      updateProductDto.Name,
      updateProductDto.CountProduct,
      updateProductDto.Characteristic);

    return product;
  }

  public async Task<List<Product>> GetProductsOfShopAsync(Guid shopId)
  {
    var products = await _context.Products
      .Where(product => product.ShopId == shopId)
      .ToListAsync();

    return products;
  }

  public async Task<ICollection<Product>> GetByName(string name)
  {
    var products = await _context.Products
    .Where(p => EF.Functions.Like(p.Name, $"%{name}%"))
    .ToListAsync();

    return products;
  }

  public async Task<ICollection<Product>> GetByTitle(string title)
  {
    var products = await _context.Products.Where(p => p.Title == title).ToListAsync();
    if (!products.Any())
    {
      throw new Exception("Продуктов с данным названием не существует");
    }
    return products;
  }

  public async Task<List<Product>> GetProductsByIdsAsyncForTopProduct(IEnumerable<Guid> productIds)
  {
    return await _context.Products
        .Where(p => productIds.Contains(p.Id))
        .ToListAsync();
  }

  public Task<List<Product>> GetTopProduct()
  {
     var topProducts = _context.Products.Where(p => p.productStatus == ProductStatus.active)
        .OrderByDescending(p => p.CountViewProduct)
        .Take(1000)
        .ToListAsync();

    return topProducts;
  }
}
