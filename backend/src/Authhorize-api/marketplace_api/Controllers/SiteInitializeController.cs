using marketplace_api.Common.interfaces;
using marketplace_api.Models;
using marketplace_api.ModelsDto;
using Microsoft.AspNetCore.Mvc;

namespace marketplace_api.Controllers;

[ApiController]
[Route("initials")]
public class SiteInitializeController : ControllerBase
{
  private readonly ISiteInitializerService _siteInitializerService;
  private readonly IAuthService _authService;

  public SiteInitializeController(
    ISiteInitializerService siteInitializerService
    , IAuthService authService)
  {
    _siteInitializerService = siteInitializerService;
    _authService = authService;
  }

  [HttpPost("init")]
  public async Task<IActionResult> Init(SiteInitializeDto dto)
  {
    await _siteInitializerService.InitializeAsync(dto);

    return Created("create site", dto);
  }

  [HttpGet("config")]
  public async Task<IActionResult> GetConfig()
  {
    var config = await _siteInitializerService.GetCurrentConfig();

    return Ok(config);
  }

  [HttpPost("create_admin")]
  public async Task<IActionResult> CreateAdmin(RegisterDto registerDto)
  {
    var admin = await _authService.RegisterAsync(registerDto, Role.Admin);

    return Ok(admin);
  }

  [HttpPatch("image")]
  public async Task<IActionResult> UpdateLogo(string ImageBase64)
  {
    var newSiteConfig = await _siteInitializerService.UpdateLogo(ImageBase64);

    return Ok(newSiteConfig);
  }

}
