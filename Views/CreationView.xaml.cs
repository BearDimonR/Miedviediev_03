﻿using System.Windows.Controls;
using Miedviediev_03.Navigation;

namespace Miedviediev_03.Views
{
    public partial class CreationView : INavigatable
    {
        public CreationView()
        {
            InitializeComponent();
        }

        public INavigatableDataContext NavigatableDataContext => (INavigatableDataContext)DataContext;
    }
}