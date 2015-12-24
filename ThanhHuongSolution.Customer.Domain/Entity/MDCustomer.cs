using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ThanhHuongSolution.Customer.Domain.Entity
{
    public class MDCustomer
    {
        public MDCustomer()
        { }

        public MDCustomer(string trackingNumber, string name, string phoneNumber, string address, List<string> transactionDetailIds, long liabilityAmount, bool isVIP, string imgURL)
        {
            TrackingNumber = trackingNumber;
            Name = name;
            PhoneNumber = phoneNumber;
            Address = address;
            TransactionDetailIds = transactionDetailIds;
            LiabilityAmount = liabilityAmount;
            IsVIP = isVIP;
            ImgURL = imgURL;
        }

        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }
        public string TrackingNumber { get; set; }
        public string Name { get; set; }
        public string PhoneNumber { get; set; }
        public string Address { get; set; }
        public List<string> TransactionDetailIds { get; set; }
        public long LiabilityAmount { get; set; }
        public bool IsVIP { get; set; }
        public string ImgURL { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public DateTime DeletedAt { get; set; }
    }
}
