namespace owrder_api.Models;

public class Review : BaseEntity
{
  public int ProductId { get; set; }
  public Guid UserId { get; set; }
  public string CustomerName { get; set; } = string.Empty;
  public string CustomerEmail { get; set; } = string.Empty;
  public int Rating { get; set; } 
  public string Comments { get; set; } = string.Empty;
}
