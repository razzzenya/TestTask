namespace EnterpriseWarehouse.API.DTO;
/// <summary>
/// DTO описывающее организацию и суммарное растояние до остальных организаций
/// </summary>
public class OrganizationRemoteDistanceDTO
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
    /// Суммарное расстояние до остальных организаций
    /// </summary>
    public required double TotalDistance { get; set; }
}
