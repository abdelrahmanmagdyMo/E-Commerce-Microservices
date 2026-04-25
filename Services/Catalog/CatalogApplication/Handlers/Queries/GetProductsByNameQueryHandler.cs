using AutoMapper;
using CatalogApplication.Queries;
using CatalogApplication.Responses;
using CatalogCore.Repositories;
using MediatR;

namespace CatalogApplication.Handlers.Queries
{
    public class GetProductsByNameQueryHandler : IRequestHandler<GetProductsByNameQuery, IList<ProductResponseDto>>
    {
        private readonly IMapper _mappper;
        private readonly IProductRepository _ProductRepository;
        public GetProductsByNameQueryHandler(IMapper mapper, IProductRepository productRepository)
        {
            _mappper = mapper;
            _ProductRepository = productRepository;
        }
        public async Task<IList<ProductResponseDto>> Handle(GetProductsByNameQuery request, CancellationToken cancellationToken)
        {
            var productList = await _ProductRepository.GetAllProductsByName(request.Name);
            var productResponseList = _mappper.Map<IList<ProductResponseDto>>(productList);
            return productResponseList;
        }
    }
}
