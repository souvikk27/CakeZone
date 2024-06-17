using AutoMapper;
using CakeZone.Services.Product.Repository.Category;
using MediatR;

namespace CakeZone.Services.Product.CQRS.Category
{
    public class UpdateCategoryCommandHandler : IRequestHandler<UpdateCategoryCommand, Model.Category>
    {
        private readonly ICategoryRepository _categoryRepository;
        private readonly IMapper _mapper;

        public UpdateCategoryCommandHandler(ICategoryRepository categoryRepository, IMapper mapper)
        {
            _categoryRepository = categoryRepository;
            _mapper = mapper;
        }

        public async Task<Model.Category> Handle(UpdateCategoryCommand request, CancellationToken cancellationToken)
        {
            var category = _mapper.Map<Model.Category>(request.CategoryUpdateDto);
            await _categoryRepository.UpdateAsync(category);
            await _categoryRepository.SaveChangesAsync();
            return category;
        }
    }
}
