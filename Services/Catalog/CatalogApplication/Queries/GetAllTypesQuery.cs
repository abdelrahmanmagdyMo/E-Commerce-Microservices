using CatalogApplication.Responses;
using MediatR;

namespace CatalogApplication.Queries
{
    public class GetAllTypesQuery : IRequest<IList<TypeResponseDto>>
    {
    }
}
