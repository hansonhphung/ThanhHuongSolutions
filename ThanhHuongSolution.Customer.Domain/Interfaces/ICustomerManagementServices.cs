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
        Task<CustomerInfo> CreateCustomer(CustomerInfo customer);
        Task<IList<CustomerInfo>> GetAllCustomer();
    }
}
