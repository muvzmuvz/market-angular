using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Products.Api.Interfaces;
using Products.Api.ModelsDto;

namespace Products.Api.Controllers;

[ApiController]
[Route("products")]
public class ProductController : ControllerBase
{
  private readonly IProductService _productService;

  public ProductController(IProductService productService)
  {
    _productService = productService;
  }

  [HttpPost()]
  [Authorize]
  public async Task<IActionResult> CreateProduct(ProductCreateDto createDto)
  {
    var userID = User.FindFirst("sub")?.Value;
    var productDto = await _productService.CreateProductAsync(createDto, new Guid(userID));

    return Created("create product", productDto);
  }

  [HttpGet("{productId}")]
  public async Task<IActionResult> GetProduct(Guid productId)
  {
    var productDto = await _productService.GetProductByIdAsync(productId);

    return Ok(productDto);
  }

  [HttpGet]
  public async Task<IActionResult> GetAllProducts()
  {
    var products = await _productService.GetAllProductsAsync();
    return Ok(products);
  }

  [HttpDelete("{productId}")]
  [Authorize]
  public async Task<IActionResult> DeleteProduct(Guid productId)
  {
    var userID = User.FindFirst("sub")?.Value;

    var isDeleted = await _productService.DeleteProductAsync(productId, new Guid(userID));
    if (isDeleted)
      return NoContent();
    return NotFound("продукт не найден");
  }

  [HttpGet("shop_product/{shopId}")]
  public async Task<IActionResult> GetProductsOfShop(Guid shopId)
  {
    var products = await _productService.GetProductsOfShopAsync(shopId);
    return Ok(products);
  }

  [HttpPut("{productId}")]
  public async Task<IActionResult> UpdateProduct(Guid productId, UpdateProductDto updateProductDto)
  {
    var updatedProduct = await _productService.UpdateProductAsync(productId, updateProductDto);
    return Ok(updatedProduct);
  }

  [HttpGet("search/name/{name}")]
  public async Task<IActionResult> GetByName(string name)
  {
    var products = await _productService.GetByName(name);
    return Ok(products);
  }

  [HttpGet("search/title/{title}")]
  public async Task<IActionResult> GetByTitle(string title)
  {
    var products = await _productService.GetByTitle(title);
    return Ok(products);
  }
}
