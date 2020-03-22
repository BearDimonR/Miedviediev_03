using System.Windows;
using Miedviediev_03.ViewModels;

namespace Miedviediev_03.Views
{
    public partial class MainWindow
    {
        public MainWindow()
        {
            InitializeComponent();
            DataContext = new MainVm(ContentControl);
        } 
    }
}