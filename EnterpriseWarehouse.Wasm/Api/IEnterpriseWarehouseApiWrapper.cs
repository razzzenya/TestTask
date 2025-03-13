
namespace EnterpriseWarehouse.Wasm.Api;

public interface IEnterpriseWarehouseApiWrapper
{
    Task<CellDTO> CreateCell(CellCreateDTO newCell);
    Task<OrganizationDTO> CreateOrganization(OrganizationCreateDTO newOrganization);
    Task<ProductDTO> CreateProduct(ProductCreateDTO newProduct);
    Task<SupplyDTO> CreateSupply(SupplyCreateDTO newSupply);
    Task DeleteCell(int id);
    Task DeleteOrganization(int id);
    Task DeleteProduct(int id);
    Task DeleteSupply(int id);
    Task<IEnumerable<CellDTO>> GetAllCells();
    Task<IEnumerable<OrganizationDTO>> GetAllOrganizations();
    Task<IEnumerable<ProductDTO>> GetAllProducts();
    Task<IEnumerable<SupplyDTO>> GetAllSupplies();
    Task<CellDTO> GetCell(int id);
    Task<OrganizationDTO> GetOrganization(int id);
    Task<ProductDTO> GetProduct(int id);
    Task<SupplyDTO> GetSupply(int id);
    Task<CellDTO> UpdateCell(int id, CellCreateDTO newCell);
    Task<OrganizationDTO> UpdateOrganization(int id, OrganizationCreateDTO newOrganization);
    Task<ProductDTO> UpdateProduct(int id, ProductCreateDTO newProduct);
    Task<SupplyDTO> UpdateSupply(int id, SupplyCreateDTO newSupply);
    Task<IEnumerable<ProductDTO>> GetAllProductsSortedByName();
    Task<IEnumerable<ProductDTO>> GetProductsRecieveOnDate(string name, DateTime date);
    Task<IEnumerable<CellDTO>> GetCurrentWarehouseState();
    Task<IEnumerable<OrganizationMaxSuppliesDTO>> GetMaxSuppliesOrganizations(DateTime startDate, DateTime endDate);
    Task<IEnumerable<ProductQuantityDTO>> GetFiveMaxQuantityProducts();
    Task<IEnumerable<ProductSupplyToOrganizationsDTO>> GetQuantityProductSupplyToOrganiztions();
    Task<IEnumerable<OrganizationDistanceDTO>> GetNearestOrganizations();
    Task<IEnumerable<OrganizationAreaDTO>> GetTop5OrganizationsByArea();
    Task<IEnumerable<OrganizationRemoteDistanceDTO>> GetMostRemoteOrganizations();
}