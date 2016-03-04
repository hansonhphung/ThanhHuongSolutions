using System.Collections.Generic;

namespace ThanhHuongSolution.DeptManagement.Domain.Model
{
    public class SearchDebtResponse
    {
        public long TotalItem { get; set; }

        public IList<Debt> LstDebt { get; set; }

        public SearchDebtResponse() { }

        public SearchDebtResponse(long totalItem, IList<Debt> lstDebt)
        {
            TotalItem = totalItem;
            LstDebt = lstDebt;
        }
    }
}
