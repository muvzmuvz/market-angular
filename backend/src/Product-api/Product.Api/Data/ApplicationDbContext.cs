using Microsoft.EntityFrameworkCore;
using Products.Api.Models;

namespace Products.Api.Data;

public class ApplicationDbContext : DbContext
{
  public DbSet<ProductHistory> ProductHistories { get; set; }
  public DbSet<Cart> Carts { get; set; }
  public DbSet<CartItem> CartItems { get; set; }
  public DbSet<ProductFavourity> ProductFavourities { get; set; }
  public DbSet<Image> Images { get; set; }
  public DbSet<Product> Products { get; set; }
  public DbSet<Review> Reviews { get; set; }
  
  public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
    {
        
    }
} 
