using GigiTrucks.Services.Orders.Domain.Entities;
using GigiTrucks.Services.Orders.Domain.Enums;
using GigiTrucks.Services.Orders.Domain.Exceptions;
using Shouldly;
using Xunit;

namespace GigiTrucks.Services.Orders.Tests.Unit.Entities;

public class OrderTests
{
    private readonly Order _order = new(Guid.NewGuid(), Guid.NewGuid(), Currency.PLN, []);

    [Fact]
    public void Approve_WhenStatusIsCreated_ShouldUpdateStatusToApprove()
    {
        //Act
        _order.Approve();

        //Assert
        _order.Status.ShouldBe(OrderStatus.Approved);
    }
    
    [Fact]
    public void Approve_WhenStatusIsCancelled_ShouldThrowException()
    {
        //Arrange
        _order.Cancel();
        
        //Act
        var exception = Record.Exception(() => _order.Approve());

        //Assert
        exception.ShouldNotBeNull();
        exception.ShouldBeOfType<InvalidOrderStatusChangeException>();
    }
    
    [Fact]
    public void Cancel_WhenStatusIsCreated_ShouldUpdateStatusToCanceled()
    {
        //Act
        _order.Cancel();

        //Assert
        _order.Status.ShouldBe(OrderStatus.Canceled);
    }
    
    [Fact]
    public void Cancel_WhenStatusIsCompleted_ShouldThrowException()
    {
        //Arrange
        _order.Complete();
        
        //Act
        var exception = Record.Exception(() => _order.Cancel());

        //Assert
        exception.ShouldNotBeNull();
        exception.ShouldBeOfType<InvalidOrderStatusChangeException>();
    }
    
    [Fact]
    public void Complete_WhenStatusIsCreated_ShouldUpdateStatusToCompleted()
    {
        //Act
        _order.Complete();

        //Assert
        _order.Status.ShouldBe(OrderStatus.Completed);
    }
    
    [Fact]
    public void Complete_WhenStatusIsCompleted_ShouldThrowException()
    {
        //Arrange
        _order.Complete();
        
        //Act
        var exception = Record.Exception(() => _order.Complete());

        //Assert
        exception.ShouldNotBeNull();
        exception.ShouldBeOfType<InvalidOrderStatusChangeException>();
    }
    
    [Fact]
    public void AddOrderLine_WhenOrderLineWasAlreadyAdded_ShouldThrowException()
    {
        //Arrange
        var orderLine = new OrderLine(Guid.NewGuid(), Guid.NewGuid(), 10.25m, 1);
        _order.AddOrderLine(orderLine);
        
        //Act
        var exception = Record.Exception(() => _order.AddOrderLine(orderLine));

        //Assert
        exception.ShouldNotBeNull();
        exception.ShouldBeOfType<OrderLineAlreadyAddedException>();
    }    
    
    [Fact]
    public void AddOrderLine_WhenOrderLineWasNotAdded_ShouldSaveNewItemOnList()
    {
        //Arrange
        var orderLine = new OrderLine(Guid.NewGuid(), Guid.NewGuid(), 10.25m, 1);
        
        //Act
        _order.AddOrderLine(orderLine);

        //Assert
        _order.OrderLines.ShouldContain(orderLine);
    }
}