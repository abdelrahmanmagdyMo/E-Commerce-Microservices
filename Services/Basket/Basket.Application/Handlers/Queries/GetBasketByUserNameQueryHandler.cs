using AutoMapper;
using Basket.Application.Queries;
using Basket.Application.Responses;
using Basket.Core.Repositories;
using MediatR;

namespace Basket.Application.Handlers.Queries
{
    public class GetBasketByUserNameQueryHandler(IBasketRepository basketRepository, IMapper mapper) : IRequestHandler<GetBasketByUserNameQuery, ShoppingCartResponse>
    {
        public async Task<ShoppingCartResponse> Handle(GetBasketByUserNameQuery request, CancellationToken cancellationToken)
        {
            var shoppingCart = await basketRepository.GetBasket(request.UserName);
            var shoppingCartResponse = mapper.Map<ShoppingCartResponse>(shoppingCart);
            return shoppingCartResponse;
        }
    }
}
