﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using System.Threading.Tasks;
using ThanhHuongSolution.Customer.Domain.Model;
using ThanhHuongSolution.Customer.Domain.Interfaces;
using ThanhHuongSolution.Common.Infrastrucure;
using ThanhHuongSolution.Notification;
using ThanhHuongSolution.Extension;
using ThanhHuongSolution.Security;
using ThanhHuongSolution.Models.Selling;


namespace ThanhHuongSolution.Controllers
{
    [CustomAuthorize]
    public class PaymentController : Controller
    {
        public async Task<ActionResult> Index()
        {
            try
            {
                var customerAPI = WebContainer.Instance.ResolveAPI<ICustomerManagementAPI>();

                var customerData = await customerAPI.GetAllCustomer();

                var lstCustomerInfo = customerData.Result.Select(x => new CustomerInfoModel(x.Id, x.TrackingNumber, x.Name)).ToList();

                var data = new PaymentInformationModel(lstCustomerInfo, DateTime.UtcNow.ToString("dd/MM/yyyy"));
                
                return View("Index", data);
            }
            catch (CustomException ex)
            {
                TempData.AddNotification(NotificationType.Failure, ex.Message);
                return View("Index"); // TODO refractor to change another view when no product or customer
            }
            
        }
    }
}