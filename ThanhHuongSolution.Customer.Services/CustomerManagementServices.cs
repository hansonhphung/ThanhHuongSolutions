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

        public async Task<bool> CreateCustomer(CustomerInfo customer)
        {
            var repository = _objectContainer.Get<ICustomerRepository>();

            var mdCustomer = customer.GetEntity();

            var oldCustomer = await repository.GetCustomerByTrackingNumber(customer.TrackingNumber);

            Check.ThrowExceptionIfNotNull(oldCustomer, CustomerManagementResources.CUSTOMER_EXIST);

            var result = await repository.CreateCustomer(mdCustomer);

            return await Task.FromResult(result);
        }

        public async Task<bool> DeleteCustomer(string customerId)
        {
            var repository = _objectContainer.Get<ICustomerRepository>();

            var result = await repository.DeleteCustomer(customerId);

            return await Task.FromResult(result);
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

        public async Task<CustomerInfo> GetCustomerById(string id)
        {
            Check.ThrowExceptionIfNullOrEmpty(id, CustomerManagementResources.CUSTOMER_ID_REQUIRED);

            var repository = _objectContainer.Get<ICustomerRepository>();

            var data = await repository.GetCustomerById(id);

            Check.ThrowExceptionIfNull(data, CustomerManagementResources.CUSTOMER_NOT_FOUND);

            return await Task.FromResult(new CustomerInfo(data));
        }

        public async Task<CustomerInfo> GetCustomerByTrackingNumber(string trackingNumber)
        {
            Check.ThrowExceptionIfNullOrEmpty(trackingNumber, CustomerManagementResources.CUSTOMER_ID_REQUIRED);

            var repository = _objectContainer.Get<ICustomerRepository>();

            var data = await repository.GetCustomerByTrackingNumber(trackingNumber);

            Check.ThrowExceptionIfNull(data, CustomerManagementResources.CUSTOMER_NOT_FOUND);

            return await Task.FromResult(new CustomerInfo(data));
        }

        public async Task<IList<CustomerInfo>> Search(string query)
        {
            var repository = _objectContainer.Get<ICustomerRepository>();

            var data = await repository.Search(query);

            var result = new List<CustomerInfo>();

            foreach (var customer in data)
            {
                result.Add(new CustomerInfo(customer));
            }

            return await Task.FromResult(result);
        }

        public async Task<bool> UpdateCustomer(CustomerInfo customer)
        {
            var repository = _objectContainer.Get<ICustomerRepository>();

            var existCustomer = await repository.GetCustomerByTrackingNumber(customer.TrackingNumber);

            Check.ThrowExceptionIfNotNull(existCustomer, CustomerManagementResources.TRACKING_NUMBER_EXIST);

            var mdCustomer = customer.GetEntity();

            var data = await repository.UpdateCustomer(mdCustomer);

            return await Task.FromResult(true);
        }
    }
}
