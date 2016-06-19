using MongoDB.Bson;
using MongoDB.Driver;
using System;
using System.Linq;
using System.Collections.Generic;
using System.Threading.Tasks;
using ThanhHuongSolution.Common.Infrastrucure;
using ThanhHuongSolution.Common.Infrastrucure.MongoDBDataAccess;
using ThanhHuongSolution.Common.Infrastrucure.MongoDBDataAccess.Entity;
using ThanhHuongSolution.Product.Domain.Entity;
using ThanhHuongSolution.Product.Domain.Interfaces;
using ThanhHuongSolution.Product.Domain.Model;

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

            var sortBy = Builders<MDProduct>.Sort.Descending(x => x.UpdatedAt);

            var data = await collection.Find(x => x.Id != null).Sort(sortBy).ToListAsync();

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
                         builder.Where(x => x.Name.Contains(keyLower))));

            var sortBy = Builders<MDProduct>.Sort.Descending(x => x.UpdatedAt);

            var data = await collection.Find(filter).Sort(sortBy).ToListAsync();

            return await Task.FromResult(data);
        }

        public async Task<bool> DeleteProduct(string productId)
        {
            var dbContext = _writeDataContextFactory.CreateMongoDBWriteContext();

            var collection = dbContext.GetCollection<MDProduct>(MongoDBEntityNames.ProductCollection.TableName);

            await collection.DeleteOneAsync(x => x.Id == productId);

            return await Task.FromResult(true);
        }

        public async Task<bool> UpdateProduct(MDProduct product)
        {
            var dbContext = _writeDataContextFactory.CreateMongoDBWriteContext();

            var collection = dbContext.GetCollection<MDProduct>(MongoDBEntityNames.ProductCollection.TableName);

            product.UpdatedAt = DateTime.UtcNow;

            await collection.ReplaceOneAsync(x => x.Id == product.Id, product);

            return await Task.FromResult(true);
        }

        public async Task<bool> UpdateProductNumber(UpdatedSellingProductInfo productInfo)
        {
            var dbContext = _writeDataContextFactory.CreateMongoDBWriteContext();

            var collection = dbContext.GetCollection<MDProduct>(MongoDBEntityNames.ProductCollection.TableName);

            var update = Builders<MDProduct>.Update.Set(x => x.UpdatedAt, DateTime.UtcNow).Set(y => y.Number, productInfo.ProductRemainingNumber);

            await collection.UpdateOneAsync(x => x.TrackingNumber == productInfo.ProductTrackingNumber, update);

            return await Task.FromResult(true);
        }

        public async Task<IList<MDProduct>> GetAllRemainingProduct()
        {
            var dbContext = _writeDataContextFactory.CreateMongoDBWriteContext();

            var collection = dbContext.GetCollection<MDProduct>(MongoDBEntityNames.ProductCollection.TableName);

            var lstProduct = await collection.Find(x => x.Number > 0).ToListAsync();

            return await Task.FromResult(lstProduct);
        }
    }
}
