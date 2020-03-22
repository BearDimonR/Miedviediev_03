using System;
using Miedviediev_03.Views;

namespace Miedviediev_03.Navigation
{
    internal class InitializationNavigationModel : BaseNavigationModel
    {
        public InitializationNavigationModel(IContentOwner contentOwner) : base(contentOwner)
        {
            
        }
   
        protected override void InitializeView(ViewType viewType)
        {
            switch (viewType)
            {
                case ViewType.CreationView:
                    ViewsDictionary.Add(viewType,new CreationView());
                    break;
                case ViewType.EditionView:
                    ViewsDictionary.Add(viewType, new EditionView());
                    break;
                case ViewType.GridView:
                    ViewsDictionary.Add(viewType, new GridView());
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(viewType), viewType, null);
            }
        }
    }
}
