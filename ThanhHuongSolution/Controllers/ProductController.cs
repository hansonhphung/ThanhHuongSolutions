using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using ThanhHuongSolution.Common.Infrastrucure;
using ThanhHuongSolution.Common.MongoDBDataAccess.Entity;
using ThanhHuongSolution.Common.MongoDBDataAccess.Interface;
using ThanhHuongSolution.Models.Product;
using ThanhHuongSolution.Product.Domain.Interfaces;

namespace ThanhHuongSolution.Controllers
{
    public class ProductController : Controller
    {
        // GET: Product
        public async Task<ActionResult> Index()
        {
            return RedirectToAction("List");
        }

        public async Task<ActionResult> List()
        {
            try
            {
                var api = WebContainer.Instance.ResolveAPI<IProductManagementAPI>();

                var data = await api.GetAllProduct();

                return View(new ListProductModel(data.Result));
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
                var api = WebContainer.Instance.ResolveAPI<IProductManagementAPI>();

                var data = await api.Search(new FrameworkParamInput<string>(query));

                return Json(new { isSuccess = true, data = data.Result}, JsonRequestBehavior.AllowGet);
            }
            catch (CustomException ex)
            {
                return Json(new { isSuccess = false, message = ex.Message }, JsonRequestBehavior.AllowGet);
            }
        }

        public async Task<ActionResult> Delete(string productId)
        {
            try
            {
                var api = WebContainer.Instance.ResolveAPI<IProductManagementAPI>();

                var data = await api.DeleteProduct(new FrameworkParamInput<string>(productId));

                return Json(new { isSuccess = true, data = data.Result}, JsonRequestBehavior.AllowGet);
            }
            catch (CustomException ex)
            {
                return Json(new { isSuccess = false, message = ex.Message}, JsonRequestBehavior.AllowGet);
            }
        }
    }
}