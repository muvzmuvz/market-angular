  using marketplace_api.Models;
using Microsoft.AspNetCore.Identity;

namespace marketplace_api.ModelsDto;

  public class ShopDto
  {
    public Guid Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public decimal Profit { get; set; }
    public Guid OwnerId { get; set; }
    public UserDto Owner { get; set; }
    public ICollection<ShopSellerDto> Sellers { get; set; }
  }
