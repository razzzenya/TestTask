namespace EnterpriseWarehouse.Domain.Repositories;
/// <summary>
/// Обобщенный интерфейс репозитория
/// </summary>
public interface IEntityRepository<T>
{
    /// <summary>
    ///Получение всех сущностей
    /// </summary>
    public Task<IEnumerable<T>> GetAll();

    /// <summary>
    ///Получение сущности по id
    /// </summary>
    public Task<T?> GetById(int id);

    /// <summary>
    ///Создание сущности
    /// </summary>
    public Task<T> Add(T entity);

    /// <summary>
    ///Изменение сущности
    /// </summary>
    public Task<T> Update(T entity);

    /// <summary>
    ///Удаление сущности
    /// </summary>
    public Task Delete(T entity);
}
