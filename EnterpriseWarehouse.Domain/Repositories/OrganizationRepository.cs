using EnterpriseWarehouse.Domain.Context;
using EnterpriseWarehouse.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace EnterpriseWarehouse.Domain.Repositories;

public class OrganizationRepository(WarehouseContext context) : IEntityRepository<Organization>
{
    public async Task<IEnumerable<Organization>> GetAll() => await context.Organizations.ToListAsync();

    public async Task<Organization?> GetById(int id) => await context.Organizations.FindAsync(id);

    public async Task<Organization> Add(Organization newOrganization)
    {
        var organization = context.Organizations.Add(newOrganization).Entity;
        await context.SaveChangesAsync();
        return organization;
    }

    public async Task Delete(Organization organization)
    {
        context.Organizations.Remove(organization);
        await context.SaveChangesAsync();
    }

    public async Task<Organization> Update(Organization updatedOrganization)
    {
        var entry = context.Entry(updatedOrganization);
        entry.State = EntityState.Modified;
        await context.SaveChangesAsync();
        return entry.Entity;
    }
}
