using marketplace_api.Common.interfaces;
using marketplace_api.ModelsDto;
using Microsoft.AspNetCore.Mvc;

namespace marketplace_api.Controllers;

[ApiController]
[Route("auths")]
public class AuthorizeController : ControllerBase
{
  private readonly IAuthService _authService;

  public AuthorizeController(IAuthService authService)
  {
    _authService = authService;
  }

  [HttpPost("reg")]
  public async Task<IActionResult> Register(RegisterDto registerDto)
  {
    var result = await _authService.RegisterAsync(registerDto, Models.Role.User);

    return Created("register user", result);
  }
}
