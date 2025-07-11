using AutoMapper;
using Products.Api.Interfaces;
using Products.Api.Models;
using Products_Api.Interfaces;
using Products_Api.ModelsDto;

namespace Products_Api.Service;

public class ProductHistoryService : IProductHistoryService
{
  private readonly IProductHistoryRepository _productHistoryRepository;
  private readonly IUnitOfWork _unitOfWork;
  private readonly IMapper _mapper;

  public ProductHistoryService(
    IProductHistoryRepository productHistoryRepository
    , IUnitOfWork unitOfWork,
    IMapper mapper)
  {
    _productHistoryRepository = productHistoryRepository;
    _unitOfWork = unitOfWork;
  }

  public async Task<ProductHistoryDto> AddProductHistoryAsync(ProductHistoryRequest productHistory)
  {
    if(productHistory == null)
    {
      throw new ArgumentNullException(nameof(productHistory), "Product history cannot be null.");
    }

    var productHistoryEntity = _mapper.Map<ProductHistory>(productHistory);

    await _productHistoryRepository.AddProductHistoryAsync(productHistoryEntity);

    await _unitOfWork.commitChange();

    return _mapper.Map<ProductHistoryDto>(productHistoryEntity);
  }

  public async Task DeleteProductHistoryAsync(Guid productHistoryId)
  {
    await _productHistoryRepository
      .DeleteProductHistoryAsync(productHistoryId);
  }

  public async Task<IEnumerable<ProductHistoryDto>> GetAllProductHistoriesAsync()
  {
    var productHistories = await _productHistoryRepository.GetAllProductHistoriesAsync();

    var productHistoryDtos = 
       _mapper.Map<IEnumerable<ProductHistoryDto>>(productHistories);

    return productHistoryDtos;
  }

  public async Task<IEnumerable<ProductHistoryDto>> GetProductHistoriesByProductIdAsync(Guid productId)
  {
    var productHistories = await _productHistoryRepository
      .GetProductHistoriesByProductIdAsync(productId);

    var productHistoryDtos =
      _mapper.Map<IEnumerable<ProductHistoryDto>>(productHistories);

    return productHistoryDtos;
  }

  public async Task<IEnumerable<ProductHistoryDto>> GetProductHistoriesByUserId(Guid userId)
  {
    var productHistories = await _productHistoryRepository
      .GetProductHistoriesByUserId(userId);

    var productHistoryDtos =  _mapper.Map<IEnumerable<ProductHistoryDto>>(productHistories);

    return productHistoryDtos;
  }

  public async Task<ProductHistoryDto> GetProductHistoryByIdAsync(Guid productHistoryId)
  {
    var productHistory = await _productHistoryRepository
      .GetProductHistoryByIdAsync(productHistoryId);

    var productHistoryDto = _mapper.Map<ProductHistoryDto>(productHistory);

    return productHistoryDto;
  }
}
