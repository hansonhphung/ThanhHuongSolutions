using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ThanhHuongSolution.BillingManagement.Domain.Entity;

namespace ThanhHuongSolution.BillingManagement.Domain.Interface
{
    public interface IBillingManagementRepository
    {
        Task<bool> CreateBill(MDBilling bill);

        Task<IList<MDBilling>> GetAllBill();

        Task<MDBilling> GetBillById(string billId);

        Task<MDBilling> GetBillByTrackingNumber(string trackingNumber);
    }
}
