using MongoDB.Bson;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ThanhHuongSolution.Common.Infrastrucure;
using ThanhHuongSolution.Common.Infrastrucure.MongoDBDataAccess;
using ThanhHuongSolution.Common.Infrastrucure.MongoDBDataAccess.Entity;
using ThanhHuongSolution.BillingManagement.Domain.Entity;
using ThanhHuongSolution.BillingManagement.Domain.Interface;
using ThanhHuongSolution.Common.Infrastrucure.Model;
using System.Linq;
using ThanhHuongSolution.BillingManagement.Domain.Model;

namespace ThanhHuongSolution.BillingManagement.MongoDBDataAccess
{
    public class BillingManagementRepository : IBillingManagementRepository
    {
        private readonly IReadDataContextFactory _readDataContextFactory;
        private readonly IWriteDataContextFactory _writeDataContextFactory;
        private const string RECEIVING_BILL = "RECEIVING_BILL";

        public BillingManagementRepository(IReadDataContextFactory readDataContextFactory, IWriteDataContextFactory writeDataContextFactory)
        {
            Check.NotNull(readDataContextFactory, "readDataContextFactory");
            Check.NotNull(writeDataContextFactory, "writeDataContextFactory");
            _readDataContextFactory = readDataContextFactory;
            _writeDataContextFactory = writeDataContextFactory;
        }

        public async Task<bool> CreateBill(MDBaseBill bill)
        {
            var dbContext = _writeDataContextFactory.CreateMongoDBWriteContext();

            var collection = dbContext.GetCollection<MDBaseBill>(MongoDBEntityNames.BillingCollection.TableName);

            bill.CreatedAt = DateTime.UtcNow;

            await collection.InsertOneAsync(bill);

            return await Task.FromResult(true);
        }

        public async Task<MDBaseBill> GetBillById(string billId)
        {
            var dbContext = _readDataContextFactory.CreateMongoDBReadContext();

            var collection = dbContext.GetCollection<MDBaseBill>(MongoDBEntityNames.BillingCollection.TableName);

            var data = await collection.Find(x => x.Id == billId).FirstOrDefaultAsync();

            return await Task.FromResult<MDBaseBill>(data);
        }

        public async Task<MDBaseBill> GetBillByTrackingNumber(string trackingNumber)
        {
            var dbContext = _readDataContextFactory.CreateMongoDBReadContext();

            var collection = dbContext.GetCollection<MDBaseBill>(MongoDBEntityNames.BillingCollection.TableName);

            var data = await collection.Find(x => x.TrackingNumber == trackingNumber).FirstOrDefaultAsync();

            return await Task.FromResult<MDBaseBill>(data);
        }
		
