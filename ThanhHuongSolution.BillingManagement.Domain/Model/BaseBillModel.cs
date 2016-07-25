using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ThanhHuongSolution.BillingManagement.Domain.Entity;
using ThanhHuongSolution.BillingManagement.Domain.Factory;

namespace ThanhHuongSolution.BillingManagement.Domain.Model
{
    public abstract class BaseBillModel
    {
        public string Id { get; set; }
        
        public string TrackingNumber { get; set; }

        public STCustomer Customer { get; set; }

        public long TotalAmount { get; set; }
        
        public DateTime CreatedAt { get; set; }

        public string BillCreatedDate { get; set; }

        //Bill created date with datetime data type
        public DateTime BillCreatedDate_DT { get; set; }

        public List<STBillingItem> Cart { get; set; }

        public BaseBillModel() { }

        public BaseBillModel(string id, string trackingNumber, STCustomer customer, long totalAmount, string billCreatedDate, DateTime createdAt, List<STBillingItem> cart)
        {
            Id = id;
            TrackingNumber = trackingNumber;
            Customer = customer;
            TotalAmount = totalAmount;
            BillCreatedDate = billCreatedDate;
            CreatedAt = createdAt;
            Cart = cart;
        }

        public abstract Task<T> Visit<T>(IGetEntityVisitor<T> visitor);
    }
}
