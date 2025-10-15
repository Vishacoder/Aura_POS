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

    public MainWindowViewModel(IUnitOfWork unitOfWork, TransactionViewModel transactionViewModel)
    {
        _unitOfWork = unitOfWork;
        // _transactionViewModel = transactionViewModel; // Optional: Store it if needed

        // Set the primary content of the main window immediately
        CurrentViewModel = transactionViewModel;

        // We can remove the DummyViewModel now!

    }
}

// Temporary placeholder for TransactionView content
//public partial class DummyViewModel : ViewModelBase
//{
//    // Placeholder to show something in the main window while setting up
//    [ObservableProperty]
//    private string _welcomeMessage = "Loading POS System...";
//}

