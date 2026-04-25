using AutoMapper;
using CatalogApplication.Queries;
using CatalogApplication.Responses;
using CatalogCore.Entities;
using CatalogCore.Repositories;
using MediatR;

namespace CatalogApplication.Handlers.Queries
{

    public class GetAllTypesQueryHandler : IRequestHandler<GetAllTypesQuery, IList<TypeResponseDto>>
    {
        private readonly IMapper _mapper;
        private readonly ITypeRepository _typeRepository;
        public GetAllTypesQueryHandler(IMapper mapper, ITypeRepository typeRepository)
        {
            _mapper = mapper;
            _typeRepository = typeRepository;
        }
        public async Task<IList<TypeResponseDto>> Handle(GetAllTypesQuery request, CancellationToken cancellationToken)
        {
            var typeList = await _typeRepository.GetALlTypes();
            var typeResponseList = _mapper.Map<IList<ProductType>, IList<TypeResponseDto>>
                (typeList.ToList());
            return typeResponseList;
        }
    }
}
