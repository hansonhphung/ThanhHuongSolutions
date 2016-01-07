using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ThanhHuongSolution.Product.Domain.Model;

namespace ThanhHuongSolution.Models.Product
{
    public class ListProductModel
    {
        public IList<ProductInfo> LstProduct { get; set; }

        public ListProductModel(IList<ProductInfo> lstProduct)
        {
            LstProduct = lstProduct;
        }
    }
}