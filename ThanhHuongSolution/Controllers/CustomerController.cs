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

        private bool TryResize(Stream stream, int? width, int? height, out Stream output)
        {
            if (width == null && height == null)
            {
                throw new Exception("Must define width or height");
            }

            var src = Image.FromStream(stream) as Bitmap;
            if (src != null)
            {
                if (width == null)
                {
                    var ratio = (Convert.ToDouble(height.Value) / src.Height);
                    width = Convert.ToInt32(src.Width * ratio);
                }
                else
                {
                    if (height == null)
                    {
                        var ratio = (Convert.ToDouble(width.Value) / src.Width);
                        height = Convert.ToInt32(src.Height * ratio);
                    }
                }

                Bitmap _bitmap = new Bitmap(width.Value, height.Value);
                using (Graphics g = Graphics.FromImage(_bitmap))
                {
                    g.DrawImage(src, new Rectangle(0, 0, _bitmap.Width, _bitmap.Height),
                        new Rectangle(0, 0, src.Width, src.Height),
                        GraphicsUnit.Pixel);

                    var memoryStream = new MemoryStream();
                    _bitmap.Save(memoryStream, src.RawFormat);

                    memoryStream.Seek(0, 0);
                    output = memoryStream;

                    return true;
                }
            }
            else
            {
                output = new MemoryStream();
                return false;
            }
        }

        public async Task<ActionResult> SaveCustomer(FormCollection formCollection)
        {
            try
            {
                var customer = new CustomerInfo();

                customer.Id = formCollection.Get("Id");
                customer.TrackingNumber = formCollection.Get("TrackingNumber");
                customer.Name = formCollection.Get("Name");
                customer.Address = formCollection.Get("Address");
                customer.PhoneNumber = formCollection.Get("PhoneNumber");
                customer.LiabilityAmount = 0;

                var liabilityAmount = formCollection.Get("LiabilityAmount");

                if (liabilityAmount != "")
                    customer.LiabilityAmount = long.Parse(liabilityAmount);

                //get uploaded image file
                if (Request.Files["customerImage"] != null && Request.Files["customerImage"].ContentLength > 0)
                {
                    var customerImg = Request.Files["customerImage"];

                    Stream imgStream = customerImg.InputStream;

                    Stream outputStream;

                    TryResize(imgStream, 80, 80, out outputStream);

                    Image img = Image.FromStream(outputStream);

                    var extension = Path.GetExtension(customerImg.FileName).ToLower();

                    var path = string.Format("/Images/Customer/{0}{1}", customer.TrackingNumber, extension);

                    var absoulutePath = Path.Combine(string.Format(Server.MapPath("~/Images/Customer/{0}{1}"), customer.TrackingNumber, extension));

                    ImageFormat format = img.RawFormat;

                    img.Save(path, format);

                    customer.ImgURL = path;
                }

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