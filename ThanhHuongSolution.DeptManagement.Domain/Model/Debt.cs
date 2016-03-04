using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ThanhHuongSolution.BillingManagement.Domain.Entity;

namespace ThanhHuongSolution.DeptManagement.Domain.Model
{
    public class Debt
    {
        public Debt() { }

        public Debt(string id, string trackingNumber, STCustomer customer, long totalAmount, DateTime createdAt, string debtCreatedDate)
        {
            Id = id;
            TrackingNumber = trackingNumber;
            Customer = customer;
            TotalAmount = totalAmount;
            CreatedAt = createdAt;
            DebtCreatedDate = debtCreatedDate;
        }

        public string Id { get; set; }

        public string TrackingNumber { get; set; }

        public STCustomer Customer { get; set; }
        
        public long TotalAmount { get; set; }

        public DateTime CreatedAt { get; set; }

        public string DebtCreatedDate { get; set; }
    }
}
