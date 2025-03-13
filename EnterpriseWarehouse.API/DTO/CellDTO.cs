namespace EnterpriseWarehouse.API.DTO;
/// <summary>
/// DTO ячейки
/// </summary>
public class CellDTO
{
    /// <summary>
    /// Идентификатор ячейки
    /// </summary>
    public required int Id { get; set; }
    /// <summary>
    /// Товар
    /// </summary>
    public ProductDTO? Product { get; set; }
    /// <summary>
    /// Количество товара в ячейке
    /// </summary>
    public required int Quantity { get; set; }
}
