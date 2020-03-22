using System.Windows.Controls;
using Miedviediev_03.Navigation;

namespace Miedviediev_03.Views
{
    public partial class GridView : INavigatable
    {
        public GridView()
        {
            InitializeComponent();
        }

        public INavigatableDataContext NavigatableDataContext => (INavigatableDataContext)DataContext;
    }
}