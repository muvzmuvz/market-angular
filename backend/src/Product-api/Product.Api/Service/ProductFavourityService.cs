using AutoMapper;
using Products.Api.Interfaces;
using Products.Api.Models;
using Products_Api.Interfaces;
using Products_Api.ModelsDto;

namespace Products_Api.Service;

public class ProductFavourityService : IProductFavourityService
{
  private readonly IProductFavourityRepository _productFavourityRepository;
  private readonly IProductRepository _productRepository;
  private readonly IUnitOfWork _unitOfWork;
  private readonly IMapper _mapper;
  private readonly ILogger<ProductFavourityService> _logger;

  public ProductFavourityService(
    IProductFavourityRepository productFavourityRepository
    ,IProductRepository productRepository,
    IUnitOfWork unitOfWork
    , IMapper mapper,
    ILogger<ProductFavourityService> logger)
  {
    _productFavourityRepository = productFavourityRepository;
    _productRepository = productRepository;
    _unitOfWork = unitOfWork;
    _mapper = mapper;
    _logger = logger;
  }

  public async Task<ProductFavourityDto> AddProductToFavourity(Guid userId, ProductFavourityRequest productFavourity)
  {
    _logger.LogInformation
      ("Adding product to favourity for user {UserId} and product {ProductId}", userId, productFavourity.ProductId);
    var product = await _productRepository.GetProductByIdAsync(productFavourity.ProductId);

    _logger.LogInformation("Product found: {ProductName}", product?.Name);
    var productFavourityModel = new ProductFavourity
    {
      UserId = userId,
      ProductId = productFavourity.ProductId,
      Product = product
    };

    _logger.LogInformation
      ("Creating product favourity model for user {UserId} and product {ProductId}", userId, productFavourity.ProductId);
    await _productFavourityRepository.AddProductToFavourity(userId, productFavourityModel);
    await _unitOfWork.commitChange();

    _logger.LogInformation
      ("Product favourity added for user {UserId} and product {ProductId}", userId, productFavourity.ProductId);
    return _mapper.Map<ProductFavourityDto>(productFavourityModel);
  }

  public async Task<List<ProductFavourityDto>> GetProductFavourities(Guid userId)
  {
    _logger.LogInformation("Retrieving product favourities for user {UserId}", userId);
    var favourities = await _productFavourityRepository.productFavourities(userId);

    _logger.LogInformation
      ("Found {Count} product favourities for user {UserId}", favourities.Count, userId);

    return _mapper.Map<List<ProductFavourityDto>>(favourities);
  }

  public async Task RemoveProductFromFavourity(Guid userId, Guid productId)
  {
    _logger.LogInformation
      ("Removing product {ProductId} from favourity for user {UserId}", productId, userId);

    await _productFavourityRepository.RemoveProductFromFavourity(userId, productId);
  }
}
