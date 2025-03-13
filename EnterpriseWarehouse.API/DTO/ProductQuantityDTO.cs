namespace EnterpriseWarehouse.API.DTO;

/// <summary>
/// DTO описывающее продукт и его количество на складе
/// </summary>
public class ProductQuantityDTO
{
    /// <summary>
    /// Название товара
    /// </summary>
    public string? ProductName { get; set; }

    /// <summary>
    /// Количество товара
    /// </summary>
    public int Quantity { get; set; }
}
