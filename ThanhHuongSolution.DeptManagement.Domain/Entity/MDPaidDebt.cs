using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ThanhHuongSolution.DeptManagement.Domain.Factory;

namespace ThanhHuongSolution.DeptManagement.Domain.Entity
{
    public class MDPaidDebt : MDBaseDebt
    {
        public override async Task<T> Visit<T>(IGetModelVisitor<T> visitor)
        {
            return await visitor.Visit(this);
        }
    }
}
