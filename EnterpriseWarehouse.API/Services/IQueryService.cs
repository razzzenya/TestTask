using EnterpriseWarehouse.API.DTO;

namespace EnterpriseWarehouse.API.Services;
/// <summary>
/// Интерфейс для запросов
/// </summary>
public interface IQueryService
{
    /// <summary>
    /// Получить все товары отсортирванные по названию
    /// </summary>
    public Task<List<ProductDTO>> GetAllProductsSortedByName();

    /// <summary>
    /// Получить сведения о продукции предприятия, полученной в указанный день получателем продукции
    /// </summary>
    public Task<List<ProductDTO>> GetProductsRecieveOnDate(string name, DateTime date);

    /// <summary>
    /// Получить состояние склада на текущий момент с указанием номеров ячеек склада и их содержимого
    /// </summary>
    public Task<List<CellDTO>> GetCurrentWarehouseState();

    /// <summary>
    /// Получить информацию об организациях, получивших максимальный объем продукции за заданный период
    /// </summary>
    public Task<List<OrganizationMaxSuppliesDTO>> GetMaxSuppliesOrganizations(DateTime startDate, DateTime endDate);

    /// <summary>
    /// Получить топ 5 товаров по наличию на складе.
    /// </summary>
    public Task<List<ProductQuantityDTO>> GetFiveMaxQuantityProducts();

    /// <summary>
    /// Получить информацию о количестве поставленного товара по каждому товару и каждой организации
    /// </summary>
    public Task<List<ProductSupplyToOrganizationsDTO>> GetQuantityProductSupplyToOrganiztions();

    /// <summary>
    /// Получить информацию о близжайших организациях к каждой организации
    /// </summary>
    public Task<List<OrganizationDistanceDTO>> GetNearestOrganizations();

    /// <summary>
    /// Получить топ 5 организаций с наибольшей площадью
    /// </summary>
    public Task<List<OrganizationAreaDTO>> GetTop5OrganizationsByArea();

    /// <summary>
    /// Получить нформацию об организациях, наиболее удаленных от остальных организаций
    /// </summary>
    public Task<List<OrganizationRemoteDistanceDTO>> GetMostRemoteOrganizations();
}
