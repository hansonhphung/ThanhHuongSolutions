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
            try
            {
                var api = WebContainer.Instance.ResolveAPI<ICustomerManagementAPI>();

                var data = await api.GetAllCustomer();

                return View(new ListCustomerModel(data.Result));
            }
            catch (CustomException ex)
            {
                return View();
            }
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

        public async Task<ActionResult> Delete(string customerId)
        {
            try
            {
                var api = WebContainer.Instance.ResolveAPI<ICustomerManagementAPI>();

                var data = await api.DeleteCustomer(new FrameworkParamInput<string>(customerId));

                return Json(new { isSuccess = true, data = data.Result }, JsonRequestBehavior.AllowGet);
            }
            catch (CustomException ex)
            {
                return Json(new { isSuccess = false, message = ex.Message }, JsonRequestBehavior.AllowGet);
            }
        }

        public async Task<ActionResult> SetVIPCustomer(string customerId, bool isVIP)
        {
            try
            {
                var api = WebContainer.Instance.ResolveAPI<ICustomerManagementAPI>();

                var data = await api.SetVIPCustomer(new FrameworkParamInput<CustomerVIPModel>(new CustomerVIPModel(customerId, isVIP)));

                return Json(new { isSuccess = true, data = data.Result }, JsonRequestBehavior.AllowGet);
            }
            catch (CustomException ex)
            {
                return Json(new { isSuccess = false, message = ex.Message }, JsonRequestBehavior.AllowGet);
            }
        }

        public async Task<ActionResult> SaveCustomer(FormCollection formCollection)
        {
            try
            {
                if (Request.Files.Count == 1) //check validation and parse uploaded image
                {
                    var file = Request.Files[0];
                }

                var customer = new CustomerInfo();

                customer.Id = formCollection.Get("Id");
                customer.TrackingNumber = formCollection.Get("TrackingNumber");
                customer.Name = formCollection.Get("Name");
                customer.Address = formCollection.Get("Address");
                customer.PhoneNumber = formCollection.Get("PhoneNumber");
                customer.LiabilityAmount = long.Parse(formCollection.Get("LiabilityAmount"));

                var api = WebContainer.Instance.ResolveAPI<ICustomerManagementAPI>();

                var result = false;

                if (Check.IsNullOrEmpty(customer.Id))
                {
                    var data = await api.CreateCustomer(new FrameworkParamInput<CustomerInfo>(customer));
                    result = data.Result;
                }
                else
                {
                    var data = await api.UpdateCustomer(new FrameworkParamInput<CustomerInfo>(customer));
                    result = data.Result;
                }

                return Json(new { isSuccess = true, data = result }, JsonRequestBehavior.AllowGet);
            }
            catch (CustomException ex)
            {
                return Json(new { isSuccess = false, message = ex.Message }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception e)
            {
                return Json(new { isSuccess = false, message = e.Message }, JsonRequestBehavior.AllowGet);
            }
        }
    }
}