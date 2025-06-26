using marketplace_api.Models;
using Microsoft.AspNetCore.Identity;

namespace marketplace_api.ModelsDto;

public class ShopSellerDto
{
  public Guid ShopId { get; set; }

  public UserDto Seller { get; set; }
  public Guid SellerId { get; set; }
}
