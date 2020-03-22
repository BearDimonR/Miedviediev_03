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
                new Person("Катерина","Білорус","bilorus@ukma.edu.ua", new DateTime(2001, 3, 5)),
                new Person("Данило","Бойко", "boyko@ukma.edu.ua", new DateTime(2005, 5, 2)),
                new Person("Євген","Бородайкевич", "borodaikevich@ukma.edu.ua", new DateTime(2002, 6, 15)),
                new Person("Данило","Ванін", "vanin@ukma.edu.ua", new DateTime(1950, 11, 25)),
                new Person("Кирило","Василенко", "vasilenko@ukma.edu.ua", new DateTime(2007, 2, 23)),
                new Person("Максим","Волошко", "voloshko@ukma.edu.ua", new DateTime(1900, 9, 30)),
                new Person("Роман","Гайворонський", "gaivoron@ukma.edu.ua", new DateTime(1918, 11, 7)),
                new Person("Артем","Гак", "hak@ukma.edu.ua", new DateTime(1968, 1, 1)),
                new Person("Михайло","Галайда", "halaida@ukma.edu.ua", new DateTime(1999, 8, 12)),
                new Person("Анна","Гінкул", "hinkyl@ukma.edu.ua", new DateTime(1899, 4, 19)),
                
                new Person("Владислав","Декрет", "dekret@ukma.edu.ua", new DateTime(1945, 3, 2)),
                new Person("Ігор","Єгоров", "ihor@ukma.edu.ua", new DateTime(2003, 5, 5)),
                new Person("Марина","Жуковська", "zhykovska@ukma.edu.ua", new DateTime(2000, 8, 13)),
                new Person("Максим","Задорожний", "zadorozh@ukma.edu.ua", new DateTime(2001, 9, 18)),
                new Person("Антон","Залізний", "zaliznyi@ukma.edu.ua", new DateTime(2001, 4, 25)),
                new Person("Віталій","Кенийз", "keniiz@ukma.edu.ua", new DateTime(1900, 8, 29)),
                new Person("Вадим","Копійка", "kopiika@ukma.edu.ua", new DateTime(2001, 3, 23)),
                new Person("Максим","Ксьондзик", "ksondzik@ukma.edu.ua", new DateTime(2007, 2, 23)),
                new Person("Аліна","Купчик", "kypchikA@ukma.edu.ua", new DateTime(2012, 10, 26)),
                new Person("Дарина","Купчик", "kypkikD@ukma.edu.ua", new DateTime(2006, 4, 28)),

                new Person("Ярослава","Кураш", "kyrash@ukma.edu.ua", new DateTime(2001, 2, 19)),
                new Person("Олена","Куренкова", "kyrenkova@ukma.edu.ua", new DateTime(2001, 5, 10)),
                new Person("Денис","Курочкін", "kyrochkin@ukma.edu.ua", new DateTime(2002, 8, 12)),
                new Person("Ярослав","Кучменко", "kychmenko@ukma.edu.ua", new DateTime(2003, 4, 24)),
                new Person("Володимир","Левчук", "levchuk@ukma.edu.ua", new DateTime(2005, 1, 24)),
                new Person("Валерія","Лунякіна", "lyniakina@ukma.edu.ua", new DateTime(1962, 9, 5)),
                new Person("Богдан","Макаренко", "makarenko@ukma.edu.ua", new DateTime(1989, 11, 6)),
                new Person("Єгор","Мальков", "malkov@ukma.edu.ua", new DateTime(1947, 1, 3)),
                new Person("Дмитро","Мєдвєдєв", "miedviediev@ukma.edu.ua", new DateTime(2001, 3, 20)),
                new Person("Олександр","Миронович", "mironivich@ukma.edu.ua", new DateTime(1975, 3, 3)),
                
                new Person("Костянтин","Молоденков", "molodenkov@ukma.edu.ua", new DateTime(2003, 3, 1)),
                new Person("Філліп","Молодцов", "molodtsov@ukma.edu.ua", new DateTime(2005, 7, 7)),
                new Person("Данило","Недосєка", "nedoseka@ukma.edu.ua", new DateTime(2001, 1, 4)),
                new Person("Юрій","Поліщук", "polishchuk@ukma.edu.ua", new DateTime(2001, 5, 13)),
                new Person("Ілля","Примаченко", "primachenko@ukma.edu.ua", new DateTime(2000, 1, 24)),
                new Person("Володимир","Рибак", "rubak@ukma.edu.ua", new DateTime(2000, 3, 26)),
                new Person("Андрій","Рожко", "rozhko@ukma.edu.ua", new DateTime(1905, 4, 3)),
                new Person("Михайло","Романенко", "romanenko@ukma.edu.ua", new DateTime(1910, 3, 8)),
                new Person("Владислав","Синицин", "sinishun@ukma.edu.ua", new DateTime(2009, 2, 23)),
                new Person("Максим","Слободяник", "slobodian@ukma.edu.ua", new DateTime(2013, 2, 22)),
                
                new Person("Михайло","Ставровський", "stavrovsk@ukma.edu.ua", new DateTime(1954, 1, 27)),
                new Person("Юлія","Хміль", "hmil@ukma.edu.ua", new DateTime(1984, 8, 16)),
                new Person("Дмитро","Холодов", "holodov@ukma.edu.ua", new DateTime(2005, 6, 12)),
                new Person("Кирило","Чорнокозинський", "chornokoz@ukma.edu.ua", new DateTime(2003, 9, 15)),
                new Person("Анна","Чурілова", "churilova@ukma.edu.ua", new DateTime(2010, 10, 5)),
                new Person("Кирило","Шевченко", "shevshenko@ukma.edu.ua", new DateTime(1985, 11, 3)),
                new Person("Катерина","Шекета", "sheketa@ukma.edu.ua", new DateTime(1997, 11, 7)),
                new Person("Таїсія","Шутяк", "shutiak@ukma.edu.ua", new DateTime(1999, 12, 14)),
                new Person("Ілля","Яцишин", "iatsishin@ukma.edu.ua", new DateTime(2002, 12, 18)),
                new Person("Катерина","Яцків", "iatskiv@ukma.edu.ua", new DateTime(1978, 12, 20)),
            };
            SaveChanges();
        }
    }
}

