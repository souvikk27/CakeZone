using CakeZone.Services.Product.Repository.Category;
using CakeZone.Services.Product.Services;
using MediatR;

namespace CakeZone.Services.Product.CQRS.Category
{
    public class GetCategoriesQueryHandler : IRequestHandler<GetCategoriesQuery, PagedList<Model.Category>>
    {
        private readonly ICategoryRepository _categoryRepository;

        public GetCategoriesQueryHandler(ICategoryRepository categoryRepository)
        {
            _categoryRepository = categoryRepository;
        }

        public async Task<PagedList<Model.Category>> Handle(GetCategoriesQuery request, CancellationToken cancellationToken)
        {
            var categories = await _categoryRepository.GetAll();
            var filteredCategories = categories.Where(attribute =>
                    (request.Parameter.AddedOn == DateTime.MinValue ||
                     request.Parameter.AddedOn == attribute.CreatedAt) &&
                    (string.IsNullOrEmpty(request.Parameter.CategoryName) ||
                     request.Parameter.CategoryName == attribute.Name))
                .ToList();

            var metadata = new MetaData().Initialize(request.Parameter.PageNumber, request.Parameter.PageSize, filteredCategories.Count());

            return PagedList<Model.Category>.ToPagedList(filteredCategories, request.Parameter.PageNumber, request.Parameter.PageSize);
        }
    }
}