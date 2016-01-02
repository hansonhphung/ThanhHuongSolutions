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

            //var data = await api.GetAllCustomer();

            //return View(new ListCustomerModel(data.Result));
            return View();
        }

        public async Task<ActionResult> Search(string query)
        {
            try
            {
                var api = WebContainer.Instance.ResolveAPI<ICustomerManagementAPI>();

                var data = await api.Search(new FrameworkParamInput<string>(query));

                return Json(new { isSuccess = true, data = data.Result }, JsonRequestBehavior.AllowGet);
            }
            catch (CustomException ex)
            {
                return Json(new { isSuccess = false, message = ex.Message }, JsonRequestBehavior.AllowGet);
            }
        }
    }
}