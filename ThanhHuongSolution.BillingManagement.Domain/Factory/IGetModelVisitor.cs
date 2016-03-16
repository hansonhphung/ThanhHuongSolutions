using System.Threading.Tasks;
using ThanhHuongSolution.BillingManagement.Domain.Entity;

namespace ThanhHuongSolution.BillingManagement.Domain.Factory
{
    public interface IGetModelVisitor<T>
    {
        Task<T> Visit(MDBill bill);
        Task<T> Visit(MDReceivingBill bill);
    }
}
