using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Miedviediev_03.Managers;
using Miedviediev_03.Models;
using Miedviediev_03.ViewModels.PersonVm;

namespace Miedviediev_03.DataStorage
{
    internal class SerializedDataStorage:IDataStorage
    {
        private List<Person> _persons;

        internal SerializedDataStorage()
        {
            try
            {
                _persons = SerializationManager.Deserialize<List<Person>>(FileFolderHelper.StorageFilePath);
            }
            catch (FileNotFoundException)
            {
                _persons = new List<Person>();
            }
        }
        public void SaveList()
        {
            SaveChanges();
        }

        public void AddPerson(Person person)
        {
            _persons.Add(person);
        }

        public void EditPerson(int index, Person person)
        {
            _persons.RemoveAt(index);
            _persons.Insert(index, person);
        }

        public void RemovePerson(Person item)
        {
            _persons.Remove(item);
        }

        public List<Person> Filter
        (string name, string surname, string email, DateTime birthday, BoolBoxType isBirthday, BoolBoxType isAdult,
            WesternZodiac westernZodiac, ChineseZodiac chineseZodiac)
        {
            List<Person> filter = _persons.ToList();
            if (name != string.Empty)
            {
                filter = filter.FindAll(x => x.Name.Contains(name));
            }
            if (filter.Count == 0) return new List<Person>();
            if (surname != string.Empty)
            {
                filter = filter.FindAll(x => x.Surname.Contains(surname));
            }
            if (filter.Count == 0) return new List<Person>();
            if (email != string.Empty)
            {
                filter = filter.FindAll(x => x.Email.Contains(email));
            }
            if (filter.Count == 0) return new List<Person>();
            if (birthday != DateTime.MinValue) 
                filter = filter.FindAll(x => x.Birthday <= birthday);
            if (filter.Count == 0) return new List<Person>();
            if(isAdult != BoolBoxType.Empty)
                filter = filter.FindAll(x => x.IsAdult == (isAdult == BoolBoxType.True));
            if (filter.Count == 0) return new List<Person>();
            if(isBirthday != BoolBoxType.Empty)
                filter = filter.FindAll(x => x.IsBirthday == (isBirthday == BoolBoxType.True));
            if (filter.Count == 0) return new List<Person>();
            if(chineseZodiac != ChineseZodiac.Empty)
                filter = filter.FindAll(x => x.ChineseSign == chineseZodiac);
            if (filter.Count == 0) return new List<Person>();
            if (westernZodiac != WesternZodiac.Empty)
                filter = filter.FindAll(x => x.WesternZodiac == westernZodiac);
            return filter;
        }

        public List<Person> PersonsList => _persons.ToList();

        private async void SaveChanges()
        {
            LoaderManager.Instance.ShowLoader();
            await Task.Run(() =>
            {
                Thread.Sleep(100);
                SerializationManager.Serialize(_persons, FileFolderHelper.StorageFilePath);
            });
            LoaderManager.Instance.HideLoader();
        }

