using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ThanhHuongSolution.DeptManagement.Domain.Entity;
using ThanhHuongSolution.DeptManagement.Domain.Model;

namespace ThanhHuongSolution.DeptManagement.Domain.Factory
{
    public class GetModelVisitor : IGetModelVisitor<GetModelVisitor>
    {
        public BaseDebtModel Debt;

        public async Task<GetModelVisitor> Visit(MDPaidDebt mdDebt)
        {
            var debt = new DebtInfo()
            {
                Id = mdDebt.Id,
                TrackingNumber = mdDebt.TrackingNumber,
                Customer = mdDebt.Customer,
                TotalAmount = mdDebt.TotalAmount,
                CreatedAt = mdDebt.CreatedAt,
                DebtCreatedDate = mdDebt.DebtCreatedDate
            };

            Debt = debt;

            return await Task.FromResult(this);
        }

        public async Task<GetModelVisitor> Visit(MDDebt mdDebt)
        {
            var debt = new DebtInfo()
            {
                Id = mdDebt.Id,
                TrackingNumber = mdDebt.TrackingNumber,
                Customer = mdDebt.Customer,
                TotalAmount = mdDebt.TotalAmount,
                CreatedAt = mdDebt.CreatedAt,
                DebtCreatedDate = mdDebt.DebtCreatedDate
            };

            Debt = debt;

            return await Task.FromResult(this);
        }
    }
}
