using CatalogApplication.Responses;
using MediatR;

namespace CatalogApplication.Queries
{
    public class GetProductsByBrandQuery : IRequest<IList<ProductResponseDto>>
    {
        public string BrandName { get; set; }
        public GetProductsByBrandQuery(string brandName)
        {
            BrandName = brandName;
        }
    }
}
