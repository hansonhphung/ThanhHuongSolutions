using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ThanhHuongSolution.Customer.Domain.Model
{
    public class CustomerDeptModel
    {
        public CustomerDeptModel() { }

        public CustomerDeptModel(string customerId, long debtAmount)
        {
            CustomerId = customerId;
            DebtAmount = debtAmount;
        }

        public string CustomerId { get; set; }

        public long DebtAmount { get; set; }

        public bool IsIncDebt { get; set; }
    }
}
