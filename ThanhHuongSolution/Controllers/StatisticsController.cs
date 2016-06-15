using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using ThanhHuongSolution.Common.Infrastrucure;
using ThanhHuongSolution.Customer.Domain.Interfaces;
using ThanhHuongSolution.Extension;
using ThanhHuongSolution.Notification;

namespace ThanhHuongSolution.Controllers
{
    public class StatisticsController : Controller
    {
        public ActionResult Index()
        {
            return View("DebtCustomerStatistics");
        }

        public async Task<ActionResult> SearchDebtCustomer(string query)
        {
            try
            {
                var api = WebContainer.Instance.ResolveAPI<ICustomerManagementAPI>();

                var data = await api.SearchDebtCustomer(new FrameworkParamInput<string>(query));

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