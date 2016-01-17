using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ThanhHuongSolution.Common.MongoDBDataAccess.Entity;

namespace ThanhHuongSolution.Common.MongoDBDataAccess.Interface
{
    public interface ITrackingNumberGenerator
    {
        Task<string> GenerateTrackingNumber(ObjectType objectType);
    }
}
