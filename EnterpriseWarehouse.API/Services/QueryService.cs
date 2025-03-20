using AutoMapper;
using EnterpriseWarehouse.API.DTO;
using EnterpriseWarehouse.Domain.Entities;
using EnterpriseWarehouse.Domain.Repositories;

namespace EnterpriseWarehouse.API.Services;

public class QueryService(IEntityRepository<Organization> organizationRepository, IEntityRepository<Cell> cellRepository, IEntityRepository<Supply> supplyRepository, IMapper mapper) : IQueryService
{
    public async Task<List<ProductDTO>> GetAllProductsSortedByName()
    {
        var cells = await cellRepository.GetAll();
        return cells.OrderBy(c => c.Product?.Name)
                .Select(c => mapper.Map<ProductDTO>(c.Product))
                .ToList();
    }

    public async Task<List<ProductDTO>> GetProductsRecieveOnDate(string name, DateTime date)
    {
        var supplies = await supplyRepository.GetAll();
        return supplies
            .Where(s => s.Organization.Name == name && s.SupplyDate.Date == date.Date)
            .Select(s => mapper.Map<ProductDTO>(s.Product))
            .ToList();
    }

    public async Task<List<CellDTO>> GetCurrentWarehouseState()
    {
        var cells = await cellRepository.GetAll();
        return mapper.Map<List<CellDTO>>(cells);
    }

    public async Task<List<OrganizationMaxSuppliesDTO>> GetMaxSuppliesOrganizations(DateTime startDate, DateTime endDate)
    {
        var supplies = await supplyRepository.GetAll();
        var organizationsWithMaxSupply = supplies
            .Where(s => s.SupplyDate >= startDate && s.SupplyDate <= endDate)
            .GroupBy(s => s.Organization)
            .Select(g => new
            {
                Organization = g.Key,
                TotalQuantity = g.Sum(s => s.Quantity)
            })
            .OrderByDescending(o => o.TotalQuantity)
            .Take(10)
            .ToList();

        if (organizationsWithMaxSupply.Count == 0)
        {
            return [];
        }

        var maxQuantity = organizationsWithMaxSupply.Max(o => o.TotalQuantity);

        var result = organizationsWithMaxSupply
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

    public async Task<List<ProductQuantityDTO>> GetFiveMaxQuantityProducts()
    {
        var cells = await cellRepository.GetAll();
        return cells
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

    public async Task<List<ProductSupplyToOrganizationsDTO>> GetQuantityProductSupplyToOrganiztions()
    {
        var supplies = await supplyRepository.GetAll();
        return supplies
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

    public async Task<List<OrganizationDistanceDTO>> GetNearestOrganizations()
    {
        var organizations = await organizationRepository.GetAll();
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

    public async Task<List<OrganizationAreaDTO>> GetTop5OrganizationsByArea()
    {
        var organizations = await organizationRepository.GetAll();
        return organizations
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

    public async Task<List<OrganizationRemoteDistanceDTO>> GetMostRemoteOrganizations()
    {
        var organizations = await organizationRepository.GetAll();
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
