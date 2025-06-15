using marketplace_api.Common.interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace marketplace_api.Controllers;

[ApiController]
[Route("accounts")]
public class AccountController : ControllerBase
{
  private readonly IAccountService _accountService;

  public AccountController(IAccountService accountService)
  {
    _accountService = accountService;
  }

  [HttpGet("me")]
  [Authorize]
  public async Task<IActionResult> GetAccount()
  {
    var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

    var userDto = await _accountService.GetUser(userId);
    return Ok(userDto);
  }

  [HttpGet("account/{identityId}")]
  public async Task<IActionResult> GetAccount(string identityId)
  {
    var userDto = await _accountService.GetUser(identityId);

    return Ok(userDto);
  }

  [HttpPut("image/me")]
  [Authorize]
  public async Task<IActionResult> UpdateImage(string ImageBase64)
  {
    var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

    string newPath = await _accountService.UpdateImage(userId, ImageBase64);

    return Created(userId, newPath);
  }

  [HttpPut("image/{identityId}")]
  [Authorize]
  public async Task<IActionResult> UpdateImage(string identityId, string ImageBase64)
  {

    string newPath = await _accountService.UpdateImage(identityId, ImageBase64);

    return Created(identityId, newPath);
  }

  [HttpPut("name/me")]
  [Authorize]
  public async Task<IActionResult> UpdateName(string newName)
  {
    var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

    await _accountService.UpdateName(newName, userId);

    return Ok(newName);
  }

  [HttpGet]
  public async Task<IActionResult> GetUsers()
  {
    return Ok(await _accountService.GetUsers());
  }

}
