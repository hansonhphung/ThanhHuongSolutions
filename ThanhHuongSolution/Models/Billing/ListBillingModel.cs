using System;
using System.Collections.Generic;
using System.Web;
using ThanhHuongSolution.BillingManagement.Domain.Model;

namespace ThanhHuongSolution.Models.Billing
{
    public class ListBillingModel
    {
        public IList<BillingInfo> LstBilling { get; set; }

        public ListBillingModel(IList<BillingInfo> lstBilling)
        {
            LstBilling = lstBilling;
        }
    }
}
