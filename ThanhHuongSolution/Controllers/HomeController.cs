using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using ThanhHuongSolution.Common.Infrastrucure;
using ThanhHuongSolution.Customer.Domain.Interfaces;
using ThanhHuongSolution.Customer.Domain.Model;
using ThanhHuongSolution.Models;

namespace ThanhHuongSolution.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            //return View();
            return RedirectToAction("Index", "Customer");
        }

        public async Task<ActionResult> About()
        {
            ViewBag.Message = "Your application description page.";

            var api = WebContainer.Instance.ResolveAPI<ICustomerManagementAPI>();

            var samplePerson = new CustomerInfo();

            //var resut = await api.CreateCustomer(new FrameworkParamInput<CustomerInfo>(samplePerson));

            var tmp = await api.CreateCustomer(new FrameworkParamInput<CustomerInfo>(samplePerson));
            
            var data = await api.GetAllCustomer();

            return View(new TestModel() { CustomerName = data.Result.Select(x => x.Id).ToList() });
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}