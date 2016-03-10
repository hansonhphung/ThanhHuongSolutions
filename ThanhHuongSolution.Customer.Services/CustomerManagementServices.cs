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

            var isExist = await IsCustomerExist(customer.Id, customer.TrackingNumber);

            Check.ThrowExceptionIfTrue(isExist, CustomerManagementResources.TRACKING_NUMBER_EXIST);

            var mdCustomer = customer.GetEntity();

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

            var result = data.Select(x => new CustomerInfo(x)).ToList();

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

            var result = data.Select(x => new CustomerInfo(x)).ToList();

            return await Task.FromResult(result);
        }

        public async Task<bool> SetVIPCustomer(string customerId, bool isVIP)
        {
            var repository = _objectContainer.Get<ICustomerRepository>();

            var existCustomer = await repository.GetCustomerById(customerId);

            Check.ThrowExceptionIfNull(existCustomer, CustomerManagementResources.CUSTOMER_NOT_EXIST);

            var data = await repository.SetVIPCustomer(customerId, isVIP);

            return await Task.FromResult(data);
        }

        public async Task<bool> UpdateCustomer(CustomerInfo customer)
        {
            var repository = _objectContainer.Get<ICustomerRepository>();

            var isExist = await IsCustomerExist(customer.Id, customer.TrackingNumber);

            Check.ThrowExceptionIfTrue(isExist, CustomerManagementResources.TRACKING_NUMBER_EXIST);

            var mdCustomer = customer.GetEntity();

            var data = await repository.UpdateCustomer(mdCustomer);

            return await Task.FromResult(data);
        }

        public async Task<bool> IsCustomerExist(string customerId, string trackingNumber)
        {
            var repository = _objectContainer.Get<ICustomerRepository>();

            var existCustomer = await repository.GetCustomerByTrackingNumber(trackingNumber);

            if (existCustomer == null || existCustomer.Id == customerId)
                return await Task.FromResult(false);

            return await Task.FromResult(true);
        }

        public async Task<bool> IsCustomerExist(string customerId)
        {
            var repository = _objectContainer.Get<ICustomerRepository>();

            var existCustomer = await repository.GetCustomerById(customerId);

            if (existCustomer == null || existCustomer.Id == customerId)
                return await Task.FromResult(false);

            return await Task.FromResult(true);
        }

        public async Task<bool> UpdateCustomerDebt(string customerId, long debtAmount)
        {
            var repository = _objectContainer.Get<ICustomerRepository>();

            var isExist = await IsCustomerExist(customerId);

            Check.ThrowExceptionIfTrue(isExist, CustomerManagementResources.CUSTOMER_EXIST);

            var result = await repository.UpdateCustomerDebt(customerId, debtAmount);

            return await Task.FromResult(result);
        }
    }
}
