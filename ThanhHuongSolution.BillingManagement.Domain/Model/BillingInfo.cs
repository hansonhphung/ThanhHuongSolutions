using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ThanhHuongSolution.BillingManagement.Domain.Entity;

namespace ThanhHuongSolution.BillingManagement.Domain.Model
{
    public class BillingInfo
    {
        public string Id { get; set; }
        
        public string TrackingNumber { get; set; }

        public STCustomer Customer { get; set; }

        public long TotalAmount { get; set; }
        
        public DateTime CreatedAt { get; set; }

        public List<STBillingItem> Cart { get; set; }

        public BillingInfo() { }

        public BillingInfo(string id, string trackingNumber, STCustomer customer, long totalAmount, DateTime createdAt, List<STBillingItem> cart)
        {
            Id = id;
            TrackingNumber = trackingNumber;
            Customer = customer;
            TotalAmount = totalAmount;
            CreatedAt = createdAt;
            Cart = cart;
        }

        public BillingInfo(MDBilling billing)
        {
            Id = billing.Id;
            TrackingNumber = billing.TrackingNumber;
            Customer = billing.Customer;
            TotalAmount = billing.TotalAmount;
            CreatedAt = billing.CreatedAt;
            Cart = billing.Cart;
        }

        public MDBilling GetEntity() {
            return new MDBilling(
                Id, 
                TrackingNumber, 
                Customer, 
                TotalAmount, 
                CreatedAt, 
                Cart);
        }
    }
}
