using MediatR;

namespace CakeZone.Services.Product.CQRS.Attribute
{
    public class GetAttributeByNameQuery : IRequest<Model.Attribute>
    {
        public GetAttributeByNameQuery(string attributeName)
        {
            AttributeName = attributeName;
        }

        public string AttributeName { get; }
    }
}
