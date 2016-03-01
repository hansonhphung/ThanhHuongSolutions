﻿using MongoDB.Bson;
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

namespace ThanhHuongSolution.BillingManagement.MongoDBDataAccess
{
    public class BillingManagementRepository : IBillingManagementRepository
    {
        private readonly IReadDataContextFactory _readDataContextFactory;
        private readonly IWriteDataContextFactory _writeDataContextFactory;

        public BillingManagementRepository(IReadDataContextFactory readDataContextFactory, IWriteDataContextFactory writeDataContextFactory)
        {
            Check.NotNull(readDataContextFactory, "readDataContextFactory");
            Check.NotNull(writeDataContextFactory, "writeDataContextFactory");
            _readDataContextFactory = readDataContextFactory;
            _writeDataContextFactory = writeDataContextFactory;
        }

        public async Task<bool> CreateBill(MDBilling bill)
        {
            var dbContext = _writeDataContextFactory.CreateMongoDBWriteContext();

            var collection = dbContext.GetCollection<MDBilling>(MongoDBEntityNames.BillingCollection.TableName);

            bill.CreatedAt = DateTime.UtcNow;

            await collection.InsertOneAsync(bill);

            return await Task.FromResult(true);
        }

        public async Task<MDBilling> GetBillById(string billId)
        {
            var dbContext = _readDataContextFactory.CreateMongoDBReadContext();

            var collection = dbContext.GetCollection<MDBilling>(MongoDBEntityNames.BillingCollection.TableName);

            var data = await collection.Find(x => x.Id == billId).FirstOrDefaultAsync();

            return await Task.FromResult<MDBilling>(data);
        }

        public async Task<MDBilling> GetBillByTrackingNumber(string trackingNumber)
        {
            var dbContext = _readDataContextFactory.CreateMongoDBReadContext();

            var collection = dbContext.GetCollection<MDBilling>(MongoDBEntityNames.BillingCollection.TableName);

            var data = await collection.Find(x => x.TrackingNumber == trackingNumber).FirstOrDefaultAsync();

            return await Task.FromResult<MDBilling>(data);
        }
		
        public async Task<IList<MDBilling>> Search(string customerId, string query, Pagination pagination)
        {
            var keyLower = query.ToLower();

            var dbContext = _readDataContextFactory.CreateMongoDBReadContext();

            var collection = dbContext.GetCollection<MDBilling>(MongoDBEntityNames.BillingCollection.TableName);

            var builder = Builders<MDBilling>.Filter;

            var filterWithoutCustomerId = builder.Or(
                builder.Or(builder.Regex(x => x.TrackingNumber, new BsonRegularExpression(keyLower, "i")),
                builder.Where(x => x.TrackingNumber.Contains(keyLower))),
                builder.Or(builder.Regex(x => x.Customer.CustomerName, new BsonRegularExpression(keyLower, "i"))),
                builder.Where(x => x.Customer.CustomerName.Contains(keyLower)),
                builder.Where(x => x.BillCreatedDate.Contains(keyLower))
                );

            var filterWithCustomerId = builder.And(
                builder.Or(
                    builder.Or(builder.Regex(x => x.TrackingNumber, new BsonRegularExpression(keyLower, "i")),
                    builder.Where(x => x.TrackingNumber.Contains(keyLower))),
                    builder.Or(builder.Regex(x => x.Customer.CustomerName, new BsonRegularExpression(keyLower, "i")),
                    builder.Where(x => x.Customer.CustomerName.Contains(keyLower))),
                    builder.Where(x => x.BillCreatedDate.Contains(keyLower))),
                builder.Where(x => x.Customer.CustomerId.Equals(customerId)));
            var sortBy = Builders<MDBilling>.Sort.Descending(pagination.SortBy);

            List<MDBilling> data = null;

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

            var collection = dbContext.GetCollection<MDBilling>(MongoDBEntityNames.BillingCollection.TableName);

            var builder = Builders<MDBilling>.Filter;

            var filterWithoutCustomerId = builder.Or(
                builder.Or(builder.Regex(x => x.TrackingNumber, new BsonRegularExpression(keyLower, "i")),
                builder.Where(x => x.TrackingNumber.Contains(keyLower))),
                builder.Or(builder.Regex(x => x.Customer.CustomerName, new BsonRegularExpression(keyLower, "i"))),
                builder.Where(x => x.Customer.CustomerName.Contains(keyLower)),
                builder.Where(x => x.BillCreatedDate.Contains(keyLower))
                );

            var filterWithCustomerId = builder.And(
                builder.Or(
                    builder.Or(builder.Regex(x => x.TrackingNumber, new BsonRegularExpression(keyLower, "i")),
                    builder.Where(x => x.TrackingNumber.Contains(keyLower))),
                    builder.Or(builder.Regex(x => x.Customer.CustomerName, new BsonRegularExpression(keyLower, "i")),
                    builder.Where(x => x.Customer.CustomerName.Contains(keyLower))),
                    builder.Where(x => x.BillCreatedDate.Contains(keyLower))),
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
