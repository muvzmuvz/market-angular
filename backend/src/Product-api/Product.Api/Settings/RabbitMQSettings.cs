namespace Products.Api.Settings;

public class RabbitMQSettings
{
  public string HostName { get; set; } 
  public string Username { get; set; }
  public string Password { get; set; } 
  public int Port { get; set; } = 5672; 
}
