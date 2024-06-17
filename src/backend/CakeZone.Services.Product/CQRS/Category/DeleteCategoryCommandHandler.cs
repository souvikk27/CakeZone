using CakeZone.Services.Product.Repository.Category;
using MediatR;

namespace CakeZone.Services.Product.CQRS.Category
{
    public class DeleteCategoryCommandHandler : IRequestHandler<DeleteCategoryCommand, Model.Category>
    {
        private readonly ICategoryRepository _categoryRepository;

        public DeleteCategoryCommandHandler(ICategoryRepository categoryRepository)
        {
            _categoryRepository = categoryRepository;
        }

        public async Task<Model.Category> Handle(DeleteCategoryCommand request, CancellationToken cancellationToken)
        {
            var category = await _categoryRepository.GetByIdAsync(request.Id);
            if (category == null)
            {
                return null;
            }
            category.IsDeleted = true;
            await _categoryRepository.UpdateAsync(category);    
            await _categoryRepository.SaveChangesAsync();
            return category;
        }
    }
}
