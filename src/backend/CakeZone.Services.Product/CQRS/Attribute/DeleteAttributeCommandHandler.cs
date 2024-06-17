using AutoMapper;
using CakeZone.Services.Product.Repository.Attribute;
using MediatR;


namespace CakeZone.Services.Product.CQRS.Attribute
{
    public class DeleteAttributeCommandHandler : IRequestHandler<DeleteAttributeCommand, Model.Attribute>
    {
        private readonly IAttributeRepository _attributeRepository;
        private readonly IMapper _mapper;

        public DeleteAttributeCommandHandler(IAttributeRepository attributeRepository, IMapper mapper)
        {
            _attributeRepository = attributeRepository;
            _mapper = mapper;
        }


        public async Task<Model.Attribute> Handle(DeleteAttributeCommand request, CancellationToken cancellationToken)
        {
            var attribute = await _attributeRepository.GetByIdAsync(request.Id);
            attribute.IsDeleted = true;
            await _attributeRepository.UpdateAsync(attribute);
            await _attributeRepository.SaveChangesAsync();
            return attribute;
        }
    }
}
