namespace Products_Api.ModelsDto;

public class ProductHistoryDto
{
  public Guid ProductId { get; set; }
  public string ProductName { get; set; }
  public Guid UserId { get; set; }
  public Guid Id { get; set; }
  public DateTime CreatedAt { get; set; } 
}
