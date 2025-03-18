using AutoMapper;
using EnterpriseWarehouse.API.DTO;
using EnterpriseWarehouse.Domain.Entities;
using EnterpriseWarehouse.Domain.Repositories;
using NetTopologySuite.Geometries;
using NetTopologySuite.IO;

namespace EnterpriseWarehouse.API.Services;

public class OrganizationService(IEntityRepository<Organization> repository, IMapper mapper) : IEntityService<OrganizationDTO, OrganizationCreateDTO>
{
    public async Task<IEnumerable<OrganizationDTO>> GetAll()
    {
        var organizations = await repository.GetAll();
        return organizations.Select(mapper.Map<OrganizationDTO>);
    }
    public async Task<OrganizationDTO?> GetById(int id)
    {
        var organization = await repository.GetById(id);
        return mapper.Map<OrganizationDTO>(organization);
    }
    public async Task<OrganizationDTO?> Add(OrganizationCreateDTO newOrganization)
    {
        var organization = await repository.Add(mapper.Map<Organization>(newOrganization));
        return mapper.Map<OrganizationDTO>(organization);
    }

    public async Task<bool> Delete(int id)
    {
        var organization = await repository.GetById(id);
        if (organization == null)
        {
            return false;
        }
        await repository.Delete(organization);
        return true;
    }

    public async Task<OrganizationDTO?> Update(int id, OrganizationCreateDTO updatedOrganization)
    {
        var organization = await repository.GetById(id);
        if (organization == null)
        {
            return null;
        }
        var wktReader = new WKTReader();
        organization.Address = updatedOrganization.Address;
        organization.Name = updatedOrganization.Name;
        organization.Geometry = (Polygon)wktReader.Read(updatedOrganization.Geometry);
        return mapper.Map<OrganizationDTO>(await repository.Update(organization));
    }
}
