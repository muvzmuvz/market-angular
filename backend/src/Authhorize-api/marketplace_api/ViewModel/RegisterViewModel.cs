using System.ComponentModel.DataAnnotations;

namespace marketplace_api.ViewModel;

public class RegisterViewModel
{
  [Required(ErrorMessage = "Требуется имя пользователя")]
  public string Email { get; set; }

  [Required(ErrorMessage = "Требуется пароль")]
  [DataType(DataType.Password)]
  public string Password { get; set; }

  public string Name { get; set; }

  public string? ReturnUrl { get; set; }
}
