using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ThanhHuongSolution.BillingManagement.Domain.Entity;

namespace ThanhHuongSolution.DeptManagement.Domain.Model
{
    public class Dept
    {
        public Dept() { }

        public Dept(string id, string trackingNumber, STCustomer customer, long totalAmount, DateTime createdAt, string deptCreatedDate)
        {
            Id = id;
            TrackingNumber = trackingNumber;
            Customer = customer;
            TotalAmount = totalAmount;
            CreatedAt = createdAt;
            DeptCreatedDate = deptCreatedDate;
        }

        public string Id { get; set; }

        public string TrackingNumber { get; set; }

        public STCustomer Customer { get; set; }
        
        public long TotalAmount { get; set; }

        public DateTime CreatedAt { get; set; }

        public string DeptCreatedDate { get; set; }
    }
}
