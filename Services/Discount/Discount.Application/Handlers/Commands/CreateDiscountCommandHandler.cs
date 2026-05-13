using AutoMapper;
using Discount.Application.Commands;
using Discount.Core.Entities;
using Discount.Core.Repositories;
using Discount.gRPC.Protos;
using MediatR;

namespace Discount.Application.Handlers.Commands
{
    public class CreateDiscountCommandHandler(IDiscountRepository _discountRepository, IMapper _mapper) : IRequestHandler<CreateDiscountCommand, CouponModel>
    {
        public async Task<CouponModel> Handle(CreateDiscountCommand request, CancellationToken cancellationToken)
        {
            var coupon = _mapper.Map<Coupon>(request);
            await _discountRepository.CreateDiscount(coupon);
            var couponModel = _mapper.Map<CouponModel>(coupon);
            return couponModel;
        }
    }
}

