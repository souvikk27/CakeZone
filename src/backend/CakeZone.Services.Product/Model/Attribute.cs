using System.ComponentModel.DataAnnotations;

namespace CakeZone.Services.Product.Model
{
    public class Attribute
    {
        [Key]
        public Guid Id { get; set; }
        public string AttributeName { get; set; }
        public string AttributeDefinition { get; set; }
        public string DisplayType { get; set; }
        public string DataType { get; set; }
        public string UnitsOfMeasure { get; set; }
        public string DataGovernanceFlags { get; set; }
        public string AttributeGroups { get; set; }
        public string InheritanceRules { get; set; }
        public string NavigationSequence { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public bool IsDeleted { get; set; }
        public DateTime? DeletedAt { get; set; }
        public virtual ICollection<Product> Products { get; set; }
    }
}
