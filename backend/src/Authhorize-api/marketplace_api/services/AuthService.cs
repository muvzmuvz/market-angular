using AutoMapper;
using marketplace_api.Common.interfaces;
using marketplace_api.Models;
using marketplace_api.ModelsDto;
using marketplace_api.ViewModel;
using Microsoft.AspNetCore.Identity;

namespace marketplace_api.services;

public class AuthService : IAuthService
{
  private readonly UserManager<UserIdentity> _userManager;
  private readonly IMapper _mapper;
  private readonly SignInManager<UserIdentity> _signInManager;


  public AuthService(
     UserManager<UserIdentity> userManager
    , IMapper mapper,
      SignInManager<UserIdentity> signInManager)
  {
    _userManager = userManager;
    _mapper = mapper;
    _signInManager = signInManager;
  }

  public async Task<SignInResult> Login(LoginDto loginDto)
  {
    var user = await _userManager.FindByEmailAsync(loginDto.Username);
    if (user == null)
    {
      return SignInResult.Failed;
    }

    var result = await _signInManager.PasswordSignInAsync(
            user, 
            loginDto.Password,
            isPersistent: true, 
            lockoutOnFailure: true 
        );

    return result;
  }

  public async Task<RegistrationResult> RegisterAsync(RegisterDto registerDto, Role role)
  {
    var user = _mapper.Map<UserIdentity>(registerDto);
    user.SecurityStamp = Guid.NewGuid().ToString();
    var result = await _userManager.CreateAsync(user, registerDto.Password);

    if (!result.Succeeded)
    {
      return RegistrationResult.Failure(result.Errors.Select(e => e.Description));
    }

    await _userManager.AddToRoleAsync(user, role.ToString());

    await _signInManager.SignInAsync(user, isPersistent: false);

    var userDto = new UserDto
    {
      Email = user.Email!,
      ExpenseSummary = user.ExpenseSummary,
      Id = user.Id,
      ImagePath = user.imagePath,
      Name = user.FirstName,
      Roles = new List<string>() { role.ToString()},
    };

    return RegistrationResult.Success(userDto);
  }
}
