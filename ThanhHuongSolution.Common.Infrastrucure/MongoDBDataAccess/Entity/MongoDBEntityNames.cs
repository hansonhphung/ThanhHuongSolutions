using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ThanhHuongSolution.Common.Infrastrucure.MongoDBDataAccess.Entity
{
    public class MongoDBEntityNames
    {
        public string TableName { get; set; }

        public static readonly MongoDBEntityNames CustomerCollection = new MongoDBEntityNames() { TableName = "Customers" };
        public static readonly MongoDBEntityNames ProductCollection = new MongoDBEntityNames() { TableName = "Products" };
        public static readonly MongoDBEntityNames BillingCollection = new MongoDBEntityNames() { TableName = "Billings" };
        public static readonly MongoDBEntityNames TrackingGeneratorCollection = new MongoDBEntityNames() { TableName = "TrackingNumberGenerator" };

        public override string ToString()
        {
            return TableName;
        }
    }
}
