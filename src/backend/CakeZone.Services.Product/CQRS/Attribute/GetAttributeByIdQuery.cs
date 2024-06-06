using MediatR;

namespace CakeZone.Services.Product.CQRS.Attribute
{
    public class GetAttributeByIdQuery : IRequest<Model.Attribute>
    {
        public GetAttributeByIdQuery(Guid id)
        {
            Id = id;
        }

        public Guid Id { get; }
    }
}
