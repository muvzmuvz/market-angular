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

  public Task DeleteProductHistoryAsync(Guid productHistoryId)
  {
    throw new NotImplementedException();
  }

  public Task<IEnumerable<ProductHistoryDto>> GetAllProductHistoriesAsync()
  {
    throw new NotImplementedException();
  }

  public Task<IEnumerable<ProductHistoryDto>> GetProductHistoriesByProductIdAsync(Guid productId)
  {
    throw new NotImplementedException();
  }

  public Task<IEnumerable<ProductHistoryDto>> GetProductHistoriesByUserId(Guid userId)
  {
    throw new NotImplementedException();
  }

  public Task<ProductHistoryDto> GetProductHistoryByIdAsync(Guid productHistoryId)
  {
    throw new NotImplementedException();
  }
}
