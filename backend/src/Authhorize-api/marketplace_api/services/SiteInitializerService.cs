using marketplace_api.Common.interfaces;
using marketplace_api.Common.Persistence;
using marketplace_api.Models;
using marketplace_api.ModelsDto;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace marketplace_api.services;

public class SiteInitializerService : ISiteInitializerService
{
  private readonly AuthorizeDbContext _authorizeDbContext;
  private readonly RoleManager<IdentityRole<Guid>> _roleManager;
  private readonly IAuthService _authService;

  public SiteInitializerService(
    AuthorizeDbContext authorizeDbContext
    , RoleManager<IdentityRole<Guid>> roleManager
    , IAuthService authService)
  {
    _authorizeDbContext = authorizeDbContext;
    _roleManager = roleManager;
    _authService = authService;
  }

  public async Task<SiteConfiguration> GetCurrentConfig()
  {
    var siteConfiguration =  await _authorizeDbContext.SiteConfigurations.FirstOrDefaultAsync()
          ?? throw new Exception("Данный сайт еще не иницилизирован");

    return siteConfiguration;
  }

  public async Task InitializeAsync(SiteInitializeDto dto)
  {
    if (await IsSiteInitialized())
    {
      throw new InvalidOperationException("Site is already initialized");
    }

    using var transaction = await _authorizeDbContext.Database.BeginTransactionAsync();

    try
    {
      await ApplyMigrationsAsync();

      await CreateDefaultRolesAsync();

      var siteConfiguration = new SiteConfiguration()
      {
        SiteName = dto.siteName,
        InitializedAt = DateTime.UtcNow
      };

      await _authService.RegisterAsync(new RegisterDto()
      {
        Email = dto.Email,
        ImageBase64 = dto.ImageBase64,
        Name = dto.Name,
        Password = dto.Password,
        Role = Role.Administrator,
      });

      await _authorizeDbContext.SiteConfigurations.AddAsync(siteConfiguration);
      await _authorizeDbContext.SaveChangesAsync();

      await transaction.CommitAsync();
    }

    catch (Exception ex)
    {
      await transaction.RollbackAsync();
      throw;
    }
    
  }


  private async Task<bool> IsSiteInitialized()
  {
    return await _authorizeDbContext.SiteConfigurations.AnyAsync();
  }

  private async Task ApplyMigrationsAsync()
  {
    try
    {
      await _authorizeDbContext.Database.MigrateAsync();
    }
    catch (Exception ex)
    {
      throw new Exception("Ошибка при применении миграций", ex);
    }
  }

  private async Task CreateDefaultRolesAsync()
  {
    string[] roleNames = { "Admin", "Seller", "User" };

    foreach (var roleName in roleNames)
    {
      if (!await _roleManager.RoleExistsAsync(roleName))
      {
        await _roleManager.CreateAsync(new IdentityRole<Guid>(roleName));
      }
    }
  }

}
