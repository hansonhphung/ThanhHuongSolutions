using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ThanhHuongSolution.Models.Selling
{
    public class ProductInfoModel
    {
        public ProductInfoModel() { }

        public ProductInfoModel(string id, string trackingNumber, string name, long wholesalePrice, long retailPrice, long number, string imageURL)
        {
            Id = id;
            TrackingNumber = trackingNumber;
            Name = name;
            WholesalePrice = wholesalePrice;
            RetailPrice = retailPrice;
            Number = number;
            ImageURL = imageURL;
        }

        public string Id { get; set; }
        public string TrackingNumber { get; set; }
        public string Name { get; set; }
        public long WholesalePrice { get; set; }
        public long RetailPrice { get; set; }
        public long Number { get; set; }
        public string ImageURL { get; set; }
    }
}