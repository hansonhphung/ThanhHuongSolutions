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
using ThanhHuongSolution.Common.LocResources;

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

            customer.CreatedAt = customer.UpdatedAt = DateTime.UtcNow;

            await collection.InsertOneAsync(customer);

            return await Task.FromResult(true);
        }

        public async Task<IList<MDCustomer>> GetAllCustomer()
        {
            var dbContext = _readDataContectFactory.CreateMongoDBReadContext();

            var collection = dbContext.GetCollection<MDCustomer>(MongoDBEntityNames.CustomerCollection.TableName);

            var sortBy = Builders<MDCustomer>.Sort.Descending(x => x.UpdatedAt);

            var data = await collection.Find(x => x.DeletedAt == null).Sort(sortBy).ToListAsync();
            
            return await Task.FromResult<IList<MDCustomer>>(data);
        }

        public async Task<MDCustomer> GetCustomerById(string id)
        {
            var dbContext = _readDataContectFactory.CreateMongoDBReadContext();

            var collection = dbContext.GetCollection<MDCustomer>(MongoDBEntityNames.CustomerCollection.TableName);

            var data = await collection.Find(x => x.Id == id && x.DeletedAt == null).FirstOrDefaultAsync();

            return await Task.FromResult(data);
        }

        public async Task<MDCustomer> GetCustomerByTrackingNumber(string trackingNumber)
        {
            var dbContext = _readDataContectFactory.CreateMongoDBReadContext();

            var collection = dbContext.GetCollection<MDCustomer>(MongoDBEntityNames.CustomerCollection.TableName);

            var data = await collection.Find(x => x.TrackingNumber == trackingNumber && x.DeletedAt == null).FirstOrDefaultAsync();

            return await Task.FromResult(data);
        }

        public async Task<IList<MDCustomer>> Search(string query)
        {

            var keyLower= query.ToLower();

            var dbContext = _readDataContectFactory.CreateMongoDBReadContext();

            var collection = dbContext.GetCollection<MDCustomer>(MongoDBEntityNames.CustomerCollection.TableName);

            var builder = Builders<MDCustomer>.Filter;


            var filter = builder.Or(
                         builder.Or(builder.Regex(x => x.TrackingNumber, new BsonRegularExpression(keyLower, "i")),
                         builder.Where(x => x.TrackingNumber.Contains(keyLower))),
                         builder.Or(builder.Regex(x => x.Name, new BsonRegularExpression(keyLower, "i")),
                         builder.Where(x => x.Name.Contains(keyLower))))
                         & builder.Where(x => x.DeletedAt == null);

            var sortBy = Builders<MDCustomer>.Sort.Descending(x => x.UpdatedAt);

            var data = await collection.Find(filter).Sort(sortBy).ToListAsync();

            return await Task.FromResult(data);
        }

        public async Task<bool> UpdateCustomer(MDCustomer customer)
        {
            var dbContext = _writeDataContextFactory.CreateMongoDBWriteContext();

            var collection = dbContext.GetCollection<MDCustomer>(MongoDBEntityNames.CustomerCollection.TableName);

            customer.UpdatedAt = DateTime.UtcNow;

            await collection.ReplaceOneAsync(x => x.Id == customer.Id, customer);

            return await Task.FromResult(true);
        }

        public async Task<bool> DeleteCustomer(string customerId)
        {
            var dbContext = _writeDataContextFactory.CreateMongoDBWriteContext();

            var collection = dbContext.GetCollection<MDCustomer>(MongoDBEntityNames.CustomerCollection.TableName);

            var oldCustomer = await GetCustomerById(customerId);

            Check.ThrowExceptionIfNull(oldCustomer, CustomerManagementResources.CUSTOMER_NOT_EXIST);

            oldCustomer.UpdatedAt = DateTime.UtcNow;

            oldCustomer.DeletedAt = DateTime.UtcNow;

            await collection.ReplaceOneAsync(x => x.Id == customerId, oldCustomer);

            return await Task.FromResult(true);
        }

        public async Task<bool> SetVIPCustomer(string customerId, bool isVIP)
        {
            var dbContext = _writeDataContextFactory.CreateMongoDBWriteContext();

            var collection = dbContext.GetCollection<MDCustomer>(MongoDBEntityNames.CustomerCollection.TableName);

            var oldCustomer = await GetCustomerById(customerId);

            Check.ThrowExceptionIfNull(oldCustomer, CustomerManagementResources.CUSTOMER_NOT_EXIST);

            oldCustomer.UpdatedAt = DateTime.UtcNow;

            oldCustomer.IsVIP = isVIP;

            await collection.ReplaceOneAsync(x => x.Id == customerId, oldCustomer);

            return await Task.FromResult(true);
        }

        public async Task<bool> UpdateCustomerDebt(string customerId, long debtAmount)
        {
            var dbContext = _writeDataContextFactory.CreateMongoDBWriteContext();

            var collection = dbContext.GetCollection<MDCustomer>(MongoDBEntityNames.CustomerCollection.TableName);

            var builder = Builders<MDCustomer>.Filter;

            var filter = builder.Where(x => x.Id.Equals(customerId));

            var update = Builders<MDCustomer>.Update.Inc(x => x.LiabilityAmount, debtAmount);

            await collection.UpdateOneAsync(filter, update);

            return await Task.FromResult(true);
        }

        public async Task<IList<MDCustomer>> GetAllDebtCustomer()
        {
            var dbContext = _writeDataContextFactory.CreateMongoDBWriteContext();

            var collection = dbContext.GetCollection<MDCustomer>(MongoDBEntityNames.CustomerCollection.TableName);

            var builder = Builders<MDCustomer>.Filter;

            var filter = builder.Where(x => x.LiabilityAmount > 0 && x.DeletedAt == null);

            var sortBy = Builders<MDCustomer>.Sort.Descending(x => x.UpdatedAt);

            var data = await collection.Find(filter).Sort(sortBy).ToListAsync();

            return await Task.FromResult(data);
        }
    }
}
