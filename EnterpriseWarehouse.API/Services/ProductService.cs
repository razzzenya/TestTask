using AutoMapper;
using EnterpriseWarehouse.API.DTO;
using EnterpriseWarehouse.Domain.Entities;
using EnterpriseWarehouse.Domain.Repositories;

namespace EnterpriseWarehouse.API.Services;

public class ProductService(IEntityRepository<Product> repository, IMapper mapper) : IEntityService<ProductDTO, ProductCreateDTO>
{
    public async Task<IEnumerable<ProductDTO>> GetAll()
    {
       var products = await repository.GetAll();
       return products.Select(mapper.Map<ProductDTO>);
    }

    public async Task<ProductDTO?> GetById(int id)
    {
        var product = await repository.GetById(id);
        return mapper.Map<ProductDTO>(product);
    }
    public async Task<ProductDTO?> Add(ProductCreateDTO newProduct)
    {
        var product = await repository.Add(mapper.Map<Product>(newProduct));
        return mapper.Map<ProductDTO>(product);
    }

    public async Task<bool> Delete(int id)
    {
        var product = await repository.GetById(id);
        if (product == null)
        {
            return false;
        }
        await repository.Delete(product);
        return true;
    }

    public async Task<ProductDTO?> Update(int id, ProductCreateDTO updatedProduct)
    {
        var product = await repository.GetById(id);
        if (product == null)
        {
            return null;
        }
        product.Name = updatedProduct.Name;
        product.Code = updatedProduct.Code;
        return mapper.Map<ProductDTO>(await repository.Update(product));
    }
}
