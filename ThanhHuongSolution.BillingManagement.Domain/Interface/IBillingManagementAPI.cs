using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ThanhHuongSolution.Common.Infrastrucure;
using ThanhHuongSolution.BillingManagement.Domain.Model;

namespace ThanhHuongSolution.BillingManagement.Domain.Interface
{
    public interface IBillingManagementAPI
    {
        Task<FrameworkParamOutput<bool>> CreateBill(FrameworkParamInput<BillingInfo> input);

        Task<FrameworkParamOutput<BillingInfo>> GetBillById(FrameworkParamInput<string> input);

        Task<FrameworkParamOutput<BillingInfo>> GetBillByTrackingNumber(FrameworkParamInput<string> input);
		
        Task<FrameworkParamOutput<SearchBillingResponse>> Search(SearchBillingRequest request);
    }
}
