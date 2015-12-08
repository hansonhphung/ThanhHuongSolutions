using System.Collections.Generic;
using System.Threading.Tasks;
using ThanhHuongSolution.Common.Infrastrucure;
using ThanhHuongSolution.Customer.Domain.Model;

namespace ThanhHuongSolution.Customer.Domain.Interfaces
{
    public interface ICustomerManagementAPI
    {
        Task<FrameworkParamOutput<IList<CustomerInfo>>> GetAllCustomer();

        Task<FrameworkParamOutput<CustomerInfo>> CreateCustomer(FrameworkParamInput<CustomerInfo> input);
    }
}
