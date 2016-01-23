using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
 
namespace ThanhHuongSolution.BillingManagement.Domain.Entity
{
    public class MDBilling
    {
        public MDBilling() { }

        public MDBilling(string id, string trackingNumber, STCustomer customer, long totalAmount, DateTime createdAt, List<STBillingItem> cart)
        {
            Id = id;
            TrackingNumber = trackingNumber;
            Customer = customer;
            TotalAmount = totalAmount;
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

        public List<STBillingItem> Cart { get; set; }
    }
}
