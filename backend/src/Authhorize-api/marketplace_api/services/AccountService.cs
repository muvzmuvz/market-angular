using AutoMapper;
using marketplace_api.Common.interfaces;
using marketplace_api.Exceptions;
using marketplace_api.Models;
using marketplace_api.ModelsDto;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace marketplace_api.services;

public class AccountService : IAccountService
{
  private readonly UserManager<UserIdentity> _userManager;
  private readonly IImageService _imageService;
  private readonly IMapper  _mapper;

  public AccountService(
     UserManager<UserIdentity> userManager
    , IImageService imageService
    , IMapper mapper)
  {
    _userManager = userManager;
    _imageService = imageService;
    _mapper = mapper;
  }

  public async Task<UserDto> GetUser(string accountId)
  {
    if(accountId == null)
        throw new ArgumentNullException(nameof(accountId));

    var identityUser = await _userManager.FindByIdAsync(accountId);
    var roles = await _userManager.GetRolesAsync(identityUser);

    var userDto = _mapper.Map<UserDto>(identityUser);
    userDto.Roles = roles;

    return userDto;
  }

  public async Task<List<UserDto>> GetUsers()
  {
    var identityUsers = await _userManager.Users.ToListAsync();
    var userDtoList = new List<UserDto>();

    foreach (var user in identityUsers)
    {
      var roles = await _userManager.GetRolesAsync(user);
      var userDto = _mapper.Map<UserDto>(user);
      userDto.Roles = roles.ToList();
      userDtoList.Add(userDto);
    }

    return userDtoList;
  }

  public async Task<string> UpdateImage(string newImageBase64, string accountId)
  {
    var newImagePath = await _imageService.AploadImage(newImageBase64);

    var userDto = await _userManager.FindByIdAsync(accountId)
        ?? throw new UserNotFoundException("такой user  не найден");
      
    userDto.imagePath = newImagePath;
    await _userManager.UpdateAsync(userDto);

    return newImageBase64;
  }

  public async Task<string> UpdateName(string newName, string accountId)
  {
    var identityUser = await _userManager.FindByIdAsync(accountId)
            ?? throw new UserNotFoundException($"пользователя нет с таким {accountId}  в identity");
    identityUser.FirstName = newName;
    
    await _userManager.UpdateAsync(identityUser);

    return identityUser.FirstName;
  }
}
