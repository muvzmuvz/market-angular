using marketplace_api.ModelsDto;

namespace marketplace_api.ViewModel;

public class RegistrationResult
{
  public bool Succeeded { get; set; }
  public UserDto User { get; set; }
  public IEnumerable<string> Errors { get; set; } = new List<string>();

  public static RegistrationResult Failure(IEnumerable<string> errors)
      => new RegistrationResult { Succeeded = false, Errors = errors };

  public static RegistrationResult Success(UserDto user)
      => new RegistrationResult { Succeeded = true, User = user };
}
