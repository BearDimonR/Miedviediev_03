namespace Miedviediev_03.Navigation
{
    internal enum ViewType
    {
        GridView,
        EditionView,
        CreationView
    }

    interface INavigationModel
    {
        void Navigate(ViewType viewType);

        void Execute(ViewType viewType, params object[] objects);
    }
}
