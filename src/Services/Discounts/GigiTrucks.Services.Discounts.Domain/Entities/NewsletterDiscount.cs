
namespace GigiTrucks.Services.Discounts.Domain.Entities;

public class NewsletterDiscount : Discount {
    public bool IsUsed { get; set; }
    public Guid CustomerId { get; set; }
}