using AutoMapper;
using CakeZone.Services.Product.Repository.Attribute;
using MediatR;

namespace CakeZone.Services.Product.CQRS.Attribute
{
    public class UpdateAttributeCommandHandler : IRequestHandler<UpdateAttributeCommand, Model.Attribute>
    {
        private readonly IAttributeRepository _attributeRepository; 
        private readonly IMapper _mapper;

        public UpdateAttributeCommandHandler(IAttributeRepository attributeRepository, IMapper mapper)
        {
            _attributeRepository = attributeRepository;
            _mapper = mapper;
        }

        public async Task<Model.Attribute> Handle(UpdateAttributeCommand request, CancellationToken cancellationToken)
        {
            var attributeExists = await _attributeRepository
                .FindAsync(r => r.AttributeName == request.UpdateAttributeDto.AttributeName);

            if (!attributeExists.Any())
            {
                return null;
            }   
            var attribute = _mapper.Map(request.UpdateAttributeDto, attributeExists.First());
            await _attributeRepository.UpdateAsync(attribute);
            await _attributeRepository.SaveAsync();
            return attribute;
        }
    }
}
