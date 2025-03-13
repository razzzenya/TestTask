namespace EnterpriseWarehouse.API.DTO;
/// <summary>
/// DTO организации
/// </summary>
public class OrganizationMaxSuppliesDTO
{
    /// <summary>
    /// Идентификатор
    /// </summary>
    public required int Id { get; set; }
    /// <summary>
    /// Название
    /// </summary>
    public required string Name { get; set; }
    /// <summary>
    /// Адрес
    /// </summary>
    public required string Address { get; set; }
    ///<summary>
    /// Геометрия в виде полигона
    /// </summary>
    public required string Geometry { get; set; }
    /// <summary>
    /// Количество полученной продукции
    /// </summary>
    public required int TotalQuantity { get; set; }
}
