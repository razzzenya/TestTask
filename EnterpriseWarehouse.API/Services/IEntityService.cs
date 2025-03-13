namespace EnterpriseWarehouse.API.Services;
/// <summary>
///  Интерфейс для сервисов сущностей
/// </summary>
public interface IEntityService<DTO, CreateDTO>
{
    /// <summary>
    /// Получение всех сущностей
    /// </summary>
    public IEnumerable<DTO> GetAll();

    /// <summary>
    /// Получение сущности при помощи id
    /// </summary>
    DTO? GetById(int id);

    /// <summary>
    /// Добавление сущности
    /// </summary>
    DTO? Add(CreateDTO entity);

    /// <summary>
    /// Удаление сущности
    /// </summary>
    bool Delete(int id);

    /// <summary>
    /// Изменение сущности
    /// </summary>
    DTO? Update(int id, CreateDTO updatedEntity);
}
