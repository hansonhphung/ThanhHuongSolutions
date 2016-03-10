using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using System.Threading.Tasks;
using ThanhHuongSolution.Customer.Domain.Model;
using ThanhHuongSolution.Customer.Domain.Interfaces;
using ThanhHuongSolution.Common.Infrastrucure;
using ThanhHuongSolution.Notification;
using ThanhHuongSolution.Extension;

namespace ThanhHuongSolution.Controllers
{
    public class PaymentController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }
    }
}