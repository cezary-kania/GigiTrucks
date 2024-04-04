namespace GigiTrucks.Services.Products.Core.Entities;

public class Category
{
    public Guid Id { get; set; }
    public string Name { get; set; } = null!;
    public Category? ParentCategory { get; set; }
    public Guid? ParentCategoryId { get; set; }
    public ICollection<Product> Products { get; set; } = [];
    public ICollection<Category> SubCategories { get; set; } = [];
}