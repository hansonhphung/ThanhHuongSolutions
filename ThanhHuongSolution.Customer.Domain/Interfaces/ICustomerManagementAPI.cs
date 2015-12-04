using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ThanhHuongSolution.Common.Infrastrucure;
using ThanhHuongSolution.Customer.Domain.Model;

namespace ThanhHuongSolution.Customer.Handler
{
    public interface ICustomerManagementAPI
    {
        Task<FrameworkParamOutput<IList<CustomerInfo>>> GetAllCustomer();

        Task<FrameworkParamOutput<CustomerInfo>> CreateCustomer(FrameworkParamInput<CustomerInfo> input);
    }
}
