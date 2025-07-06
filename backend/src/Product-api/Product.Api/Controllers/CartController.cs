using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using Products_Api.Interfaces;

namespace Products_Api.Controllers;

[ApiController]
[Route("carts")]
public class CartController : ControllerBase
{
  private readonly ICartService _cartService;

  public CartController(ICartService cartService)
  {
    _cartService = cartService;
  }

  [HttpGet]
  public async Task<IActionResult> GetCart()
  {
    var items = await _cartService.GetCartByCartIdItems(Guid.Empty);

    return Ok(items);
  }
}
