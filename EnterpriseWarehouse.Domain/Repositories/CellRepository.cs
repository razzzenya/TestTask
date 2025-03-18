using EnterpriseWarehouse.Domain.Context;
using EnterpriseWarehouse.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace EnterpriseWarehouse.Domain.Repositories;

public class CellRepository(WarehouseContext context) : IEntityRepository<Cell>
{
    public async Task<IEnumerable<Cell>> GetAll()
    {
        return await context.Cells
             .Include(s => s.Product)
             .ToListAsync();
    }

    public async Task<Cell?> GetById(int id)
    {
        return await context.Cells
            .Include(c => c.Product)
            .FirstOrDefaultAsync(c => c.Id == id);
    }

    public async Task<Cell> Add(Cell newCell)
    {
        var cell = await context.Cells.AddAsync(newCell);
        await context.SaveChangesAsync();
        return cell.Entity;
    }

    public async Task Delete(Cell cell)
    {
        context.Cells.Remove(cell);
        await context.SaveChangesAsync();
    }

    public async Task<Cell> Update(Cell updatedCell)
    {
        var entry = context.Entry(updatedCell);
        entry.State = EntityState.Modified;
        await context.SaveChangesAsync();
        return entry.Entity;
    }
}
