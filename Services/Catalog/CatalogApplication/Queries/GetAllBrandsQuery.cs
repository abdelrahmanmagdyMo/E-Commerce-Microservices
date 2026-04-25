using CatalogApplication.Responses;
using MediatR;

namespace CatalogApplication.Queries
{
    public class GetAllBrandsQuery : IRequest<IList<BrandResponseDto>>
    {
    }
}
