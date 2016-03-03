using System.Web.Mvc;
using ThanhHuongSolution.Security;

namespace ThanhHuongSolution.Controllers
{
    public class DeptController : Controller
    {
        [CustomAuthorize]
        public ActionResult Index()
        {
            return View();
        }
    }
}