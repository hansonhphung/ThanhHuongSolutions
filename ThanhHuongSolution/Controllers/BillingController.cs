using System;
using System.Web;
using System.Web.Mvc;
using System.Threading.Tasks;

namespace ThanhHuongSolution.Controllers
{
    public class BillingController : Controller
    {
        public async Task<ActionResult> Index()
        {
            return RedirectToAction("List");
        }

        public ActionResult List()
        {
            return View();
        }
    }
}
