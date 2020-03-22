using System;
using System.Collections;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Controls;
using Miedviediev_03.Managers;
using Miedviediev_03.Models;
using Miedviediev_03.Navigation;

namespace Miedviediev_03.ViewModels 
{
    public class DataGridVm:BaseVm, INavigatableDataContext
    {
        private ObservableCollection<Person> _persons;
        public ObservableCollection<Person> Persons
        {
            get => _persons;
            set
            {
                _persons = value;
                OnPropertyChanged(nameof(Persons));
            }
        }
        
        public RelayCommand<Person> DeleteCommand { get; }

        public RelayCommand<Person> EditCommand { get; }

        public RelayCommand<object> CreateCommand { get; }

        public RelayCommand<object> SaveCommand { get; }
        
        public DataGridVm()
        {
            _persons = new ObservableCollection<Person>(StationManager.DataStorage.PersonsList);
            DeleteCommand = new RelayCommand<Person>(DeletePerson);
            EditCommand = new RelayCommand<Person>(EditPerson);
            CreateCommand = new RelayCommand<object>(CreatePerson);
            SaveCommand = new RelayCommand<object>(SavePersons);
        }

        public void Execute(params object[] obj)
        {
            if (!(obj[0] is Person person)) return;
            if (obj.Length > 1 && obj[1] is int index)
            {
                _persons.RemoveAt(index);
                _persons.Insert(index, person);
            }
            else
                _persons.Add(person);
        }

        private void DeletePerson(Person item)
        {
            _persons.Remove(item);
        }
        
        private void EditPerson(Person selectedPerson)
        {
            NavigationManager.Instance.Execute
                (ViewType.EditionView, selectedPerson, _persons.IndexOf(selectedPerson));
            NavigationManager.Instance.Navigate(ViewType.EditionView);
        }
        
        private void CreatePerson(object obj)
        {
            NavigationManager.Instance.Navigate(ViewType.CreationView);
        }
        
        private void SavePersons(object obj)
        {
            StationManager.DataStorage.SaveList(_persons.ToList());
        }
    }
}