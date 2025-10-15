using Aura.Core.Interfaces;
using Aura.Core.Models;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aura.Core.ViewModels;

public partial class TransactionViewModel : ViewModelBase
{
    private readonly IUnitOfWork _unitOfWork;

    // Observable Collection for the sale cart (TR-1)
    public ObservableCollection<CartItem> CartItems { get; set; } = new ObservableCollection<CartItem>();

    // Observable field for product search (TR-1)
    [ObservableProperty]
    private string _searchQuery = string.Empty;

    // Calculated properties for totals (TR-4)
    public decimal SubtotalDisplay => CartItems.Sum(item => item.Subtotal);

    public decimal GrandTotalDisplay => SubtotalDisplay; // Assuming 0 tax for now

    [ObservableProperty]
    [NotifyPropertyChangedFor(nameof(ChangeDueDisplay))]
    private decimal _tenderedAmount; // Amount customer paid (TR-5)

    public decimal ChangeDueDisplay => TenderedAmount > GrandTotalDisplay ? TenderedAmount - GrandTotalDisplay : 0.00m;

    public TransactionViewModel(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;

        // Subscribe to changes in the CartItems collection to update totals
        CartItems.CollectionChanged += (sender, e) =>
        {
            OnPropertyChanged(nameof(SubtotalDisplay));
            OnPropertyChanged(nameof(GrandTotalDisplay));
            OnPropertyChanged(nameof(ChangeDueDisplay));
        };
    }

    // Command to add product via search/scan (TR-1)
    [RelayCommand]
    private async Task AddProductAsync()
    {
        if (string.IsNullOrWhiteSpace(SearchQuery)) return;

        // 1. Find the product in the database by SKU
        var product = await _unitOfWork.Products.GetBySkuAsync(SearchQuery);

        if (product == null)
        {
            // TODO: Display error message on UI
            SearchQuery = string.Empty;
            return;
        }

        // 2. Check if the item is already in the cart
        var existingItem = CartItems.FirstOrDefault(i => i.ProductId == product.Id);

        if (existingItem != null)
        {
            // Item exists: increase quantity (TR-2)
            existingItem.Quantity++;
        }
        else
        {
            // Item is new: add new cart item
            CartItems.Add(new CartItem(product));
        }

        // 3. Clear search box and update UI totals
        SearchQuery = string.Empty;
    }

    // Command to remove selected item (TR-3)
    [RelayCommand]
    private void RemoveItem(CartItem? itemToRemove)
    {
        if (itemToRemove != null)
        {
            CartItems.Remove(itemToRemove);
        }
    }

    // Command to process the sale (TR-5, TR-6)
    [RelayCommand]
    private async Task ProcessSaleAsync()
    {
        if (GrandTotalDisplay <= 0 || TenderedAmount < GrandTotalDisplay)
        {
            // TODO: Display error for invalid amount/empty cart
            return;
        }

        IsBusy = true;
        try
        {
            // 1. Create the Sale Entity
            var sale = new Sale
            {
                SaleDate = DateTime.Now,
                TotalAmount = GrandTotalDisplay,
            };
            // Note: We'll need to create an ISaleRepository and use UnitOfWork to save this

            // 2. Update stock for all products
            // TODO: Implement stock update logic

            // 3. Commit the transaction
            // await _unitOfWork.CompleteAsync(); 

            // TODO: Clear cart and reset view
        }
        finally
        {
            IsBusy = false;
        }
    }
}