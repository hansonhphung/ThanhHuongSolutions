using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ThanhHuongSolution.Common.Infrastrucure;
using ThanhHuongSolution.Customer.Domain.Interfaces;
using ThanhHuongSolution.Customer.Domain.Model;
using ThanhHuongSolution.Customer.MongoDBDataAccess;

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

        [TestMethod]
        public async Task TestMethodUpdateCustomer()
        {
            var service = _objectContainer.Get<ICustomerManagementServices>();

            var customer = new CustomerInfo("KH002", "HHH", "0934641608", "123", null, 0, false, "");

            var result = await service.UpdateCustomer(customer);
        }
    }
}