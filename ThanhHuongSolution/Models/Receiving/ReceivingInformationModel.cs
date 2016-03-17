using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ThanhHuongSolution.Models.Selling;

namespace ThanhHuongSolution.Models.Receiving
{
    public class ReceivingInformationModel
    {
        public IList<ProductInfoModel> LstProduct { get; set; }

        public string CreateBillDate { get; set; }

        public ReceivingInformationModel() { }

        public ReceivingInformationModel(IList<ProductInfoModel> lstProduct, string createBillDate)
        {
            LstProduct = lstProduct;
            CreateBillDate = createBillDate;
        }
    }
}