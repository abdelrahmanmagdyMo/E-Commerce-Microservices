using MediatR;

namespace CatalogApplication.Commands
{
    public class DeleteProductCommand : IRequest<bool>
    {
        public string Id { get; set; }
        public DeleteProductCommand(string id)
        {
            Id = id;
        }
    }
}
