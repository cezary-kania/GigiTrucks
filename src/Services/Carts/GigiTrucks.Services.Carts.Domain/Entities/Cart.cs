using GigiTrucks.Services.Carts.Domain.Enums;
using GigiTrucks.Services.Carts.Domain.Exceptions;
using GigiTrucks.Services.Carts.Domain.ValueTypes;

namespace GigiTrucks.Services.Carts.Domain.Entities;

public class Cart
{
    public CustomerId CustomerId { get; }
    public CartStatus Status { get; private set; }
    public List<CartItem> Items { get; private set; } = [];

    protected Cart()
    {
    }
    
    public Cart(CustomerId customerId, List<CartItem> items)
    {
        CustomerId = customerId;
        Status = CartStatus.New;
        SetItems(items);
    }

    public void Submit()
    {
        if (Items.Count < 1)
        {
            throw new CantSubmitEmptyCartException();
        }
        Status = CartStatus.Submitted;
    }
    
    public void SetItems(List<CartItem> items)
    {
        var productsWithQuantity = new Dictionary<ProductId, Quantity>();
        var productsWithOrderNo = new Dictionary<ProductId, OrderNo>();
        foreach (var item in items)
        {
            var productId = item.ProductId;
            if (productsWithQuantity.ContainsKey(productId))
            {
                productsWithQuantity[productId] += item.Quantity;
                continue;
            }
            productsWithQuantity.Add(productId, item.Quantity);
            productsWithOrderNo.Add(productId, item.DisplayOrderNo);
        }

        Items = productsWithQuantity.Select(x 
                => new CartItem { ProductId = x.Key, Quantity = x.Value, DisplayOrderNo = productsWithOrderNo[x.Key] })
            .ToList();
    }
}