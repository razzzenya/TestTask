namespace EnterpriseWarehouse.API.DTO;
/// <summary>
/// DTO описывающее организацию и ее площадь
/// </summary>
public class OrganizationAreaDTO
{
    /// <summary>
    /// Идентификатор организации
    /// </summary>
    public required int Id { get; set; }
    /// <summary>
    /// Название организации
    /// </summary>
    public required string Name { get; set; }
    /// <summary>
    /// Расстояние до близжайшей организации
    /// </summary>
    public required double Area { get; set; }
}
