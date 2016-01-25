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

        public async Task<FrameworkParamOutput<bool>> CreateBill(FrameworkParamInput<BillingInfo> input)
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

        public async Task<FrameworkParamOutput<IList<BillingInfo>>> GetAllBill()
        {
            try
            {
                var services = _objectContainer.Get<IBillingManagementServices>();

                var data = await services.GetAllBill();

                return await Task.FromResult(new FrameworkParamOutput<IList<BillingInfo>>(data));
            }
            catch (CustomException ex)
            {
                throw new CustomException(ex);
            }
        }

        public async Task<FrameworkParamOutput<BillingInfo>> GetBillById(FrameworkParamInput<string> input)
        {
            try
            {
                var id = input.Request;

                var services = _objectContainer.Get<IBillingManagementServices>();

                var data = await services.GetBillById(id);

                return await Task.FromResult(new FrameworkParamOutput<BillingInfo>(data));
            }
            catch (CustomException ex)
            {
                throw new CustomException(ex);
            }
        }

        public async Task<FrameworkParamOutput<BillingInfo>> GetBillByTrackingNumber(FrameworkParamInput<string> input)
        {
            try
            {
                var trackingNumber = input.Request;

                var services = _objectContainer.Get<IBillingManagementServices>();

                var data = await services.GetBillByTrackingNumber(trackingNumber);

                return await Task.FromResult(new FrameworkParamOutput<BillingInfo>(data));
            }
            catch(CustomException ex)
            {
                throw new CustomException(ex);
            }
        }
    }
}
