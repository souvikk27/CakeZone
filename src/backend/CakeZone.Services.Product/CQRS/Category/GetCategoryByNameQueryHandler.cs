using CakeZone.Services.Product.Repository.Category;
using MediatR;
using NuGet.Protocol.Plugins;

namespace CakeZone.Services.Product.CQRS.Category
{
    public class GetCategoryByNameQueryHandler : IRequestHandler<GetCategoryByNameQuery, Model.Category>
    {
        private readonly ICategoryRepository _categoryRepository;

        public GetCategoryByNameQueryHandler(ICategoryRepository categoryRepository)
        {
            _categoryRepository = categoryRepository;
        }

        public async Task<Model.Category> Handle(GetCategoryByNameQuery request, CancellationToken cancellationToken)
        {
            var category = await _categoryRepository.FindAsync(c => c.Name == request.Name);
            return !category.Any() ? null : category.FirstOrDefault();
        }
    }
}
