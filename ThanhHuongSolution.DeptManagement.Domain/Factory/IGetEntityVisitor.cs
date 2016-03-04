using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ThanhHuongSolution.DeptManagement.Domain.Model;

namespace ThanhHuongSolution.DeptManagement.Domain.Factory
{
    public interface IGetEntityVisitor<T>
    {
        Task<T> Visit(DebtInfo debt);
        Task<T> Visit(PaidDebtInfo debt);
    }
}
