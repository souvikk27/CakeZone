using MediatR;

namespace CakeZone.Services.Product.CQRS.Category
{
    public class GetCategoryByIdQuery : IRequest<Model.Category>
    {
        public GetCategoryByIdQuery(Guid id)
        {
            Id = id;
        }

        public Guid Id { get; }
    }
}
