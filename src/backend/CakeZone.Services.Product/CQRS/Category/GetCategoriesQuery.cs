using CakeZone.Services.Product.Services;
using CakeZone.Services.Product.Services.Filters;
using MediatR;

namespace CakeZone.Services.Product.CQRS.Category
{
    public class GetCategoriesQuery : IRequest<PagedList<Model.Category>>
    {
        public GetCategoriesQuery(CategoryParameter parameter)
        {
            Parameter = parameter;
        }

        public CategoryParameter Parameter { get; }
    }
}
