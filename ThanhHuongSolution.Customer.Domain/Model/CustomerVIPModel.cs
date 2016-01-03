using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ThanhHuongSolution.Customer.Domain.Model
{
    public class CustomerVIPModel
    {
        public CustomerVIPModel() { }

        public CustomerVIPModel(string customerId, bool isVIP) {
            CustomerId = customerId;
            IsVIP = isVIP;
        }

        public string CustomerId { get; set; }

        public bool IsVIP { get; set; }
    }
}
