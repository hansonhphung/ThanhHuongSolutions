using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ThanhHuongSolution.Common.Infrastrucure;
using ThanhHuongSolution.Common.LocResources;
using ThanhHuongSolution.BillingManagement.Domain.Interface;
using ThanhHuongSolution.BillingManagement.Domain.Model;
using ThanhHuongSolution.Common.Infrastrucure.Model;
using ThanhHuongSolution.BillingManagement.Domain.Factory;

namespace ThanhHuongSolution.BillingManagement.Services
{
    public class BillingManagementServices : IBillingManagementServices
    {
        public IObjectContainer _objectContainer;

        public BillingManagementServices(IObjectContainer objectContainer)
        {
            _objectContainer = objectContainer;
        }

        public async Task<bool> CreateBill(BaseBillModel bill)
        {
            var repository = _objectContainer.Get<IBillingManagementRepository>();

            var visitor = await bill.Visit(new GetEntityVisitor());

            var mdBill = visitor.MDBaseBill;

            var oldBill = await repository.GetBillByTrackingNumber(bill.TrackingNumber);

            Check.ThrowExceptionIfNotNull(oldBill, BillManagementResources.BILL_EXIST);

            var result = await repository.CreateBill(mdBill);

            return await Task.FromResult(result);
        }

        public async Task<BaseBillModel> GetBillById(string billId)
        {
            var repository = _objectContainer.Get<IBillingManagementRepository>();

            var data = await repository.GetBillById(billId);

            var visitor = await data.Visit(new GetModelVisitor());

            var bill = visitor.Bill;

            //Check.ThrowExceptionIfNotNull(oldBill, BillManagementResources.BILL_EXIST);

            return await Task.FromResult(bill);
        }
        
        public async Task<BaseBillModel> GetBillByTrackingNumber(string trackingNumber)
        {
            var repository = _objectContainer.Get<IBillingManagementRepository>();

            var data = await repository.GetBillByTrackingNumber(trackingNumber);

            var visitor = await data.Visit(new GetModelVisitor());

            var bill = visitor.Bill;

            //Check.ThrowExceptionIfNotNull(oldBill, BillManagementResources.BILL_EXIST);

            return await Task.FromResult(bill);
        }

        public async Task<SearchBillingResponse> Search(string customerId, string query, Pagination pagination, string billType)
        {
            var repository = _objectContainer.Get<IBillingManagementRepository>();

            var data = await repository.Search(customerId, query, pagination, billType);

            var result = new List<BaseBillModel>();

            foreach (var mdBill in data)
            {
                var visitor = await mdBill.Visit(new GetModelVisitor());

                var bill = visitor.Bill;

                result.Add(bill);
            }

            var totalItem = await repository.Count(customerId, query, billType);

            return await Task.FromResult(new SearchBillingResponse(totalItem, result));
        }

        public  async Task<bool> IsCustomerHaveTransaction(string customerId)
        {
            var repository = _objectContainer.Get<IBillingManagementRepository>();

            var data = await repository.IsCustomerHaveTransaction(customerId);

            return await Task.FromResult(data);
        }

        public async Task<long> GetProductLastPrice(string productTrackingNumber)
        {
            var repository = _objectContainer.Get<IBillingManagementRepository>();

            var data = await repository.GetProductLastPrice(productTrackingNumber);

            return await Task.FromResult(data);
        }

        public async Task<SearchBillingResponse> GetBillInRangeDate(string query, DateTime fromDate, DateTime toDate, Pagination pagination, string billType)
        {
            var repository = _objectContainer.Get<IBillingManagementRepository>();

            var data = await repository.GetBillInRangeDate(query, fromDate, toDate,pagination, billType);

            var result = new List<BaseBillModel>();

            foreach (var mdBill in data)
            {
                var visitor = await mdBill.Visit(new GetModelVisitor());

                var bill = visitor.Bill;

                result.Add(bill);
            }

            var totalItem = await repository.CountStatisticsBill(query, fromDate, toDate, billType);

            return await Task.FromResult(new SearchBillingResponse(totalItem, result));
        }
    }
}
