using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ThanhHuongSolution.Product.Domain.Model;

namespace ThanhHuongSolution.Models.Selling
{
    public class PaymentInformationModel
    {
        public IList<CustomerInfoModel> LstCustomer { get; set; }

        public string CreateBillDate { get; set; }

        public PaymentInformationModel() { }

        public PaymentInformationModel(IList<CustomerInfoModel> lstCustomer, string createBillDate)
        {
            LstCustomer = lstCustomer;
            CreateBillDate = createBillDate;
        }
    }
}