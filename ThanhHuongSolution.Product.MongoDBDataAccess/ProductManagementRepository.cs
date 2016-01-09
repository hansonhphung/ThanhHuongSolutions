using MongoDB.Bson;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ThanhHuongSolution.Common.Infrastrucure;
using ThanhHuongSolution.Common.Infrastrucure.MongoDBDataAccess;
using ThanhHuongSolution.Common.Infrastrucure.MongoDBDataAccess.Entity;
using ThanhHuongSolution.Product.Domain.Entity;
using ThanhHuongSolution.Product.Domain.Interfaces;

namespace ThanhHuongSolution.Product.MongoDBDataAccess
{
    public class ProductManagementRepository : IProductManagementRepository
    {
        private readonly IReadDataContextFactory _readDataContectFactory;
        private readonly IWriteDataContextFactory _writeDataContextFactory;

        public ProductManagementRepository(IReadDataContextFactory readDataContextFactory, IWriteDataContextFactory writeDataContextFactory)
        {
            Check.NotNull(readDataContextFactory, "readDataContextFactory");
            Check.NotNull(writeDataContextFactory, "writeDataContextFactory");

            _readDataContectFactory = readDataContextFactory;
            _writeDataContextFactory = writeDataContextFactory;
        }

        public async Task<bool> CreateProduct(MDProduct product)
        {
            var dbContext = _writeDataContextFactory.CreateMongoDBWriteContext();

            var collection = dbContext.GetCollection<MDProduct>(MongoDBEntityNames.ProductCollection.TableName);

            product.CreatedAt = product.UpdatedAt = DateTime.UtcNow;

            await collection.InsertOneAsync(product);

            return await Task.FromResult(true);
        }

        public async Task<IList<MDProduct>> GetAllProduct()
        {
            var dbContext = _readDataContectFactory.CreateMongoDBReadContext();

            var collection = dbContext.GetCollection<MDProduct>(MongoDBEntityNames.ProductCollection.TableName);

            var data = await collection.Find(x => x.Id != null).ToListAsync();

            return await Task.FromResult<IList<MDProduct>>(data);
        }

        public async Task<MDProduct> GetProductById(string productId)
        {
            var dbContext = _readDataContectFactory.CreateMongoDBReadContext();

            var collection = dbContext.GetCollection<MDProduct>(MongoDBEntityNames.ProductCollection.TableName);

            var data = await collection.Find(x => x.Id == productId).FirstOrDefaultAsync();

            return await Task.FromResult<MDProduct>(data);
        }

        public async Task<MDProduct> GetProductByTrackingNumber(string trackingNumber)
        {
            var dbContext = _readDataContectFactory.CreateMongoDBReadContext();

            var collection = dbContext.GetCollection<MDProduct>(MongoDBEntityNames.ProductCollection.TableName);

            var data = await collection.Find(x => x.TrackingNumber == trackingNumber).FirstOrDefaultAsync();

            return await Task.FromResult<MDProduct>(data);
        }

        public async Task<IList<MDProduct>> Search(string query)
        {
            var keyLower = query.ToLower();

            var dbContext = _readDataContectFactory.CreateMongoDBReadContext();

            var collection = dbContext.GetCollection<MDProduct>(MongoDBEntityNames.ProductCollection.TableName);

            var builder = Builders<MDProduct>.Filter;


            var filter = builder.Or(
                         builder.Or(builder.Regex(x => x.TrackingNumber, new BsonRegularExpression(keyLower, "i")),
                         builder.Where(x => x.TrackingNumber.Contains(keyLower))),
                         builder.Or(builder.Regex(x => x.Name, new BsonRegularExpression(keyLower, "i")),
                         builder.Where(x => x.TrackingNumber.Contains(keyLower))));

            var data = await collection.Find(filter).ToListAsync();

            return await Task.FromResult(data);
        }

        
    }
}
