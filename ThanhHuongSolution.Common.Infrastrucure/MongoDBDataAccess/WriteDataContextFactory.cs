using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ThanhHuongSolution.Common.Infrastrucure.MongoDBDataAccess
{
    public class WriteDataContextFactory : IWriteDataContextFactory 
    {
        private readonly MongoClient _mongoDBClient;
        private readonly string _connectionString;

        public WriteDataContextFactory(string connectionString)
        {
            Check.NotEmpty(connectionString, "connectionString");

            _connectionString = connectionString;

            _mongoDBClient = new MongoClient(connectionString);
        }

        public IMongoDatabase CreateMongoDBWriteContext()
        {
            var dbName = new MongoUrl(_connectionString).DatabaseName;

            return _mongoDBClient.GetDatabase(dbName);
        }
    }
}
