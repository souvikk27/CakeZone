using AutoMapper;
using CakeZone.Services.Product.Repository.Category;
using MediatR;
namespace CakeZone.Services.Product.CQRS.Category
{
    public class CreateCategoryCommandHandler : IRequestHandler<CreateCategoryCommand, Model.Category>
    {
        private readonly ICategoryRepository _categoryRepository;
        private readonly IMapper _mapper;

        public CreateCategoryCommandHandler(ICategoryRepository categoryRepository, IMapper mapper)
        {
            _categoryRepository = categoryRepository;
            _mapper = mapper;
        }

        public async Task<Model.Category> Handle(CreateCategoryCommand request, CancellationToken cancellationToken)
        {
            var category = _mapper.Map<Model.Category>(request.CategoryCreateDto);
            await _categoryRepository.AddAsync(category);
            await _categoryRepository.SaveChangesAsync();
            return category;
        }
    }
}
