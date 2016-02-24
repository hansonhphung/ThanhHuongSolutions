using System;
using System.Web;
using System.Web.Mvc;
using System.Threading.Tasks;
using ThanhHuongSolution.Common.Infrastrucure;

using ThanhHuongSolution.BillingManagement.Domain.Interface;

using ThanhHuongSolution.Models.Billing;
using ThanhHuongSolution.Extension;
using ThanhHuongSolution.Notification;
using ThanhHuongSolution.Security;

namespace ThanhHuongSolution.Controllers
{
    [CustomAuthorize]
    public class BillingController : Controller
    {
        public ActionResult Index()
        {
            return RedirectToAction("List");
        }

        public async Task<ActionResult> List()
        {
            try
            {
                var api = WebContainer.Instance.ResolveAPI<IBillingManagementAPI>();

                var data = await api.GetAllBill();

                return View(new ListBillingModel(data.Result));
            }
            catch(CustomException ex)
            {
                TempData.AddNotification(NotificationType.Failure, ex.Message);
                return View();
            }
        }

        public async Task<ActionResult> Search(string query)
        {
            try
            {
                var api = WebContainer.Instance.ResolveAPI<IBillingManagementAPI>();

                var data = await api.Search(query);

                return Json(new { isSuccess = true, data = data.Result }, JsonRequestBehavior.AllowGet);
            }
            catch (CustomException ex)
            {
                TempData.AddNotification(NotificationType.Failure, ex.Message);
                return Json(new { isSuccess = false, message = ex.Message }, JsonRequestBehavior.AllowGet);
            }
        }

        public async Task<ActionResult> History()
        {
            try
            {
                string customerId = Request.QueryString["id"];

                var api = WebContainer.Instance.ResolveAPI<IBillingManagementAPI>();

                var data = await api.GetBillsByCustomerId(customerId);

                return View(new ListBillingModel(data.Result));
            }
            catch (CustomException ex)
            {
                TempData.AddNotification(NotificationType.Failure, ex.Message);
                return View();
            }
        }
    }
}
