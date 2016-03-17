using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Mvc;
using ThanhHuongSolution.Common.Infrastrucure;
using ThanhHuongSolution.Extension;
using ThanhHuongSolution.Models.Receiving;
using ThanhHuongSolution.Models.Selling;
using ThanhHuongSolution.Notification;
using ThanhHuongSolution.Product.Domain.Interfaces;

namespace ThanhHuongSolution.Controllers
{
    public class ReceivingController : Controller
    {
        public async Task<ActionResult> Index()
        {
            try
            {
                var productAPI = WebContainer.Instance.ResolveAPI<IProductManagementAPI>();

                var productData = await productAPI.GetAllProduct();

                var lstProductInfo = productData.Result.Select(x => new ProductInfoModel(x.Id, x.TrackingNumber, x.Name, x.WholesalePrice, x.RetailPrice, x.Number)).ToList();

                var data = new ReceivingInformationModel(lstProductInfo, DateTime.UtcNow.ToString("dd/MM/yyyy"));

                return View("Index", data);

            }
            catch (CustomException ex)
            {
                TempData.AddNotification(NotificationType.Failure, ex.Message);
                return View("Index");
            }
        }
    }
}