using marketplace_api.Models;
using marketplace_api.ModelsDto;

namespace marketplace_api.Common.interfaces;

public interface IAuthService
{
  public Task<UserDto> RegisterAsync(RegisterDto registerDto, Role role);
  public Task Login(LoginDto loginDto);
}
