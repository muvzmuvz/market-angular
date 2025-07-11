using Microsoft.AspNetCore.Mvc;
using Products_Api.Interfaces;
using Products_Api.ModelsDto;

namespace Products_Api.Controllers;

[ApiController]
[Route("product_history")]
public class ProductHistoryController : ControllerBase
{
  private readonly IProductHistoryService _productHistoryService;

  public ProductHistoryController(IProductHistoryService productHistoryService)
  {
    _productHistoryService = productHistoryService;
  }

  [HttpGet("{productHistoryId}")]
  public async Task<IActionResult> GetProductHistoryByIdAsync(Guid productHistoryId)
  {
    var productHistory = await _productHistoryService.GetProductHistoryByIdAsync(productHistoryId);

    return Ok(productHistory);
  }

  [HttpPost]
  public async Task<IActionResult> CreateProductHistoryAsync(ProductHistoryRequest productHistoryRequest)
  {
    var productHistory = await _productHistoryService.AddProductHistoryAsync(productHistoryRequest);

    return CreatedAtAction(nameof(GetProductHistoryByIdAsync), new { productHistoryId = productHistory.Id }, productHistory);
  }

  [HttpDelete("{productHistoryId}")]
  public async Task<IActionResult> DeleteProductHistoryAsync(Guid productHistoryId)
  {
    await _productHistoryService.DeleteProductHistoryAsync(productHistoryId);

    return NoContent();
  }

  [HttpGet("product/{productId}")]
  public async Task<IActionResult> GetProductHistoriesByProductIdAsync(Guid productId)
  {
    var productHistories = await _productHistoryService.GetProductHistoriesByProductIdAsync(productId);

    return Ok(productHistories);
  }

  [HttpGet("user/{userId}")]
  public async Task<IActionResult> GetProductHistoriesByUserIdAsync(Guid userId)
  {
    var productHistories = await _productHistoryService.GetProductHistoriesByUserId(userId);

    return Ok(productHistories);
  }

  [HttpGet]
  public async Task<IActionResult> GetAllProductHistoriesAsync()
  {
    var productHistories = await _productHistoryService.GetAllProductHistoriesAsync();

    return Ok(productHistories);
  }
}
