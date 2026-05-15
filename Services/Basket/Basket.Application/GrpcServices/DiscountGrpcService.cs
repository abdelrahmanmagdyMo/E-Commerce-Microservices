using Discount.gRPC.Protos;

namespace Basket.Application.GrpcServices
{
    public class DiscountGrpcService(DiscountProtoService.DiscountProtoServiceClient _discountProtoService)
    {
        public async Task<CouponModel> GetDiscount(string productName)
        {
            var discountRequest = new GetDiscountRequest() { ProductName = productName };
            return await _discountProtoService.GetDiscountAsync(discountRequest);
        }
    }
}
