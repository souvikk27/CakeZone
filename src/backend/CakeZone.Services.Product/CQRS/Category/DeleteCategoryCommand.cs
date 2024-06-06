using MediatR;

namespace CakeZone.Services.Product.CQRS.Category
{
    public class DeleteCategoryCommand : IRequest<Model.Category>
    {
        public DeleteCategoryCommand(Guid id)
        {
            Id = id;
        }

        public Guid Id { get; }

    }
}
