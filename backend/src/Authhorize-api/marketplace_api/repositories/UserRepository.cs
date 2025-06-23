using marketplace_api.Common.interfaces;
using marketplace_api.Common.Persistence;
using marketplace_api.Exceptions;
using marketplace_api.Models;
using Microsoft.EntityFrameworkCore;
using System.Runtime.InteropServices;

namespace marketplace_api.repositories;

public class UserRepository : IUserRepository
{
  private readonly AuthorizeDbContext _authorizeDbContext;

  public UserRepository(AuthorizeDbContext authorizeDbContext)
  {
    _authorizeDbContext = authorizeDbContext;
  }

  public async Task<DomainUser> CreateUser(DomainUser user)
  {
    var domainUser = await _authorizeDbContext.DomainUser.FindAsync(user.Id);

    if (domainUser != null)
    {
      throw new UserAlreadyExists($"данный пользователь уже существует с таким {user.Id}");
    }

    await _authorizeDbContext.DomainUser.AddAsync(user);

    return user;
  }

  public async Task<DomainUser> GetUser(Guid identityUserId)
  {
    var user = await _authorizeDbContext.DomainUser.FirstOrDefaultAsync(domainUser => domainUser.IdentityId == identityUserId)
                ?? throw new UserNotFoundException($"пользователя с таким {identityUserId} нет");

    return user;
  }

  public async Task<List<DomainUser>> GetUsers()
  {
    var users = await _authorizeDbContext.DomainUser.ToListAsync();

    return users;
  }

  public async Task<DomainUser> UpdateImage(string newImagePath, Guid identityUserId)
  {
    var domainUser = await GetUser(identityUserId);

    domainUser.imagePath = newImagePath;

    return domainUser;
  }
}
