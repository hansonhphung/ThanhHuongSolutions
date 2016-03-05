using MongoDB.Bson;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ThanhHuongSolution.Common.Infrastrucure;
using ThanhHuongSolution.Common.Infrastrucure.Model;
using ThanhHuongSolution.Common.Infrastrucure.MongoDBDataAccess;
using ThanhHuongSolution.Common.Infrastrucure.MongoDBDataAccess.Entity;
using ThanhHuongSolution.DeptManagement.Domain.Entity;
using ThanhHuongSolution.DeptManagement.Domain.Interfaces;

namespace ThanhHuongSolution.DeptManagement.MongoDataAccess
{
    public class DebtManagementRepository : IDebtManagementRepository
    {
        private readonly IReadDataContextFactory _readDataContextFactory;
        private readonly IWriteDataContextFactory _writeDataContextFactory;

        public DebtManagementRepository(IReadDataContextFactory readDataContextFactory, IWriteDataContextFactory writeDataContextFactory)
        {
            Check.NotNull(readDataContextFactory, "readDataContextFactory");
            Check.NotNull(writeDataContextFactory, "writeDataContextFactory");
            _readDataContextFactory = readDataContextFactory;
            _writeDataContextFactory = writeDataContextFactory;
        }

        public async Task<bool> CreateDebt(MDBaseDebt debt)
        {
            var dbContext = _writeDataContextFactory.CreateMongoDBWriteContext();

            var collection = dbContext.GetCollection<MDBaseDebt>(MongoDBEntityNames.DebtCollection.TableName);

            debt.CreatedAt = DateTime.UtcNow;

            await collection.InsertOneAsync(debt);

            return await Task.FromResult(true);
        }

        public async Task<MDBaseDebt> GetDebtById(string debtId)
        {
            var dbContext = _readDataContextFactory.CreateMongoDBReadContext();

            var collection = dbContext.GetCollection<MDBaseDebt>(MongoDBEntityNames.DebtCollection.TableName);

            var data = await collection.Find(x => x.Id == debtId).FirstOrDefaultAsync();

            return await Task.FromResult<MDBaseDebt>(data);
        }

        public async Task<MDBaseDebt> GetDebtByTrackingNumber(string trackingNumber)
        {
            var dbContext = _readDataContextFactory.CreateMongoDBReadContext();

            var collection = dbContext.GetCollection<MDBaseDebt>(MongoDBEntityNames.DebtCollection.TableName);

            var data = await collection.Find(x => x.TrackingNumber == trackingNumber).FirstOrDefaultAsync();

            return await Task.FromResult<MDBaseDebt>(data);
        }

        public async Task<IList<MDBaseDebt>> Search(string customerId, string query, Pagination pagination)
        {
            var keyLower = query.ToLower();

            var dbContext = _readDataContextFactory.CreateMongoDBReadContext();

            var collection = dbContext.GetCollection<MDBaseDebt>(MongoDBEntityNames.DebtCollection.TableName);

            var builder = Builders<MDBaseDebt>.Filter;

            var filterWithoutCustomerId = builder.Or(
                builder.Or(builder.Regex(x => x.TrackingNumber, new BsonRegularExpression(keyLower, "i")),
                builder.Where(x => x.TrackingNumber.Contains(keyLower))),
                builder.Or(builder.Regex(x => x.Customer.CustomerName, new BsonRegularExpression(keyLower, "i"))),
                builder.Where(x => x.Customer.CustomerName.Contains(keyLower)),
                builder.Where(x => x.DebtCreatedDate.Contains(keyLower))
                );

            var filterWithCustomerId = builder.And(
                builder.Or(
                    builder.Or(builder.Regex(x => x.TrackingNumber, new BsonRegularExpression(keyLower, "i")),
                    builder.Where(x => x.TrackingNumber.Contains(keyLower))),
                    builder.Or(builder.Regex(x => x.Customer.CustomerName, new BsonRegularExpression(keyLower, "i")),
                    builder.Where(x => x.Customer.CustomerName.Contains(keyLower))),
                    builder.Where(x => x.DebtCreatedDate.Contains(keyLower))),
                builder.Where(x => x.Customer.CustomerId.Equals(customerId)));
            var sortBy = Builders<MDBaseDebt>.Sort.Descending(pagination.SortBy);

            List<MDBaseDebt> data = null;

            if (Check.IsNullOrEmpty(customerId))
                data = await collection.Find(filterWithoutCustomerId)
                    .Sort(sortBy)
                    .Skip((pagination.PageIndex - 1) * pagination.PageSize)
                    .Limit(pagination.PageSize)
                    .ToListAsync();
            else
                data = await collection.Find(filterWithCustomerId)
                    .Sort(sortBy)
                    .Skip((pagination.PageIndex - 1) * pagination.PageSize)
                    .Limit(pagination.PageSize)
                    .ToListAsync();

            return await Task.FromResult(data);
        }

        public async Task<long> Count(string customerId, string query)
        {
            var keyLower = query.ToLower();

            var dbContext = _readDataContextFactory.CreateMongoDBReadContext();

            var collection = dbContext.GetCollection<MDBaseDebt>(MongoDBEntityNames.BillingCollection.TableName);

            var builder = Builders<MDBaseDebt>.Filter;

            var filterWithoutCustomerId = builder.Or(
                builder.Or(builder.Regex(x => x.TrackingNumber, new BsonRegularExpression(keyLower, "i")),
                builder.Where(x => x.TrackingNumber.Contains(keyLower))),
                builder.Or(builder.Regex(x => x.Customer.CustomerName, new BsonRegularExpression(keyLower, "i"))),
                builder.Where(x => x.Customer.CustomerName.Contains(keyLower)),
                builder.Where(x => x.DebtCreatedDate.Contains(keyLower))
                );

            var filterWithCustomerId = builder.And(
                builder.Or(
                    builder.Or(builder.Regex(x => x.TrackingNumber, new BsonRegularExpression(keyLower, "i")),
                    builder.Where(x => x.TrackingNumber.Contains(keyLower))),
                    builder.Or(builder.Regex(x => x.Customer.CustomerName, new BsonRegularExpression(keyLower, "i")),
                    builder.Where(x => x.Customer.CustomerName.Contains(keyLower))),
                    builder.Where(x => x.DebtCreatedDate.Contains(keyLower))),
                builder.Where(x => x.Customer.CustomerId.Equals(customerId)));

            long count = 0;

            if (Check.IsNullOrEmpty(customerId))
                count = await collection.CountAsync(filterWithoutCustomerId);
            else
                count = await collection.CountAsync(filterWithCustomerId);

            return await Task.FromResult(count);
        }
    }
}
