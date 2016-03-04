using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Threading.Tasks;
using ThanhHuongSolution.BillingManagement.Domain.Entity;
using ThanhHuongSolution.DeptManagement.Domain.Factory;

namespace ThanhHuongSolution.DeptManagement.Domain.Entity
{
    [BsonKnownTypes(typeof(MDDebt), typeof(MDPaidDebt))]
    public abstract class MDBaseDebt
    {
        public MDBaseDebt() { }

        public MDBaseDebt(string id, string trackingNumber, STCustomer customer, long totalAmount, DateTime createdAt, string debtCreatedDate)
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

        public abstract Task<T> Visit<T>(IGetModelVisitor<T> visitor);
    }
}
