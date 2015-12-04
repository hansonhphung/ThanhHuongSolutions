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

        #region Customer
        public static readonly MongoDBEntityNames CustomerCollection = new MongoDBEntityNames() { TableName = "Customers" };
        #endregion

        public override string ToString()
        {
            return TableName;
        }
    }
}
