using Microsoft.AspNetCore.Mvc;

namespace Products.Api.Controllers;

[ApiController]
[Route("products")]
public class ProductController : ControllerBase
{

  [HttpGet]
  public IActionResult GetProduct()
  {
    return Ok("Вот ваш продукт");
  }
}
