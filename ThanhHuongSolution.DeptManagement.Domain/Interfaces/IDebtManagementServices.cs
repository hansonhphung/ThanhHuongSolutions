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
    public interface IDebtManagementServices
    {
        Task<bool> CreateDebt(BaseDebtModel debt);

        Task<bool> CreatePaidDebt(BaseDebtModel paidDebt);

        Task<BaseDebtModel> GetDebtById(string debtId);

        Task<BaseDebtModel> GetDebtByTrackingNumber(string trackingNumber);

        Task<SearchDebtResponse> Search(string customerId, string query, Pagination pagination, string debtType);
    }
}
