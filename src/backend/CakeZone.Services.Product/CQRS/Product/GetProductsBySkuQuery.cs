using MediatR;
namespace CakeZone.Services.Product.CQRS.Product
{
    public class GetProductsBySkuQuery : IRequest<Model.Product>
    {
        public string Sku { get; set; }

        public GetProductsBySkuQuery(string sku)
        {
            Sku = sku;
        }
    }
}
