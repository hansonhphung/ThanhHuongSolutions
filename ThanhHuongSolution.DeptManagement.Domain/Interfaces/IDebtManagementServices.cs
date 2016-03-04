using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ThanhHuongSolution.Common.Infrastrucure.Model;
using ThanhHuongSolution.DeptManagement.Domain.Entity;

namespace ThanhHuongSolution.DeptManagement.Domain.Interfaces
{
    public interface IDebtManagementServices
    {
        Task<bool> CreateDept(MDDebt dept);

        Task<MDDebt> GetDeptById(string deptId);

        Task<MDDebt> GetDeptByTrackingNumber(string trackingNumber);

        Task<IList<MDDebt>> Search(string customerId, string query, Pagination pagination);

        Task<long> Count(string customerId, string query);
    }
}
