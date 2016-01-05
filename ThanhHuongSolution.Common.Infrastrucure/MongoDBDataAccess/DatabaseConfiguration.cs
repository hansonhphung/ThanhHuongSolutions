using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;

namespace ThanhHuongSolution.Common.Infrastrucure.MongoDBDataAccess
{
    public class DatabaseConfiguration
    {
        public static string MongoDbConnection
        {
            get { return ConfigurationManager.ConnectionStrings["MongoDB"].ConnectionString; }
            //get { return "mongodb://127.0.0.1:27017/ThanhHuongSolution"; }
            
        }
    }
}
