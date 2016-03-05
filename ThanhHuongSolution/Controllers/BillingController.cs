using System.Web.Mvc;
using System.Threading.Tasks;
using ThanhHuongSolution.Common.Infrastrucure;
using ThanhHuongSolution.BillingManagement.Domain.Interface;
using ThanhHuongSolution.BillingManagement.Domain.Model;
using ThanhHuongSolution.Extension;
using ThanhHuongSolution.Notification;
using ThanhHuongSolution.Security;
using ThanhHuongSolution.Common.Infrastrucure.Model;
using ThanhHuongSolution.Models.Billing;

namespace ThanhHuongSolution.Controllers
{
    [CustomAuthorize]
    public class BillingController : Controller
    {
        public ActionResult Index()
        {
            return RedirectToAction("List");
        }

        public ActionResult List(string id)
        {
            if (Check.IsNullOrEmpty(id))
                id = "";
            return View(new BillingCustomerModel(id));
        }

        public async Task<ActionResult> Search(string customerId, string query, Pagination pagination)
        {
            try
            {
                var api = WebContainer.Instance.ResolveAPI<IBillingManagementAPI>();

                var data = await api.Search(new FrameworkParamInput<SearchBillingRequest>(new SearchBillingRequest(customerId, query, pagination)));

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
