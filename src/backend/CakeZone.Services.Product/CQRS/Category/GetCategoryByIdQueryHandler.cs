using CakeZone.Services.Product.Repository.Category;
using MediatR;
using NuGet.Protocol.Plugins;

namespace CakeZone.Services.Product.CQRS.Category
{
    public class GetCategoryByIdQueryHandler : IRequestHandler<GetCategoryByIdQuery, Model.Category>
    {
        private readonly ICategoryRepository _categoryRepository;

        public GetCategoryByIdQueryHandler(ICategoryRepository categoryRepository)
        {
            _categoryRepository = categoryRepository;
        }

        public async Task<Model.Category> Handle(GetCategoryByIdQuery request, CancellationToken cancellationToken)
        {
            var category = await _categoryRepository.GetById(request.Id);
            return category;
        }
    }
}
