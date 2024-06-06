using CakeZone.Services.Product.Shared.Attributes;
using MediatR;

namespace CakeZone.Services.Product.CQRS.Attribute
{
    public class UpdateAttributeCommand : IRequest<Model.Attribute>
    {
        public UpdateAttributeDto UpdateAttributeDto { get; }

        public UpdateAttributeCommand(UpdateAttributeDto updateAttributeDto)
        {
            UpdateAttributeDto = updateAttributeDto;
        }
    }
}
