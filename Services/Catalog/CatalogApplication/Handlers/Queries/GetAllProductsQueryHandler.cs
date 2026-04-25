using AutoMapper;
using CatalogApplication.Queries;
using CatalogApplication.Responses;
using CatalogCore.Repositories;
using CatalogCore.Specs;
using MediatR;

namespace CatalogApplication.Handlers.Queries
{
    public class GetAllProductsQueryHandler :
        IRequestHandler<GetAllProductsQuery, Pagination<ProductResponseDto>>
    {
        private readonly IMapper _mapper;
        private readonly IProductRepository _productRepository;
        public GetAllProductsQueryHandler(IMapper mapper, IProductRepository productRepository)
        {
            _mapper = mapper;
            _productRepository = productRepository;
        }
        public async Task<Pagination<ProductResponseDto>> Handle(GetAllProductsQuery request, CancellationToken ct)
        {
            var productList = await _productRepository.GetAllProducts(request.Specs);
            var productResponseList = _mapper.Map<Pagination<ProductResponseDto>>(productList);

            return productResponseList;
        }
    }
}
