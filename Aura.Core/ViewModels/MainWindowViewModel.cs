using Aura.Core.Interfaces;
using CommunityToolkit.Mvvm.ComponentModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aura.Core.ViewModels;

public partial class MainWindowViewModel : ViewModelBase
{
    private readonly IUnitOfWork _unitOfWork;

    // Observable property to hold the currently displayed ViewModel/Content
    [ObservableProperty]
    private ViewModelBase? _currentViewModel;

    public MainWindowViewModel(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;

        // Initial setup: Load the core transaction view model upon startup
        // Note: For now, we'll just set up the current view.
        // In the next step, we'll replace this with a real TransactionViewModel
        CurrentViewModel = new DummyViewModel();

        // We can test data access here later by calling unitOfWork.Products.GetAllAsync()
    }
}

// Temporary placeholder for TransactionView content
public partial class DummyViewModel : ViewModelBase
{
    // Placeholder to show something in the main window while setting up
    [ObservableProperty]
    private string _welcomeMessage = "Loading POS System...";
}