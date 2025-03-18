using EnterpriseWarehouse.API.DTO;
using EnterpriseWarehouse.API.Services;
using Microsoft.AspNetCore.Mvc;

namespace EnterpriseWarehouse.API.Controllers;

/// <summary>
/// Контроллер для управления организациями.
/// </summary>
[Route("api/[controller]")]
[ApiController]
public class OrganizationController(IEntityService<OrganizationDTO, OrganizationCreateDTO> service) : ControllerBase
{
    /// <summary>
    /// Возвращает список всех организаций.
    /// </summary>
    /// <returns>Коллекция объектов <see cref="OrganizationDTO"/>.</returns>
    /// <response code="200">Список организаций успешно возвращён.</response>
    [HttpGet]
    public async Task<ActionResult<IEnumerable<OrganizationDTO>>> Get()
    {
        return Ok(await service.GetAll());
    }

    /// <summary>
    /// Возвращает информацию об организации по её идентификатору.
    /// </summary>
    /// <param name="id">Идентификатор организации.</param>
    /// <returns>Объект <see cref="OrganizationDTO"/>, представляющий организацию.</returns>
    /// <response code="200">Организация найдена и информация успешно возвращена.</response>
    /// <response code="404">Организация с указанным идентификатором не найдена.</response>
    [HttpGet("{id}")]
    public async Task<ActionResult<OrganizationDTO>> Get(int id)
    {
        var organization = await service.GetById(id);
        if (organization == null)
        {
            return NotFound();
        }
        return Ok(organization);
    }

    /// <summary>
    /// Добавляет новую организацию.
    /// </summary>
    /// <param name="newOrganization">Объект <see cref="OrganizationCreateDTO"/>, содержащий данные новой организации.</param>
    /// <returns>Объект <see cref="OrganizationDTO"/>, представляющий добавленную организацию.</returns>
    /// <response code="200">Организация успешно добавлена.</response>
    [HttpPost]
    public async Task<ActionResult<OrganizationDTO>> Post(OrganizationCreateDTO newOrganization)
    {
        var result = await service.Add(newOrganization);
        return Ok(result);
    }

    /// <summary>
    /// Обновляет существующую организацию.
    /// </summary>
    /// <param name="id">Идентификатор обновляемой организации.</param>
    /// <param name="newOrganization">Объект <see cref="OrganizationCreateDTO"/>, содержащий обновлённые данные организации.</param>
    /// <returns>Объект <see cref="OrganizationDTO"/>, представляющий обновлённую организацию.</returns>
    /// <response code="200">Организация успешно обновлена.</response>
    /// <response code="404">Организация с указанным идентификатором не найдена.</response>
    [HttpPut("{id}")]
    public async Task<ActionResult<OrganizationDTO>> Put(int id, OrganizationCreateDTO newOrganization)
    {
        var result = await service.Update(id, newOrganization);
        if (result == null)
        {
            return NotFound();
        }
        return Ok(result);
    }

    /// <summary>
    /// Удаляет организацию по её идентификатору.
    /// </summary>
    /// <param name="id">Идентификатор организации для удаления.</param>
    /// <returns>Результат операции удаления.</returns>
    /// <response code="200">Организация успешно удалена.</response>
    /// <response code="404">Организация с указанным идентификатором не найдена.</response>
    [HttpDelete("{id}")]
    public async Task<ActionResult> Delete(int id)
    {
        var result = await service.Delete(id);
        if (!result)
        {
            return NotFound();
        }
        return Ok();
    }
}
