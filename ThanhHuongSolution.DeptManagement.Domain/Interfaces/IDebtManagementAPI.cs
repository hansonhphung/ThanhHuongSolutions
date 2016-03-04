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
        Task<FrameworkParamOutput<bool>> CreateBill(FrameworkParamInput<BaseDebtModel> input);

        Task<FrameworkParamOutput<BaseDebtModel>> GetBillById(FrameworkParamInput<string> input);

        Task<FrameworkParamOutput<BaseDebtModel>> GetBillByTrackingNumber(FrameworkParamInput<string> input);

        Task<FrameworkParamOutput<SearchDebtResponse>> Search(SearchDebtRequest request);
    }
}
