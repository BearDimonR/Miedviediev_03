using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace Miedviediev_03.ViewModels
{
    public class BaseVm:INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}