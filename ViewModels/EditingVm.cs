namespace Miedviediev_03.ViewModels
{
    using System;
    using System.Threading.Tasks;
    using System.Windows;
    using Exceptions;
    using Managers;
    using Models;
    using Navigation;

    public sealed class EditingVm : BaseVm, INavigatableDataContext
    {
        private Person _person;
        
        private int _index;
        
        public string Name { get; set; }

        public string Surname { get; set; }

        public string Email { get; set; }

        public DateTime Birthday { get; set; }

        public RelayCommand<object> ClickCommand { get; }

        public RelayCommand<object> CancelCommand { get; }

        public EditingVm()
        {
            _person = null;
            ClickCommand = new RelayCommand<object>(EditPerson);
            CancelCommand = new RelayCommand<object>(Cancel);
        }

        private void Cancel(object obj)
        {
            NavigationManager.Instance.Execute(ViewType.GridView, _person, _index);
            NavigationManager.Instance.Navigate(ViewType.GridView);          
        }

        private async void EditPerson(object o)
        {
            try
            {
                try
                {
                    LoaderManager.Instance.ShowLoader();
                    await Task.Run(() => { _person = new Person(Name, Surname, Email, Birthday); });
                    ClearForm();
                    NavigationManager.Instance.Execute(ViewType.GridView, _person, _index);
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
                Birthday = _person.Birthday;
                OnPropertyChanged(nameof(Birthday));
            }
            catch (InvalidPersonName exception)
            {
                MessageBox.Show(exception.Message,
                    "Input Error",
                    MessageBoxButton.OK,
                    MessageBoxImage.Exclamation);
                Name = _person.Name;
                OnPropertyChanged(nameof(Name));
            }
            catch (InvalidPersonSurname exception)
            {
                MessageBox.Show(exception.Message,
                    "Input Error",
                    MessageBoxButton.OK,
                    MessageBoxImage.Exclamation);
                Surname = _person.Surname;
                OnPropertyChanged(nameof(Surname));
            }
            catch (NotEduMailException exception)
            {
                MessageBox.Show(exception.Message,
                    "Input Error",
                    MessageBoxButton.OK,
                    MessageBoxImage.Exclamation);
                Email = _person.Email;
                OnPropertyChanged(nameof(Email));
            }
            catch (Exception)
            {
                MessageBox.Show("Something went wrong...\nProgram will stop!",
                    "Error",
                    MessageBoxButton.OK,
                    MessageBoxImage.Error);
                Environment.Exit(-1);
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
            if (objects.Length < 2 || !(objects[0] is Person person) || !(objects[1] is int index)) return;
            _person = person;
            _index = index;
            UpdateField();
        }

        private void UpdateField()
        {
            Name = _person.Name;
            Surname = _person.Surname;
            Email = _person.Email;
            Birthday = _person.Birthday;
            OnPropertyChanged(nameof(Name));
            OnPropertyChanged(nameof(Surname));
            OnPropertyChanged(nameof(Email));
            OnPropertyChanged(nameof(Birthday));
        }
    }
}