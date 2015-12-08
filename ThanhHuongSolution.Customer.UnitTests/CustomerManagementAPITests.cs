using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ThanhHuongSolution.Common.Infrastrucure;
using ThanhHuongSolution.Customer.Domain.Interfaces;
using ThanhHuongSolution.Customer.Domain.Model;

namespace ThanhHuongSolution.Customer.UnitTests
{
    [TestClass]
    public class CustomerManagementAPITests
    {
        private IObjectContainer _objectContainer;

        [TestInitialize]
        public void Init()
        {
            _objectContainer = UnitTestHelper.SetupKernelBootstraper();

            
        }

        [TestMethod]
        public async Task CustomerManagementAPITest()
        {
            var api = _objectContainer.Get<ICustomerManagementAPI>();

            var result = await api.CreateCustomer(new FrameworkParamInput<Domain.Model.CustomerInfo>(new CustomerInfo()));
        }
    }
}