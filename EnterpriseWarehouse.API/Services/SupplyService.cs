using AutoMapper;
using EnterpriseWarehouse.API.DTO;
using EnterpriseWarehouse.Domain.Entities;
using EnterpriseWarehouse.Domain.Repositories;

namespace EnterpriseWarehouse.API.Services;

public class SupplyService(IEntityRepository<Supply> supplyRepository, IEntityRepository<Organization> organizationRepository, IEntityRepository<Product> productRepository, IMapper mapper) : IEntityService<SupplyDTO, SupplyCreateDTO>
{
    public async Task<IEnumerable<SupplyDTO>> GetAll()
    {
       var supplies = await supplyRepository.GetAll();
       return supplies.Select(mapper.Map<SupplyDTO>);
    }

    public async Task<SupplyDTO?> GetById(int id)
    {
        var supply = await supplyRepository.GetById(id);
        return mapper.Map<SupplyDTO>(supply);
    }

    public async Task<SupplyDTO?> Add(SupplyCreateDTO newSupply)
    {
        var organization = await organizationRepository.GetById(newSupply.OrganizationId);
        var product = await productRepository.GetById(newSupply.ProductId);
        if (organization == null || product == null)
        {
            return null;
        }
        var supply = new Supply
        {
            Organization = organization,
            Product = product,
            SupplyDate = newSupply.SupplyDate,
            Quantity = newSupply.Quantity,
        };
        return mapper.Map<SupplyDTO>(await supplyRepository.Add(supply));
    }

    public async Task<bool> Delete(int id)
    {
        var supply = await supplyRepository.GetById(id);
        if (supply == null)
        {
            return false;
        }
        await supplyRepository.Delete(supply);
        return true;
    }

    public async Task<SupplyDTO?> Update(int id, SupplyCreateDTO updatedSupply)
    {
        var supply = await supplyRepository.GetById(id);
        if (supply == null)
        {
            return null;
        }
        var product = await productRepository.GetById(updatedSupply.ProductId);
        var organization = await organizationRepository.GetById(updatedSupply.OrganizationId);
        if (product == null || organization == null)
        {
            return null;
        }
        supply.Product = product;
        supply.Organization = organization;
        supply.SupplyDate = updatedSupply.SupplyDate;
        supply.Quantity = updatedSupply.Quantity;
        var newSupply = await supplyRepository.Update(supply);
        return mapper.Map<SupplyDTO>(newSupply);
    }
}
