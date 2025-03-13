namespace EnterpriseWarehouse.API.DTO;
/// <summary>
/// DTO описывающее близжайшую организацию и расстояние до неё
/// </summary>
public class OrganizationDistanceDTO
{
    /// <summary>
    /// Название организации
    /// </summary>
    public required string Name { get; set; }
    /// <summary>
    /// Название близжайшей организации
    /// </summary>
    public required string NearestOrganization { get; set; }
    /// <summary>
    /// Расстояние до близжайшей организации
    /// </summary>
    public required double Distance { get; set; }
}
