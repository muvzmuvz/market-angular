using marketplace_api.Common.interfaces;
using marketplace_api.Exceptions;
using marketplace_api.Models;
using marketplace_api.ModelsDto;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace marketplace_api.services;

public class AccountService : IAccountService
{
  private readonly IUserRepository _userRepository;
  private readonly UserManager<UserIdentity> _userManager;
  private readonly IImageService _imageService;
  private readonly IUnitOfWork _unitOfWork;

  public AccountService(
    IUserRepository userRepository
    , UserManager<UserIdentity> userManager
    , IImageService imageService,
      IUnitOfWork unitOfWork)
  {
    _userRepository = userRepository;
    _userManager = userManager;
    _imageService = imageService;
    _unitOfWork = unitOfWork;
  }

  public async Task<UserDto> GetUser(string accountId)
  {
    if(accountId == null)
        throw new ArgumentNullException(nameof(accountId));

    var domainUser = await _userRepository.GetUser(new Guid(accountId));

    var identityUser = await _userManager.FindByIdAsync(accountId);
    var roles = await _userManager.GetRolesAsync(identityUser);

    var userDto = new UserDto()
    {
      Email = identityUser!.UserName!,
      ExpenseSummary = domainUser.ExpenseSummary,
      Id = identityUser.Id,
      ImagePath = domainUser.imagePath,
      Name = identityUser.FirstName,
      Roles = roles,
    };

    return userDto;
  }

  public async Task<List<UserDto>> GetUsers()
  {
    var domainUsers = await _userRepository.GetUsers();
    var identityUsers = await _userManager.Users.ToListAsync();

    var usersDto = new List<UserDto>();

    foreach (var user in identityUsers)
    {
      var domainUser = domainUsers.FirstOrDefault(du => du.IdentityId == user.Id);
      if (domainUser == null) continue;

      usersDto.Add(new UserDto()
      {
        Email = user.UserName,
        ExpenseSummary = domainUser.ExpenseSummary,
        Id = user.Id,
        ImagePath = domainUser.imagePath,
        Name = user.FirstName,
        Roles = await _userManager.GetRolesAsync(user)
      });
    }

    return usersDto;
  }

  public async Task<string> UpdateImage(string newImageBase64, string accountId)
  {
    var newImagePath = await _imageService.AploadImage(newImageBase64);

    var newDomainUser = await _userRepository.UpdateImage(newImagePath, new Guid(accountId));

    await _unitOfWork.commitChange();

    return newImageBase64;
  }

  public async Task<string> UpdateName(string newName, string accountId)
  {
    var identityUser = await _userManager.FindByIdAsync(accountId)
            ?? throw new UserNotFoundException($"пользователя нет с таким {accountId}  в identity");
    identityUser.UserName = newName;
    
    await _userManager.UpdateAsync(identityUser);

    await _unitOfWork.commitChange();

    return newName;
  }
}
