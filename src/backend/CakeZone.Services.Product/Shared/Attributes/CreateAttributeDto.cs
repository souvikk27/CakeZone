using System.ComponentModel.DataAnnotations;

namespace CakeZone.Services.Product.Shared.Attributes
{
    public record CreateAttributeDto
    {
        public Guid AttributeId = Guid.NewGuid();

        [Required(ErrorMessage = "Attribute name is required")]
        [StringLength(255, MinimumLength = 3, ErrorMessage = "Attribute name must be between 3 and 255 characters")]
        public string AttributeName { get; set; }
        [Required(ErrorMessage = "Attribute definition is required")]
        [StringLength(255, MinimumLength = 3, ErrorMessage = "Attribute definition must be between 3 and 255 characters")]
        public string AttributeDefinition { get; set; }
        [Required(ErrorMessage = "Display type is required")]
        [StringLength(50, MinimumLength = 3, ErrorMessage = "Display type must be between 3 and 50 characters")]
        public string DisplayType { get; set; }
        [Required(ErrorMessage = "Data type is required")]
        [StringLength(50, MinimumLength = 3, ErrorMessage = "Data type must be between 3 and 50 characters")]
        public string DataType { get; set; }
        [Required(ErrorMessage = "Units of measure is required")]
        public string UnitsOfMeasure { get; set; }
        [Required(ErrorMessage = "List of values is required")]
        public string ListOfValues { get; set; }
        [Required(ErrorMessage = "Data governance flags is required")]
        public string DataGovernanceFlags { get; set; }
        [Required(ErrorMessage = "Attribute groups is required")]
        public string AttributeGroups { get; set; }
        [Required(ErrorMessage = "Inheritance rules is required")]
        public string InheritanceRules { get; set; }
        [Required(ErrorMessage = "Navigation sequence is required")]
        public string NavigationSequence { get; set; }

        public DateTime CreatedAt = DateTime.Now;
    }
}
