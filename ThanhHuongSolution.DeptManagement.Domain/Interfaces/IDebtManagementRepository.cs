using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ThanhHuongSolution.Common.Infrastrucure.Model;
using ThanhHuongSolution.DeptManagement.Domain.Entity;
using ThanhHuongSolution.DeptManagement.Domain.Model;

namespace ThanhHuongSolution.DeptManagement.Domain.Interfaces
{
    public interface IDebtManagementRepository
    {
        Task<bool> CreateDept(MDBaseDebt dept);

        Task<MDBaseDebt> GetDeptById(string deptId);

        Task<MDBaseDebt> GetDeptByTrackingNumber(string trackingNumber);

        Task<IList<MDBaseDebt>> Search(string customerId, string query, Pagination pagination);

        Task<long> Count(string customerId, string query);
    }
}
