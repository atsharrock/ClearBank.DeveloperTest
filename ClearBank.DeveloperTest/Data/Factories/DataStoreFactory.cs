using ClearBank.DeveloperTest.Data.Interfaces;

namespace ClearBank.DeveloperTest.Data.Factories
{
    public static class DataStoreFactory
    {
        public static IDataStore CreateDataStore(string dataStoreType)
        {
            return dataStoreType == "Backup" ? new BackupAccountDataStore() : new AccountDataStore();
        }
    }
}