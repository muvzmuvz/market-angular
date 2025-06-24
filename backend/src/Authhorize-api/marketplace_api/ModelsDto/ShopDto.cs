  using marketplace_api.Models;

  namespace marketplace_api.ModelsDto;

  public class ShopDto
  {
    public Guid Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public decimal Profit { get; set; }
    public Guid UserId { get; set; }
    public ICollection<ShopSellerDto> Sellers { get; set; }
  }
