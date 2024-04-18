using GigiTrucks.Services.Orders.Domain.Entities.Common;
using GigiTrucks.Services.Orders.Domain.Enums;
using GigiTrucks.Services.Orders.Domain.Exceptions;
using GigiTrucks.Services.Orders.Domain.ValueTypes;

namespace GigiTrucks.Services.Orders.Domain.Entities;

public class Order : AuditableEntity
{
    public OrderId Id { get; }
    public OrderStatus Status { get; private set; }
    public Currency Currency { get; private set; }
    public IList<OrderLine> OrderLines { get; } = new List<OrderLine>();

    protected Order()
    {
    }

    public Order(OrderId id, Currency currency, IList<OrderLine> orderLines)
    {
        Id = id;
        OrderLines = orderLines;
        Status = OrderStatus.Created;
        Currency = currency;
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
        if (Status is OrderStatus.Approved or OrderStatus.Completed or OrderStatus.Canceled)
        {
            throw new InvalidOrderStatusChangeException(Id, OrderStatus.Approved);
        }
        Status = OrderStatus.Approved;
    }
    
    public void Complete()
    {
        if (Status is OrderStatus.Completed or OrderStatus.Canceled)
        {
            throw new InvalidOrderStatusChangeException(Id, OrderStatus.Completed);
        }
        Status = OrderStatus.Completed;
    }
    
    public void Cancel()
    {
        if (Status is OrderStatus.Completed or OrderStatus.Canceled)
        {
            throw new InvalidOrderStatusChangeException(Id, OrderStatus.Canceled);
        }
        Status = OrderStatus.Canceled;
    }
}