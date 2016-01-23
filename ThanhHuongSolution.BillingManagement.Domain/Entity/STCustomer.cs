using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ThanhHuongSolution.BillingManagement.Domain.Entity
{
    public class STCustomer
    {
        public string CustomerId { get; set; }

        public string CustomerTrackingNumber { get; set; }

        public string CustomerName { get; set; }

        public STCustomer() { }

        public STCustomer(string customerId, string customerTrackingNumber, string customerName) {
            CustomerId = customerId;
            CustomerTrackingNumber = customerTrackingNumber;
            CustomerName = customerName;
        }
    }
}
