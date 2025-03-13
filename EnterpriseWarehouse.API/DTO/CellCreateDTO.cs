namespace EnterpriseWarehouse.API.DTO;

/// <summary>
/// DTO для создания ячейки
/// </summary>
public class CellCreateDTO
{
    /// <summary>
    /// Идентификатор товара
    /// </summary>
    public required int ProductId { get; set; }
    /// <summary>
    /// Количество товара в ячейке
    /// </summary>
    public required int Quantity { get; set; }
}
