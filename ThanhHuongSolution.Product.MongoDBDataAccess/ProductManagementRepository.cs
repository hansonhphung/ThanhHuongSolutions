using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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
    }
}
