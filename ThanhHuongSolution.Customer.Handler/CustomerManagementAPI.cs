using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ThanhHuongSolution.Common.Infrastrucure;
using ThanhHuongSolution.Customer.Domain.Interfaces;
using ThanhHuongSolution.Customer.Domain.Model;

namespace ThanhHuongSolution.Customer.Handler
{
    public class CustomerManagementAPI : ICustomerManagementAPI
    {
        public IObjectContainer _objectContainer;

        public CustomerManagementAPI(IObjectContainer objectContainer)
        {
            _objectContainer = objectContainer;
        }

        public async Task<FrameworkParamOutput<CustomerInfo>> CreateCustomer(FrameworkParamInput<CustomerInfo> input)
        {
            try
            {
                var request = input.Request;

                var services = _objectContainer.Get<ICustomerManagementServices>();

                var data = await services.CreateCustomer(request);

                return await Task.FromResult(new FrameworkParamOutput<CustomerInfo>(data));
            }
            catch (CustomException ex)
            {
                throw new CustomException(ex);
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
                throw new CustomException(ex);
            }
        }
    }
}
