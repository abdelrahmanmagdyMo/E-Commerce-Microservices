using AutoMapper;
using CatalogApplication.Responses;
using CatalogCore.Entities;
using CatalogCore.Specs;

namespace CatalogApplication.Mappers
{
    public class ProductMappingProfile : Profile
    {
        public ProductMappingProfile()
        {
            CreateMap<ProductBrand, BrandResponseDto>();

            CreateMap<ProductType, TypeResponseDto>();
            CreateMap<Product, ProductResponseDto>()
                .ForMember(d => d.Brand, op => op.MapFrom(b => b.Brand))
                .ForMember(d => d.Type, op => op.MapFrom(s => s.Type));
            CreateMap<Pagination<Product>, Pagination<ProductResponseDto>>().ReverseMap();
        }
    }
}
