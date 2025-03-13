using AutoMapper;
using EnterpriseWarehouse.API.DTO;
using EnterpriseWarehouse.Domain.Entities;
using EnterpriseWarehouse.Domain.Repositories;

namespace EnterpriseWarehouse.API.Services;

public class ProductService(IEntityRepository<Product> repository, IMapper mapper) : IEntityService<ProductDTO, ProductCreateDTO>
{
    public IEnumerable<ProductDTO> GetAll() => repository.GetAll().Select(mapper.Map<ProductDTO>);

    public ProductDTO? GetById(int id) => mapper.Map<ProductDTO>(repository.GetById(id));

    public ProductDTO Add(ProductCreateDTO newProduct) => mapper.Map<ProductDTO>(repository.Add(mapper.Map<Product>(newProduct)));

    public bool Delete(int id)
    {
        var product = repository.GetById(id);
        if (product == null)
        {
            return false;
        }
        repository.Delete(product);
        return true;
    }

    public ProductDTO? Update(int id, ProductCreateDTO updatedProduct)
    {
        var product = repository.GetById(id);
        if (product == null)
        {
            return null;
        }
        product.Name = updatedProduct.Name;
        product.Code = updatedProduct.Code;
        return mapper.Map<ProductDTO>(repository.Update(product));
    }
}
