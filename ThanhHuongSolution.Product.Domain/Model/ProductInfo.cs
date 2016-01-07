using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ThanhHuongSolution.Product.Domain.Entity;

namespace ThanhHuongSolution.Product.Domain.Model
{
    public class ProductInfo
    {
        public ProductInfo() { }

        public ProductInfo(string trackingNumber, string name, string description, UnitType unitType, ProductType productType, long wholesalePrice, long retailPrice, long number, string imgURL)
        {
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

        public ProductInfo(MDProduct mdProduct)
        {
            Id = mdProduct.Id;
            TrackingNumber = mdProduct.TrackingNumber;
            Name = mdProduct.Name;
            Description = mdProduct.Description;
            UnitType = mdProduct.UnitType;
            ProductType = mdProduct.ProductType;
            WholesalePrice = mdProduct.WholesalePrice;
            RetailPrice = mdProduct.RetailPrice;
            Number = mdProduct.Number;
            ImgURL = mdProduct.ImgURL;
        }

        public MDProduct GetEntity()
        {
            return new MDProduct(Id,
                TrackingNumber,
                Name,
                Description,
                UnitType,
                ProductType,
                WholesalePrice,
                RetailPrice,
                Number,
                ImgURL);
        }

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
