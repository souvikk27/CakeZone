using CakeZone.Services.Product.Shared.Categories;
using MediatR;

namespace CakeZone.Services.Product.CQRS.Category
{
    public class CreateCategoryCommand : IRequest<Model.Category>
    {
        public CreateCategoryCommand(CategoryCreateDto categoryCreateDto)
        {
            CategoryCreateDto = categoryCreateDto;
        }

        public CategoryCreateDto CategoryCreateDto { get; }
    }
}
