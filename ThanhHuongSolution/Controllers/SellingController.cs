using System;
using System.Web.Mvc;
using System.Threading.Tasks;
using ThanhHuongSolution.Common.Infrastrucure;
using ThanhHuongSolution.Customer.Domain.Interfaces;
using ThanhHuongSolution.Product.Domain.Interfaces;
using ThanhHuongSolution.Extension;
using ThanhHuongSolution.Notification;
using ThanhHuongSolution.Models.Selling;
using ThanhHuongSolution.Customer.Domain.Model;
using System.Collections.Generic;
using System.Linq;

namespace ThanhHuongSolution.Controllers
{
    public class SellingController : Controller
    {
        public async Task<ActionResult> Index()
        {
            try
            {
                var customerAPI = WebContainer.Instance.ResolveAPI<ICustomerManagementAPI>();
                var productAPI = WebContainer.Instance.ResolveAPI<IProductManagementAPI>();

                var customerData = await customerAPI.GetAllCustomer();

                var lstCustomerInfo = customerData.Result.Select(x => new CustomerInfoModel(x.Id, x.TrackingNumber, x.Name)).ToList();

                var productData = await productAPI.GetAllProduct();

                var lstProductInfo = productData.Result.Select(x => new ProductInfoModel(x.Id, x.TrackingNumber, x.Name, x.WholesalePrice, x.RetailPrice, x.Number)).ToList();

                var data = new SellingInformationModel(lstCustomerInfo, lstProductInfo);

                return View("Selling", data);

            }
            catch (CustomException ex)
            {
                TempData.AddNotification(NotificationType.Failure, ex.Message);
                return View("Selling"); // TODO refractor to change another view when no product or customer
            }
        }
    }
}
