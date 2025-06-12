using marketplace_api.Common.interfaces;
using marketplace_api.ModelsDto;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace marketplace_api.Controllers;

[ApiController]
[Route("initial")]
public class SiteInitializeController : ControllerBase
{
  private readonly ISiteInitializerService _siteInitializerService;

  public SiteInitializeController(
    ISiteInitializerService siteInitializerService)
  {
    _siteInitializerService = siteInitializerService;
  }

  [HttpPost]
  public async Task<IActionResult> Init(SiteInitializeDto dto)
  {
    await _siteInitializerService.InitializeAsync(dto);

    return Created("сайт создан", dto);

  }
}
