using AutoMapper;
using CatalogApplication.Commands;
using CatalogApplication.Responses;
using CatalogCore.Entities;
using CatalogCore.Repositories;
using MediatR;

namespace CatalogApplication.Handlers.Commands
{
    public class CreateProductCommandHandler : IRequestHandler<CreateProductCommand, ProductResponseDto>
    {
        private readonly IMapper _mapper;
        private readonly IProductRepository _productRepository;
        public CreateProductCommandHandler(IMapper mapper, IProductRepository productRepository)
        {
            _mapper = mapper;
            _productRepository = productRepository;
        }
        public async Task<ProductResponseDto> Handle(CreateProductCommand request, CancellationToken cancellationToken)
        {
            var productEntity = _mapper.Map<Product>(request);
            var newProduct = await _productRepository.CreateProduct(productEntity);
            var productDto = _mapper.Map<ProductResponseDto>(newProduct);
            return productDto;

        }
    }
}
