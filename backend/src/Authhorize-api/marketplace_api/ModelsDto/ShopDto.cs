  using marketplace_api.Models;

  namespace marketplace_api.ModelsDto;

  public class ShopDto
  {
    public string Name { get; set; }
    public string Description { get; set; }
    public Guid OwnerId { get; set; }
    public DomainUser Owner { get; set; }
    public decimal Profit { get; set; }
    public Guid UserId { get; set; }
    public ICollection<ShopSellerDto> Sellers { get; set; }
  }
