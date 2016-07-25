using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ThanhHuongSolution.Product.Domain.Model;

namespace ThanhHuongSolution.Models.Selling
{
    public class SellingInformationModel
    {
        public IList<CustomerInfoModel> LstCustomer { get; set; }

        public IList<ProductInfoModel> LstProduct { get; set; }

        public SellingInformationModel() { }

        public SellingInformationModel(IList<CustomerInfoModel> lstCustomer, IList<ProductInfoModel> lstProduct)
        {
            LstCustomer = lstCustomer;
            LstProduct = lstProduct;
        }
    }
}