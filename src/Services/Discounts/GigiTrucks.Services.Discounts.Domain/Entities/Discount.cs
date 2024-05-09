
namespace GigiTrucks.Services.Discounts.Domain.Entities;

public class Discount {
    public Guid Id { get; set; }
    public string? Name { get; set; }
    public string? Description { get; set; }
    public double Amount { get; set; }
}