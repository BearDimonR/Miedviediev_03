using System;
using System.Threading.Tasks;
using System.Windows;
using Miedviediev_03.Exceptions;
using Miedviediev_03.Managers;
using Miedviediev_03.Models;
using Miedviediev_03.Navigation;

namespace Miedviediev_03.ViewModels.PersonVm
{
    public sealed class PersonVm : BaseVm, INavigatableDataContext
    {
        public string Name { get; set; }

        public string Surname { get; set; }

        public string Email { get; set; }

        public DateTime Birthday {get; set; }

        public RelayCommand<object> ClickCommand { get; }

        public RelayCommand<object> CancelCommand { get; }

        public PersonVm()
        {
            Birthday = DateTime.Today;
            ClickCommand = new RelayCommand<object>(ProceedPerson);
            CancelCommand = new RelayCommand<object>(Cancel);
        }

        private async void ProceedPerson(object o)
        {
            try
            {
                try
                {
                    LoaderManager.Instance.ShowLoader();
                    Person person = null;
                    await Task.Run(() => { person = new Person(Name, Surname, Email, Birthday); });
                    ClearForm();
                    NavigationManager.Instance.Execute(ViewType.GridView, person);
                    NavigationManager.Instance.Navigate(ViewType.GridView);
                    LoaderManager.Instance.HideLoader();
                }
                catch (Exception exception)
                {
                    LoaderManager.Instance.HideLoader();
                    // get rid of wrapper exception if present
                    if (exception.InnerException != null)
                        throw exception.InnerException;
                    throw;
                }
            }
            catch (AgeException exception)
            {
                MessageBox.Show(exception.Message,
                    "Input Error",
                    MessageBoxButton.OK,
                    MessageBoxImage.Exclamation);
                Birthday = DateTime.Today;
                OnPropertyChanged(nameof(Birthday));
            }
            catch (InvalidPersonName exception)
            {
                MessageBox.Show(exception.Message,
                    "Input Error",
                    MessageBoxButton.OK,
                    MessageBoxImage.Exclamation);
                Name = string.Empty;
                OnPropertyChanged(nameof(Name));
            }
            catch (InvalidPersonSurname exception)
            {
                MessageBox.Show(exception.Message,
                    "Input Error",
                    MessageBoxButton.OK,
                    MessageBoxImage.Exclamation);
                Surname = string.Empty;
                OnPropertyChanged(nameof(Surname));
            }
            catch (NotEduMailException exception)
            {
                MessageBox.Show(exception.Message,
                    "Input Error",
                    MessageBoxButton.OK,
                    MessageBoxImage.Exclamation);
                Email = string.Empty;
                OnPropertyChanged(nameof(Email));
            }
            catch (Exception)
            {
                MessageBox.Show("Something went wrong...\nProgram will stop!",
                    "Error",
                    MessageBoxButton.OK,
                    MessageBoxImage.Error);
                StationManager.CloseApp();
            }
        }
        private void ClearForm()
        {
            Name = string.Empty;
            Surname = string.Empty;
            Email = string.Empty;
            Birthday = DateTime.Today;
            OnPropertyChanged(nameof(Name));
            OnPropertyChanged(nameof(Surname));
            OnPropertyChanged(nameof(Email));
            OnPropertyChanged(nameof(Birthday));
        }

        public void Execute(params object[] objects)
        {
            if (objects[0] is Person person)
                UpdateField(person);
        }

        private void UpdateField(Person person)
        {
            Name = person.Name;
            Surname = person.Surname;
            Email = person.Email;
            Birthday = person.Birthday;
            OnPropertyChanged(nameof(Name));
            OnPropertyChanged(nameof(Surname));
            OnPropertyChanged(nameof(Email));
            OnPropertyChanged(nameof(Birthday));
        }
        
        private void Cancel(object obj)
        {
            NavigationManager.Instance.Navigate(ViewType.GridView);
        }
    }
}