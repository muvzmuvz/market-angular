using marketplace_api.Common.interfaces;
using marketplace_api.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using System.Threading;

namespace marketplace_api.Common.Persistence;

public class AuthorizeDbContext : IdentityDbContext<UserIdentity, IdentityRole<Guid>, Guid>, IUnitOfWork
{
  public DbSet<DomainUser> DomainUser { get; set; }
  public DbSet<SiteConfiguration> SiteConfigurations { get; set; }
  public DbSet<Shop> Shops { get; set; }

  public AuthorizeDbContext(DbContextOptions<AuthorizeDbContext> options)
       : base(options)
  {
  }

  protected override void OnModelCreating(ModelBuilder builder)
  {
    base.OnModelCreating(builder);
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
