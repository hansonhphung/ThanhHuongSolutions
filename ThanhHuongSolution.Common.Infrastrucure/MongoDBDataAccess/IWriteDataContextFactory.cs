using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ThanhHuongSolution.Common.Infrastrucure.MongoDBDataAccess
{
    public interface IWriteDataContextFactory
    {
        IMongoDatabase CreateMongoDBWriteContext();
    }
}
