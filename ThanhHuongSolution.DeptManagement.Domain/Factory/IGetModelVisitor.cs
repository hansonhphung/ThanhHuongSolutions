using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ThanhHuongSolution.DeptManagement.Domain.Entity;

namespace ThanhHuongSolution.DeptManagement.Domain.Factory
{
    public interface IGetModelVisitor<T>
    {
        Task<T> Visit(MDPaidDebt mdDebt);
        Task<T> Visit(MDDebt debt);
    }
}
