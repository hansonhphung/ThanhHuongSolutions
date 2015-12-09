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
    }
}
