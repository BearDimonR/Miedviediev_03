using System;
using System.Collections.Generic;
using Miedviediev_03.Models;
using Miedviediev_03.ViewModels.PersonVm;

namespace Miedviediev_03.DataStorage
{
    internal interface IDataStorage
    {
        void SaveList();
        void AddPerson(Person person);

        void EditPerson(int index, Person person);
        void RemovePerson(Person item);
        List<Person> Filter
            (string name, string surname, string email, DateTime birthday, BoolBoxType isBirthday, BoolBoxType isAdult, 
            WesternZodiac westernZodiac, ChineseZodiac chineseZodiac);
        void DoFirstInit();
        List<Person> PersonsList { get; }
    }
}
