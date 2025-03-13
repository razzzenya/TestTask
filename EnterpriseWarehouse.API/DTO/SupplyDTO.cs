namespace EnterpriseWarehouse.API.DTO;
/// <summary>
/// DTO поставки
/// </summary>
public class SupplyDTO
{
    /// <summary>
    /// Идентификатор
    /// </summary>
    public required int Id { get; set; }
    /// <summary>
    /// Товар
    /// </summary>
    public required ProductDTO Product { get; set; }
    /// <summary>
    /// Организация
    /// </summary>
    public required OrganizationDTO Organization { get; set; }
    /// <summary>
    /// Дата поставки
    /// </summary>
    public required DateTime SupplyDate { get; set; }
    /// <summary>
    /// Количество поставленного товара
    /// </summary>
    public required int Quantity { get; set; }
}
