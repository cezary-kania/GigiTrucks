using GigiTrucks.Services.Orders.Domain.Entities.Common;
using GigiTrucks.Services.Orders.Domain.ValueObjects;

namespace GigiTrucks.Services.Orders.Domain.Entities;

public class OrderLine : AuditableEntity
{
    public OrderLineId Id {get;}
    public ProductId ProductId { get; }
    public decimal UnitPrice { get; }
    public int Quantity { get; }


    protected OrderLine()
    {
    }
    
    public OrderLine(OrderLineId id, ProductId productId, decimal unitPrice, int quantity)
    {
        Id = id;
        ProductId = productId;
        UnitPrice = unitPrice;
        Quantity = quantity;
    }
}