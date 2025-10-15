using Aura.Core.Interfaces;
using Aura.Core.Models;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System;
using System.Windows;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aura.Core.ViewModels;

public partial class ProductManagementViewModel : ViewModelBase
{
    private readonly IUnitOfWork _unitOfWork;

    public ObservableCollection<Product> Products { get; set; } = new ObservableCollection<Product>();

    [ObservableProperty]
    private Product _currentProduct = new();

    [ObservableProperty]
    private Product? _selectedProduct;

    public ProductManagementViewModel(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;

        PropertyChanged += async (s, e) =>
        {
            if (e.PropertyName == nameof(SelectedProduct) && _selectedProduct != null)
            {
                CurrentProduct = SelectedProduct.Clone();
            }
        };
    }

    public override async Task LoadDataAsync()
    {
        IsBusy = true;
        Products.Clear();
        var products = await _unitOfWork.Products.GetAllAsync();
        foreach ( var p in products)
        {
            Products.Add(p);
        }
        IsBusy = false;
        CurrentProduct = new Product(); 
    }

    [RelayCommand]

    private async Task SaveProductAsync()
    {
        if (CurrentProduct == null || string.IsNullOrWhiteSpace(CurrentProduct.Name))
        {
            MessageBox.Show("Product name and SKU are required.", "Validation Error");
            return;
        }

    }

}
