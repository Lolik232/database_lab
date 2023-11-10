using Asp.Versioning;
using DAL.Interfaces;
using Microsoft.AspNetCore.Mvc;
using rest_api.v1.DTO;

namespace rest_api.v1;

[Route("v{version:apiVersion}/[controller]")]
[ApiController]
[ApiVersion("1.0")]
public class ShopController : ControllerBase
{
    private readonly ILogger<ShopController> _logger;
    private readonly IShopRepository _shopRepository;

    public ShopController(ILogger<ShopController> logger, IShopRepository shopRepository)
    {
        _logger = logger;
        _shopRepository = shopRepository;
    }

    [HttpGet]
    /// <summary>
    /// Данный метод возвращает список доступных магазинов
    /// </summary>
    /// <responce code = "200">Если запрос выполнился успешно</responce>
    public IEnumerable<ShopDTO> Get()
    {
        _logger.LogInformation($"{Request.Method} accepted");
        return _shopRepository.GetAll().Select(e => new ShopDTO(e.Id, e.Name, e.Address)).ToList();
    }

    /// <summary>
    /// Данный метод возвращает информацию о магазине
    /// </summary>
    /// <responce code = "200">Если по такому id нашлась запись</responce>
    /// <responce code = "404">Если по такому id не нашлась запись</responce>
    /// <param name="id">id магазина, о котором нужна информация</param>
    [HttpGet("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public ShopDetailDTO Get(int id)
    {
        _logger.LogInformation($"{Request.Method} accepted");

        var shop = _shopRepository.Get(id);
        var productStocksDTO = shop.ProductStocks.Select(ps => new ProductStockDTO(ps.Count, new ProductDTO(ps.Product.Id, ps.Product.Name, ps.Product.Price))).ToList();

        return new ShopDetailDTO(shop.Id, shop.Name, shop.Address, productStocksDTO);
    }

}
