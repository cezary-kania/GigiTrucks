namespace GigiTrucks.Services.Products.Core.Entities;

public class Product
{
    public Guid Id { get; set; }
    public string Name { get; set; } = null!;
    public string Description { get; set; } = null!;
    public Category? Category { get; set; }
    public ICollection<Image> Images { get; set; } = [];
}