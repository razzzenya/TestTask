using AutoMapper;
using EnterpriseWarehouse.API.DTO;
using EnterpriseWarehouse.Domain.Entities;
using EnterpriseWarehouse.Domain.Repositories;

namespace EnterpriseWarehouse.API.Services;

public class CellService(IEntityRepository<Cell> cellRepository, IEntityRepository<Product> productRepository, IMapper mapper) : IEntityService<CellDTO, CellCreateDTO>
{
    public IEnumerable<CellDTO> GetAll() => cellRepository.GetAll().Select(mapper.Map<CellDTO>);

    public CellDTO? GetById(int id) => mapper.Map<CellDTO>(cellRepository.GetById(id));

    public CellDTO? Add(CellCreateDTO newCell)
    {
        var product = productRepository.GetById(newCell.ProductId);
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
    public bool Delete(int id)
    {
        var cell = cellRepository.GetById(id);
        if (cell == null)
        {
            return false;
        }
        cellRepository.Delete(cell);
        return true;
    }

    public CellDTO? Update(int id, CellCreateDTO updatedCell)
    {
        var cell = cellRepository.GetById(id);
        if (cell == null)
        {
            return null;
        }
        var product = productRepository.GetById(updatedCell.ProductId);
        if (product == null)
        {
            return null;
        }
        cell.Product = product;
        cell.Quantity = updatedCell.Quantity;
        return mapper.Map<CellDTO>(cellRepository.Update(cell));
    }
}
