﻿using System.ComponentModel;
using System.Windows;

namespace Miedviediev_03.Managers
{
    internal interface ILoaderOwner : INotifyPropertyChanged
    {
        Visibility LoaderVisibility
        {
            set;
        }

        bool IsControlEnabled { set; } 
    }
}