using System;
using System.Threading.Tasks;
using ThanhHuongSolution.BillingManagement.Domain.Entity;
using ThanhHuongSolution.DeptManagement.Domain.Factory;

namespace ThanhHuongSolution.DeptManagement.Domain.Model
{
    public abstract class BaseDebtModel
    {
        public string Id { get; set; }

        public string TrackingNumber { get; set; }

        public STCustomer Customer { get; set; }
        
        public long TotalAmount { get; set; }

        public DateTime CreatedAt { get; set; }

        public string DebtCreatedDate { get; set; }

        public abstract Task<T> Visit<T>(IGetEntityVisitor<T> visitor);
    }
}
