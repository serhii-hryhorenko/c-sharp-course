using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Lab1.ViewModel;

namespace Lab1.View
{
    public sealed partial class MainPage : Page
    {
        public BitrhdayViewModel ViewModel { get; }

        public MainPage()
        {
            this.InitializeComponent();
            ViewModel = new BitrhdayViewModel();
            DataContext = ViewModel;
        }
    }
}