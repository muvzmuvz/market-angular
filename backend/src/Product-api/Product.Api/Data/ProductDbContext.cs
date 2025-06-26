using Microsoft.EntityFrameworkCore;
using Products.Api.Models;

namespace Products.Api.Data;

public class ProductDbContext : DbContext
{
  public DbSet<ProductHistory> ProductHistories { get; set; }
  public DbSet<Cart> Carts { get; set; }
  public DbSet<CartItem> CartItems { get; set; }
  public DbSet<ProductFavourity> ProductFavourities { get; set; }
  public DbSet<Image> Images { get; set; }
  public DbSet<Product> Products { get; set; }
  public DbSet<Review> Reviews { get; set; }
  
  public ProductDbContext(DbContextOptions<ProductDbContext> options) : base(options)
    {
      
    }
} 
