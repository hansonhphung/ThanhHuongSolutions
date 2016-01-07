using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using ThanhHuongSolution.Common.Infrastrucure;
using ThanhHuongSolution.Models.Product;
using ThanhHuongSolution.Product.Domain.Interfaces;

namespace ThanhHuongSolution.Controllers
{
    public class ProductController : Controller
    {
        // GET: Product
        public ActionResult Index()
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
    }
}