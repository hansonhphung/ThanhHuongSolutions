using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ThanhHuongSolution.Common.Infrastrucure;
using ThanhHuongSolution.Common.LocResources;
using ThanhHuongSolution.Product.Domain.Interfaces;
using ThanhHuongSolution.Product.Domain.Model;

namespace ThanhHuongSolution.Product.Services
{
    public class ProductManagementServices : IProductManagementServices
    {
        public IObjectContainer _objectContainer;

        public ProductManagementServices(IObjectContainer objectContainer)
        {
            _objectContainer = objectContainer;
        }

        public async Task<bool> CreateProduct(ProductInfo product)
        {
            var repository = _objectContainer.Get<IProductManagementRepository>();

            var mdProduct = product.GetEntity();

            var oldProduct = await repository.GetProductByTrackingNumber(product.TrackingNumber);

            Check.ThrowExceptionIfNotNull(oldProduct, ProductManagementResources.PRODUCT_EXIST);

            var result = await repository.CreateProduct(mdProduct);

            return await Task.FromResult(result);
        }

        public async Task<IList<ProductInfo>> GetAllProduct()
        {
            var repository = _objectContainer.Get<IProductManagementRepository>();

            var data = await repository.GetAllProduct();

            Check.ThrowExceptionIfCollectionIsNullOrZero(data, ProductManagementResources.NO_PRODUCT);

            var result = data.Select(x => new ProductInfo(x)).ToList();            

            return await Task.FromResult<IList<ProductInfo>>(result);
        }

        public async Task<ProductInfo> GetProductById(string productId)
        {
            var repository = _objectContainer.Get<IProductManagementRepository>();

            var data = await repository.GetProductById(productId);

            Check.ThrowExceptionIfNull(data, ProductManagementResources.NO_PRODUCT);

            return await Task.FromResult<ProductInfo>(new ProductInfo(data));
        }

        public async Task<ProductInfo> GetProductByTrackingNumber(string trackingNumber)
        {
            var repository = _objectContainer.Get<IProductManagementRepository>();

            var data = await repository.GetProductByTrackingNumber(trackingNumber);

            Check.ThrowExceptionIfNull(data, ProductManagementResources.NO_PRODUCT);

            return await Task.FromResult<ProductInfo>(new ProductInfo(data));
        }

        public async Task<IList<ProductInfo>> Search(string query)
        {
            var repository = _objectContainer.Get<IProductManagementRepository>();

            var data = await repository.Search(query);

            var result = data.Select(x => new ProductInfo(x)).ToList();

            return await Task.FromResult(result);
        }

        public async Task<bool> DeleteProduct(string productId)
        {
            var repository = _objectContainer.Get<IProductManagementRepository>();

            var oldProduct = await repository.GetProductById(productId);

            if (oldProduct.Number > 0)
                throw new CustomException(ProductManagementResources.PRODUCT_NUMBER_GREATER_THAN_ZERO);

            var result = await repository.DeleteProduct(productId);

            return await Task.FromResult(result);
        }
    }
}
