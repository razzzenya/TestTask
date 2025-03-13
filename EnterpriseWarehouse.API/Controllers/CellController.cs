using EnterpriseWarehouse.API.DTO;
using EnterpriseWarehouse.API.Services;
using Microsoft.AspNetCore.Mvc;

namespace EnterpriseWarehouse.API.Controllers;

/// <summary>
/// Контроллер для управления ячейками склада.
/// </summary>
[Route("api/[controller]")]
[ApiController]
public class CellController(IEntityService<CellDTO, CellCreateDTO> service) : ControllerBase
{
    /// <summary>
    /// Возвращает список всех ячеек склада.
    /// </summary>
    /// <returns>Коллекция объектов <see cref="CellDTO"/>.</returns>
    /// <response code="200">Список ячеек успешно возвращён.</response>
    [HttpGet]
    public ActionResult<IEnumerable<CellDTO>> Get()
    {
        return Ok(service.GetAll());
    }

    /// <summary>
    /// Получает информацию о ячейке склада по её идентификатору.
    /// </summary>
    /// <param name="id">Идентификатор ячейки.</param>
    /// <returns>Объект <see cref="CellDTO"/>, представляющий ячейку.</returns>
    /// <response code="200">Ячейка найдена и информация успешно возвращена.</response>
    /// <response code="404">Ячейка с указанным идентификатором не найдена.</response>
    [HttpGet("{id}")]
    public ActionResult<CellDTO> Get(int id)
    {
        var cell = service.GetById(id);
        if (cell == null)
        {
            return NotFound();
        }
        return Ok(cell);
    }

    /// <summary>
    /// Добавляет новую ячейку на склад.
    /// </summary>
    /// <param name="newCell">Объект <see cref="CellCreateDTO"/>, содержащий информацию о новой ячейке.</param>
    /// <returns>Объект <see cref="CellDTO"/>, представляющий добавленную ячейку.</returns>
    /// <response code="200">Ячейка успешно добавлена.</response>
    /// <response code="400">Ошибка при добавлении ячейки.</response>
    [HttpPost]
    public ActionResult<CellDTO> Post(CellCreateDTO newCell)
    {
        var result = service.Add(newCell);
        if (result == null)
        {
            return BadRequest();
        }
        return Ok(result);
    }

    /// <summary>
    /// Обновляет информацию о существующей ячейке.
    /// </summary>
    /// <param name="id">Идентификатор ячейки.</param>
    /// <param name="newCell">Объект <see cref="CellCreateDTO"/>, содержащий обновлённые данные ячейки.</param>
    /// <returns>Объект <see cref="CellDTO"/>, представляющий обновлённую ячейку.</returns>
    /// <response code="200">Ячейка успешно обновлена.</response>
    /// <response code="404">Ячейка с указанным идентификатором не найдена.</response>
    [HttpPut("{id}")]
    public ActionResult<CellDTO> Put(int id, CellCreateDTO newCell)
    {
        var result = service.Update(id, newCell);
        if (result == null)
        {
            return NotFound();
        }
        return Ok(result);
    }

    /// <summary>
    /// Удаляет ячейку склада по её идентификатору.
    /// </summary>
    /// <param name="id">Идентификатор ячейки.</param>
    /// <returns>Результат операции удаления.</returns>
    /// <response code="200">Ячейка успешно удалена.</response>
    /// <response code="404">Ячейка с указанным идентификатором не найдена.</response>
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
