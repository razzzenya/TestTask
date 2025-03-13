namespace EnterpriseWarehouse.API.DTO;

/// <summary>
/// DTO для создания организации
/// </summary>
public class OrganizationCreateDTO
{
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
}
