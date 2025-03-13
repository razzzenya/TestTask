using EnterpriseWarehouse.API.DTO;
using EnterpriseWarehouse.API.Services;
using Microsoft.AspNetCore.Mvc;

namespace EnterpriseWarehouse.API.Controllers;

/// <summary>
/// Контроллер для выполнения аналитических запросов.
/// </summary>
[Route("api/[controller]")]
[ApiController]
public class QueryController(IQueryService service) : ControllerBase
{
    /// <summary>
    /// Получает список всех продуктов, отсортированных по названию.
    /// </summary>
    /// <returns>Список продуктов, отсортированных по названию.</returns>
    [HttpGet]
    [Route("sorted-products")]
    public ActionResult<IEnumerable<ProductDTO>> GetAllProductsSortedByName()
    {
        return Ok(service.GetAllProductsSortedByName());
    }

    /// <summary>
    /// Получает список продуктов, полученных указанной организацией на заданную дату.
    /// </summary>
    /// <param name="name">Название организации.</param>
    /// <param name="date">Дата поставки.</param>
    /// <returns>Список продуктов, полученных на заданную дату.</returns>
    [HttpGet]
    [Route("products-recived-on-date")]
    public ActionResult<IEnumerable<ProductDTO>> GetProductsRecieveOnDate([FromQuery] string name, [FromQuery] DateTime date)
    {
        return Ok(service.GetProductsRecieveOnDate(name, date));
    }

    /// <summary>
    /// Получает текущее состояние склада, включая информацию о ячейках.
    /// </summary>
    /// <returns>Список всех ячеек на складе.</returns>
    [HttpGet]
    [Route("warehouse-state")]
    public ActionResult<IEnumerable<CellDTO>> GetCurrentWarehouseState()
    {
        return Ok(service.GetCurrentWarehouseState());
    }

    /// <summary>
    /// Получает список организаций с максимальным количеством поставок за указанный период.
    /// </summary>
    /// <param name="startDate">Начальная дата периода.</param>
    /// <param name="endDate">Конечная дата периода.</param>
    /// <returns>Список организаций с максимальной поставкой.</returns>
    [HttpGet]
    [Route("max-supplies-organizations")]
    public ActionResult<IEnumerable<OrganizationMaxSuppliesDTO>> GetMaxSuppliesOrganizations([FromQuery] DateTime startDate, [FromQuery] DateTime endDate)
    {
        return Ok(service.GetMaxSuppliesOrganizations(startDate, endDate));
    }

    /// <summary>
    /// Получает пять продуктов с максимальном количеством на складе.
    /// </summary>
    /// <returns>Список продуктов с их количеством на складе.</returns>
    [HttpGet]
    [Route("five-max-quantity-products")]
    public ActionResult<IEnumerable<ProductQuantityDTO>> GetFiveMaxQuantityProducts()
    {
        return Ok(service.GetFiveMaxQuantityProducts());
    }

    /// <summary>
    /// Получает количество поставок продуктов для каждой организации.
    /// </summary>
    /// <returns>Список продуктов и количество их поставок по организациям.</returns>
    [HttpGet]
    [Route("get-quantity-product-supply-to-organizations")]
    public ActionResult<IEnumerable<ProductSupplyToOrganizationsDTO>> GetQuantityProductSupplyToOrganiztions()
    {
        return Ok(service.GetQuantityProductSupplyToOrganiztions());
    }

    /// <summary>
    /// Получает сведения о ближайших организациях к каждой организации.
    /// </summary>
    /// <returns>Список организаций и близжайшие к ним.</returns>
    [HttpGet]
    [Route("get-nearest-organizations")]
    public ActionResult<IEnumerable<OrganizationDistanceDTO>> GetNearestOrganizations()
    {
        return Ok(service.GetNearestOrganizations());
    }

    /// <summary>
    /// Получает топ организаций с наибольшей площадью.
    /// </summary>
    /// <returns>Список организаций с их площадью.</returns>
    [HttpGet]
    [Route("get-top-5-organizations-by-area")]
    public ActionResult<IEnumerable<OrganizationAreaDTO>> GetTop5OrganizationsByArea()
    {
        return Ok(service.GetTop5OrganizationsByArea());
    }

    /// <summary>
    /// Получает информацию об организациях, наиболее удаленных от остальных организаций.
    /// </summary>
    /// <returns>Список организаций с их суммарным расстоянием до остальных организаций.</returns>
    [HttpGet]
    [Route("get-most-remote-organizations")]
    public ActionResult<IEnumerable<OrganizationRemoteDistanceDTO>> GetMostRemoteOrganizations()
    {
        return Ok(service.GetMostRemoteOrganizations());
    }
}
