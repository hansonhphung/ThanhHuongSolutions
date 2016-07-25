using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ThanhHuongSolution.BillingManagement.Domain.Entity;
using ThanhHuongSolution.BillingManagement.Domain.Factory;

namespace ThanhHuongSolution.BillingManagement.Domain.Model
{
    public class BillingInfo : BaseBillModel
    {
        public BillingInfo() { }

        public BillingInfo(string id, string trackingNumber, STCustomer customer, long totalAmount, string billCreatedDate, DateTime billCreatedDate_DT, DateTime createdAt, List<STBillingItem> cart)
        {
            Id = id;
            TrackingNumber = trackingNumber;
            Customer = customer;
            TotalAmount = totalAmount;
            BillCreatedDate = billCreatedDate;
            BillCreatedDate_DT = billCreatedDate_DT;
            CreatedAt = createdAt;
            Cart = cart;
        }

        public override async Task<T> Visit<T>(IGetEntityVisitor<T> visitor)
        {
            return await visitor.Visit(this);
        }
    }
}
