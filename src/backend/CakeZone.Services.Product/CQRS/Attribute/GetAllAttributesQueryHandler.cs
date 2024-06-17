using CakeZone.Services.Product.Repository.Attribute;
using CakeZone.Services.Product.Services;
using MediatR;

namespace CakeZone.Services.Product.CQRS.Attribute
{
    public class GetAllAttributesQueryHandler : IRequestHandler<GetAllAttributesQuery, PagedList<Model.Attribute>>
    {
        private readonly IAttributeRepository _attributeRepository;

        public GetAllAttributesQueryHandler(IAttributeRepository attributeRepository)
        {
            _attributeRepository = attributeRepository;
        }

        public async Task<PagedList<Model.Attribute>> Handle(GetAllAttributesQuery request, CancellationToken cancellationToken)
        {
            var attributes = await _attributeRepository.ListAllAsync();

            var filteredAttribute = attributes.Where(attribute =>
                    (request.Parameter.CreatedOn == DateTime.MinValue ||
                     request.Parameter.CreatedOn == attribute.CreatedAt) &&
                    (string.IsNullOrEmpty(request.Parameter.AttributeName) ||
                     request.Parameter.AttributeName == attribute.AttributeName))
                .ToList();

            var metadata = new MetaData().Initialize(request.Parameter.PageNumber, request.Parameter.PageSize, filteredAttribute.Count());
            var pagedList = PagedList<Model.Attribute>.ToPagedList(filteredAttribute, request.Parameter.PageNumber, request.Parameter.PageSize);
            return pagedList;
        }
    }
}