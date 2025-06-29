using AutoMapper;
using Products.Api.Interfaces;
using Products.Api.Models;
using Products.Api.ModelsDto;

namespace Products.Api.Service;

public class ProductService : IProductService
{
  private readonly IProductRepository _productRepository;
  private readonly ILogger<ProductService> _logger;
  private readonly IMapper _mapper;
  private readonly IImageService _imageService;
  private readonly IUnitOfWork _unitOfWork;

  public ProductService(
        IProductRepository productRepository,
        ILogger<ProductService> logger,
        IMapper mapper,
        IUnitOfWork unitOfWork,
        IImageService imageService)
  {
    _productRepository = productRepository;
    _logger = logger;
    _mapper = mapper;
    _unitOfWork = unitOfWork;
    _imageService = imageService;
  }

  public async Task<ProductDto> CreateProductAsync(ProductCreateDto createProductDto)
  {
    if (createProductDto == null)
      throw new ArgumentNullException(nameof(createProductDto));

    _logger.LogInformation("маппинг обьекта  {productDto} в product ", createProductDto);
    var product = _mapper.Map<Product>(createProductDto);

    foreach(var image in createProductDto.ImagesBase64)
    {
      var imagePath = await _imageService.AploadImage(image);
      product.Images.Add(new Image()
      {
        ImagePath = imagePath,
        ProductId = product.Id
      });
    }

    var productDto = _mapper.Map<ProductDto>(await _productRepository.AddProductAsync(product));
    await _unitOfWork.commitChange();

    return productDto;
  }

  public async Task<bool> DeleteProductAsync(Guid id)
  {
    var product = await _productRepository.GetProductByIdAsync(id)
      ?? throw new Exception("такого продукта не существует");

    product.DeleteProduct();
    return true;
  }

  public async Task<IEnumerable<Product>> GetAllProductsAsync()
  {
    var products = await _productRepository.GetAllProductsAsync();

    return products;
  }

  public async Task<ICollection<ProductDto>> GetByName(string name)
  {
    var products = await _productRepository.GetByName(name);

    var productsDto = _mapper.Map<ICollection<ProductDto>>(products);

    return productsDto;
  }

  public async Task<ICollection<ProductDto>> GetByTitle(string title)
  {
    var products = await _productRepository.GetByTitle(title);

    var productsDto = _mapper.Map<ICollection<ProductDto>>(products);

    return productsDto;
  }

  public async Task<ProductDto?> GetProductByIdAsync(Guid id)
  {
    var product = await _productRepository.GetProductByIdAsync(id);

    var productDto = _mapper.Map<ProductDto>(product);

    return productDto;
  }

  public async Task<List<ProductDto>> GetProductsOfShopAsync(Guid shopId)
  {
    var products = await _productRepository.GetProductsOfShopAsync(shopId);

    var productsDto = _mapper.Map<List<ProductDto>>(products);

    return productsDto;
  }

  public async Task<ProductDto> UpdateProductAsync(Guid id, UpdateProductDto updateProductDto)
  {
    var product = await _productRepository.UpdateProductAsync(id, updateProductDto);

    return _mapper.Map<ProductDto>(product);
  }
}
