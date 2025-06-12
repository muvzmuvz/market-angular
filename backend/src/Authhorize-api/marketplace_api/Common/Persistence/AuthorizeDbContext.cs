using marketplace_api.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace marketplace_api.Common.Persistence;

public class AuthorizeDbContext : IdentityDbContext<IdentityUser<Guid>, IdentityRole<Guid>, Guid>
{
  public DbSet<DomainUser> DomainUser { get; set; }
  public DbSet<SiteConfiguration> SiteConfigurations { get; set; }

  public AuthorizeDbContext(DbContextOptions<AuthorizeDbContext> options)
       : base(options)
  {
  }

  protected override void OnModelCreating(ModelBuilder builder)
  {
    base.OnModelCreating(builder);
  }
}
