using AutoMapper;
using EnterpriseWarehouse.API.DTO;
using EnterpriseWarehouse.Domain.Entities;
using EnterpriseWarehouse.Domain.Repositories;

namespace EnterpriseWarehouse.API.Services;

public class QueryService(IEntityRepository<Organization> organizationRepository, IEntityRepository<Cell> cellRepository, IEntityRepository<Supply> supplyRepository, IMapper mapper) : IQueryService
{
    public List<ProductDTO> GetAllProductsSortedByName()
    {
        return cellRepository.GetAll()
            .OrderBy(c => c.Product?.Name)
            .Select(c => mapper.Map<ProductDTO>(c.Product))
            .ToList();
    }

    public List<ProductDTO> GetProductsRecieveOnDate(string name, DateTime date)
    {
        return supplyRepository.GetAll()
            .Where(s => s.Organization.Name == name && s.SupplyDate.Date == date.Date)
            .Select(s => mapper.Map<ProductDTO>(s.Product))
            .ToList();
    }

    public List<CellDTO> GetCurrentWarehouseState()
    {
        return mapper.Map<List<CellDTO>>(cellRepository.GetAll());
    }

    public List<OrganizationMaxSuppliesDTO> GetMaxSuppliesOrganizations(DateTime startDate, DateTime endDate)
    {
        var organizationsWithMaxSupply = supplyRepository.GetAll()
            .Where(s => s.SupplyDate >= startDate && s.SupplyDate <= endDate)
            .GroupBy(s => s.Organization)
            .Select(g => new
            {
                Organization = g.Key,
                TotalQuantity = g.Sum(s => s.Quantity)
            })
            .ToList();

        if (organizationsWithMaxSupply.Count == 0)
        {
            return [];
        }

        var maxQuantity = organizationsWithMaxSupply.Max(o => o.TotalQuantity);

        var result = organizationsWithMaxSupply
            .Where(o => o.TotalQuantity == maxQuantity)
            .Select(o => new OrganizationMaxSuppliesDTO
            {
                Id = o.Organization.Id,
                Name = o.Organization.Name,
                Address = o.Organization.Address,
                Geometry = o.Organization.Geometry.ToText(),
                TotalQuantity = o.TotalQuantity
            })
            .ToList();

        return result;
    }

    public List<ProductQuantityDTO> GetFiveMaxQuantityProducts()
    {
        return cellRepository.GetAll()
            .GroupBy(c => c.Product?.Name)
            .Select(g => new ProductQuantityDTO
            {
                ProductName = g.Key,
                Quantity = g.Sum(c => c.Quantity),
            })
            .OrderByDescending(p => p.Quantity)
            .Take(5)
            .ToList();
    }

    public List<ProductSupplyToOrganizationsDTO> GetQuantityProductSupplyToOrganiztions()
    {
        return supplyRepository.GetAll()
            .GroupBy(p => new { ProductName = p.Product.Name, OrganizationName = p.Organization.Name })
            .Select(g => new ProductSupplyToOrganizationsDTO
            {
                TotalQuantity = g.Sum(p => p.Quantity),
                ProductName = g.Key.ProductName,
                OrganizationName = g.Key.OrganizationName,
            })
            .OrderByDescending(p => p.TotalQuantity)
            .ToList();
    }

    public List<OrganizationDistanceDTO> GetNearestOrganizations()
    {
        var organizations = organizationRepository.GetAll().ToList();
        var result = new List<OrganizationDistanceDTO>();

        foreach (var org in organizations)
        {
            var nearestOrg = organizations
                .Where(o => o.Id != org.Id)
                .OrderBy(o => o.Geometry.Distance(org.Geometry))
                .FirstOrDefault();
            if (nearestOrg != null)
            {
                result.Add(new OrganizationDistanceDTO
                {
                    Name = org.Name,
                    NearestOrganization = nearestOrg.Name,
                    Distance = org.Geometry.Distance(nearestOrg.Geometry)
                });
            }
        }
        return result;
    }

    public List<OrganizationAreaDTO> GetTop5OrganizationsByArea()
    {
        return organizationRepository.GetAll()
        .Select(org => new OrganizationAreaDTO
        {
            Id = org.Id,
            Name = org.Name,
            Area = org.Geometry.Area
        })
        .OrderByDescending(o => o.Area)
        .Take(5)
        .ToList();
    }

    public List<OrganizationRemoteDistanceDTO> GetMostRemoteOrganizations()
    {
        var organizations = organizationRepository.GetAll().ToList();
        return organizations
        .Select(org => new OrganizationRemoteDistanceDTO
        {
            Id = org.Id,
            Name = org.Name,
            TotalDistance = organizations
                .Where(o => o.Id != org.Id)
                .Sum(o => o.Geometry.Distance(org.Geometry))
        })
        .OrderByDescending(o => o.TotalDistance)
        .ToList();
    }
}
