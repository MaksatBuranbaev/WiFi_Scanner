using System.Windows;
using WiFi_Scanner.ViewModels;

namespace WiFi_Scanner.Views
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            DataContext = new MainViewModel();
        }
    }
}