using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ThanhHuongSolution.Product.Domain.Model;

namespace ThanhHuongSolution.Product.Domain.Interfaces
{
    public interface IProductManagementServices
    {
        Task<bool> CreateProduct(ProductInfo product);

        Task<IList<ProductInfo>> GetAllProduct();

        Task<ProductInfo> GetProductByTrackingNumber(string trackingNumber);

        Task<ProductInfo> GetProductById(string productId);

        Task<IList<ProductInfo>> Search(string query);

        Task<bool> DeleteProduct(string productId);

        Task<bool> UpdateProduct(ProductInfo product);

        Task<bool> IsProductExist(string productId, string trackingNumber);
    }
}
