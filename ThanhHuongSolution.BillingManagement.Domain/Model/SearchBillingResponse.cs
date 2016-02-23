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

        public IList<BillingInfo> LstBilling { get; set; }

        public SearchBillingResponse() { }

        public SearchBillingResponse(long totalItem, IList<BillingInfo> lstBilling)
        {
            TotalItem = totalItem;
            LstBilling = lstBilling;
        }
    }
}
