namespace owrder_api.Models;

public class BaseEntity
{
  public Guid Id { get; set; }
  public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
}
