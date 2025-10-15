using Aura.Core.ViewModels;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Aura.WPF;

public partial class MainWindow : Window
{
    // ... (Your constructor is here) ...

    public MainWindow(MainWindowViewModel viewModel)
    {
        InitializeComponent();
        DataContext = viewModel;
    }

    // THIS IS THE MISSING METHOD
    private void TextBox_KeyDown(object sender, KeyEventArgs e)
    {
        if (e.Key == Key.Enter)
        {
            // Safely check if the DataContext and CurrentViewModel are of the correct types
            if (DataContext is MainWindowViewModel mainVM && mainVM.CurrentViewModel is TransactionViewModel transactionVM)
            {
                // Execute the AddProductCommand when Enter is pressed
                transactionVM.AddProductCommand.Execute(null);
            }
        }
    }
}