using CommunityToolkit.Mvvm.ComponentModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aura.Core.Models;

public partial class CartItem : ObservableObject
{
    // Product details (read-only once added to cart)
    public int ProductId { get; }
    public string Name { get; }
    public string SKU { get; }
    public decimal Price { get; }

    // Observable fields for the transaction view
    [ObservableProperty]
    [NotifyPropertyChangedFor(nameof(Subtotal))] // Automatically raises property changed for Subtotal when Quantity changes
    private int _quantity = 1;

    // Calculated property
    public decimal Subtotal => Quantity * Price;

    public CartItem(Product product)
    {
        ProductId = product.Id;
        Name = product.Name;
        SKU = product.SKU;
        Price = product.Price;
    }
}
