namespace Products_Api.Cron;

public class Shop
{
  public Guid Id { get; set; }
  public List<SellerInfo> Sellers { get; set; } = new();
}
