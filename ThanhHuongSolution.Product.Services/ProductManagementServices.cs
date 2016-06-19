using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ThanhHuongSolution.BillingManagement.Domain.Interface;
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

        public async Task<bool> IsProductExist(string productId, string trackingNumber)
        {
            var repository = _objectContainer.Get<IProductManagementRepository>();

            var existProduct = await repository.GetProductByTrackingNumber(trackingNumber);

            if (existProduct == null || existProduct.Id == productId)
                return await Task.FromResult(false);

            return await Task.FromResult(true);
        }

        public async Task<bool> UpdateProduct(ProductInfo product)
        {
            Check.ThrowExceptionIfLessThanZero(product.Number, ProductManagementResources.NUMBER_LESS_THAN_ZERO);

            var repository = _objectContainer.Get<IProductManagementRepository>();

            var isExist = await IsProductExist(product.Id, product.TrackingNumber);

            Check.ThrowExceptionIfTrue(isExist, ProductManagementResources.TRACKING_NUMBER_EXIST);

            var mdProduct = product.GetEntity();

            var data = await repository.UpdateProduct(mdProduct);

            return await Task.FromResult(data);
        }

        public async Task<bool> UpdateListProductNumber(IList<UpdatedSellingProductInfo> lstProductInfo)
        {
            var repository = _objectContainer.Get<IProductManagementRepository>();

            foreach (var productInfo in lstProductInfo)
            {
                Check.ThrowExceptionIfLessThanZero(productInfo.ProductRemainingNumber, ProductManagementResources.NUMBER_LESS_THAN_ZERO);

                var data = await repository.UpdateProductNumber(productInfo);
            }

            return await Task.FromResult(true);
        }

        public async Task<IList<RemainingProductInfo>> GetAllRemainingProduct()
        {
            var productRepository = _objectContainer.Get<IProductManagementRepository>();

            var billRepository = _objectContainer.Get<IBillingManagementRepository>();

            var data = await productRepository.GetAllRemainingProduct();

            Check.ThrowExceptionIfCollectionIsNullOrZero(data, ProductManagementResources.NO_REMAINING_PRODUCT);

            var result = new List<RemainingProductInfo>();

            foreach (var product in data)
            {
                var remainingProduct = new RemainingProductInfo() {
                    Id = product.Id,
                    TrackingNumber = product.TrackingNumber,
                    Name = product.Name,
                    Description = product.Description,
                    UnitType = product.UnitType,
                    ProductType = product.ProductType,
                    Price = await billRepository.GetProductLastPrice(product.TrackingNumber),
                    Quantity = product.Number
                };

                result.Add(remainingProduct);
            }

            return await Task.FromResult(result);
        }
    }
}
