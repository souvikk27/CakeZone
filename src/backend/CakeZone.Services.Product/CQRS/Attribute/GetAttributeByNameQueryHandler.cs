using CakeZone.Services.Product.Repository.Attribute;
using MediatR;
using NuGet.Protocol.Plugins;

namespace CakeZone.Services.Product.CQRS.Attribute
{
    public class GetAttributeByNameQueryHandler : IRequestHandler<GetAttributeByNameQuery, Model.Attribute>
    {
        private readonly IAttributeRepository _attributeRepository;

        public GetAttributeByNameQueryHandler(IAttributeRepository attributeRepository)
        {
            _attributeRepository = attributeRepository;
        }


        public async Task<Model.Attribute> Handle(GetAttributeByNameQuery request, CancellationToken cancellationToken)
        {
            var attribute = await _attributeRepository.FindAsync(a => a.AttributeName == request.AttributeName);

            return !attribute.Any() ? null : attribute.First();
        }
    }
}
