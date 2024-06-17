using AutoMapper;
using CakeZone.Services.Product.Repository.Attribute;
using MediatR;
using Attributes = CakeZone.Services.Product.Model.Attribute;


namespace CakeZone.Services.Product.CQRS.Attribute
{
    public class CreateAttributeCommandHandler : IRequestHandler<CreateAttributeCommand, Attributes>
    {
        private readonly IAttributeRepository _attributeRepository;
        private readonly IMapper _mapper;

        public CreateAttributeCommandHandler(IAttributeRepository attributeRepository, IMapper mapper)
        {
            _attributeRepository = attributeRepository;
            _mapper = mapper;
        }


        public async Task<Attributes> Handle(CreateAttributeCommand request, CancellationToken cancellationToken)
        {
            // check if attribute exists 
            var attributeExists =
                await _attributeRepository.FindAsync(r => r.AttributeName == request.CreateAttributeDto.AttributeName);

            if (attributeExists.Any())
            {
                return null;
            }   

            var attribute = _mapper.Map<Attributes>(request.CreateAttributeDto);
            await _attributeRepository.AddAsync(attribute);
            await _attributeRepository.SaveChangesAsync();
            return attribute;
        }
    }
}
