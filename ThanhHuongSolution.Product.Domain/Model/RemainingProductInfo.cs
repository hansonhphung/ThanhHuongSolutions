using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ThanhHuongSolution.Product.Domain.Entity;

namespace ThanhHuongSolution.Product.Domain.Model
{
    public class RemainingProductInfo
    {
        public RemainingProductInfo() { }

        public RemainingProductInfo(string id, string trackingNumber, string name, string description, UnitType unitype, ProductType productType, long totalCost, long quantity)
        {
            Id = id;
            Name = name;
            Description = description;
            UnitType = unitype;
            ProductType = productType;
            //Gia tien cua 1 don vi tinh. Ex: gia tien 1 kg
            TotalCost = totalCost;
            Quantity = quantity;
        }

        public string Id { get; set; }
        public string TrackingNumber { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public UnitType UnitType { get; set; }
        public ProductType ProductType { get; set; }
        public long TotalCost { get; set; }
        public long Quantity { get; set; }
    }
}
