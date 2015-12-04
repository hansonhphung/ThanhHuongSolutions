using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ThanhHuongSolution.Customer.Domain.Entity;

namespace ThanhHuongSolution.Customer.Domain.Model
{
    public class CustomerInfo
    {
        public CustomerInfo() { }

        public CustomerInfo(string trackingNumber, string name, string phoneNumber, string address, List<string> transactionDetailIds, long liabilityAmount)
        {
            TrackingNumber = trackingNumber;
            Name = name;
            PhoneNumber = phoneNumber;
            Address = address;
            TransactionDetailIds = transactionDetailIds;
            LiabilityAmount = liabilityAmount;
        }

        public CustomerInfo(MDCustomer mdCustomer)
        {
            Id = mdCustomer.Id;
            TrackingNumber = mdCustomer.TrackingNumber;
            Name = mdCustomer.Name;
            PhoneNumber = mdCustomer.PhoneNumber;
            Address = mdCustomer.Address;
            TransactionDetailIds = mdCustomer.TransactionDetailIds;
            LiabilityAmount = mdCustomer.LiabilityAmount;
        }

        public MDCustomer GetEntity()
        {
            return new 
                MDCustomer(TrackingNumber, 
                Name, 
                PhoneNumber, 
                Address, 
                TransactionDetailIds, 
                LiabilityAmount);
        }

        public string Id { get; set; }
        public string TrackingNumber { get; set; }
        public string Name { get; set; }
        public string PhoneNumber { get; set; }
        public string Address { get; set; }
        public List<string> TransactionDetailIds { get; set; }
        public long LiabilityAmount { get; set; }
    }
}
