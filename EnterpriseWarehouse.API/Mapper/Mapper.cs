using AutoMapper;
using EnterpriseWarehouse.API.DTO;
using EnterpriseWarehouse.Domain.Entities;
using NetTopologySuite.Features;
using NetTopologySuite.Geometries;
using NetTopologySuite.IO;

namespace EnterpriseWarehouse.API.Mapper;

public class MappingProfile : Profile
{
    private Polygon? WktToPolygon(string wkt)
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

        CreateMap<Organization, IFeature>()
            .ConstructUsing(src => new Feature
            {
                Geometry = src.Geometry,
                Attributes = new AttributesTable
                {
                    { "Id", src.Id },
                    { "Name", src.Name },
                    { "Address", src.Address }
                }
            });

        CreateMap<IFeature, Organization>()
            .ForMember(dest => dest.Id, opt => opt.MapFrom(src => (int)src.Attributes["Id"]))
            .ForMember(dest => dest.Name, opt => opt.MapFrom(src => (string)src.Attributes["Name"]))
            .ForMember(dest => dest.Address, opt => opt.MapFrom(src => (string)src.Attributes["Address"]))
            .ForMember(dest => dest.Geometry, opt => opt.MapFrom(src => (Polygon)src.Geometry));


        CreateMap<Product, ProductDTO>().ReverseMap();
        CreateMap<Product, ProductCreateDTO>().ReverseMap();

        CreateMap<Cell, CellDTO>().ReverseMap();
        CreateMap<Cell, CellCreateDTO>().ReverseMap();

        CreateMap<Supply, SupplyDTO>().ReverseMap();
    }
}
