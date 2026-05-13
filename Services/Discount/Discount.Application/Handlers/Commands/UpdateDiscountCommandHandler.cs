using AutoMapper;
using Discount.Application.Commands;
using Discount.Core.Entities;
using Discount.Core.Repositories;
using Discount.gRPC.Protos;
using MediatR;

namespace Discount.Application.Handlers.Commands
{
    public class UpdateDiscountCommandHandler(IDiscountRepository _discountRepository, IMapper _mapper) : IRequestHandler<UpdateDiscountCommand, CouponModel>
    {
        public async Task<CouponModel> Handle(UpdateDiscountCommand request, CancellationToken cancellationToken)
        {
            var coupon = _mapper.Map<Coupon>(request);
            await _discountRepository.UpdateDiscount(coupon);
            var couponModel = _mapper.Map<CouponModel>(coupon);
            return couponModel;
        }
    }
}
