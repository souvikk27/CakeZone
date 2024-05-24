using MediatR;
namespace CakeZone.Services.Product.CQRS.Product
{
    public class DeleteProductCommand : IRequest<Model.Product>
    {
        public Guid Id { get; set; }

        public DeleteProductCommand(Guid id)
        {
            Id = id;
        }
    }
}
