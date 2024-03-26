namespace GigiTrucks.Services.Products.Core.Entities;

public class Image
{
    public Guid Id { get; set; }
    public string Uri { get; set; } = string.Empty;
    public string Name { get; set; } = string.Empty;
    public int DisplayOrder { get; set; }
    public Guid ProductId { get; set; }
}