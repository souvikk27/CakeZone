using System.Linq.Expressions;

namespace CakeZone.Services.Product.Specification.Attribute
{
    public class AttributeSpecification : BaseSpecification<Model.Attribute>
    {
        public AttributeSpecification(Expression<Func<Model.Attribute, bool>> criteria) : base(criteria)
        {
        }

        public AttributeSpecification()
        {
        }
        public void ApplyPagination(int pageNumber, int pageSize)
        {
            ApplyPaging((pageNumber - 1) * pageSize, pageSize);
        }
    }
}
