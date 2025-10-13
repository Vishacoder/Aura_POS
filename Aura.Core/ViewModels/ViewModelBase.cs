using CommunityToolkit.Mvvm.ComponentModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Aura.Core.ViewModels;

// All ViewModels will inherit from this base class
public partial class ViewModelBase : ObservableObject
{
    // A useful property to track when a ViewModel is busy (e.g., loading data)
    [ObservableProperty]
    private bool _isBusy;

    // Optional: A method that can be overridden by specific ViewModels 
    // to load initial data when they are first navigated to or initialized.
    public virtual Task LoadDataAsync()
    {
        return Task.CompletedTask;
    }
}
