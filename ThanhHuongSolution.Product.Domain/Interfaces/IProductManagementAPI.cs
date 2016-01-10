using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ThanhHuongSolution.Common.Infrastrucure;
using ThanhHuongSolution.Product.Domain.Model;

namespace ThanhHuongSolution.Product.Domain.Interfaces
{
    public interface IProductManagementAPI
    {
        Task<FrameworkParamOutput<bool>> CreateProduct(FrameworkParamInput<ProductInfo> input);

        Task<FrameworkParamOutput<IList<ProductInfo>>> GetAllProduct();

        Task<FrameworkParamOutput<ProductInfo>> GetProductByTrackingNumber(FrameworkParamInput<string> input);

        Task<FrameworkParamOutput<ProductInfo>> GetProductById(FrameworkParamInput<string> input);

        Task<FrameworkParamOutput<IList<ProductInfo>>> Search(FrameworkParamInput<string> input);

        Task<FrameworkParamOutput<bool>> DeleteProduct(FrameworkParamInput<string> input);
    }
}
