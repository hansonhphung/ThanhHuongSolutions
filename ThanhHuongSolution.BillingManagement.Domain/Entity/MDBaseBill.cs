using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ThanhHuongSolution.BillingManagement.Domain.Factory;

namespace ThanhHuongSolution.BillingManagement.Domain.Entity
{
    [BsonKnownTypes(typeof(MDBill), typeof(MDReceivingBill))]
    public abstract class MDBaseBill
    {
        public MDBaseBill() { }

        public MDBaseBill(string id, string trackingNumber, STCustomer customer, long totalAmount, string billCreatedDate, DateTime createdAt, List<STBillingItem> cart)
        {
            Id = id;
            TrackingNumber = trackingNumber;
            Customer = customer;
            TotalAmount = totalAmount;
            BillCreatedDate = billCreatedDate;
            CreatedAt = createdAt;
            Cart = cart;
        }

        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }

        public string TrackingNumber { get; set; }

        public STCustomer Customer { get; set; }

        public long TotalAmount { get; set; }

        public DateTime CreatedAt { get; set; }

        public string BillCreatedDate { get; set; }

        public List<STBillingItem> Cart { get; set; }

        public abstract Task<T> Visit<T>(IGetModelVisitor<T> visitor);
    }
}
