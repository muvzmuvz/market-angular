namespace marketplace_api.Models;

public class SiteConfiguration
{
  public Guid Id { get; set; } = Guid.Empty;
  public string SiteName { get; set; } = "WildBobr";
  public DateTime InitializedAt { get; set; }
  public string LogoPath { get; set; } = "defaultImage";
}
