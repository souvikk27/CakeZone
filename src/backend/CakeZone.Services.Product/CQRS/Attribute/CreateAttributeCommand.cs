using CakeZone.Services.Product.Shared.Attributes;
using MediatR;

namespace CakeZone.Services.Product.CQRS.Attribute
{
    public class CreateAttributeCommand : IRequest<Model.Attribute>
    {
        public CreateAttributeDto CreateAttributeDto { get; }

        public CreateAttributeCommand(CreateAttributeDto createAttributeDto)
        {
            CreateAttributeDto = createAttributeDto;
        }
    }
}
