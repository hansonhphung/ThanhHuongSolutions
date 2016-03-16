using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ThanhHuongSolution.BillingManagement.Domain.Model;

namespace ThanhHuongSolution.BillingManagement.Domain.Factory
{
    public interface IGetEntityVisitor<T>
    {
        Task<T> Visit(BillingInfo bill);
        Task<T> Visit(ReceivingBillingInfo bill);
    }
}
