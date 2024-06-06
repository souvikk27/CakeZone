using MediatR;

namespace CakeZone.Services.Product.CQRS.Category
{
    public class GetCategoryByNameQuery : IRequest<Model.Category>
    {
        public GetCategoryByNameQuery(string name)
        {
            Name = name;
        }

        public string Name { get; }
    }
}
