using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ThanhHuongSolution.Common.Infrastrucure.Model;

namespace ThanhHuongSolution.BillingManagement.Domain.Model
{
    public class SearchBillingRequest
    {
        public SearchBillingRequest() { }

        public SearchBillingRequest(string customerId, string query, Pagination pagination, string billType)
        {
            CustomerId = customerId;
            Query = query;
            Pagination = pagination;
            BillType = billType;
        }

        public string CustomerId { get; set; }

        public string Query { get; set; }

        public Pagination Pagination { get; set; }

        public string BillType { get; set; }
    }
}
