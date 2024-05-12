namespace CakeZone.Services.Product.Shared.Categories;

public record CategoryUpdateDto
{
    public Guid CategoryId { get; set; }
    public string Name { get; set; }
    public Guid? ParentCategoryId { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime? UpdatedAt = DateTime.Now;
    public bool IsDeleted = false;
}