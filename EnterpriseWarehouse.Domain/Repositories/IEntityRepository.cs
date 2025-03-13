namespace EnterpriseWarehouse.Domain.Repositories;
/// <summary>
/// Обобщенный интерфейс репозитория
/// </summary>
public interface IEntityRepository<T>
{
    /// <summary>
    ///Получение всех сущностей
    /// </summary>
    public IEnumerable<T> GetAll();

    /// <summary>
    ///Получение сущности по id
    /// </summary>
    public T? GetById(int id);

    /// <summary>
    ///Создание сущности
    /// </summary>
    public T Add(T entity);

    /// <summary>
    ///Изменение сущности
    /// </summary>
    public T Update(T entity);

    /// <summary>
    ///Удаление сущности
    /// </summary>
    public void Delete(T entity);
}
