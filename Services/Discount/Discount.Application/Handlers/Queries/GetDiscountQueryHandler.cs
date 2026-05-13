using Discount.Application.Queries;
using Discount.Core.Repositories;
using Discount.gRPC.Protos;
using Grpc.Core;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Discount.Application.Handlers.Queries
{
    public class GetDiscountQueryHandler(IDiscountRepository _discountRepository, ILogger<GetDiscountQueryHandler> _logger) : IRequestHandler<GetDiscountQuery, CouponModel>
    {
        public async Task<CouponModel> Handle(GetDiscountQuery request, CancellationToken cancellationToken)
        {
            var coupon = await _discountRepository.GetDiscount(request.ProductName);
            if (coupon is null)
            {
                throw new RpcException(new Status(StatusCode.NotFound, $"There Is No Discount Available For {request.ProductName}"));
            }
            var couponModel = new CouponModel()
            {
                Id = coupon.Id,
                ProductName = coupon.ProductName,
                Description = coupon.Description,
                Amount = coupon.Amount
            };
            _logger.LogInformation($"Coupon For {request.ProductName} Is Fetched");
            return couponModel;
        }
    }
}
