using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ThanhHuongSolution.Common.Infrastrucure;
using ThanhHuongSolution.Customer.Domain.Interfaces;
using ThanhHuongSolution.Customer.Domain.Model;
using ThanhHuongSolution.Customer.MongoDBDataAccess;
using ThanhHuongSolution.Common.LocResources;

namespace ThanhHuongSolution.Customer.Services
{
    public class CustomerManagementServices : ICustomerManagementServices
    {
        public IObjectContainer _objectContainer;

        public CustomerManagementServices(IObjectContainer objectContainer)
        {
            _objectContainer = objectContainer;
        }

        public async Task<CustomerInfo> CreateCustomer(CustomerInfo customer)
        {
            var repository = _objectContainer.Get<ICustomerRepository>();

            var mdCustomer = customer.GetEntity();

            var result = await repository.CreateCustomer(mdCustomer);

            return await Task.FromResult<CustomerInfo>(new CustomerInfo(result));
        }

        public async Task<IList<CustomerInfo>> GetAllCustomer()
        {
            var repository = _objectContainer.Get<ICustomerRepository>();

            var data = await repository.GetAllCustomer();

            Check.ThrowExceptionIfCollectionIsNullOrZero(data, CustomerManagementResources.NO_CUSTOMER);

            var result = new List<CustomerInfo>();

            foreach (var customer in data)
            {
                result.Add(new CustomerInfo(customer));
            }

            return await Task.FromResult<IList<CustomerInfo>>(result);
        }
    }
}
