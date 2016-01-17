using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ThanhHuongSolution.Common.Infrastrucure;

namespace ThanhHuongSolution.Common.MongoDBDataAccess.CompositionRoot
{
    public class Bootstraper
    {
        public static void Load(IObjectContainer objectContainer)
        {
            DIRegister.RegisterMongoDBDataAccessService(objectContainer);
        }
    }
}
