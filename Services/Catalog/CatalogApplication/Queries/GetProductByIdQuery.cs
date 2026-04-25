using CatalogApplication.Responses;
using MediatR;

namespace CatalogApplication.Queries
{
    public class GetProductByIdQuery : IRequest<ProductResponseDto>
    {
        public string Id { get; set; }
        public GetProductByIdQuery(string id)
        {
            Id = id;
        }
    }
}
