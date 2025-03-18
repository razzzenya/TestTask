namespace EnterpriseWarehouse.API.Services;
/// <summary>
///  Интерфейс для сервисов сущностей
/// </summary>
public interface IEntityService<DTO, CreateDTO>
{
    /// <summary>
    /// Получение всех сущностей
    /// </summary>
    public Task<IEnumerable<DTO>> GetAll();

    /// <summary>
    /// Получение сущности при помощи id
    /// </summary>
    Task<DTO?> GetById(int id);

    /// <summary>
    /// Добавление сущности
    /// </summary>
    Task<DTO?> Add(CreateDTO entity);

    /// <summary>
    /// Удаление сущности
    /// </summary>
    Task<bool> Delete(int id);

    /// <summary>
    /// Изменение сущности
    /// </summary>
    Task<DTO?> Update(int id, CreateDTO updatedEntity);
}
