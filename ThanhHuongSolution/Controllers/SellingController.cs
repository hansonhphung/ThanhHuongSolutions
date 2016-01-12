using System;
using System.Web.Mvc;
using System.Threading.Tasks;

namespace ThanhHuongSolution.Controllers
{
    public class SellingController : Controller
    {
        public ActionResult Index()
        {
            return RedirectToAction("Selling");
        }

        public ActionResult Selling()
        {
            return View();
        }
    }
}
