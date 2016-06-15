using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using ThanhHuongSolution.Common.Infrastrucure;
using ThanhHuongSolution.Common.Infrastrucure.Utilities;
using ThanhHuongSolution.Common.MongoDBDataAccess.Entity;
using ThanhHuongSolution.Common.MongoDBDataAccess.Interface;
using ThanhHuongSolution.Customer.Domain.Interfaces;
using ThanhHuongSolution.Customer.Domain.Model;
using ThanhHuongSolution.Models;
using ThanhHuongSolution.Models.Customer;
using ThanhHuongSolution.Extension;
using ThanhHuongSolution.Notification;
using ThanhHuongSolution.Security;
using ThanhHuongSolution.BillingManagement.Domain.Interface;

namespace ThanhHuongSolution.Controllers
{
    [CustomAuthorize]
    public class CustomerController : Controller
    {
        public const string RETAIL_CUSTOMER_TRACKING_NUMBER = "KH-0000001";
        public const string RETAIL_CUSTOMER_NAME = "Khách hàng bán lẻ";

        private async Task CreateRetailCustomer()
        {
            var api = WebContainer.Instance.ResolveAPI<ICustomerManagementAPI>();

            var retailCustomerInfo = new CustomerInfo() {
                TrackingNumber = RETAIL_CUSTOMER_TRACKING_NUMBER,
                Name = RETAIL_CUSTOMER_NAME
            };

            var result = await api.CreateCustomer(new FrameworkParamInput<CustomerInfo>(retailCustomerInfo));
        }

        // GET: Customer
        public ActionResult Index()
        {
            return View("List");
        }

        public async Task<ActionResult> List()
        {
            try
            {
                var api = WebContainer.Instance.ResolveAPI<ICustomerManagementAPI>();

                var isRetailCustomerCreated = await api.IsRetailCustomerCreated();

                if (!isRetailCustomerCreated.Result)
                    await CreateRetailCustomer();

                var data = await api.GetAllCustomer();

                return Json(new { isSuccess = true, data = data.Result }, JsonRequestBehavior.AllowGet);
            }
            catch (CustomException ex)
            {
                TempData.AddNotification(NotificationType.Failure, ex.Message);
                return View();
            }
        }

        public async Task<ActionResult> Search(string query)
        {
            try
            {
                var api = WebContainer.Instance.ResolveAPI<ICustomerManagementAPI>();

                var data = await api.Search(new FrameworkParamInput<string>(query));

                return Json(new { isSuccess = true, data = data.Result }, JsonRequestBehavior.AllowGet);
            }
            catch (CustomException ex)
            {
                TempData.AddNotification(NotificationType.Failure, ex.Message);
                return Json(new { isSuccess = false, message = ex.Message }, JsonRequestBehavior.AllowGet);
            }
        }

        public async Task<ActionResult> Delete(string customerId)
        {
            try
            {
                var api = WebContainer.Instance.ResolveAPI<ICustomerManagementAPI>();

                var billAPI = WebContainer.Instance.ResolveAPI<IBillingManagementAPI>();

                var isHaveTransaction = await billAPI.IsCustomerHaveTransaction(new FrameworkParamInput<string>(customerId));

                if (!isHaveTransaction.Result)
                {
                    var data = await api.DeleteCustomer(new FrameworkParamInput<string>(customerId));

                    return Json(new { isSuccess = true, data = data.Result }, JsonRequestBehavior.AllowGet);
                }

                return Json(new { isSuccess = true, data = false, message = Common.LocResources.CustomerManagementResources.CUSTOMER_CAN_NOT_DELETE }, JsonRequestBehavior.AllowGet);

            }
            catch (CustomException ex)
            {
                TempData.AddNotification(NotificationType.Failure, ex.Message);
                return Json(new { isSuccess = false, message = ex.Message }, JsonRequestBehavior.AllowGet);
            }
        }

        public async Task<ActionResult> SetVIPCustomer(string customerId, bool isVIP)
        {
            try
            {
                var api = WebContainer.Instance.ResolveAPI<ICustomerManagementAPI>();

                var data = await api.SetVIPCustomer(new FrameworkParamInput<CustomerVIPModel>(new CustomerVIPModel(customerId, isVIP)));

                return Json(new { isSuccess = true, data = data.Result }, JsonRequestBehavior.AllowGet);
            }
            catch (CustomException ex)
            {
                TempData.AddNotification(NotificationType.Failure, ex.Message);
                return Json(new { isSuccess = false, message = ex.Message }, JsonRequestBehavior.AllowGet);
            }
        }