        public async Task<IList<MDBaseBill>> Search(string customerId, string query, Pagination pagination, string billType)
        {
            var keyLower = query.ToLower();

            var dbContext = _readDataContextFactory.CreateMongoDBReadContext();

            var collection = dbContext.GetCollection<MDBaseBill>(MongoDBEntityNames.BillingCollection.TableName);

            var builder = Builders<MDBaseBill>.Filter;

            var filterWithoutCustomerId = builder.And(
                builder.Or(
                    builder.Or(builder.Regex(x => x.TrackingNumber, new BsonRegularExpression(keyLower, "i")),
                    builder.Where(x => x.TrackingNumber.Contains(keyLower))),
                    builder.Or(builder.Regex(x => x.Customer.CustomerName, new BsonRegularExpression(keyLower, "i"))),
                    builder.Where(x => x.Customer.CustomerName.Contains(keyLower)),
                    builder.Where(x => x.BillCreatedDate.Contains(keyLower))),
                builder.Eq("_t", billType));

            var filterWithCustomerId = builder.And(
                builder.Or(
                    builder.Or(builder.Regex(x => x.TrackingNumber, new BsonRegularExpression(keyLower, "i")),
                    builder.Where(x => x.TrackingNumber.Contains(keyLower))),
                    builder.Or(builder.Regex(x => x.Customer.CustomerName, new BsonRegularExpression(keyLower, "i")),
                    builder.Where(x => x.Customer.CustomerName.Contains(keyLower))),
                    builder.Where(x => x.BillCreatedDate.Contains(keyLower))),
                builder.Where(x => x.Customer.CustomerId.Equals(customerId)),
                builder.Eq("_t", billType));
            var sortBy = Builders<MDBaseBill>.Sort.Descending(pagination.SortBy);

            List<MDBaseBill> data = null;

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

        public async Task<long> Count(string customerId, string query, string billType)
        {
            var keyLower = query.ToLower();

            var dbContext = _readDataContextFactory.CreateMongoDBReadContext();

            var collection = dbContext.GetCollection<MDBaseBill>(MongoDBEntityNames.BillingCollection.TableName);

            var builder = Builders<MDBaseBill>.Filter;

            var filterWithoutCustomerId = builder.And(
                builder.Or(
                    builder.Or(builder.Regex(x => x.TrackingNumber, new BsonRegularExpression(keyLower, "i")),
                    builder.Where(x => x.TrackingNumber.Contains(keyLower))),
                    builder.Or(builder.Regex(x => x.Customer.CustomerName, new BsonRegularExpression(keyLower, "i"))),
                    builder.Where(x => x.Customer.CustomerName.Contains(keyLower)),
                    builder.Where(x => x.BillCreatedDate.Contains(keyLower))),
                builder.Eq("_t", billType));

            var filterWithCustomerId = builder.And(
                builder.Or(
                    builder.Or(builder.Regex(x => x.TrackingNumber, new BsonRegularExpression(keyLower, "i")),
                    builder.Where(x => x.TrackingNumber.Contains(keyLower))),
                    builder.Or(builder.Regex(x => x.Customer.CustomerName, new BsonRegularExpression(keyLower, "i")),
                    builder.Where(x => x.Customer.CustomerName.Contains(keyLower))),
                    builder.Where(x => x.BillCreatedDate.Contains(keyLower))),
                builder.Where(x => x.Customer.CustomerId.Equals(customerId)),
                builder.Eq("_t", billType));

            long count = 0;

            if (Check.IsNullOrEmpty(customerId))
                count = await collection.CountAsync(filterWithoutCustomerId);
            else
                count = await collection.CountAsync(filterWithCustomerId);

            return await Task.FromResult(count);
        }

        public async Task<bool> IsCustomerHaveTransaction(string customerId)
        {
            var dbContext = _readDataContextFactory.CreateMongoDBReadContext();

            var collection = dbContext.GetCollection<MDBaseBill>(MongoDBEntityNames.BillingCollection.TableName);

            var bills = await collection.Find(x => x.Customer.CustomerId == customerId).ToListAsync();

            return await Task.FromResult(!Check.CollectionIsNullOrEmpty(bills));
        }

        public async Task<long> GetProductLastPrice(string productTrackingNumber)
        {
            var dbContext = _writeDataContextFactory.CreateMongoDBWriteContext();

            var collection = dbContext.GetCollection<MDBaseBill>(MongoDBEntityNames.BillingCollection.TableName);

            var builder = Builders<MDBaseBill>.Filter;

            var filter = builder.And(
                builder.Eq("_t", RECEIVING_BILL),
                builder.Where(x => x.Cart.Any(y => y.ProductTrackingNumber == productTrackingNumber)));

            var sortBy = Builders<MDBaseBill>.Sort.Descending(x => x.CreatedAt);

            var bill = await collection.Find(filter).Sort(sortBy).FirstOrDefaultAsync();

            var totalPrice = bill.Cart.Where(x => x.ProductTrackingNumber == productTrackingNumber).FirstOrDefault().Price;

            var quantity = bill.Cart.Where(x => x.ProductTrackingNumber == productTrackingNumber).FirstOrDefault().Number;

            var price = totalPrice / quantity;

            return await Task.FromResult(price);
        }

        public async Task<IList<MDBaseBill>> GetBillInRangeDate(string query, DateTime fromDate, DateTime toDate, Pagination pagination, string billType)
        {
            var keyLower = query.ToLower();

            var dbContext = _readDataContextFactory.CreateMongoDBReadContext();

            var collection = dbContext.GetCollection<MDBaseBill>(MongoDBEntityNames.BillingCollection.TableName);

            var builder = Builders<MDBaseBill>.Filter;

            var filterWithoutCustomerId = builder.And(
                builder.Where(x => x.BillCreatedDate_DT >= fromDate),
                builder.Where(x => x.BillCreatedDate_DT <= toDate),
                builder.Or(
                    builder.Or(builder.Regex(x => x.TrackingNumber, new BsonRegularExpression(keyLower, "i")),
                    builder.Where(x => x.TrackingNumber.Contains(keyLower))),
                    builder.Or(builder.Regex(x => x.Customer.CustomerName, new BsonRegularExpression(keyLower, "i"))),
                    builder.Where(x => x.Customer.CustomerName.Contains(keyLower))),
                builder.Eq("_t", billType));

            var sortBy = Builders<MDBaseBill>.Sort.Descending(pagination.SortBy);

            List<MDBaseBill> data = null;

            
            data = await collection.Find(filterWithoutCustomerId)
                .Sort(sortBy)
                .Skip((pagination.PageIndex - 1) * pagination.PageSize)
                .Limit(pagination.PageSize)
                .ToListAsync();

            return await Task.FromResult(data);
        }

        public async Task<StatisticsBillingInfo> CountStatisticsBill(string query, DateTime fromDate, DateTime toDate, string billType)
        {
            var keyLower = query.ToLower();

            var dbContext = _readDataContextFactory.CreateMongoDBReadContext();

            var collection = dbContext.GetCollection<MDBaseBill>(MongoDBEntityNames.BillingCollection.TableName);

            var builder = Builders<MDBaseBill>.Filter;

            var filterWithoutCustomerId = builder.And(
                builder.Where(x => x.BillCreatedDate_DT >= fromDate),
                builder.Where(x => x.BillCreatedDate_DT <= toDate),
                builder.Or(
                    builder.Or(builder.Regex(x => x.TrackingNumber, new BsonRegularExpression(keyLower, "i")),
                    builder.Where(x => x.TrackingNumber.Contains(keyLower))),
                    builder.Or(builder.Regex(x => x.Customer.CustomerName, new BsonRegularExpression(keyLower, "i"))),
                    builder.Where(x => x.Customer.CustomerName.Contains(keyLower))),
                builder.Eq("_t", billType));

            //long count = await collection.CountAsync(filterWithoutCustomerId);

            var lstBill = await collection.Find(filterWithoutCustomerId).ToListAsync();

            long totalCost = 0;

            foreach (var bill in lstBill)
            {
                totalCost += bill.TotalAmount;
            }

            return await Task.FromResult(new StatisticsBillingInfo() { NumberOfBill = lstBill.Count, TotalCost = totalCost});
        }
    }
}
