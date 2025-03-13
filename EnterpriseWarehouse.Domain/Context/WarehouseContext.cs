using EnterpriseWarehouse.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System.Data;

namespace EnterpriseWarehouse.Domain.Context;

public class WarehouseContext(DbContextOptions<WarehouseContext> options, DataProvider dataProvider) : DbContext(options)
{
    public DbSet<Cell> Cells { get; set; }
    public DbSet<Product> Products { get; set; }
    public DbSet<Organization> Organizations { get; set; }
    public DbSet<Supply> Supplies { get; set; }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<Organization>(entity =>
        {
            entity.Property(o => o.Geometry)
                  .HasColumnType("geometry(Polygon, 3857)");
            entity.HasData(dataProvider.organizations);
        });

        modelBuilder.Entity<Product>(entity =>
        {
            entity.HasData(dataProvider.products);
        });

        modelBuilder.Entity<Cell>(entity =>
        {
            entity.HasOne(c => c.Product)
                  .WithMany()
                  .HasForeignKey("product")
                  .OnDelete(DeleteBehavior.Cascade);

            entity.Navigation(c => c.Product);
            entity.HasData(dataProvider.cells.Select(c => new
            {
                c.Id,
                product = c.Product!.Id,
                c.Quantity
            }));
        });

        modelBuilder.Entity<Supply>(entity =>
        {
            entity.HasOne(s => s.Product)
                  .WithMany()
                  .HasForeignKey("product")
                  .OnDelete(DeleteBehavior.Cascade);

            entity.HasOne(s => s.Organization)
                  .WithMany()
                  .HasForeignKey("organization")
                  .OnDelete(DeleteBehavior.Cascade);

            entity.Navigation(s => s.Product);

            entity.Navigation(s => s.Organization);

            entity.HasData(dataProvider.supplies.Select(s => new
            {
                s.Id,
                s.Quantity,
                s.SupplyDate,
                product = s.Product.Id,
                organization = s.Organization.Id
            }));
        });
    }
}
