using Discount.Core.Repositories;
using MediatR;

namespace Discount.Application.Handlers.Commands
{
    public class DeleteDiscountCommandHandler(IDiscountRepository _discountRepository) : IRequestHandler<DeleteDiscountCommand, bool>
    {
        public async Task<bool> Handle(DeleteDiscountCommand request, CancellationToken cancellationToken)
        {
            var deleted = await _discountRepository.DeleteDiscount(request.ProductName);
            return deleted;
        }
    }
}
