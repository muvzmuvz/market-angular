using AutoMapper;
using FluentAssertions;
using Microsoft.Extensions.Logging;
using Moq;
using Products.Api.Interfaces;
using Products.Api.Models;
using Products.Api.ModelsDto;
using Products.Api.Service;
using Products_Api.Interfaces;

namespace Test_Product;

public class ProductServiceTest
{
  private readonly Mock<IProductRepository> _productRepository = new Mock<IProductRepository>();
  private readonly Mock<ILogger<ProductService>> _logger = new Mock<ILogger<ProductService>>();
  private readonly Mock<IMapper> _mapper = new Mock<IMapper>();
  private readonly Mock<IImageService> _imageService = new Mock<IImageService>();
  private readonly Mock<IUnitOfWork> _unitOfWork = new Mock<IUnitOfWork>();
  private readonly Mock<IRedisShopService>   _redisShopService = new Mock<IRedisShopService>();
  private readonly Mock<IProductHistoryRepository> _productHistoryRepository =
    new Mock<IProductHistoryRepository>();
  private readonly IProductService _productService;

  public ProductServiceTest()
  {
    _productService = new ProductService(
      _productRepository.Object
      , _logger.Object
      , _mapper.Object
      , _unitOfWork.Object
      , _imageService.Object
      , _redisShopService.Object
      , _productHistoryRepository.Object);
  }

    [Fact]
    public async Task CreateProduct_WhenValid_SholdReturnNewProdicut()
    {
    // Arrange
    var shopId = Guid.NewGuid();
    var userId = Guid.NewGuid();
    var productId = Guid.NewGuid();

    var productCreateDto = new ProductCreateDto
    {
      Name = "Test Product",
      Price = 100,
      UserId = userId,
      ShopId = shopId,
      Description = "Test Description",
      Title = "Test Title",
      Category = Category.Compucter,
      CountProduct = 10,
      Characteristic = "Test Characteristic",
      ImagesBase64 = new List<string> { "image1Base64" }
    };

    var expectedProduct = new Product
    {
      Id = productId,
      Name = productCreateDto.Name,
      Price = productCreateDto.Price,
      ShopId = shopId
    };

    var expectedProductDto = new ProductDto
    {
      Id = productId,
      Name = productCreateDto.Name,
      Price = productCreateDto.Price
    };

    _imageService.Setup(x => x.AploadImage(It.IsAny<string>()))
        .ReturnsAsync("path/to/image.jpg");

    _redisShopService.Setup(x => x.ShopSeller(shopId, userId))
        .ReturnsAsync(true);

    _mapper.Setup(x => x.Map<Product>(productCreateDto))
        .Returns(expectedProduct);

    _mapper.Setup(x => x.Map<ProductDto>(It.Is<Product>(p => p.Id == productId)))
        .Returns(expectedProductDto);

    _productRepository.Setup(x => x.AddProductAsync(It.IsAny<Product>()))
        .ReturnsAsync(expectedProduct);

    _unitOfWork.Setup(x => x.commitChange())
        .Returns(Task.CompletedTask);

    // Act
    var result = await _productService.CreateProductAsync(productCreateDto, userId);

    // Assert
    result.Should().NotBeNull();
    result.Should().BeEquivalentTo(expectedProductDto);

    _redisShopService.Verify(x => x.ShopSeller(shopId, userId), Times.Once);
    _imageService.Verify(x => x.AploadImage(It.IsAny<string>()), Times.Once);
    _productRepository.Verify(x => x.AddProductAsync(It.IsAny<Product>()), Times.Once);
    _unitOfWork.Verify(x => x.commitChange(), Times.Once);
    _mapper.Verify(x => x.Map<Product>(productCreateDto), Times.Once);
    _mapper.Verify(x => x.Map<ProductDto>(It.IsAny<Product>()), Times.Once);
  }

  [Fact]
  public async Task CreateProduct_WhenSellerNotInShop_ShouldThrowException()
  {
    // Arrange
    var shopId = Guid.NewGuid();
    var userId = Guid.NewGuid();

    var invalidProductCreateDto = new ProductCreateDto
    {
      ShopId = shopId,
      UserId = userId,
      Name = "Test Product",
      Price = 100,
      ImagesBase64 = new List<string> { "image1Base64" }
    };

    _redisShopService.Setup(x => x.ShopSeller(shopId, userId))
        .ReturnsAsync(false);

    // Act + Assert
    await Assert.ThrowsAsync<Exception>(() =>
        _productService.CreateProductAsync(invalidProductCreateDto, userId));

    _redisShopService.Verify(x => x.ShopSeller(shopId, userId), Times.Once);
    _imageService.Verify(x => x.AploadImage(It.IsAny<string>()), Times.Never);
    _productRepository.Verify(x => x.AddProductAsync(It.IsAny<Product>()), Times.Never);
    _unitOfWork.Verify(x => x.commitChange(), Times.Never);
  }

  public async Task GetProductById_ShouldReturnProductDto()
  {
    // Arrange
    var productId = Guid.NewGuid();
    var expectedProduct = new Product
    {
      Id = productId,
      Name = "Test Product",
      Price = 100
    };
    var expectedProductDto = new ProductDto
    {
      Id = productId,
      Name = "Test Product",
      Price = 100
    };
    _productRepository.Setup(x => x.GetProductByIdAsync(productId))
        .ReturnsAsync(expectedProduct);

    _mapper.Setup(x => x.Map<ProductDto>(expectedProduct))
        .Returns(expectedProductDto);

    // Act
    var result = await _productService.GetProductByIdAsync(productId);

    // Assert
    result.Should().NotBeNull();
    result.Should().BeEquivalentTo(expectedProductDto);
    _productRepository.Verify(x => x.GetProductByIdAsync(productId), Times.Once);
    _mapper.Verify(x => x.Map<ProductDto>(expectedProduct), Times.Once);
  }
}
