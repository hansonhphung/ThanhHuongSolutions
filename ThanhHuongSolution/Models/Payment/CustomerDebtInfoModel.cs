using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ThanhHuongSolution.Models.Payment
{
    public class CustomerDebtInfoModel
    {
        public string CustomerId { get; set; }

        public string CustomerTrackingNumber { get; set; }

        public string CustomerName { get; set; }

        public long LiabilityAmount { get; set; }

        public CustomerDebtInfoModel() { }

        public CustomerDebtInfoModel(string customerId, string customerTrackingNumber, string customerName, long liabilityAmount)
        {
            CustomerId = customerId;
            CustomerTrackingNumber = customerTrackingNumber;
            CustomerName = customerName;
            LiabilityAmount = liabilityAmount;
        }
    }
}