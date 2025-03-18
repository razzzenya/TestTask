using EnterpriseWarehouse.Domain.Context;
using EnterpriseWarehouse.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace EnterpriseWarehouse.Domain.Repositories;

public class ProductRepository(WarehouseContext context) : IEntityRepository<Product>
{
    public async Task<IEnumerable<Product>> GetAll() => await context.Products.ToListAsync();

    public async Task<Product?> GetById(int id) => await context.Products.FindAsync(id);

    public async Task<Product> Add(Product newProduct)
    {
        var product = context.Products.Add(newProduct).Entity;
        await context.SaveChangesAsync();
        return product;
    }

    public async Task Delete(Product product)
    {
        context.Products.Remove(product);
        await context.SaveChangesAsync();
    }

    public async Task<Product> Update(Product updatedProduct)
    {
        var entry = context.Entry(updatedProduct);
        entry.State = EntityState.Modified;
        await context.SaveChangesAsync();
        return entry.Entity;
    }
}
