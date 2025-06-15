using marketplace_api.Models;

namespace marketplace_api.ModelsDto;

public class RegisterDto
{
  public string Name { get; set; }
  public string Password { get; set; }
  public string Email { get; set; }
}
