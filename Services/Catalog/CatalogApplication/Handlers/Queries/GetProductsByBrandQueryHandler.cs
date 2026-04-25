using AutoMapper;
using CatalogApplication.Queries;
using CatalogApplication.Responses;
using CatalogCore.Entities;
using CatalogCore.Repositories;
using MediatR;

namespace CatalogApplication.Handlers.Queries
{
    public class GetProductsByBrandQueryHandler : IRequestHandler<GetProductsByNameQuery, IList<ProductResponseDto>>
    {
        private readonly IMapper _mapper;
        private readonly IProductRepository _productRepository;
        public GetProductsByBrandQueryHandler(IMapper mapper, IProductRepository productRepository)
        {
            _mapper = mapper;
            _productRepository = productRepository;
        }
        public async Task<IList<ProductResponseDto>> Handle(GetProductsByNameQuery request, CancellationToken cancellationToken)
        {
            var productList = await _productRepository.GetAllProductsByBrand(request.Name);
            var productResponseList = _mapper.Map<IList<Product>, IList<ProductResponseDto>>(productList.ToList());
            return productResponseList;
        }
    }
}
