using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ThanhHuongSolution.Common.Infrastrucure;
using ThanhHuongSolution.Common.LocResources;
using ThanhHuongSolution.Product.Domain.Interfaces;
using ThanhHuongSolution.Product.Domain.Model;

namespace ThanhHuongSolution.Product.Handler
{
    public class ProductManagementAPI : IProductManagementAPI
    {
        public IObjectContainer _objectContainer;

        public ProductManagementAPI(IObjectContainer objectContainer)
        {
            _objectContainer = objectContainer;
        }

        public async Task<FrameworkParamOutput<bool>> CreateProduct(FrameworkParamInput<ProductInfo> input)
        {
            try
            {
                var request = input.Request;

                var services = _objectContainer.Get<IProductManagementServices>();

                var data = await services.CreateProduct(request);

                return await Task.FromResult(new FrameworkParamOutput<bool>(data));
            }
            catch (CustomException ex)
            {
                throw new CustomException(ex);
            }
        }

        public async Task<FrameworkParamOutput<IList<ProductInfo>>> GetAllProduct()
        {
            try
            {
                var services = _objectContainer.Get<IProductManagementServices>();

                var data = await services.GetAllProduct();

                return await Task.FromResult(new FrameworkParamOutput<IList<ProductInfo>>(data));
            }
            catch (CustomException ex)
            {
                throw new CustomException(ex.Message);
            }
        }

        public async Task<FrameworkParamOutput<ProductInfo>> GetProductById(FrameworkParamInput<string> input)
        {
            Check.ThrowExceptionIfIsInvalidIdType(input.Request, ProductManagementResources.ID_INVALID);

            try
            {
                var id = input.Request;

                var services = _objectContainer.Get<IProductManagementServices>();

                var data = await services.GetProductById(id);

                return await Task.FromResult(new FrameworkParamOutput<ProductInfo>(data));
            }
            catch (CustomException ex)
            {
                throw new CustomException(ex.Message);
            }
        }

        public async Task<FrameworkParamOutput<ProductInfo>> GetProductByTrackingNumber(FrameworkParamInput<string> input)
        {
            Check.ThrowExceptionIfIsInvalidIdType(input.Request, ProductManagementResources.TRACKING_NUMBER_EMPTY);

            try
            {
                var id = input.Request;

                var services = _objectContainer.Get<IProductManagementServices>();

                var data = await services.GetProductById(id);

                return await Task.FromResult(new FrameworkParamOutput<ProductInfo>(data));
            }
            catch (CustomException ex)
            {
                throw new CustomException(ex.Message);
            }
        }
    }
}
