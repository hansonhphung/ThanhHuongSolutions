using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ThanhHuongSolution.Common.Infrastrucure.Model;
using ThanhHuongSolution.DeptManagement.Domain.Entity;

namespace ThanhHuongSolution.DeptManagement.Domain.Model
{
    public class SearchDebtRequest
    {
        public SearchDebtRequest() { }

        public SearchDebtRequest(string customerId, string query, Pagination pagination, string debtType)
        {
            CustomerId = customerId;
            Query = query;
            Pagination = pagination;
            DebtType = debtType;
        }

        public string CustomerId { get; set; }

        public string Query { get; set; }

        public Pagination Pagination { get; set; }

        public string DebtType { get; set; }
    }
}
