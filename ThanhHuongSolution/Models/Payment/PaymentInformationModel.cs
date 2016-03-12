using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ThanhHuongSolution.Product.Domain.Model;

namespace ThanhHuongSolution.Models.Payment
{
    public class PaymentInformationModel
    {
        public IList<CustomerDebtInfoModel> LstCustomer { get; set; }

        public string CreateBillDate { get; set; }

        public PaymentInformationModel() { }

        public PaymentInformationModel(IList<CustomerDebtInfoModel> lstCustomer,  string createBillDate)
        {
            LstCustomer = lstCustomer;
            CreateBillDate = createBillDate;
        }
    }
}