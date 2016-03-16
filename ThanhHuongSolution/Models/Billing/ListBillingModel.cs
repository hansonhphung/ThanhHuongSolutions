using System;
using System.Collections.Generic;
using System.Web;
using ThanhHuongSolution.BillingManagement.Domain.Model;

namespace ThanhHuongSolution.Models.Billing
{
    public class ListBillingModel
    {
        public IList<BaseBillModel> LstBilling { get; set; }

        public ListBillingModel(IList<BaseBillModel> lstBilling)
        {
            LstBilling = lstBilling;
        }
    }
}
