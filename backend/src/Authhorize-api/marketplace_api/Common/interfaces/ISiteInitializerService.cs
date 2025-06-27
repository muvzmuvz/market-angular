using marketplace_api.Models;
using marketplace_api.ModelsDto;

namespace marketplace_api.Common.interfaces;

public interface ISiteInitializerService
{
  Task InitializeAsync(SiteInitializeDto dto);
  Task<SiteConfiguration> GetCurrentConfig();
  Task<SiteConfiguration> UpdateLogo(string imageBase64);
}
