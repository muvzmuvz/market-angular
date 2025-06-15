namespace marketplace_api.Exceptions;

public class NotPErmissionDenied : Exception
{
  public NotPErmissionDenied(string message) : base(message) { }
}
