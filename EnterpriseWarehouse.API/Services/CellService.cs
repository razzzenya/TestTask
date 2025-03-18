using AutoMapper;
using EnterpriseWarehouse.API.DTO;
using EnterpriseWarehouse.Domain.Entities;
using EnterpriseWarehouse.Domain.Repositories;

namespace EnterpriseWarehouse.API.Services;

public class CellService(IEntityRepository<Cell> cellRepository, IEntityRepository<Product> productRepository, IMapper mapper) : IEntityService<CellDTO, CellCreateDTO>
{
    public async Task<IEnumerable<CellDTO>> GetAll()
    {
        var cells = await cellRepository.GetAll();
        return cells.Select(mapper.Map<CellDTO>);
    }

    public async Task<CellDTO?> GetById(int id)
    {
        var cell = await cellRepository.GetById(id);
        return mapper.Map<CellDTO>(cell);
    }

    public async Task<CellDTO?> Add(CellCreateDTO newCell)
    {
        var product = await productRepository.GetById(newCell.ProductId);
        if (product == null)
        {
            return null;
        }
        var cell = new Cell
        {
            Product = product,
            Quantity = newCell.Quantity,
        };
        return mapper.Map<CellDTO>(cellRepository.Add(cell));
    }
    public async Task<bool> Delete(int id)
    {
        var cell = await cellRepository.GetById(id);
        if (cell == null)
        {
            return false;
        }
        await cellRepository.Delete(cell);
        return true;
    }

    public async Task<CellDTO?> Update(int id, CellCreateDTO updatedCell)
    {
        var cell = await cellRepository.GetById(id);
        if (cell == null)
        {
            return null;
        }
        var product = await productRepository.GetById(updatedCell.ProductId);
        if (product == null)
        {
            return null;
        }
        cell.Product = product;
        cell.Quantity = updatedCell.Quantity;
        return mapper.Map<CellDTO>(cellRepository.Update(cell));
    }
}
