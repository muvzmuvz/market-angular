using marketplace_api.Models;
using marketplace_api.ModelsDto;

namespace marketplace_api.Common.interfaces;

public interface IAccountService
{
  public Task<UserDto> GetUser(string accountId);
  public Task<List<UserDto>> GetUsers();
  public Task<string> UpdateImage(string newImageBase64, string accountId);
  public Task<string> UpdateName(string newName, string accountId);
  public Task<Role> UpdateRole(Role role, string accountId);
}
