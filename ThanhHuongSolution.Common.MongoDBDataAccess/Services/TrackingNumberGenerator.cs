using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ThanhHuongSolution.Common.Infrastrucure;
using ThanhHuongSolution.Common.Infrastrucure.MongoDBDataAccess;
using ThanhHuongSolution.Common.Infrastrucure.MongoDBDataAccess.Entity;
using ThanhHuongSolution.Common.MongoDBDataAccess.Entity;
using ThanhHuongSolution.Common.MongoDBDataAccess.Interface;

namespace ThanhHuongSolution.Common.MongoDBDataAccess.Services
{
    public class TrackingNumberGenerator : ITrackingNumberGenerator
    {
        private readonly IReadDataContextFactory _readDataContectFactory;
        private readonly IWriteDataContextFactory _writeDataContextFactory;

        public TrackingNumberGenerator(IReadDataContextFactory readDataContextFactory, IWriteDataContextFactory writeDataContextFactory)
        {
            Check.NotNull(readDataContextFactory, "readDataContextFactory");
            Check.NotNull(writeDataContextFactory, "writeDataContextFactory");

            _readDataContectFactory = readDataContextFactory;
            _writeDataContextFactory = writeDataContextFactory;
        }

        public async Task<string> GenerateTrackingNumber(ObjectType objectType)
        {
            var dbContext = _writeDataContextFactory.CreateMongoDBWriteContext();

            var collection = dbContext.GetCollection<MDTrackingNumberGenerator>(MongoDBEntityNames.TrackingGeneratorCollection.TableName);

            var builder = Builders<MDTrackingNumberGenerator>.Filter;

            var filter = builder.Where(x => x.ObjectType == objectType);

            var update = Builders<MDTrackingNumberGenerator>.Update.Inc(x => x.Ordinal, 1);

            await collection.UpdateOneAsync(filter, update);

            var data = await collection.Find(x => x.ObjectType == objectType).FirstOrDefaultAsync();

            var result = string.Format("{0}-{1}", data.Prefix, data.Ordinal.ToString("D9"));

            return await Task.FromResult(result);
        }
    }
}
