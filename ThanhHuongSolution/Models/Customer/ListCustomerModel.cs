using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ThanhHuongSolution.Customer.Domain.Model;

namespace ThanhHuongSolution.Models.Customer
{
    public class ListCustomerModel
    {
        public IList<CustomerInfo> LstCustomer { get; set; }

        public ListCustomerModel(IList<CustomerInfo> lstCustomer)
        {
            LstCustomer = lstCustomer;
        }
    }
}