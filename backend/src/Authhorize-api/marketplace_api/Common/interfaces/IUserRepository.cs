using marketplace_api.Models;

namespace marketplace_api.Common.interfaces;

public interface IUserRepository
{
  public Task<DomainUser> GetUser(Guid identityUserId);
  public Task<DomainUser> CreateUser(DomainUser user);
  public Task<List<DomainUser>> GetUsers();
  public Task<DomainUser> UpdateImage(string newImagePath, Guid identityUserId);
}
