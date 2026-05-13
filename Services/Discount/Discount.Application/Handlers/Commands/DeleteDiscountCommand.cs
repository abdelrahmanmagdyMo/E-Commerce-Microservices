using MediatR;

namespace Discount.Application.Handlers.Commands
{
    public class DeleteDiscountCommand : IRequest<bool>
    {
        public string ProductName { get; set; }
        public DeleteDiscountCommand(string productName)
        {
            ProductName = productName;
        }
    }
}
