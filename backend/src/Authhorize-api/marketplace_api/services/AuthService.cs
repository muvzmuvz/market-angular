using marketplace_api.Common.interfaces;
using marketplace_api.Models;
using marketplace_api.ModelsDto;
using Microsoft.AspNetCore.Identity;

namespace marketplace_api.services;

public class AuthService : IAuthService
{
  private readonly IUserRepository _userRepository;
  private readonly UserManager<IdentityUser<Guid>> _userManager;
  private readonly IImageService _imageService;

  public AuthService(
    IUserRepository userRepository
    , UserManager<IdentityUser<Guid>> userManager
    , IImageService imageService)
  {
    _userRepository = userRepository;
    _userManager = userManager;
    _imageService = imageService;
  }

  public async Task<UserDto> RegisterAsync(RegisterDto registerDto)
  {
    var user = new IdentityUser<Guid>()
    {
      UserName = registerDto.Name,
      Email = registerDto.Email,
    };

    var imageUri = await _imageService.AploadImage(registerDto.ImageBase64);

    var result = await _userManager.CreateAsync(user, registerDto.Password);

    await _userManager.AddToRoleAsync(user, registerDto.Role.ToString());

    var domainUser = new DomainUser
    {
      IdentityId = user.Id,
      Role = registerDto.Role,
      ExpenseSummary = 0,
      imagePath = imageUri
    };

    await _userRepository.CreateUser(domainUser);

    return new UserDto
    {
      Email = registerDto.Email,
      ExpenseSummary = 0,
      Id = user.Id,
      ImagePath = imageUri,
      Name = registerDto.Name,
      Role = registerDto.Role
    };
  }
}
