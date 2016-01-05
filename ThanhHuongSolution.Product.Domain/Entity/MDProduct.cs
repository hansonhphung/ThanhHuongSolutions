using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace ThanhHuongSolution.Product.Domain.Entity
{
    public class MDProduct
    {
        public MDProduct()
        { }

        public MDProduct(string id, string trackingNumber, string name, string description, UnitType unitType, ProductType productType, long wholesalePrice, long retailPrice, long number, string imgURL)
        {
            Id = id;
            TrackingNumber = trackingNumber;
            Name = name;
            Description = description;
            UnitType = unitType;
            ProductType = productType;
            WholesalePrice = wholesalePrice;
            RetailPrice = retailPrice;
            Number = number;
            ImgURL = imgURL;
        }

        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }
        public string TrackingNumber { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public UnitType UnitType { get; set; }
        public ProductType ProductType { get; set; }
        public long WholesalePrice { get; set; }
        public long RetailPrice { get; set; }
        public long Number { get; set; }
        public string ImgURL { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
    }
}
