using System;
using System.Collections.ObjectModel;
using Miedviediev_03.Managers;
using Miedviediev_03.Models;
using Miedviediev_03.Navigation;
using Miedviediev_03.ViewModels.PersonVm;

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

        public RelayCommand<object> FilterCommand { get; }

        private string _nameSearch;
        public string NameSearch { set => _nameSearch = value; }

        private string _surnameSearch;
        public string SurnameSearch { set => _surnameSearch = value; }

        private string _emailSearch;
        public string EmailSearch { set => _emailSearch = value; }

        private DateTime _dateSearch;
        public DateTime DateSearch { set => _dateSearch = value; }

        private BoolBoxType _isBirthdaySearch;
        public BoolBoxType IsBirthdaySearch { set => _isBirthdaySearch = value; }

        private BoolBoxType _isAdultSearch;
        public BoolBoxType IsAdultSearch { set => _isAdultSearch = value; }

        private WesternZodiac _westernZodiacSearch;
        public WesternZodiac WesternZodiacSearch { set => _westernZodiacSearch = value; }

        private ChineseZodiac _chineseZodiacSearch;
        public ChineseZodiac ChineseZodiacSearch { set => _chineseZodiacSearch = value; }

        
        public DataGridVm()
        {
            _persons = new ObservableCollection<Person>(StationManager.DataStorage.PersonsList);
            DeleteCommand = new RelayCommand<Person>(DeletePerson);
            EditCommand = new RelayCommand<Person>(EditPerson);
            CreateCommand = new RelayCommand<object>(CreatePerson);
            SaveCommand = new RelayCommand<object>(SavePersons);
            FilterCommand = new RelayCommand<object>(FilterPersons);
        }

        private void FilterPersons(object obj)
        {
            Persons = new ObservableCollection<Person>(
                StationManager.DataStorage.Filter(_nameSearch, _surnameSearch, _emailSearch, _dateSearch,
                    _isBirthdaySearch, _isAdultSearch, _westernZodiacSearch, _chineseZodiacSearch));
        }

        public void Execute(params object[] obj)
        {
            if (!(obj[0] is Person person)) return;
            if (obj.Length > 1 && obj[1] is int index)
            {
                StationManager.DataStorage.EditPerson(index, person);
            }
            else
                StationManager.DataStorage.AddPerson(person);
            Persons = new ObservableCollection<Person>(StationManager.DataStorage.PersonsList);
            
        }

        private void DeletePerson(Person item)
        {
            StationManager.DataStorage.RemovePerson(item);
            Persons = new ObservableCollection<Person>(StationManager.DataStorage.PersonsList);
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
            StationManager.DataStorage.SaveList();
        }
    }
}