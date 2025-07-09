using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using Products_Api.Interfaces;
using Products_Api.ModelsDto;

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

  [HttpGet("cart/{cartId}")]
  public async Task<IActionResult> GetCart(Guid cartId)
  {
    var userId = User.FindFirst("sub")?.Value;
    if (string.IsNullOrEmpty(userId))
    {
      var itemsByCartId = await _cartService.GetCartByCartIdItems(cartId);

      return Ok(itemsByCartId);
    }

    var itemsByUserId = await _cartService.GetCartByUserIdItems(new Guid(userId));

    return Ok(itemsByUserId);
  }

  [HttpPost("{cartId}")]
  public async Task<IActionResult> AddItemToCart(CartItemDto cartItemDto, Guid cartId)
  {
    var userId = User.FindFirst("sub")?.Value;
    if (string.IsNullOrEmpty(userId))
    {
      await _cartService.AddCartByCartId(cartItemDto, cartId);
      return StatusCode(201);
    }

    await _cartService.AddCartByUserId(cartItemDto, new Guid(userId));  

    return StatusCode(201);
  }

  [HttpDelete("{cartId}/items/{productId}")]
  public async Task<IActionResult> DeleteItemFromCart(Guid productId, Guid cartId)
  {
    var userId = User.FindFirst("sub")?.Value;
    if (string.IsNullOrEmpty(userId))
    {
      await _cartService.DeleteCartItemByCartId(productId, cartId);
      return NoContent();
    }

    await _cartService.DeleteCartItemByUserId(productId, new Guid(userId));
    return NoContent();
  }

  [HttpGet("clear/{cartId}")]
  public async Task<IActionResult> ClearCart(Guid cartId)
  {
    var userId = User.FindFirst("sub")?.Value;
    if (string.IsNullOrEmpty(userId))
    {
      await _cartService.ClearByCartIdCart(cartId);
      return NoContent();
    }

    await _cartService.ClearByUserIdCart(new Guid(userId));
    return NoContent();
  }
}
