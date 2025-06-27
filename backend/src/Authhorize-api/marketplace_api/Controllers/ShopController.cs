using marketplace_api.Common.interfaces;
using marketplace_api.ModelsDto;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace marketplace_api.Controllers;

[ApiController]
[Route("shops")]
public class ShopController : ControllerBase
{
  private readonly IShopService _shopService;

  public ShopController(IShopService shopService)
  {
    _shopService = shopService;
  }

  [HttpGet("shop/{shopId}/details")]
  public async Task<IActionResult> GetFullInforamationShop(Guid shopId)
  {
    var shop = await _shopService.GetFullInformtionShop(shopId);

    return Ok(shop);
  }

  [HttpGet("active")]
  public async Task<IActionResult> GetActiveShops()
  {
    var shops = await _shopService.GetActiveShops();

    return Ok(shops);
  }

  [Authorize]
  [HttpPost]
  public async Task<IActionResult> CreateShop(ShopDtoRequest dto)
  {
    var userId = User.FindFirstValue(ClaimTypes.NameIdentifier)
          ?? User.FindFirstValue(ClaimTypes.NameIdentifier);

    var shop = await _shopService.CreateShop(dto, new Guid(userId));

    return Created("create",shop);
  }

  [HttpGet("inactive")]
  public async Task<IActionResult> GetInActiveShops()
  {
     var shops = await _shopService.GetInActiveShops();

    return Ok(shops);
  }

  
  [HttpGet("shop/{shopId}")]
  public async Task<IActionResult> GetShop(Guid shopId)
  {
    var shop = await _shopService.GetShop(shopId);

    return Ok(shop);
  }

  [Authorize]
  [HttpGet("shop/me")]
  public async Task<IActionResult> GetMyShop()
  {
    var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

    var shop = await _shopService.GetMyShop(new Guid(userId));

    return Ok(shop);
  }

  [HttpPatch("activate/{shopId}")]
  public async Task<IActionResult> ActivateShop(Guid shopId)
  {
    var activateShop = await _shopService.ActivateTheShop(shopId);

    return Ok(activateShop);
  }

}
