using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ThanhHuongSolution.Common.Infrastrucure;
using ThanhHuongSolution.Customer.MongoDBDataAccess;

namespace ThanhHuongSolution.Customer.Services
{
    public class CustomerManagementServices
    {
        public IObjectContainer _objectContainer;

        public CustomerManagementServices(IObjectContainer objectContainer)
        {
            _objectContainer = objectContainer;
        }
    }
}
