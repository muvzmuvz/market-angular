using marketplace_api.Models;

namespace marketplace_api.ModelsDto;

public class UserDto
{
  public Guid Id { get; set; }
  public string Name { get; set; }
  public string Email { get; set; }
  public string ImagePath { get; set; }
  public decimal ExpenseSummary { get; set; }
  public Role Role { get; set; }
}
