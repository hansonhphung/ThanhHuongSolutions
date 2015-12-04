﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ThanhHuongSolution.Common.Infrastrucure;
using ThanhHuongSolution.Common.Infrastrucure.MongoDBDataAccess;
using ThanhHuongSolution.Common.Infrastrucure.MongoDBDataAccess.Entity;
using ThanhHuongSolution.Customer.Domain.Entity;
using MongoDB.Driver;

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

        public async Task<MDCustomer> CreateCustomer(MDCustomer customer)
        {
            var dbContext = _writeDataContextFactory.CreateMongoDBWriteContext();

            var collection = dbContext.GetCollection<MDCustomer>(MongoDBEntityNames.CustomerCollection.TableName);

            await collection.InsertOneAsync(customer);

            return await Task.FromResult<MDCustomer>(customer);
        }

        public async Task<IList<MDCustomer>> GetAllCustomer()
        {
            var dbContext = _readDataContectFactory.CreateMongoDBReadContext();

            var collection = dbContext.GetCollection<MDCustomer>(MongoDBEntityNames.CustomerCollection.TableName);

            var data = await collection.Find(x => x.Id != null).ToListAsync();

            return await Task.FromResult<IList<MDCustomer>>(data);
        }
    }
}
