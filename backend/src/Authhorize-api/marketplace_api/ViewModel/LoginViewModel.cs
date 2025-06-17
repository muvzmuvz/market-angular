using System.ComponentModel.DataAnnotations;

namespace marketplace_api.ViewModel;

public class LoginViewModel
{
  [Required(ErrorMessage = "Требуется имя пользователя")]
  public string Username { get; set; }

  [Required(ErrorMessage = "Требуется пароль")]
  [DataType(DataType.Password)]
  public string Password { get; set; }

  public bool RememberMe { get; set; }

  public string? ReturnUrl { get; set; }
}
