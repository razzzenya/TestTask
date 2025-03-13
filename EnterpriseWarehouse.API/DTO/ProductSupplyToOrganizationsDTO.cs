namespace EnterpriseWarehouse.API.DTO;

/// <summary>
/// DTO описывающее количество товара поставленного организации
/// </summary>
public class ProductSupplyToOrganizationsDTO
{
    /// <summary>
    /// Количество товара
    /// </summary>
    public int TotalQuantity { get; set; }

    /// <summary>
    /// Название товара
    /// </summary>
    public required string ProductName { get; set; }

    /// <summary>
    /// Название организации
    /// </summary>
    public required string OrganizationName { get; set; }
}
