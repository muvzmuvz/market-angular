using marketplace_api.Models;
using marketplace_api.ModelsDto;
using marketplace_api.ViewModel;
using Microsoft.AspNetCore.Identity;

namespace marketplace_api.Common.interfaces;

public interface IAuthService
{
  public Task<RegistrationResult> RegisterAsync(RegisterDto registerDto, Role role);
  public Task<SignInResult> Login(LoginDto loginDto);
}
