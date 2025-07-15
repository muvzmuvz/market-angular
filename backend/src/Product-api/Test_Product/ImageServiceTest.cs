using FluentAssertions;
using marketplace_api.services;
using Microsoft.AspNetCore.Hosting;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Test_Product;

public class ImageServiceTest : IDisposable
{
  private readonly Mock<IWebHostEnvironment> _envMock;
  private readonly ImageService _imageService;
  private readonly string _tempTestDir;

  public ImageServiceTest()
  {
    _tempTestDir = Path.Combine(Path.GetTempPath(), Guid.NewGuid().ToString());
    Directory.CreateDirectory(_tempTestDir);

    _envMock = new Mock<IWebHostEnvironment>();
    _envMock.Setup(x => x.WebRootPath).Returns(_tempTestDir);
    _imageService = new ImageService(_envMock.Object);
  }


  [Fact]
  public async Task AploadImage_ValidBase64Image_ShouldReturnFilePath()
  {
    // Arrange
    var validBase64 = "data:image/jpeg;base64,iVBORw0KGgoAAAANSUhEUgAAAAEAAAABCAYAAAAfFcSJAAAADUlEQVR42mP8z/C/HgAGgwJ/lK3Q6wAAAABJRU5ErkJggg==";

    // Act
    var result = await _imageService.AploadImage(validBase64);

    // Assert
    Assert.NotNull(result);
    Assert.StartsWith(Path.Combine(_tempTestDir, "images"), result);
    Assert.EndsWith(".jpg", result);
    Assert.True(File.Exists(result));
  }

  public void Dispose()
  {
    // Удаляем временную директорию после тестов
    if (Directory.Exists(_tempTestDir))
    {
      Directory.Delete(_tempTestDir, true);
    }
  }
} 
