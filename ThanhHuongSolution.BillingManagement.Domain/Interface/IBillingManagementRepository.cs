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
        Task<bool> CreateBill(MDBilling bill);

        Task<MDBilling> GetBillById(string billId);

        Task<MDBilling> GetBillByTrackingNumber(string trackingNumber);

        Task<IList<MDBilling>> Search(string customerId, string query, Pagination pagination);

        Task<long> Count(string customerId, string query);
    }
}
