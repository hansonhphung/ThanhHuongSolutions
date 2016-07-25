using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ThanhHuongSolution.Common.Infrastrucure;
using ThanhHuongSolution.Common.LocResources;
using ThanhHuongSolution.Customer.Domain.Interfaces;
using ThanhHuongSolution.Customer.Domain.Model;

namespace ThanhHuongSolution.Customer.Handler
{
    public class CustomerManagementAPI : ICustomerManagementAPI
    {
        public const string RETAIL_CUSTOMER_TRACKING_NUMBER = "KH-0000001";

        public IObjectContainer _objectContainer;

        public CustomerManagementAPI(IObjectContainer objectContainer)
        {
            _objectContainer = objectContainer;
        }

        public async Task<FrameworkParamOutput<bool>> CreateCustomer(FrameworkParamInput<CustomerInfo> input)
        {
            try
            {
                var request = input.Request;

                var services = _objectContainer.Get<ICustomerManagementServices>();

                var data = await services.CreateCustomer(request);

                return await Task.FromResult(new FrameworkParamOutput<bool>(data));
            }
            catch (CustomException ex)
            {
                throw new CustomException(ex.Message);
            }
        }

        public async Task<FrameworkParamOutput<bool>> DeleteCustomer(FrameworkParamInput<string> input)
        {
            try
            {
                var request = input.Request;

                var services = _objectContainer.Get<ICustomerManagementServices>();

                var data = await services.DeleteCustomer(request);

                return await Task.FromResult(new FrameworkParamOutput<bool>(data));
            }
            catch (CustomException ex)
            {
                throw new CustomException(ex.Message);
            }
        }

        public async Task<FrameworkParamOutput<IList<CustomerInfo>>> GetAllCustomer()
        {
            try
            {
                var services = _objectContainer.Get<ICustomerManagementServices>();

                var data = await services.GetAllCustomer();

                return await Task.FromResult(new FrameworkParamOutput<IList<CustomerInfo>>(data));
            }
            catch (CustomException ex)
            {
                throw new CustomException(ex.Message);
            }
        }

        public async Task<FrameworkParamOutput<CustomerInfo>> GetCustomerById(FrameworkParamInput<string> input)
        {
            Check.ThrowExceptionIfIsInvalidIdType(input.Request, CustomerManagementResources.ID_INVALID);

            try
            {
                var id = input.Request;

                var services = _objectContainer.Get<ICustomerManagementServices>();

                var data = await services.GetCustomerById(id);

                return await Task.FromResult(new FrameworkParamOutput<CustomerInfo>(data));
            }
            catch (CustomException ex)
            {
                throw new CustomException(ex.Message);
            }
        }

        public async Task<FrameworkParamOutput<CustomerInfo>> GetCustomerByTrackingNumber(FrameworkParamInput<string> input)
        {
            try
            {
                var trackingNumber = input.Request;

                var services = _objectContainer.Get<ICustomerManagementServices>();

                var data = await services.GetCustomerByTrackingNumber(trackingNumber);

                return await Task.FromResult(new FrameworkParamOutput<CustomerInfo>(data));
            }
            catch (CustomException ex)
            {
                throw new CustomException(ex.Message);
            }
        }

        public async Task<FrameworkParamOutput<IList<CustomerInfo>>> Search(FrameworkParamInput<string> input)
        {
            try
            {
                var query = input.Request;

                var services = _objectContainer.Get<ICustomerManagementServices>();

                var data = await services.Search(query);

                return await Task.FromResult(new FrameworkParamOutput<IList<CustomerInfo>>(data));
            }
            catch (CustomException ex)
            {
                throw new CustomException(ex.Message);
            }
        }

        public async Task<FrameworkParamOutput<bool>> SetVIPCustomer(FrameworkParamInput<CustomerVIPModel> input)
        {
            try
            {
                var customerInfo = input.Request;

                var services = _objectContainer.Get<ICustomerManagementServices>();

                var data = await services.SetVIPCustomer(customerInfo.CustomerId, customerInfo.IsVIP);

                return await Task.FromResult(new FrameworkParamOutput<bool>(data));
            }
            catch (CustomException ex)
            {
                throw new CustomException(ex.Message);
            }
        }

        public async Task<FrameworkParamOutput<bool>> UpdateCustomer(FrameworkParamInput<CustomerInfo> input)
        {
            try
            {
                var request = input.Request;

                var services = _objectContainer.Get<ICustomerManagementServices>();

                var data = await services.UpdateCustomer(request);

                return await Task.FromResult(new FrameworkParamOutput<bool>(data));
            }
            catch (CustomException ex)
            {
                throw new CustomException(ex.Message);
            }
        }

        public async Task<FrameworkParamOutput<bool>> UpdateCustomerDebt(FrameworkParamInput<CustomerDeptModel> input)
        {
            try
            {
                var request = input.Request;

                var services = _objectContainer.Get<ICustomerManagementServices>();

                var result = await services.UpdateCustomerDebt(request.CustomerId, request.DebtAmount, request.IsIncDebt);

                return await Task.FromResult(new FrameworkParamOutput<bool>(result));

            }
            catch (CustomException ex)
            {
                throw new CustomException(ex.Message);
            }
        }

        public async Task<FrameworkParamOutput<bool>> IsRetailCustomerCreated()
        {
            try
            {
                var services = _objectContainer.Get<ICustomerManagementServices>();

                var retailCustomer = await services.GetCustomerByTrackingNumber(RETAIL_CUSTOMER_TRACKING_NUMBER);

                if (!Check.IsNull(retailCustomer))
                    return await Task.FromResult(new FrameworkParamOutput<bool>(true));
                return await Task.FromResult(new FrameworkParamOutput<bool>(false));
            }
            catch (CustomException ex)
            {
                return await Task.FromResult(new FrameworkParamOutput<bool>(false));
            }
        }

        public async Task<FrameworkParamOutput<IList<CustomerInfo>>> SearchDebtCustomer(FrameworkParamInput<string> input)
        {
            try
            {
                var query = input.Request;

                var services = _objectContainer.Get<ICustomerManagementServices>();

                var data = await services.SearchDebtCustomer(query);

                return await Task.FromResult(new FrameworkParamOutput<IList<CustomerInfo>>(data));
            }
            catch (CustomException ex)
            {
                throw new CustomException(ex);
            }
        }
    }
}
