using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ThanhHuongSolution.Models.Billing
{
    public class BillingCustomerModel
    {
        public string CustomerId { get; set; }

        public BillingCustomerModel() { }

        public BillingCustomerModel(string customerId)
        {
            CustomerId = customerId;
        }
    }
}