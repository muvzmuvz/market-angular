using Microsoft.AspNetCore.Mvc;
using Products_Api.Interfaces;
using Products_Api.ModelsDto;

namespace Products_Api.Controllers;

[ApiController]
[Route("[controller]")]
public class ProductFavourityController : ControllerBase
{
  private readonly IProductFavourityService _productFavourityService;

  public ProductFavourityController(IProductFavourityService productFavourityService)
  {
    _productFavourityService = productFavourityService;
  }

  [HttpGet]
  [Route("GetProductFavourities/{userId:guid}")]
  public async Task<IActionResult> GetProductFavourities(Guid userId)
  {
    var favourities = await _productFavourityService.GetProductFavourities(userId);
    return Ok(favourities);
  }

  [HttpPost]
  [Route("AddProductToFavourity/{userId:guid}")]
  public async Task<IActionResult> AddProductToFavourity
    (Guid userId, [FromBody] ProductFavourityRequest productFavourity)
  {
    var result = await _productFavourityService.AddProductToFavourity(userId, productFavourity);
    return CreatedAtAction(nameof(GetProductFavourities), new { userId }, result);
  }

  [HttpDelete]
  [Route("RemoveProductFromFavourity/{userId:guid}/{productId:guid}")]
  public async Task<IActionResult> RemoveProductFromFavourity(Guid userId, Guid productId)
  {
    await _productFavourityService.RemoveProductFromFavourity(userId, productId);
    return NoContent();
  }
}