        public async Task<ActionResult> SaveCustomer(FormCollection formCollection)
        {
            try
            {
                var customer = new CustomerInfo();

                customer.Id = formCollection.Get("Id");
                customer.TrackingNumber = formCollection.Get("TrackingNumber");
                customer.Name = formCollection.Get("Name");
                customer.Address = formCollection.Get("Address");
                customer.PhoneNumber = formCollection.Get("PhoneNumber");
                customer.IsVIP = Boolean.Parse(formCollection.Get("IsVIP"));
                customer.LiabilityAmount = 0;

                var liabilityAmount = formCollection.Get("LiabilityAmount");

                if (liabilityAmount != "")
                    customer.LiabilityAmount = long.Parse(liabilityAmount);

                if (Check.IsNullOrEmpty(customer.TrackingNumber))
                {
                    var trackingNumberGenerator = WebContainer.Instance.ResolveAPI<ITrackingNumberGenerator>();

                    var number = await trackingNumberGenerator.GenerateTrackingNumber(ObjectType.KhachHang);

                    customer.TrackingNumber = number;
                }

                //get uploaded image file
                if (Request.Files["customerImage"] != null && Request.Files["customerImage"].ContentLength > 0)
                {
                    var customerImg = Request.Files["customerImage"];

                    Stream imgStream = customerImg.InputStream;

                    Stream outputStream;

                    ImageProcessingHelper.TryResize(imgStream, 200, 200, out outputStream);

                    Image img = Image.FromStream(outputStream);

                    var extension = Path.GetExtension(customerImg.FileName).ToLower();

                    var imagesDirectory = Server.MapPath("~/Images/Customer");

                    if (!Directory.Exists(imagesDirectory))
                    {
                        Directory.CreateDirectory(imagesDirectory);
                    }

                    var path = string.Format("/Images/Customer/{0}{1}", customer.TrackingNumber, extension);

                    var absoulutePath = Path.Combine(string.Format(Server.MapPath("~/Images/Customer/{0}{1}"), customer.TrackingNumber, extension));

                    if (System.IO.File.Exists(absoulutePath))
                    {
                        System.IO.File.Delete(absoulutePath);
                    }

                    img.Save(absoulutePath, ImageFormat.Png);

                    customer.ImgURL = path;
                }

                var api = WebContainer.Instance.ResolveAPI<ICustomerManagementAPI>();

                var result = false;

                if (Check.IsNullOrEmpty(customer.Id))
                {
                    var data = await api.CreateCustomer(new FrameworkParamInput<CustomerInfo>(customer));
                    result = data.Result;
                }
                else
                {
                    var data = await api.UpdateCustomer(new FrameworkParamInput<CustomerInfo>(customer));
                    result = data.Result;
                }

                return Json(new { isSuccess = true, data = result }, JsonRequestBehavior.AllowGet);
            }
            catch (CustomException ex)
            {
                TempData.AddNotification(NotificationType.Failure, ex.Message);
                return Json(new { isSuccess = false, message = ex.Message }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception e)
            {
                return Json(new { isSuccess = false, message = e.Message }, JsonRequestBehavior.AllowGet);
            }
        }

        public async Task<ActionResult> UpdateCustomerDebt(FormCollection formCollection)
        {
            try
            {
                var customerDebt = new CustomerDeptModel();

                customerDebt.CustomerId = formCollection.Get("Id");
                customerDebt.DebtAmount = long.Parse(formCollection.Get("DebtAmount"));
                customerDebt.IsIncDebt = bool.Parse(formCollection.Get("IsIncDebt"));

                var api = WebContainer.Instance.ResolveAPI<ICustomerManagementAPI>();

                var data = await api.UpdateCustomerDebt(new FrameworkParamInput<CustomerDeptModel>(customerDebt));

                return Json(new { isSuccess = true, data = data.Result }, JsonRequestBehavior.AllowGet);
            }
            catch (CustomException ex)
            {
                TempData.AddNotification(NotificationType.Failure, ex.Message);
                return Json(new { isSuccess = false, message = ex.Message }, JsonRequestBehavior.AllowGet);
            }
        }
    }
}