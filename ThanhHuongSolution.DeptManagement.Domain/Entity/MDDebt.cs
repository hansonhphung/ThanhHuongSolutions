using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ThanhHuongSolution.BillingManagement.Domain.Entity;

namespace ThanhHuongSolution.DeptManagement.Domain.Entity
{
    public class MDDebt
    {
        public MDDebt() { }

        public MDDebt(string id, string trackingNumber, STCustomer customer, long totalAmount, DateTime createdAt, string debtCreatedDate)
        {
            Id = id;
            TrackingNumber = trackingNumber;
            Customer = customer;
            TotalAmount = totalAmount;
            CreatedAt = createdAt;
            DebtCreatedDate = debtCreatedDate;
        }

        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }

        public string TrackingNumber { get; set; }

        public STCustomer Customer { get; set; }

        public long TotalAmount { get; set; }

        public DateTime CreatedAt { get; set; }

        public string DebtCreatedDate { get; set; }
    }
}
