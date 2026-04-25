using AutoMapper;
using CatalogApplication.Queries;
using CatalogApplication.Responses;
using CatalogCore.Entities;
using CatalogCore.Repositories;
using MediatR;

namespace CatalogApplication.Handlers.Queries
{
    public class GetAllBrandsQueryHandler : IRequestHandler<GetAllBrandsQuery, IList<BrandResponseDto>>
    {
        private readonly IMapper _mapper;
        private readonly IBrandRepository _brandRepository;
        public GetAllBrandsQueryHandler(IMapper mapper, IBrandRepository brandRepository)
        {
            _mapper = mapper;
            _brandRepository = brandRepository;
        }
        public async Task<IList<BrandResponseDto>> Handle(GetAllBrandsQuery request, CancellationToken ct)
        {
            var brandList = await _brandRepository.GetAllBrands();
            var brandResponseList = _mapper.Map<IList<ProductBrand>, IList<BrandResponseDto>>
                 (brandList.ToList());
            return brandResponseList;
        }
    }
}
