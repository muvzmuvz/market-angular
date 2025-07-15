
namespace marketplace_api.ValueObjects;

public class ExpiresAtValueObject : ValueObject
{
  public DateTime CreatedAt { get; set; }
  public DateTime? ExpiresAt { get; set; }

  protected override IEnumerable<object> GetEqualityComponents()
  {
    yield return CreatedAt;
    yield return ExpiresAt;
  }

}
