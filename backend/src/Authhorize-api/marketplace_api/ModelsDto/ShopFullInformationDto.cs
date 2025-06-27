using marketplace_api.Models;

namespace marketplace_api.ModelsDto;

public class ShopFullInformationDto
{
  public string Name { get; set; }
  public bool IsActive { get; set; } = false;
  public string PassportOwner { get; init; }
  public string INN { get; init; }
  public string ImageOwnerPath { get; init; }
  public string Description { get; set; }
  public Guid OwnerId { get; set; }
  public UserDto Owner { get; set; }
}
