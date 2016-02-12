using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ThanhHuongSolution.BillingManagement.Domain.Model;

namespace ThanhHuongSolution.BillingManagement.Domain.Interface
{
    public interface IBillingManagementServices
    {
        Task<bool> CreateBill(BillingInfo bill);

        Task<IList<BillingInfo>> GetAllBill();

        Task<BillingInfo> GetBillById(string billId);

        Task<BillingInfo> GetBillByTrackingNumber(string trackingNumber);

        Task<IList<BillingInfo>> Search(string query);
    }
}
