using System.Threading.Tasks;
using System.Web.Mvc;
using ThanhHuongSolution.Common.Infrastrucure;
using ThanhHuongSolution.Extension;
using ThanhHuongSolution.Notification;
using ThanhHuongSolution.Common.MongoDBDataAccess.Interface;
using ThanhHuongSolution.Common.Infrastrucure.Utilities;
using ThanhHuongSolution.Security;
using ThanhHuongSolution.Common.LocResources;

namespace ThanhHuongSolution.Controllers
{
    [AllowAnonymous]
    public class AccountController : Controller
    {
        [AllowAnonymous]
        public ActionResult Index()
        {
            if (!string.IsNullOrEmpty(SessionProvider.UserName))
                return Redirect("/Billing/Index");
            return View();
        }

        [AllowAnonymous]
        public async Task<ActionResult> Login(string username, string password)
        {
            try {
                var userManagementService = WebContainer.Instance.ResolveAPI<IUserManagementRepository>();

                var encryptPassword = HashUtility.CalculateMD5Hash(password);

                var result = await userManagementService.Login(username, encryptPassword);
                if (result)
                {
                    SessionProvider.UserName = username;
                    return RedirectToAction("Index","Billing",new { area = ""});
                }

                TempData.AddNotification(NotificationType.Failure, UserManagementResources.USERNAME_PASS_INCORECT);
                return RedirectToAction("Index");
            }
            catch (CustomException ex)
            {
                TempData.AddNotification(NotificationType.Failure, ex.Message);
                return Json(new { isSuccess = false, message = ex.Message}, JsonRequestBehavior.AllowGet);
            }
        }

        [CustomAuthorize]
        public async Task<ActionResult> Logout()
        {
            SessionProvider.UserName = null;

            return RedirectToAction("Index");
        }
    }
}