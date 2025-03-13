using EnterpriseWarehouse.API.DTO;
using EnterpriseWarehouse.API.Services;
using Microsoft.AspNetCore.Mvc;

namespace EnterpriseWarehouse.API.Controllers;

/// <summary>
/// Контроллер для управления товарами.
/// </summary>
[Route("api/[controller]")]
[ApiController]
public class ProductController(IEntityService<ProductDTO, ProductCreateDTO> service) : ControllerBase
{
    /// <summary>
    /// Возвращает список всех товаров.
    /// </summary>
    /// <returns>Коллекция объектов <see cref="ProductDTO"/>.</returns>
    /// <response code="200">Список товаров успешно возвращён.</response>
    [HttpGet]
    public ActionResult<IEnumerable<ProductDTO>> Get()
    {
        return Ok(service.GetAll());
    }

    /// <summary>
    /// Возвращает информацию о товаре по его идентификатору.
    /// </summary>
    /// <param name="id">Идентификатор товара.</param>
    /// <returns>Объект <see cref="ProductDTO"/>, представляющий товар.</returns>
    /// <response code="200">Товар найден и информация успешно возвращена.</response>
    /// <response code="404">Товар с указанным идентификатором не найден.</response>
    [HttpGet("{id}")]
    public ActionResult<ProductDTO> Get(int id)
    {
        var product = service.GetById(id);
        if (product == null)
        {
            return NotFound();
        }
        return Ok(product);
    }

    /// <summary>
    /// Добавляет новый товар.
    /// </summary>
    /// <param name="newProduct">Объект <see cref="ProductCreateDTO"/>, содержащий информацию о новом товаре.</param>
    /// <returns>Объект <see cref="ProductDTO"/>, представляющий добавленный товар.</returns>
    /// <response code="200">Товар успешно добавлен.</response>
    [HttpPost]
    public ActionResult<ProductDTO> Post(ProductCreateDTO newProduct)
    {
        var result = service.Add(newProduct);
        return Ok(result);
    }

    /// <summary>
    /// Обновляет информацию о существующем товаре.
    /// </summary>
    /// <param name="id">Идентификатор товара.</param>
    /// <param name="newProduct">Объект <see cref="ProductCreateDTO"/>, содержащий обновлённые данные товара.</param>
    /// <returns>Объект <see cref="ProductDTO"/>, представляющий обновлённый товар.</returns>
    /// <response code="200">Товар успешно обновлён.</response>
    /// <response code="404">Товар с указанным идентификатором не найден.</response>
    [HttpPut("{id}")]
    public ActionResult<ProductDTO> Put(int id, ProductCreateDTO newProduct)
    {
        var result = service.Update(id, newProduct);
        if (result == null)
        {
            return NotFound();
        }
        return Ok(result);
    }

    /// <summary>
    /// Удаляет товар по его идентификатору.
    /// </summary>
    /// <param name="id">Идентификатор товара.</param>
    /// <returns>Результат операции удаления.</returns>
    /// <response code="200">Товар успешно удалён.</response>
    /// <response code="404">Товар с указанным идентификатором не найден.</response>
    [HttpDelete("{id}")]
    public ActionResult Delete(int id)
    {
        var result = service.Delete(id);
        if (!result)
        {
            return NotFound();
        }
        return Ok();
    }
}
