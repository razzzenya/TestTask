using EnterpriseWarehouse.Domain.Context;
using EnterpriseWarehouse.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace EnterpriseWarehouse.Domain.Repositories;

public class SupplyRepository(WarehouseContext context) : IEntityRepository<Supply>
{
    public async Task<IEnumerable<Supply>> GetAll()
    {
        return await context.Supplies
            .Include(s => s.Product)
            .Include(s => s.Organization)
            .ToListAsync();
    }

    public async Task<Supply?> GetById(int id)
    {
        return await context.Supplies
            .Include(s => s.Product)
            .Include(s => s.Organization)
            .FirstOrDefaultAsync(s => s.Id == id);
    }

    public async Task<Supply> Add(Supply newSupply)
    {
        var supply = context.Supplies.Add(newSupply).Entity;
        await context.SaveChangesAsync();
        return supply;
    }

    public async Task Delete(Supply supply)
    {
        context.Supplies.Remove(supply);
        await context.SaveChangesAsync();
    }

    public async Task<Supply> Update(Supply updatedSupply)
    {
        var entry = context.Entry(updatedSupply);
        entry.State = EntityState.Modified;
        await context.SaveChangesAsync();
        return entry.Entity;
    }
}
