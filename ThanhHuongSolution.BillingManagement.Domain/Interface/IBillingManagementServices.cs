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
        Task<bool> CreateBill(BaseBillModel bill);

        Task<BaseBillModel> GetBillById(string billId);

        Task<BaseBillModel> GetBillByTrackingNumber(string trackingNumber);

        Task<SearchBillingResponse> Search(string customerId, string query, Pagination pagination, string billType);

        Task<bool> IsCustomerHaveTransaction(string customerId);

        Task<long> GetProductLastPrice(string productTrackingNumber);
    }
}
