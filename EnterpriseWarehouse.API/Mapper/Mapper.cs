using AutoMapper;
using EnterpriseWarehouse.API.DTO;
using EnterpriseWarehouse.Domain.Entities;
using NetTopologySuite.Geometries;
using NetTopologySuite.IO;

namespace EnterpriseWarehouse.API.Mapper;

public class MappingProfile : Profile
{
    private Polygon WktToPolygon(string wkt)
    {
        if (string.IsNullOrEmpty(wkt))
        {
            return null;
        }
        var reader = new WKTReader();
        return (Polygon)reader.Read(wkt);
    }
    public MappingProfile()
    {
        CreateMap<OrganizationCreateDTO, Organization>()
            .ForMember(dest => dest.Geometry, opt => opt.MapFrom(src => WktToPolygon(src.Geometry))).ReverseMap();

        CreateMap<Organization, OrganizationDTO>()
              .ForMember(dest => dest.Geometry, opt => opt.MapFrom(src => src.Geometry.ToText())).ReverseMap();

        //CreateMap<Organization, OrganizationDTO>().ReverseMap();
        //CreateMap<Organization, OrganizationCreateDTO>().ReverseMap();

        CreateMap<Product, ProductDTO>().ReverseMap();
        CreateMap<Product, ProductCreateDTO>().ReverseMap();

        CreateMap<Cell, CellDTO>().ReverseMap();

        CreateMap<Supply, SupplyDTO>().ReverseMap();
    }
}
