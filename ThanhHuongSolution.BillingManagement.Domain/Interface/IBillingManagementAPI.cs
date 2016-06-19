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
        Task<FrameworkParamOutput<bool>> CreateBill(FrameworkParamInput<BaseBillModel> input);

        Task<FrameworkParamOutput<BaseBillModel>> GetBillById(FrameworkParamInput<string> input);

        Task<FrameworkParamOutput<BaseBillModel>> GetBillByTrackingNumber(FrameworkParamInput<string> input);
		
        Task<FrameworkParamOutput<SearchBillingResponse>> Search(FrameworkParamInput<SearchBillingRequest> input);

        Task<FrameworkParamOutput<bool>> IsCustomerHaveTransaction(FrameworkParamInput<string> input);

        Task<FrameworkParamOutput<long>> GetProductLastPrice(FrameworkParamInput<string> input);
    }
}
