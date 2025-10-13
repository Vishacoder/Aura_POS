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
    // Inject the ViewModel via the constructor
    public MainWindow(MainWindowViewModel viewModel)
    {
        InitializeComponent();

        // Assign the injected ViewModel as the DataContext
        DataContext = viewModel;
    }
}