using EnterpriseWarehouse.Domain.Context;
using EnterpriseWarehouse.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace EnterpriseWarehouse.Domain.Repositories;

public class CellRepository(WarehouseContext context) : IEntityRepository<Cell>
{
    public IEnumerable<Cell> GetAll()
    {
        return context.Cells
             .Include(s => s.Product);
    }

    public Cell? GetById(int id)
    {
        return context.Cells
            .Include(c => c.Product)
            .FirstOrDefault(c => c.Id == id);
    }

    public Cell Add(Cell newCell)
    {
        var cell = context.Cells.Add(newCell).Entity;
        context.SaveChanges();
        return cell;
    }

    public void Delete(Cell cell)
    {
        context.Cells.Remove(cell);
        context.SaveChanges();
    }

    public Cell Update(Cell updatedCell)
    {
        var entry = context.Entry(updatedCell);
        entry.State = EntityState.Modified;
        context.SaveChanges();
        return entry.Entity;
    }
}
