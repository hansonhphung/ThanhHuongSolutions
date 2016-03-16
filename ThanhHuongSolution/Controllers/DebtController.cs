using System;
using System.Threading.Tasks;
using System.Web.Mvc;
using ThanhHuongSolution.Common.Infrastrucure;
using ThanhHuongSolution.Common.Infrastrucure.Model;
using ThanhHuongSolution.Common.MongoDBDataAccess.Entity;
using ThanhHuongSolution.Common.MongoDBDataAccess.Interface;
using ThanhHuongSolution.DeptManagement.Domain.Entity;
using ThanhHuongSolution.DeptManagement.Domain.Interfaces;
using ThanhHuongSolution.DeptManagement.Domain.Model;
using ThanhHuongSolution.Extension;
using ThanhHuongSolution.Notification;
using ThanhHuongSolution.Security;

namespace ThanhHuongSolution.Controllers
{
    public class DebtController : Controller
    {
        [CustomAuthorize]
        public ActionResult Index()
        {
            return View();
        }

        public async Task<ActionResult> Search(string customerId, string query, Pagination pagination, string debtType)
        {
            try
            {
                var api = WebContainer.Instance.ResolveAPI<IDebtManagementAPI>();

                var data = await api.Search(new FrameworkParamInput<SearchDebtRequest>(new SearchDebtRequest(customerId, query, pagination, debtType)));

                return Json(new { isSuccess = true, data = data.Result }, JsonRequestBehavior.AllowGet);
            }
            catch (CustomException ex)
            {
                TempData.AddNotification(NotificationType.Failure, ex.Message);
                return Json(new { isSuccess = false, message = ex.Message }, JsonRequestBehavior.AllowGet);
            }
        }

        public async Task<ActionResult> CreatePaidDebt(PaidDebtInfo debt)
        {
            try
            {
                var debtAPI = WebContainer.Instance.ResolveAPI<IDebtManagementAPI>();

                var trackingNumberGenerator = WebContainer.Instance.ResolveAPI<ITrackingNumberGenerator>();

                debt.TrackingNumber = await trackingNumberGenerator.GenerateTrackingNumber(ObjectType.PhieuTraNo);

                var result = await debtAPI.CreateDebt(new FrameworkParamInput<BaseDebtModel>(debt));

                return Json(new { isSuccess = true, message = "" }, JsonRequestBehavior.AllowGet);
            }
            catch (CustomException ex)
            {
                TempData.AddNotification(NotificationType.Failure, ex.Message);
                return Json(new { isSuccess = false, message = ex.Message }, JsonRequestBehavior.AllowGet);
            }
        }
    }
}