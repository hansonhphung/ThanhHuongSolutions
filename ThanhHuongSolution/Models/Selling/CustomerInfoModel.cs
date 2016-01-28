using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ThanhHuongSolution.Models.Selling
{
    public class CustomerInfoModel
    {
        public string CustomerId { get; set; }

        public string CustomerTrackingNumber { get; set; }

        public string CustomerName { get; set; }

        public CustomerInfoModel() { }

        public CustomerInfoModel(string customerId, string customerTrackingNumber, string customerName)
        {
            CustomerId = customerId;
            CustomerTrackingNumber = customerTrackingNumber;
            CustomerName = customerName;
        }
    }
}