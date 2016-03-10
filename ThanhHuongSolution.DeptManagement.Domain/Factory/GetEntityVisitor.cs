using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ThanhHuongSolution.DeptManagement.Domain.Entity;
using ThanhHuongSolution.DeptManagement.Domain.Model;

namespace ThanhHuongSolution.DeptManagement.Domain.Factory
{
    public class GetEntityVisitor : IGetEntityVisitor<GetEntityVisitor>
    {
        public MDBaseDebt MDBaseDebt;

        public async Task<GetEntityVisitor> Visit(PaidDebtInfo debt)
        {
            var mdPaidDebt = new MDPaidDebt()
            {
                Id = debt.Id,
                TrackingNumber = debt.TrackingNumber,
                Customer = debt.Customer,
                TotalAmount = debt.TotalAmount,
                CreatedAt = debt.CreatedAt,
                DebtCreatedDate = debt.DebtCreatedDate
            };

            MDBaseDebt = mdPaidDebt;

            return await Task.FromResult(this);
        }

        public async Task<GetEntityVisitor> Visit(DebtInfo debt)
        {
            var mdDebt = new MDDebt()
            {
                Id = debt.Id,
                TrackingNumber = debt.TrackingNumber,
                Customer = debt.Customer,
                TotalAmount = debt.TotalAmount,
                CreatedAt = debt.CreatedAt,
                DebtCreatedDate = debt.DebtCreatedDate
            };

            MDBaseDebt = mdDebt;

            return await Task.FromResult(this);
        }
    }
}
