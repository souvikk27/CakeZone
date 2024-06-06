using MediatR;

namespace CakeZone.Services.Product.CQRS.Attribute
{
    public class DeleteAttributeCommand : IRequest<Model.Attribute>
    {
        public Guid Id { get; }

        public DeleteAttributeCommand(Guid id)
        {
            Id = id;
        }
    }
}
