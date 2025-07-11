using AutoMapper;
using Products.Api.Interfaces;
using Products.Api.Models;
using Products.Api.ModelsDto;
using Products_Api.Interfaces;
using System.Net.WebSockets;

namespace Products.Api.Service;

public class ProductService : IProductService
{
  private readonly IProductRepository _productRepository;
  private readonly ILogger<ProductService> _logger;
  private readonly IMapper _mapper;
  private readonly IImageService _imageService;
  private readonly IUnitOfWork _unitOfWork;
  private readonly IRedisShopService _redisShopService;
  private readonly IProductHistoryRepository _productHistoryRepository;

  public ProductService(
        IProductRepository productRepository,
        ILogger<ProductService> logger,
        IMapper mapper,
        IUnitOfWork unitOfWork,
        IImageService imageService,
        IRedisShopService redisShopService,
        IProductHistoryRepository productHistoryRepository)
  {
    _productRepository = productRepository;
    _logger = logger;
    _mapper = mapper;
    _unitOfWork = unitOfWork;
    _imageService = imageService;
    _redisShopService = redisShopService;
    _productHistoryRepository = productHistoryRepository;
  }

  public async Task<ProductDto> CreateProductAsync(ProductCreateDto createProductDto, Guid userId)
  {
    if (createProductDto == null)
      throw new ArgumentNullException(nameof(createProductDto));

    var getSeller = await _redisShopService.ShopSeller(createProductDto.ShopId, userId);
    _logger.LogInformation("проверка на наличие продавца в магазине {getSeller}", getSeller);

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

  public async Task<bool> DeleteProductAsync(Guid shopId, Guid userId)
  {
    var getSeller = await _redisShopService.ShopSeller(shopId, userId);
    _logger.LogInformation("проверка на наличие продавца в магазине {getSeller}", getSeller);

    var product = await _productRepository.GetProductByIdAsync(shopId)
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

  public async Task<ICollection<ProductDto>> GetTopProduct(Guid userId)
  {
    if(userId == Guid.Empty)
    {
      var topProduct = await _productRepository.GetTopProduct();

      return _mapper.Map<ICollection<ProductDto>>(topProduct);
    }

    var productHistories = await _productHistoryRepository.GetProductHistoriesByUserId(userId);
    var viewedProductIds = productHistories.Select(ph => ph.ProductId).Distinct().ToList();

    var viewedProducts = await _productRepository.GetProductsByIdsAsyncForTopProduct(viewedProductIds);

    var preferredCategoryIds = viewedProducts
        .GroupBy(p => p.Category)
        .OrderByDescending(g => g.Count())
        .Select(g => g.Key)
        .ToList();

    var allProducts = await _productRepository.GetAllProductsAsync();

    var candidateProducts = allProducts
        .Where(p => !viewedProductIds.Contains(p.Id)) 
        .ToList();

    var recommended = new List<(Product product, int score)>();

    foreach (var product in candidateProducts)
    {
      int score = 0;

      if (preferredCategoryIds.Contains(product.Category))
        score += 5;

      if (score > 0)
      {
        recommended.Add((product, score));
      }
    }

    var topProducts = recommended
        .OrderByDescending(r => r.score)
        .Select(r => r.product)
        .Take(20)
        .ToList();

    return _mapper.Map<ICollection<ProductDto>>(topProducts);
  }

  public async Task<ProductDto> UpdateProductAsync(Guid id, UpdateProductDto updateProductDto)
  {
    var product = await _productRepository.UpdateProductAsync(id, updateProductDto);

    return _mapper.Map<ProductDto>(product);
  }
}
