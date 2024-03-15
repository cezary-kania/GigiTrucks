using GigiTrucks.Services.Orders.Domain.Entities.Common;
using GigiTrucks.Services.Orders.Domain.Enums;
using GigiTrucks.Services.Orders.Domain.Exceptions;
using GigiTrucks.Services.Orders.Domain.ValueObjects;

namespace GigiTrucks.Services.Orders.Domain.Entities;

public class Order : AuditableEntity
{
    public OrderId Id { get; }
    public OrderStatus Status { get; private set; }
    public IList<OrderLine> OrderLines { get; } = new List<OrderLine>();

    protected Order()
    {
    }

    public Order(OrderId id, IList<OrderLine> orderLines)
    {
        Id = id;
        OrderLines = orderLines;
        Status = OrderStatus.Created;
    }

    public void AddOrderLine(OrderLine orderLine)
    {
        var existingOrderLine = OrderLines.FirstOrDefault(ol => ol.Id == orderLine.Id);
        if (OrderLines.Any(ol => ol.Id == orderLine.Id))
        {
            throw new OrderLineAlreadyAddedException(orderLine.Id);
        }
        OrderLines.Add(orderLine);
    }

    public void Approve()
    {
        Status = OrderStatus.Approved;
    }
}