namespace marketplace_api.Common.interfaces;

public interface IImageService
{
  public Task<string> AploadImage(string base64Image);
}