        public void DoFirstInit()
        {
            _persons = new List<Person>
            {
                new Person("Білорус", "Катерина", "bilorus@ukma.edu.ua", new DateTime(2001, 3, 5)),
                new Person("Бойко", "Данило", "boyko@ukma.edu.ua", new DateTime(2005, 5, 2)),
                new Person("Бородайкевич", "Євген", "borodaikevich@ukma.edu.ua", new DateTime(2002, 6, 15)),
                new Person("Ванін", "Данило", "vanin@ukma.edu.ua", new DateTime(1950, 11, 25)),
                new Person("Василенко", "Кирило", "vasilenko@ukma.edu.ua", new DateTime(2007, 2, 23)),
                new Person("Волошко", "Максим", "voloshko@ukma.edu.ua", new DateTime(1900, 9, 30)),
                new Person("Гайворонський", "Роман", "gaivoron@ukma.edu.ua", new DateTime(1918, 11, 7)),
                new Person("Гак", "Артем", "hak@ukma.edu.ua", new DateTime(1968, 1, 1)),
                new Person("Галайда", "Михайло", "halaida@ukma.edu.ua", new DateTime(1999, 8, 12)),
                new Person("Гінкул", "Анна", "hinkyl@ukma.edu.ua", new DateTime(1899, 4, 19)),
                
                new Person("Декрет", "Владислав", "dekret@ukma.edu.ua", new DateTime(1945, 3, 2)),
                new Person("Єгоров", "Ігор", "ihor@ukma.edu.ua", new DateTime(2003, 5, 5)),
                new Person("Жуковська", "Марина", "zhykovska@ukma.edu.ua", new DateTime(2000, 8, 13)),
                new Person("Задорожний", "Максим", "zadorozh@ukma.edu.ua", new DateTime(2001, 9, 18)),
                new Person("Залізний", "Антон", "zaliznyi@ukma.edu.ua", new DateTime(2001, 4, 25)),
                new Person("Кенийз", "Віталій", "keniiz@ukma.edu.ua", new DateTime(1900, 8, 29)),
                new Person("Копійка", "Вадим", "kopiika@ukma.edu.ua", new DateTime(2001, 3, 23)),
                new Person("Ксьондзик", "Максим", "ksondzik@ukma.edu.ua", new DateTime(2007, 2, 23)),
                new Person("Купчик", "Аліна", "kypchikA@ukma.edu.ua", new DateTime(2012, 10, 26)),
                new Person("Купчик", "Дарина", "kypkikD@ukma.edu.ua", new DateTime(2006, 4, 28)),

                new Person("Кураш", "Ярослава", "kyrash@ukma.edu.ua", new DateTime(2001, 2, 19)),
                new Person("Куренкова", "Олена", "kyrenkova@ukma.edu.ua", new DateTime(2001, 5, 10)),
                new Person("Курочкін", "Денис", "kyrochkin@ukma.edu.ua", new DateTime(2002, 8, 12)),
                new Person("Кучменко", "Ярослав", "kychmenko@ukma.edu.ua", new DateTime(2003, 4, 24)),
                new Person("Левчук", "Володимир", "levchuk@ukma.edu.ua", new DateTime(2005, 1, 24)),
                new Person("Лунякіна", "Валерія", "lyniakina@ukma.edu.ua", new DateTime(1962, 9, 5)),
                new Person("Макаренко", "Богдан", "makarenko@ukma.edu.ua", new DateTime(1989, 11, 6)),
                new Person("Мальков", "Єгор", "malkov@ukma.edu.ua", new DateTime(1947, 1, 3)),
                new Person("Мєдвєдєв", "Дмитро", "miedviediev@ukma.edu.ua", new DateTime(2001, 3, 20)),
                new Person("Миронович", "Олександр", "mironivich@ukma.edu.ua", new DateTime(1975, 3, 3)),
                
                new Person("Молоденков", "Костянтин", "molodenkov@ukma.edu.ua", new DateTime(2003, 3, 1)),
                new Person("Молодцов", "Філліп", "molodtsov@ukma.edu.ua", new DateTime(2005, 7, 7)),
                new Person("Недосєка", "Данило", "nedoseka@ukma.edu.ua", new DateTime(2001, 1, 4)),
                new Person("Поліщук", "Юрій", "polishchuk@ukma.edu.ua", new DateTime(2001, 5, 13)),
                new Person("Примаченко", "Ілля", "primachenko@ukma.edu.ua", new DateTime(2000, 1, 24)),
                new Person("Рибак", "Володимир", "rubak@ukma.edu.ua", new DateTime(2000, 3, 26)),
                new Person("Рожко", "Андрій", "rozhko@ukma.edu.ua", new DateTime(1905, 4, 3)),
                new Person("Романенко", "Михайло", "romanenko@ukma.edu.ua", new DateTime(1910, 3, 8)),
                new Person("Синицин", "Владислав", "sinishun@ukma.edu.ua", new DateTime(2009, 2, 23)),
                new Person("Слободяник", "Максим", "slobodian@ukma.edu.ua", new DateTime(2013, 2, 22)),
                
                new Person("Ставровський", "Михайло", "stavrovsk@ukma.edu.ua", new DateTime(1954, 1, 27)),
                new Person("Хміль", "Юлія", "hmil@ukma.edu.ua", new DateTime(1984, 8, 16)),
                new Person("Холодов", "Дмитро", "holodov@ukma.edu.ua", new DateTime(2005, 6, 12)),
                new Person("Чорнокозинський", "Кирило", "chornokoz@ukma.edu.ua", new DateTime(2003, 9, 15)),
                new Person("Чурілова", "Анна", "churilova@ukma.edu.ua", new DateTime(2010, 10, 5)),
                new Person("Шевченко", "Кирило", "shevshenko@ukma.edu.ua", new DateTime(1985, 11, 3)),
                new Person("Шекета", "Катерина", "sheketa@ukma.edu.ua", new DateTime(1997, 11, 7)),
                new Person("Шутяк", "Таїсія", "shutiak@ukma.edu.ua", new DateTime(1999, 12, 14)),
                new Person("Яцишин", "Ілля", "iatsishin@ukma.edu.ua", new DateTime(2002, 12, 18)),
                new Person("Яцків", "Катерина", "iatskiv@ukma.edu.ua", new DateTime(1978, 12, 20)),
            };
            SaveChanges();
        }
    }
}

