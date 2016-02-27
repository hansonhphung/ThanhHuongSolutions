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
using Newtonsoft.Json;
using System.Web.Script.Serialization;
using ThanhHuongSolution.BillingManagement.Domain.Model;
using ThanhHuongSolution.Common.MongoDBDataAccess.Interface;
using ThanhHuongSolution.Common.MongoDBDataAccess.Entity;
using ThanhHuongSolution.BillingManagement.Domain.Interface;
using ThanhHuongSolution.Security;
using ThanhHuongSolution.Product.Domain.Model;

namespace ThanhHuongSolution.Controllers
{
    [CustomAuthorize]
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

                var data = new SellingInformationModel(lstCustomerInfo, lstProductInfo, DateTime.UtcNow.ToString("dd/MM/yyyy"));

                return View("Selling", data);

            }
            catch (CustomException ex)
            {
                TempData.AddNotification(NotificationType.Failure, ex.Message);
                return View("Selling"); // TODO refractor to change another view when no product or customer
            }
        }

        public async Task<ActionResult> CreateBilling(FormCollection formCollection)
        {
            try
            {
                var billingModel = new BillingInfo();

                if (TryUpdateModel(billingModel, formCollection))
                {
                    var trackingNumberGenerator = WebContainer.Instance.ResolveAPI<ITrackingNumberGenerator>();

                    billingModel.TrackingNumber = await trackingNumberGenerator.GenerateTrackingNumber(ObjectType.HoaDon);

                    billingModel.BillCreatedDate = DateTime.UtcNow.ToString("dd/MM/yyyy");

                    var billingAPI = WebContainer.Instance.ResolveAPI<IBillingManagementAPI>();

                    var result = await billingAPI.CreateBill(new FrameworkParamInput<BillingInfo>(billingModel));

                    return Json(new { isSuccess = true, data = result.Result }, JsonRequestBehavior.AllowGet);
                }

                return Json(new { isSuccess = false, message = "Can not update model" }, JsonRequestBehavior.AllowGet);
            }
            catch (CustomException ex)
            {
                TempData.AddNotification(NotificationType.Failure, ex.Message);
                return Json(new { isSuccess = false, message = ex.Message}, JsonRequestBehavior.AllowGet);
            }
        }

        public async Task<ActionResult> UpdateListSellingProduct(IList<UpdatedSellingProductInfo> lstProductInfo)
        {
            try
            {
                var productAPI = WebContainer.Instance.ResolveAPI<IProductManagementAPI>();

                var result = await productAPI.UpdateListProductNumber(new FrameworkParamInput<IList<UpdatedSellingProductInfo>>(lstProductInfo));

                return Json(new { isSuccess = true, message = ""}, JsonRequestBehavior.AllowGet);
            }
            catch (CustomException ex)
            {
                TempData.AddNotification(NotificationType.Failure, ex.Message);
                return Json(new { isSuccess = false, message = ex.Message}, JsonRequestBehavior.AllowGet);
            }
        }
    }
}
