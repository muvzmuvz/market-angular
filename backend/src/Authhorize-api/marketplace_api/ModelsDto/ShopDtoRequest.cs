using marketplace_api.Models;

namespace marketplace_api.ModelsDto;

public class ShopDtoRequest
{
  public string Name { get; set; }
  public string Description { get; set; }
  public Guid UserId { get; set; }
  public string PassportOwner { get; set; }
  public string INN { get; set; }
  public string OwnerImageBase64 { get; set; }
}
