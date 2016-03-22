using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ThanhHuongSolution.BillingManagement.Domain.Entity;
using ThanhHuongSolution.Common.Infrastrucure.Model;

namespace ThanhHuongSolution.BillingManagement.Domain.Interface
{
    public interface IBillingManagementRepository
    {
        Task<bool> CreateBill(MDBaseBill bill);

        Task<MDBaseBill> GetBillById(string billId);

        Task<MDBaseBill> GetBillByTrackingNumber(string trackingNumber);

        Task<IList<MDBaseBill>> Search(string customerId, string query, Pagination pagination, string billType);

        Task<long> Count(string customerId, string query, string billType);

        Task<bool> IsCustomerHaveTransaction(string customerId);
    }
}
