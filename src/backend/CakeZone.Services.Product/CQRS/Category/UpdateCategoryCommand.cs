using CakeZone.Services.Product.Shared.Categories;
using MediatR;

namespace CakeZone.Services.Product.CQRS.Category
{
    public class UpdateCategoryCommand : IRequest<Model.Category>
    {
        public CategoryUpdateDto CategoryUpdateDto { get; }
        public UpdateCategoryCommand(CategoryUpdateDto categoryUpdateDto)
        {
            CategoryUpdateDto = categoryUpdateDto;
        }   
    }
}
