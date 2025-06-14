namespace marketplace_api.Exceptions;

public class UserAlreadyExists : Exception
{
  public UserAlreadyExists(string message): base(message) { }
}
