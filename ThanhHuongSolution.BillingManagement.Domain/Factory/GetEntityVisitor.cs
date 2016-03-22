using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ThanhHuongSolution.BillingManagement.Domain.Entity;
using ThanhHuongSolution.BillingManagement.Domain.Interface;
using ThanhHuongSolution.BillingManagement.Domain.Model;

namespace ThanhHuongSolution.BillingManagement.Domain.Factory
{
    public class GetEntityVisitor : IGetEntityVisitor<GetEntityVisitor>
    {
        public MDBaseBill MDBaseBill;

        public async Task<GetEntityVisitor> Visit(ReceivingBillingInfo bill)
        {
            var mdBaseBill = new MDReceivingBill()
            {
                Id = bill.Id,
                TrackingNumber = bill.TrackingNumber,
                Customer = bill.Customer,
                TotalAmount = bill.TotalAmount,
                BillCreatedDate = bill.BillCreatedDate,
                CreatedAt = bill.CreatedAt,
                Cart = bill.Cart,
                IncurredCost = bill.IncurredCost,
                FinalTotalAmount = bill.FinalTotalAmount
            };

            MDBaseBill = mdBaseBill;

            return await Task.FromResult(this);
        }

        public async Task<GetEntityVisitor> Visit(BillingInfo bill)
        {
            var mdBaseBill = new MDBill()
            {
                Id = bill.Id,
                TrackingNumber = bill.TrackingNumber,
                Customer = bill.Customer,
                TotalAmount = bill.TotalAmount,
                BillCreatedDate = bill.BillCreatedDate,
                CreatedAt = bill.CreatedAt,
                Cart = bill.Cart
            };

            MDBaseBill = mdBaseBill;

            return await Task.FromResult(this);
        }
    }
}
