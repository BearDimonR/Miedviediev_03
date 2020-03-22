using System.Windows.Controls;
using Miedviediev_03.Navigation;

namespace Miedviediev_03.Views
{
    public partial class EditionView : INavigatable
    {
        public EditionView()
        {
            InitializeComponent();
        }

        public INavigatableDataContext NavigatableDataContext => (INavigatableDataContext)DataContext;
    }
}