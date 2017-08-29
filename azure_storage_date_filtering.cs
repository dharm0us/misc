using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading;
using SecureKeysManager;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Table;

namespace payloadParsing
{
    public class AzureStorageHelper
    {
        /*
        C# code for filtering by start/end date on a datetime column of a table in Azure Table Storage.
        */
        private const string TableName = "test_table";
        private static CloudTable TableReference = null;
        public const int BatchSize = 50;

        public static List<DynamicTableEntity> FetchAll(string startDateString, string endDateString)
        {
            DateTime startDate = DateTime.ParseExact(startDateString + " 00:00:00 AM", "yyyy-%M-%d %h:%m:%s tt", null);
            DateTime endDate = DateTime.ParseExact(endDateString + " 11:59:59 PM", "yyyy-%M-%d %h:%m:%s tt", null);

            var table = GetTableReference();
            string startDateFilter = TableQuery.GenerateFilterConditionForDate(
                               Constants.EventTimestampKey, QueryComparisons.GreaterThanOrEqual,
                               startDate);
            string endDateFilter = TableQuery.GenerateFilterConditionForDate(
                               Constants.EventTimestampKey, QueryComparisons.LessThanOrEqual,
                               endDate);
            var finalFilter = TableQuery.CombineFilters(startDateFilter, TableOperators.And, endDateFilter);
            var query =
                new TableQuery<DynamicTableEntity>();
            var entities = table.ExecuteQuery(query.Where(finalFilter));

            return entities.ToList();
        }

      
        private static CloudStorageAccount GetCloudStorageAccount()
        {
            var secretKey = "secret_key";
            var connectionString = SecureKeys.ReadSecretStringAsync(secretKey, CancellationToken.None).Result;

            CloudStorageAccount storageAccount;
            CloudStorageAccount.TryParse(connectionString, out storageAccount);
            return storageAccount;
        }

        public static CloudTable GetTableReference()
        {
            if (TableReference == null)
            {
                CloudStorageAccount storageAccount = GetCloudStorageAccount();
                var tableClientLocal = storageAccount.CreateCloudTableClient();
                TableReference = tableClientLocal.GetTableReference(TableName);
            }
            return TableReference;
        }


    }
}
