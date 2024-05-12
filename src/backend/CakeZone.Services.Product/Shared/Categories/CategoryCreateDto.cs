using System.ComponentModel.DataAnnotations;

namespace CakeZone.Services.Product.Shared.Categories;

public class CategoryCreateDto
{
    public Guid CategoryId = Guid.NewGuid();
    [Required(ErrorMessage = "Category name is required!")]
    [StringLength(100, MinimumLength = 5, ErrorMessage = "Category name must be between 5 and 100 characters")]
    public string Name { get; set; }
    public Guid? ParentCategoryId { get; set; }
    public DateTime CreatedAt = DateTime.Now;
    public bool IsDeleted = false;
}