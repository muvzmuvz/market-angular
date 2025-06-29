namespace Products.Api.Interfaces;

public interface IImageService
{
  public Task<string> AploadImage(string base64Image);
}
