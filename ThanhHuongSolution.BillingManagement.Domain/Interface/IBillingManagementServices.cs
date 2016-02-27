using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ThanhHuongSolution.BillingManagement.Domain.Model;
using ThanhHuongSolution.Common.Infrastrucure.Model;

namespace ThanhHuongSolution.BillingManagement.Domain.Interface
{
    public interface IBillingManagementServices
    {
        Task<bool> CreateBill(BillingInfo bill);

        Task<BillingInfo> GetBillById(string billId);

        Task<BillingInfo> GetBillByTrackingNumber(string trackingNumber);

        Task<IList<BillingInfo>> GetBillsByCustomerId(string customerId);

        Task<SearchBillingResponse> Search(string customerId, string query, Pagination pagination);
    }
}
