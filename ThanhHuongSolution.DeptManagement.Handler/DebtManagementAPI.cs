using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ThanhHuongSolution.Common.Infrastrucure;
using ThanhHuongSolution.DeptManagement.Domain.Interfaces;
using ThanhHuongSolution.DeptManagement.Domain.Model;

namespace ThanhHuongSolution.DeptManagement.Handler
{
    public class DebtManagementAPI : IDebtManagementAPI
    {
        public IObjectContainer _objectContainer;

        public DebtManagementAPI(IObjectContainer objectContainer)
        {
            _objectContainer = objectContainer;
        }

        public async Task<FrameworkParamOutput<bool>> CreateDebt(FrameworkParamInput<BaseDebtModel> input)
        {
            try
            {
                var request = input.Request;

                var services = _objectContainer.Get<IDebtManagementServices>();

                var data = await services.CreateDebt(request);

                return await Task.FromResult(new FrameworkParamOutput<bool>(data));
            }
            catch (CustomException ex)
            {
                throw new CustomException(ex);
            }
        }

        public async Task<FrameworkParamOutput<bool>> CreatePaidDebt(FrameworkParamInput<BaseDebtModel> input)
        {
            try
            {
                var request = input.Request;

                var services = _objectContainer.Get<IDebtManagementServices>();

                var data = await services.CreateDebt(request);

                return await Task.FromResult(new FrameworkParamOutput<bool>(data));
            }
            catch (CustomException ex)
            {
                throw new CustomException(ex);
            }
        }

        public async Task<FrameworkParamOutput<BaseDebtModel>> GetDebtById(FrameworkParamInput<string> input)
        {
            try
            {
                var id = input.Request;

                var services = _objectContainer.Get<IDebtManagementServices>();

                var data = await services.GetDebtById(id);

                return await Task.FromResult(new FrameworkParamOutput<BaseDebtModel>(data));
            }
            catch (CustomException ex)
            {
                throw new CustomException(ex);
            }
        }

        public async Task<FrameworkParamOutput<BaseDebtModel>> GetDebtByTrackingNumber(FrameworkParamInput<string> input)
        {
            try
            {
                var trackingNumber = input.Request;

                var services = _objectContainer.Get<IDebtManagementServices>();

                var data = await services.GetDebtByTrackingNumber(trackingNumber);

                return await Task.FromResult(new FrameworkParamOutput<BaseDebtModel>(data));
            }
            catch (CustomException ex)
            {
                throw new CustomException(ex);
            }
        }

        public async Task<FrameworkParamOutput<SearchDebtResponse>> Search(FrameworkParamInput<SearchDebtRequest> input)
        {
            try
            {
                var services = _objectContainer.Get<IDebtManagementServices>();

                var request = input.Request;

                var data = await services.Search(request.CustomerId, request.Query, request.Pagination, request.DebtType);

                return await Task.FromResult(new FrameworkParamOutput<SearchDebtResponse>(data));
            }
            catch (CustomException ex)
            { 
                throw new CustomException(ex);
            }
        }
    }
}
