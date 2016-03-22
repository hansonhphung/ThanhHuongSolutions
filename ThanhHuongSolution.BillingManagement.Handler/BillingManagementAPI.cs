using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ThanhHuongSolution.Common.Infrastrucure;
using ThanhHuongSolution.Common.LocResources;
using ThanhHuongSolution.BillingManagement.Domain.Interface;
using ThanhHuongSolution.BillingManagement.Domain.Model;

namespace ThanhHuongSolution.BillingManagement.Handler
{
    public class BillingManagementAPI : IBillingManagementAPI
    {
        public IObjectContainer _objectContainer;

        public BillingManagementAPI(IObjectContainer objectContainer)
        {
            _objectContainer = objectContainer;
        }

        public async Task<FrameworkParamOutput<bool>> CreateBill(FrameworkParamInput<BaseBillModel> input)
        {
            try
            {
                var request = input.Request;

                var services = _objectContainer.Get<IBillingManagementServices>();

                var data = await services.CreateBill(request);

                return await Task.FromResult(new FrameworkParamOutput<bool>(data));
            }
            catch (CustomException ex)
            {
                throw new CustomException(ex);
            }
        }

        public async Task<FrameworkParamOutput<BaseBillModel>> GetBillById(FrameworkParamInput<string> input)
        {
            try
            {
                var id = input.Request;

                var services = _objectContainer.Get<IBillingManagementServices>();

                var data = await services.GetBillById(id);

                return await Task.FromResult(new FrameworkParamOutput<BaseBillModel>(data));
            }
            catch (CustomException ex)
            {
                throw new CustomException(ex);
            }
        }

        public async Task<FrameworkParamOutput<BaseBillModel>> GetBillByTrackingNumber(FrameworkParamInput<string> input)
        {
            try
            {
                var trackingNumber = input.Request;

                var services = _objectContainer.Get<IBillingManagementServices>();

                var data = await services.GetBillByTrackingNumber(trackingNumber);

                return await Task.FromResult(new FrameworkParamOutput<BaseBillModel>(data));
            }
            catch(CustomException ex)
            {
                throw new CustomException(ex);
            }
        }

        public async Task<FrameworkParamOutput<SearchBillingResponse>> Search(FrameworkParamInput<SearchBillingRequest> input)
        {
            try
            {
                var services = _objectContainer.Get<IBillingManagementServices>();

                var request = input.Request;

                var data = await services.Search(request.CustomerId, request.Query, request.Pagination, request.BillType);

                return await Task.FromResult(new FrameworkParamOutput<SearchBillingResponse>(data));
            } 
            catch(CustomException ex)
            {
                throw new CustomException(ex);
            }
        }

        public async Task<FrameworkParamOutput<bool>> IsCustomerHaveTransaction(FrameworkParamInput<string> input)
        {
            try
            {
                var services = _objectContainer.Get<IBillingManagementServices>();

                var request = input.Request;

                var data = await services.IsCustomerHaveTransaction(request);

                return await Task.FromResult(new FrameworkParamOutput<bool>(data));
            }
            catch (CustomException ex)
            {
                throw new CustomException(ex);
            }
        }
    }
}
