using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ThanhHuongSolution.Common.Infrastrucure;
using ThanhHuongSolution.DeptManagement.Domain.Model;

namespace ThanhHuongSolution.DeptManagement.Domain.Interfaces
{
    public interface IDebtManagementAPI
    {
        Task<FrameworkParamOutput<bool>> CreateDebt(FrameworkParamInput<BaseDebtModel> input);

        Task<FrameworkParamOutput<BaseDebtModel>> GetDebtById(FrameworkParamInput<string> input);

        Task<FrameworkParamOutput<BaseDebtModel>> GetDebtByTrackingNumber(FrameworkParamInput<string> input);

        Task<FrameworkParamOutput<SearchDebtResponse>> Search(FrameworkParamInput<SearchDebtRequest> input);
    }
}
