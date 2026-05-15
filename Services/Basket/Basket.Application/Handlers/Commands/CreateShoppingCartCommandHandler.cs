using AutoMapper;
using Basket.Application.Commands;
using Basket.Application.GrpcServices;
using Basket.Application.Responses;
using Basket.Core.Entities;
using Basket.Core.Repositories;
using MediatR;

namespace Basket.Application.Handlers.Commands
{
    public class CreateShoppingCartCommandHandler(IBasketRepository basketRepository, IMapper mapper, DiscountGrpcService _discountGrpcService) : IRequestHandler<CreateShoppingCartCommand, ShoppingCartResponse>
    {
        public async Task<ShoppingCartResponse> Handle(CreateShoppingCartCommand request, CancellationToken cancellationToken)
        {
            foreach (var item in request.Items)
            {
                var coupon = await _discountGrpcService.GetDiscount(item.ProductName);
                if (coupon is not null)
                {
                    item.Price -= coupon.Amount;
                }
            }
            var shoppingCart = await basketRepository.UpdateBasket(new ShoppingCart
            {
                UserName = request.UserName.Trim(),
                Items = request.Items
            });
            var shoppingCartResponse = mapper.Map<ShoppingCartResponse>(shoppingCart);
            return shoppingCartResponse;
        }
    }
}
