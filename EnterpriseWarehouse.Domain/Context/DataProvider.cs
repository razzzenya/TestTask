using EnterpriseWarehouse.Domain.Entities;
using NetTopologySuite.Geometries;

namespace EnterpriseWarehouse.Domain.Context;

public class DataProvider
{
    private static readonly Envelope SamaraRegionEnvelope = new(5335671.7541069425642490, 5851651.2207264527678490, 6759127.5581133114174008, 7299460.4899313366040587);
    private readonly GeometryFactory _geometryFactory = new(new PrecisionModel(), 3857);
    public required List<Organization> organizations;
    public required List<Product> products;
    public required List<Cell> cells;
    public required List<Supply> supplies;

    public Polygon GeneratePolygon()
    {
        var random = new Random();
        double x = SamaraRegionEnvelope.MinX + random.NextDouble() * (SamaraRegionEnvelope.MaxX - SamaraRegionEnvelope.MinX);
        double y = SamaraRegionEnvelope.MinY + random.NextDouble() * (SamaraRegionEnvelope.MaxY - SamaraRegionEnvelope.MinY);
        double width = random.NextDouble() * 1000 + 500;
        double height = random.NextDouble() * 1000 + 500;

        var coordinates = new Coordinate[]
        {
            new Coordinate(x, y),
            new Coordinate(x + width, y),
            new Coordinate(x + width, y + height),
            new Coordinate(x, y + height),
            new Coordinate(x, y)
        };
        return _geometryFactory.CreatePolygon(coordinates);
    }

    public void GenerateOrganizations()
    {
        for (int i = 1; i <= 500; i++)
        {
            organizations.Add(new Organization
            {
                Id = i,
                Name = $"Organization {i}",
                Address = $"Address {i}",
                Geometry = GeneratePolygon()
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
