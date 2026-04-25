using CatalogApplication.Responses;
using CatalogCore.Specs;
using MediatR;

namespace CatalogApplication.Queries
{
    public class GetAllProductsQuery : IRequest<Pagination<ProductResponseDto>>
    {
        public CatalogSpecParams Specs { get; set; }
        public GetAllProductsQuery(CatalogSpecParams specParams)
        {
            Specs = specParams;
        }
    }
}
