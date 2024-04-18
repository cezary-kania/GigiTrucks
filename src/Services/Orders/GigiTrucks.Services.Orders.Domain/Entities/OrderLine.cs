using GigiTrucks.Services.Orders.Domain.Entities.Common;
using GigiTrucks.Services.Orders.Domain.ValueTypes;

namespace GigiTrucks.Services.Orders.Domain.Entities;

public class OrderLine : AuditableEntity
{
    public OrderLineId Id {get;}
    public ProductId ProductId { get; }
    public UnitPrice UnitPrice { get; }
    public Quantity Quantity { get; }


    protected OrderLine()
    {
    }
    
    public OrderLine(OrderLineId id, ProductId productId, UnitPrice unitPrice, Quantity quantity)
    {
        Id = id;
        ProductId = productId;
        UnitPrice = unitPrice;
        Quantity = quantity;
    }
}