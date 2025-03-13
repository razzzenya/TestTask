using EnterpriseWarehouse.Domain.Context;
using EnterpriseWarehouse.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace EnterpriseWarehouse.Domain.Repositories;

public class OrganizationRepository(WarehouseContext context) : IEntityRepository<Organization>
{
    public IEnumerable<Organization> GetAll() => context.Organizations;

    public Organization? GetById(int id) => context.Organizations.Find(id);

    public Organization Add(Organization newOrganization)
    {
        var organization = context.Organizations.Add(newOrganization).Entity;
        context.SaveChanges();
        return organization;
    }

    public void Delete(Organization organization)
    {
        context.Organizations.Remove(organization);
        context.SaveChanges();
    }

    public Organization Update(Organization updatedOrganization)
    {
        var entry = context.Entry(updatedOrganization);
        entry.State = EntityState.Modified;
        context.SaveChanges();
        return entry.Entity;
    }
}
