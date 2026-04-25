using CatalogApplication.Responses;
using MediatR;

namespace CatalogApplication.Queries
{
    public class GetProductsByNameQuery : IRequest<IList<ProductResponseDto>>
    {
        public string Name { get; set; }
        public GetProductsByNameQuery(string name)
        {
            Name = name;
        }
    }
}
