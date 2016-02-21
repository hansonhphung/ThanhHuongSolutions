using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using ThanhHuongSolution.Common.Infrastrucure;
using ThanhHuongSolution.Common.Infrastrucure.Utilities;
using ThanhHuongSolution.Common.MongoDBDataAccess.Entity;
using ThanhHuongSolution.Common.MongoDBDataAccess.Interface;
using ThanhHuongSolution.Extension;
using ThanhHuongSolution.Models.Product;
using ThanhHuongSolution.Notification;
using ThanhHuongSolution.Product.Domain.Entity;
using ThanhHuongSolution.Product.Domain.Interfaces;
using ThanhHuongSolution.Product.Domain.Model;
using ThanhHuongSolution.Security;

namespace ThanhHuongSolution.Controllers
{
    [CustomAuthorize]
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
                TempData.AddNotification(NotificationType.Failure, ex.Message);
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
                TempData.AddNotification(NotificationType.Failure, ex.Message);
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
                TempData.AddNotification(NotificationType.Failure, ex.Message);
                return Json(new { isSuccess = false, message = ex.Message}, JsonRequestBehavior.AllowGet);
            }
        }

        public async Task<ActionResult> SaveProduct(FormCollection formCollection)
        {
            try
            {
                var product = new ProductInfo();

                product.Id = formCollection.Get("Id");
                product.TrackingNumber = formCollection.Get("TrackingNumber");
                product.Name = formCollection.Get("Name");
                product.Description = formCollection.Get("Description");
                product.UnitType = (UnitType) Enum.Parse(typeof(UnitType), formCollection.Get("UnitType"));
                product.ProductType= (ProductType) Enum.Parse(typeof(ProductType),formCollection.Get("ProductType"));
                product.WholesalePrice = long.Parse(formCollection.Get("WholesalePrice"));
                product.RetailPrice = long.Parse(formCollection.Get("RetailPrice"));
                product.Number = long.Parse(formCollection.Get("Number"));

                if (Check.IsNullOrEmpty(product.Id))
                {
                    var trackingNumberGenerator = WebContainer.Instance.ResolveAPI<ITrackingNumberGenerator>();

                    var productType = (ObjectType) Enum.Parse(typeof (ObjectType), product.ProductType.ToString());

                    var number = await trackingNumberGenerator.GenerateTrackingNumber(productType);

                    product.TrackingNumber = number;
                }

                //get uploaded image file
                if (Request.Files["productImage"] != null && Request.Files["productImage"].ContentLength > 0)
                {
                    var productImg = Request.Files["productImage"];

                    Stream imgStream = productImg.InputStream;

                    Stream outputStream;

                    ImageProcessingHelper.TryResize(imgStream, 200, 200, out outputStream);

                    Image img = Image.FromStream(outputStream);

                    var extension = Path.GetExtension(productImg.FileName).ToLower();

                    var imagesDirectory = Server.MapPath("~/Images/Product");

                    if (!Directory.Exists(imagesDirectory))
                    {
                        Directory.CreateDirectory(imagesDirectory);
                    }

                    var path = string.Format("/Images/Product/{0}{1}", product.TrackingNumber, extension);

                    var absoulutePath = Path.Combine(string.Format(Server.MapPath("~/Images/Product/{0}{1}"), product.TrackingNumber, extension));

                    if (System.IO.File.Exists(absoulutePath))
                    {
                        System.IO.File.Delete(absoulutePath);
                    }

                    img.Save(absoulutePath, ImageFormat.Png);

                    product.ImgURL = path;
                }

                var api = WebContainer.Instance.ResolveAPI<IProductManagementAPI>();

                var result = false;

                if (Check.IsNullOrEmpty(product.Id))
                {
                    var data = await api.CreateProduct(new FrameworkParamInput<ProductInfo>(product));
                    result = data.Result;
                }
                else
                {
                    var data = await api.UpdateProduct(new FrameworkParamInput<ProductInfo>(product));
                    result = data.Result;
                }

                return Json(new { isSuccess = true, data = result }, JsonRequestBehavior.AllowGet);
            }
            catch (CustomException ex)
            {
                TempData.AddNotification(NotificationType.Failure, ex.Message);
                return Json(new { isSuccess = false, message = ex.Message }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception e)
            {
                return Json(new { isSuccess = false, message = e.Message }, JsonRequestBehavior.AllowGet);
            }
        }
    }
}
