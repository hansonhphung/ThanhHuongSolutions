using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ThanhHuongSolution.Common.Infrastrucure.Model;
using ThanhHuongSolution.DeptManagement.Domain.Model;

namespace ThanhHuongSolution.DeptManagement.Domain.Interfaces
{
    public interface IDebtManagementServices
    {
        Task<bool> CreateDept(BaseDebtModel dept);

        Task<BaseDebtModel> GetDeptById(string deptId);

        Task<BaseDebtModel> GetDeptByTrackingNumber(string trackingNumber);

        Task<IList<BaseDebtModel>> Search(string customerId, string query, Pagination pagination);

        Task<long> Count(string customerId, string query);
    }
}
