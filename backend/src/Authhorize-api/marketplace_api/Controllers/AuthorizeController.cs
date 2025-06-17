using Duende.IdentityServer.Services;
using marketplace_api.Common.interfaces;
using marketplace_api.ModelsDto;
using marketplace_api.ViewModel;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualBasic;

namespace marketplace_api.Controllers;

public class AuthorizeController : Controller
{
  private readonly IAuthService _authService;
  private readonly IIdentityServerInteractionService _interaction;

  public AuthorizeController(IAuthService authService,
    IIdentityServerInteractionService interaction)
  {
    _authService = authService;
    _interaction = interaction;
  }

  [HttpGet]
  public IActionResult Register(string returnUrl)
  {
    var registerModel = new RegisterViewModel()
    {
      ReturnUrl = returnUrl
    };

    return View(registerModel);
  }

  [HttpPost]
  [ValidateAntiForgeryToken]
  public async Task<IActionResult> Register(RegisterViewModel model)
  {
    if (!ModelState.IsValid)
    {
      return View(model); 
    }

    var registerDto = new RegisterDto()
    {
      Email = model.Email,
      Name = model.Name,
      Password = model.Password,
    };

    var result = await _authService.RegisterAsync(registerDto, Models.Role.User);

    if (result.Succeeded)
    {
      if (_interaction.IsValidReturnUrl(model.ReturnUrl) || Url.IsLocalUrl(model.ReturnUrl))
      {
        return Redirect(model.ReturnUrl);
      }
      return Redirect("~/"); 
    }

    foreach (var error in result.Errors)
    {
      ModelState.AddModelError(string.Empty, error);
    }
    return View(model);
  }

  [HttpGet]
  public IActionResult Login(string returnUrl)
  {
    var viewModel = new LoginViewModel
    {
      ReturnUrl = returnUrl,
      RememberMe = true
    };
    return View(viewModel);
  }

  [HttpPost]
  public async Task<IActionResult> Login(LoginViewModel model)
  {
    var context = await _interaction.GetAuthorizationContextAsync(model.ReturnUrl);
    if (context == null && !string.IsNullOrEmpty(model.ReturnUrl) && !Url.IsLocalUrl(model.ReturnUrl))
    {
      ModelState.AddModelError(string.Empty, "Некорректный URL возврата.");
    }

    if (!ModelState.IsValid)
    {
      return View(model);
    }

    var loginDto = new LoginDto()
    {
      Password = model.Password,
      Username = model.Username
    };

    var result = await _authService.Login(loginDto);

    if (result.Succeeded)
    {
      if (_interaction.IsValidReturnUrl(model.ReturnUrl) || Url.IsLocalUrl(model.ReturnUrl))
      {
        return Redirect(model.ReturnUrl);
      }
      return Redirect("~/");
    }

    if (result.IsLockedOut)
    {
      ModelState.AddModelError(string.Empty, "Аккаунт заблокирован.");
    }
    else
    {
      ModelState.AddModelError(string.Empty, "Неверный логин или пароль.");
    }

    return View(model);
  
}

}
