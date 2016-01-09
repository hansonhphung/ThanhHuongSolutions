using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ThanhHuongSolution.Product.Domain.Entity;

namespace ThanhHuongSolution.Product.Domain.Interfaces
{
    public interface IProductManagementRepository
    {
        Task<bool> CreateProduct(MDProduct product);

        Task<IList<MDProduct>> GetAllProduct();

        Task<MDProduct> GetProductByTrackingNumber(string trackingNumber);

        Task<MDProduct> GetProductById(string productId);

        Task<IList<MDProduct>> Search(string query);
    }
}
