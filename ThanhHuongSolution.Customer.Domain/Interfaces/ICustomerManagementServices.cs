using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ThanhHuongSolution.Customer.Domain.Model;

namespace ThanhHuongSolution.Customer.Domain.Interfaces
{
    public interface ICustomerManagementServices
    {
        Task<bool> CreateCustomer(CustomerInfo customer);

        Task<IList<CustomerInfo>> GetAllCustomer();

        Task<CustomerInfo> GetCustomerById(string id);

        Task<CustomerInfo> GetCustomerByTrackingNumber(string trackingNumber);

        Task<IList<CustomerInfo>> Search(string query);

        Task<bool> UpdateCustomer(CustomerInfo customer);

        Task<bool> DeleteCustomer(string customerId);

        Task<bool> SetVIPCustomer(string customerId, bool isVIP);

        Task<bool> IsCustomerExist(string customerId, string trackingNumber);

        Task<bool> UpdateCustomerDebt(string customerId, long debtAmount, bool isIncDebt);

        Task<IList<CustomerInfo>> GetAllDebtCustomer();
    }
}
