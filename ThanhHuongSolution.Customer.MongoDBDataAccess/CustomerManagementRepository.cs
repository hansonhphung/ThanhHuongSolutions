using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ThanhHuongSolution.Common.Infrastrucure;
using ThanhHuongSolution.Common.Infrastrucure.MongoDBDataAccess;
using ThanhHuongSolution.Common.Infrastrucure.MongoDBDataAccess.Entity;
using ThanhHuongSolution.Customer.Domain.Entity;
using MongoDB.Driver;
using MongoDB.Bson;

namespace ThanhHuongSolution.Customer.MongoDBDataAccess
{
    public class CustomerManagementRepository : ICustomerRepository
    {
        private readonly IReadDataContextFactory _readDataContectFactory;
        private readonly IWriteDataContextFactory _writeDataContextFactory;

        public CustomerManagementRepository(IReadDataContextFactory readDataContextFactory, IWriteDataContextFactory writeDataContextFactory)
        {
            Check.NotNull(readDataContextFactory, "readDataContextFactory");
            Check.NotNull(writeDataContextFactory, "writeDataContextFactory");

            _readDataContectFactory = readDataContextFactory;
            _writeDataContextFactory = writeDataContextFactory;
        }

        public async Task<bool> CreateCustomer(MDCustomer customer)
        {
            var dbContext = _writeDataContextFactory.CreateMongoDBWriteContext();

            var collection = dbContext.GetCollection<MDCustomer>(MongoDBEntityNames.CustomerCollection.TableName);

            await collection.InsertOneAsync(customer);

            return await Task.FromResult(true);
        }

        public async Task<IList<MDCustomer>> GetAllCustomer()
        {
            var dbContext = _readDataContectFactory.CreateMongoDBReadContext();

            var collection = dbContext.GetCollection<MDCustomer>(MongoDBEntityNames.CustomerCollection.TableName);

            var data = await collection.Find(x => x.Id != null).ToListAsync();
            
            return await Task.FromResult<IList<MDCustomer>>(data);
        }

        public async Task<MDCustomer> GetCustomerById(string id)
        {
            var dbContext = _readDataContectFactory.CreateMongoDBReadContext();

            var collection = dbContext.GetCollection<MDCustomer>(MongoDBEntityNames.CustomerCollection.TableName);

            var data = await collection.Find(x => x.Id == id).FirstOrDefaultAsync();

            return await Task.FromResult(data);
        }

        public async Task<MDCustomer> GetCustomerByTrackingNumber(string trackingNumber)
        {
            var dbContext = _readDataContectFactory.CreateMongoDBReadContext();

            var collection = dbContext.GetCollection<MDCustomer>(MongoDBEntityNames.CustomerCollection.TableName);

            var data = await collection.Find(x => x.TrackingNumber == trackingNumber).FirstOrDefaultAsync();

            return await Task.FromResult(data);
        }

        public async Task<IList<MDCustomer>> Search(string query)
        {
            var keyLower= query.ToLower();

            var dbContext = _readDataContectFactory.CreateMongoDBReadContext();

            var collection = dbContext.GetCollection<MDCustomer>(MongoDBEntityNames.CustomerCollection.TableName);

            var builder = Builders<MDCustomer>.Filter;

            var filter = builder.Or(builder.Regex(x => x.Name, new BsonRegularExpression(keyLower, "i"))
                       & builder.Or(builder.Regex(x => x.TrackingNumber, new BsonRegularExpression(keyLower, "i"))
                       & builder.Where(x => x.TrackingNumber.Contains(keyLower) || x.Name.Contains(keyLower))));

            var data = await collection.Find(filter).ToListAsync();

            return await Task.FromResult(data);
        }
    }
}
