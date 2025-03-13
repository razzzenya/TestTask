using EnterpriseWarehouse.Domain.Entities;
using NetTopologySuite.Geometries;

namespace EnterpriseWarehouse.Domain.Context;

public class DataProvider
{
    private readonly GeometryFactory _geometryFactory = new(new PrecisionModel(), 3857);
    public required List<Organization> organizations;
    public required List<Product> products;
    public required List<Cell> cells;
    public required List<Supply> supplies;
    public void GenerateOrganizations()
    {
        for (int i = 1; i <= 500; i++)
        {

            var polygon = _geometryFactory.CreatePolygon(
            [
                new(i, i),
                new(i + 1, i),
                new(i + 1, i + 1),
                new(i, i + 1),
                new(i, i)
            ]);
            organizations.Add(new Organization
            {
                Id = i,
                Name = $"Organization {i}",
                Address = $"Address {i}",
                Geometry = polygon
            }
            );
        }
    }

    public void GenerateProducts()
    {
        for (int i = 1; i <= 500; i++)
        {
            products.Add(new Product
            {
                Id = i,
                Code = i + 1,
                Name = $"Product {i}"
            }
            );
        }
    }

    public void GenerateCells()
    {
        var random = new Random();
        for (int i = 1; i <= 500; i++)
        {
            cells.Add(new Cell
            {
                Id = i,
                Product = products[random.Next(0, 500)],
                Quantity = random.Next(10, 100),
            }
            );
        }
    }

    public void GenerateSupplies()
    {
        var random = new Random();
        for (int i = 1; i <= 500; i++)
        {
            supplies.Add(new Supply
            {
                Id = i,
                Product = products[random.Next(0, 500)],
                Organization = organizations[random.Next(0, 500)],
                SupplyDate = DateTime.UtcNow.AddDays(-random.Next(1, 365)),
                Quantity = random.Next(10, 101)
            }
            );
        }
    }

    public DataProvider()
    {
        organizations = new List<Organization>();
        products = new List<Product>();
        cells = new List<Cell>();
        supplies = new List<Supply>();
        GenerateOrganizations();
        GenerateProducts();
        GenerateCells();
        GenerateSupplies();
    }
}
