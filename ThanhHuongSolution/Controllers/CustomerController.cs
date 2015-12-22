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
using ThanhHuongSolution.Models.Customer;

namespace ThanhHuongSolution.Controllers
{
    public class CustomerController : Controller
    {
        // GET: Customer
        public async Task<ActionResult> Index()
        {

            //return RedirectToAction("List", new TestModel() { CustomerName = data.Result.Select(x => x.Id).ToList() });
            return RedirectToAction("List");
        }

        public async Task<ActionResult> List()
        {
            var api = WebContainer.Instance.ResolveAPI<ICustomerManagementAPI>();

            var data = await api.GetAllCustomer();

            return View(new ListCustomerModel(data.Result));
        }
    }
}