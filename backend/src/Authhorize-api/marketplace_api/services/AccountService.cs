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

    var userDto = new UserDto()
    {
      Email = identityUser.Email,
      ExpenseSummary = domainUser.ExpenseSummary,
      Id = identityUser.Id,
      ImagePath = domainUser.imagePath,
      Name = identityUser.UserName,
      Role = domainUser.Role,
    };

    return userDto;
  }

  public async Task<List<UserDto>> GetUsers()
  {
    var domainUsers = await _userRepository.GetUsers();
    var idenittyUsers = await _userManager.Users.ToListAsync();

    var usersDto = (from duser in domainUsers
                  join user in idenittyUsers
                  on duser.IdentityId equals user.Id
                  select new UserDto()
                  {
                    Email = user.Email,
                    ExpenseSummary = duser.ExpenseSummary,
                    Id = user.Id,
                    ImagePath = duser.imagePath,
                    Name = user.UserName,
                    Role = duser.Role,
                  }).ToList();

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

  public async Task<Role> UpdateRole(Role role, string accountId)
  {
    if (role == Role.Administrator || role == Role.Admin)
      throw new NotPErmissionDenied("не имеете  прав");

    var domainUser = await _userRepository.GetUser(new Guid(accountId));

    var identityUser = await _userManager.FindByIdAsync(accountId)
      ?? throw new UserNotFoundException($"пользователя нет с таким {accountId}  в identity");

    await _userManager.RemoveFromRolesAsync(identityUser, await _userManager.GetRolesAsync(identityUser));
    await _userManager.AddToRoleAsync(identityUser, role.ToString());

    await _userManager.UpdateAsync(identityUser);

    return role;
  }
}
