using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ThanhHuongSolution.BillingManagement.Domain.Entity
{
    public class STBillingItem
    {
        public string ProductTrackingNumber { get; set;}

        public string ProductName { get; set; }

        // Total price, equal to Number * price per item
        // So if want to calculae price for per item, use Price / Number
        public long Price { get; set; }

        public long Number { get; set; }

        public STBillingItem() { }

        public STBillingItem(string productTrackingNumber, string productName, long price, long number)
        {
            ProductTrackingNumber = productTrackingNumber;
            ProductName = productName;
            Price = price;
            Number = number;
        }
    }
}
