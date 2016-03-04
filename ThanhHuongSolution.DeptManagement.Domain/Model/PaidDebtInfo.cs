using System;
using System.Threading.Tasks;
using ThanhHuongSolution.BillingManagement.Domain.Entity;
using ThanhHuongSolution.DeptManagement.Domain.Factory;

namespace ThanhHuongSolution.DeptManagement.Domain.Model
{
    public class PaidDebtInfo : BaseDebtModel
    {
        public PaidDebtInfo() { }

        public PaidDebtInfo(string id, string trackingNumber, STCustomer customer, long totalAmount, DateTime createdAt, string debtCreatedDate)
        {
            Id = id;
            TrackingNumber = trackingNumber;
            Customer = customer;
            TotalAmount = totalAmount;
            CreatedAt = createdAt;
            DebtCreatedDate = debtCreatedDate;
        }

        public override async Task<T> Visit<T>(IGetEntityVisitor<T> visitor)
        {
            return await visitor.Visit(this);
        }
    }
}
