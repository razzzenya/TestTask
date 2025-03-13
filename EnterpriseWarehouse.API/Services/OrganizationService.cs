using AutoMapper;
using EnterpriseWarehouse.API.DTO;
using EnterpriseWarehouse.Domain.Entities;
using EnterpriseWarehouse.Domain.Repositories;
using NetTopologySuite.Geometries;
using NetTopologySuite.IO;

namespace EnterpriseWarehouse.API.Services;

public class OrganizationService(IEntityRepository<Organization> repository, IMapper mapper) : IEntityService<OrganizationDTO, OrganizationCreateDTO>
{
    public IEnumerable<OrganizationDTO> GetAll() => repository.GetAll().Select(mapper.Map<OrganizationDTO>);
    public OrganizationDTO? GetById(int id) => mapper.Map<OrganizationDTO>(repository.GetById(id));

    public OrganizationDTO Add(OrganizationCreateDTO newOrganization)
    {
        return mapper.Map<OrganizationDTO>(repository.Add(mapper.Map<Organization>(newOrganization)));
    }

    public bool Delete(int id)
    {
        var organization = repository.GetById(id);
        if (organization == null)
        {
            return false;
        }
        repository.Delete(organization);
        return true;
    }

    public OrganizationDTO? Update(int id, OrganizationCreateDTO updatedOrganization)
    {
        var organization = repository.GetById(id);
        if (organization == null)
        {
            return null;
        }
        var wktReader = new WKTReader();
        organization.Address = updatedOrganization.Address;
        organization.Name = updatedOrganization.Name;
        organization.Geometry = (Polygon)wktReader.Read(updatedOrganization.Geometry);
        return mapper.Map<OrganizationDTO>(repository.Update(organization));
    }
}
