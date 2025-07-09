using Microsoft.EntityFrameworkCore;
using Products.Api.Interfaces;
using Products.Api.Models;

namespace Products.Api.Data;

public class ProductDbContext : DbContext, IUnitOfWork
{
  public DbSet<ProductHistory> ProductHistories { get; set; }
  public DbSet<Cart> Carts { get; set; }
  public DbSet<CartItem> CartItems { get; set; }
  public DbSet<ProductFavourity> ProductFavourities { get; set; }
  public DbSet<Image> Images { get; set; }
  public DbSet<Product> Products { get; set; }
  
  public ProductDbContext(DbContextOptions<ProductDbContext> options) : base(options)
    {
      
    }

  public async Task commitChange()
  {
    await using var transaction = await Database.BeginTransactionAsync();

    try
    {
      await SaveChangesAsync();
      await transaction.CommitAsync();
    }
    catch
    {
      await transaction.RollbackAsync();
      throw;
    }
  }
} 
