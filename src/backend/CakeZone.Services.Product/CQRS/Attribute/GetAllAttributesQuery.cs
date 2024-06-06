using CakeZone.Services.Product.Services;
using CakeZone.Services.Product.Services.Filters;
using MediatR;

namespace CakeZone.Services.Product.CQRS.Attribute
{
    public class GetAllAttributesQuery : IRequest<PagedList<Model.Attribute>>
    {
        public GetAllAttributesQuery(AttributeParameter parameter)
        {
            Parameter = parameter;
        }

        public AttributeParameter Parameter { get; }
    }
}
