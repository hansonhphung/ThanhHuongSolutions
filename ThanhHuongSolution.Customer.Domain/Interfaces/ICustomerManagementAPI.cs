using System.Collections.Generic;
using System.Threading.Tasks;
using ThanhHuongSolution.Common.Infrastrucure;
using ThanhHuongSolution.Customer.Domain.Model;

namespace ThanhHuongSolution.Customer.Domain.Interfaces
{
    public interface ICustomerManagementAPI
    {
        Task<FrameworkParamOutput<IList<CustomerInfo>>> GetAllCustomer();

        Task<FrameworkParamOutput<bool>> CreateCustomer(FrameworkParamInput<CustomerInfo> input);

        Task<FrameworkParamOutput<CustomerInfo>> GetCustomerById(FrameworkParamInput<string> input);

        Task<FrameworkParamOutput<CustomerInfo>> GetCustomerByTrackingNumber(FrameworkParamInput<string> input);

        Task<FrameworkParamOutput<IList<CustomerInfo>>> Search(FrameworkParamInput<string> input);

        Task<FrameworkParamOutput<bool>> UpdateCustomer(FrameworkParamInput<CustomerInfo> input);

        Task<FrameworkParamOutput<bool>> DeleteCustomer(FrameworkParamInput<string> input);

        Task<FrameworkParamOutput<bool>> SetVIPCustomer(FrameworkParamInput<CustomerVIPModel> input);

        Task<FrameworkParamOutput<bool>> UpdateCustomerDebt(FrameworkParamInput<CustomerDeptModel> input);
    }
}
