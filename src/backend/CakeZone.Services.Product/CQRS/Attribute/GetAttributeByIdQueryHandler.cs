using CakeZone.Services.Product.Repository.Attribute;
using MediatR;

namespace CakeZone.Services.Product.CQRS.Attribute
{
    public class GetAttributeByIdQueryHandler : IRequestHandler<GetAttributeByIdQuery, Model.Attribute>
    {
        private readonly IAttributeRepository _attributeRepository;

        public GetAttributeByIdQueryHandler(IAttributeRepository attributeRepository)
        {
            _attributeRepository = attributeRepository;
        }

        public async Task<Model.Attribute> Handle(GetAttributeByIdQuery request, CancellationToken cancellationToken)
        {
            var attribute = await _attributeRepository.GetById(request.Id);
            return attribute;
        }
    }
}
