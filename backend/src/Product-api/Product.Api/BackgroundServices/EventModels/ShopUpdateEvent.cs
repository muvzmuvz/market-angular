namespace Products.Api.BackgroundServices.EventModels;

public class ShopUpdateEvent
{
  public Guid ShopId { get; set; }
  public Guid SellerId { get; set; }
}
