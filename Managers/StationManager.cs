using System;
using System.Windows;
using Miedviediev_03.DataStorage;

namespace Miedviediev_03.Managers
{
    internal static class StationManager
    {
        private static IDataStorage _dataStorage;
        internal static IDataStorage DataStorage => _dataStorage;

        internal static void Initialize(IDataStorage dataStorage)
        {
            _dataStorage = dataStorage;
        }
        
        internal static void CloseApp()
        {
            MessageBox.Show("ShuttingDown...");
            Environment.Exit(1);
        }

        public static void CheckFirstLaunch()
        {
            if (!FileFolderHelper.CreateFolderAndCheckFileExistance(FileFolderHelper.StorageFilePath))
                _dataStorage.DoFirstInit();
        }
    }
}
