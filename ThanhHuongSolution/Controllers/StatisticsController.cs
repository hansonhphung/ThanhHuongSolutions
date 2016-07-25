using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using ThanhHuongSolution.BillingManagement.Domain.Interface;
using ThanhHuongSolution.BillingManagement.Domain.Model;
using ThanhHuongSolution.Common.Infrastrucure;
using ThanhHuongSolution.Common.Infrastrucure.Model;
using ThanhHuongSolution.Customer.Domain.Interfaces;
using ThanhHuongSolution.Extension;
using ThanhHuongSolution.Notification;
using ThanhHuongSolution.Product.Domain.Interfaces;

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

        public ActionResult RemainingProductStatistics() {
            return View();
        }

        public async Task<ActionResult> SearchRemainingProduct(string query)
        {
            try
            {
                var api = WebContainer.Instance.ResolveAPI<IProductManagementAPI>();

                var data = await api.GetAllRemainingProduct(new FrameworkParamInput<string>(query));

                return Json(new { isSuccess = true, data = data.Result}, JsonRequestBehavior.AllowGet);
            }
            catch (CustomException ex)
            {
                TempData.AddNotification(NotificationType.Failure, ex.Message);
                return Json(new { isSuccess = false, message = ex.Message}, JsonRequestBehavior.AllowGet);
            }
        }

        public ActionResult BillingStatistics() {
            return View();
    }

        public async Task<ActionResult> SearchBillInDateRange(string query, DateTime fromDate, DateTime toDate, Pagination pagination, string billType)
        {
            try
            {
                var api = WebContainer.Instance.ResolveAPI<IBillingManagementAPI>();

                var input = new FrameworkParamInput<StatisticsBillRequest>(new StatisticsBillRequest()
                {
                    Query = query,
                    FromDate = fromDate,
                    ToDate = toDate,
                    Pagination = pagination,
                    BillType = billType
                });

                var data = await api.GetBillInRangeDate(input);

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