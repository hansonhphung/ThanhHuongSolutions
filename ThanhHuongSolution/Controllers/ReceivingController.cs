using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Mvc;
using ThanhHuongSolution.BillingManagement.Domain.Interface;
using ThanhHuongSolution.BillingManagement.Domain.Model;
using ThanhHuongSolution.Common.Infrastrucure;
using ThanhHuongSolution.Common.MongoDBDataAccess.Entity;
using ThanhHuongSolution.Common.MongoDBDataAccess.Interface;
using ThanhHuongSolution.Extension;
using ThanhHuongSolution.Models.Receiving;
using ThanhHuongSolution.Models.Selling;
using ThanhHuongSolution.Notification;
using ThanhHuongSolution.Product.Domain.Interfaces;

namespace ThanhHuongSolution.Controllers
{
    public class ReceivingController : Controller
    {
        public async Task<ActionResult> Index()
        {
            try
            {
                var productAPI = WebContainer.Instance.ResolveAPI<IProductManagementAPI>();

                var productData = await productAPI.GetAllProduct();

                var lstProductInfo = productData.Result.Select(x => new ProductInfoModel(x.Id, x.TrackingNumber, x.Name, x.WholesalePrice, x.RetailPrice, x.Number, x.ImgURL)).ToList();

                var data = new ReceivingInformationModel(lstProductInfo, DateTime.UtcNow.ToString("dd/MM/yyyy"));

                return View("Index", data);

            }
            catch (CustomException ex)
            {
                TempData.AddNotification(NotificationType.Failure, ex.Message);
                return View("Index");
            }
        }

        public async Task<ActionResult> CreateReceivingBill(FormCollection formCollection)
        {
            try
            {
                var billingModel = new ReceivingBillingInfo();

                if (TryUpdateModel(billingModel, formCollection))
                {
                    var trackingNumberGenerator = WebContainer.Instance.ResolveAPI<ITrackingNumberGenerator>();

                    billingModel.TrackingNumber = await trackingNumberGenerator.GenerateTrackingNumber(ObjectType.HoaDonNhapHang);

                    DateTime billCreatedDate_DT = DateTime.ParseExact(billingModel.BillCreatedDate, "dd/MM/yyyy", null, DateTimeStyles.None);

                    billingModel.BillCreatedDate_DT = billCreatedDate_DT;

                    var billingAPI = WebContainer.Instance.ResolveAPI<IBillingManagementAPI>();

                    var result = await billingAPI.CreateBill(new FrameworkParamInput<BaseBillModel>(billingModel));

                    return Json(new { isSuccess = true, data = result.Result, billingTrackingNumber = billingModel.TrackingNumber }, JsonRequestBehavior.AllowGet);
                }

                return Json(new { isSuccess = false, message = "Can not update model" }, JsonRequestBehavior.AllowGet);
            }
            catch (CustomException ex)
            {
                TempData.AddNotification(NotificationType.Failure, ex.Message);
                return Json(new { isSuccess = false, message = ex.Message }, JsonRequestBehavior.AllowGet);
            }
        }
    }
}