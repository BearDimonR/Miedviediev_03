using System.Collections.Generic;
using Miedviediev_03.Models;

namespace Miedviediev_03.DataStorage
{
    internal interface IDataStorage
    {
        void SaveList(List<Person> persons);

        void DoFirstInit();
        List<Person> PersonsList { get; }
    }
}
