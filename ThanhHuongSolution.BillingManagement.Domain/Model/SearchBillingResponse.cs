using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ThanhHuongSolution.BillingManagement.Domain.Model
{
    public class SearchBillingResponse
    {
        public long TotalItem { get; set; }

        public long TotalCost { get; set; }

        public IList<BaseBillModel> LstBilling { get; set; }

        public SearchBillingResponse() { }

        public SearchBillingResponse(long totalItem, IList<BaseBillModel> lstBilling)
        {
            TotalItem = totalItem;
            LstBilling = lstBilling;
        }

        public SearchBillingResponse(long totalItem, long totalCost, IList<BaseBillModel> lstBilling)
        {
            TotalItem = totalItem;
            TotalCost = totalCost;
            LstBilling = lstBilling;
        }
    }
}
