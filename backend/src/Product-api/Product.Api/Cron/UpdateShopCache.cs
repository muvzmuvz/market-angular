using Products.Api.Interfaces;
using System.Text.Json;

namespace Products_Api.Cron;

public class UpdateShopCache
{
  private readonly ILogger<UpdateShopCache> _logger;
  private readonly IRedisShopService _redisShopService;
  private readonly HttpClient _httpClient;

  public UpdateShopCache(
    ILogger<UpdateShopCache> logger
    , IRedisShopService redisShopService
    , HttpClient httpClient)
  {
    _httpClient = httpClient;
    _httpClient.BaseAddress = new Uri("http://localhost:5042/shops/active");

    _logger = logger;
    _redisShopService = redisShopService;
    _httpClient = httpClient;
  }


  public async Task GetDataFromApiAsync()
  {
    var response = await _httpClient.GetAsync("");
    var content = await response.Content.ReadAsStringAsync();

    var options = new JsonSerializerOptions
    {
      PropertyNameCaseInsensitive = true
    };

    var apiShops = JsonSerializer.Deserialize<List<ApiShop>>(content, options);

    _logger.LogInformation("After deserialization - first shop ID: {Id}", apiShops?.FirstOrDefault()?.Id);

    var shops = apiShops.Select(apiShop =>
    {
      var shop = new Shop
      {
        Id = apiShop.Id,
        Sellers = apiShop.Sellers.Select(s => new SellerInfo
        {
          SellerId = s.SellerId
        }).ToList()
      };

      _logger.LogInformation("Mapping shop - ID: {Id}", shop.Id);
      return shop;
    }).ToList();

    _logger.LogInformation("Before cache update - first shop ID: {Id}", shops.FirstOrDefault()?.Id);

    await _redisShopService.UpdateShopCacheAsync(shops);
  }

  public class ApiShop
  {
    public Guid Id { get; set; }
    public List<ApiSeller> Sellers { get; set; } = new();
  }

  public class ApiSeller
  {
    public Guid ShopId { get; set; }
    public Guid SellerId { get; set; }
  }
}
