namespace EnterpriseWarehouse.API.DTO;

/// <summary>
/// DTO для создания товара
/// </summary>
public class ProductCreateDTO
{
    /// <summary>
    /// Код товара
    /// </summary>
    public required int Code { get; set; }

    /// <summary>
    /// Название
    /// </summary>
    public required string Name { get; set; }
}
