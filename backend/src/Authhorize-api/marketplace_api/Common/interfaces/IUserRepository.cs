using marketplace_api.Models;

namespace marketplace_api.Common.interfaces;

public interface IUserRepository
{
  public Task<DomainUser> GetUser(Guid userId);
  public Task<DomainUser> CreateUser(DomainUser user);
}
