using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ThanhHuongSolution.Common.Infrastrucure;
using ThanhHuongSolution.Common.LocResources;
using ThanhHuongSolution.BillingManagement.Domain.Interface;
using ThanhHuongSolution.BillingManagement.Domain.Model;

namespace ThanhHuongSolution.BillingManagement.Services
{
    public class BillingManagementServices : IBillingManagementServices
    {
        IObjectContainer _objectContainer;

        public BillingManagementServices(IObjectContainer objectContainer)
        {
            _objectContainer = objectContainer;
        }

        public async Task<bool> CreateBill(BillingInfo bill)
        {
            var repository = _objectContainer.Get<IBillingManagementRepository>();

            var mdBill = bill.GetEntity();

            var oldBill = await repository.GetBillByTrackingNumber(bill.TrackingNumber);

            //Check.ThrowExceptionIfNotNull(oldBill, BillManagementResources.BILL_EXIST);

            var result = await repository.CreateBill(mdBill);

            return await Task.FromResult(result);
        }

        public async Task<IList<BillingInfo>> GetAllBill()
        {
            var repository = _objectContainer.Get<IBillingManagementRepository>();

            var data = await repository.GetAllBill();

            //Check.ThrowExceptionIfNotNull(oldBill, BillManagementResources.BILL_EXIST);

            var result = data.Select(x => new BillingInfo(x)).ToList();

            return await Task.FromResult<IList<BillingInfo>>(result);
        }

        public async Task<BillingInfo> GetBillById(string billId)
        {
            var repository = _objectContainer.Get<IBillingManagementRepository>();

            var data = await repository.GetBillById(billId);

            //Check.ThrowExceptionIfNotNull(oldBill, BillManagementResources.BILL_EXIST);

            return await Task.FromResult<BillingInfo>(new BillingInfo(data));
        }

        public async Task<BillingInfo> GetBillByTrackingNumber(string trackingNumber)
        {
            var repository = _objectContainer.Get<IBillingManagementRepository>();

            var data = await repository.GetBillById(trackingNumber);

            //Check.ThrowExceptionIfNotNull(oldBill, BillManagementResources.BILL_EXIST);

            return await Task.FromResult<BillingInfo>(new BillingInfo(data));
        }

        public async Task<IList<BillingInfo>> GetBillsByCustomerId(string customerId)
        {
            var repository = _objectContainer.Get<IBillingManagementRepository>();

            var data = await repository.GetBillsByCustomerId(customerId);

            //Check.ThrowExceptionIfNotNull(oldBill, BillManagementResources.BILL_EXIST);

            var result = data.Select(x => new BillingInfo(x)).ToList();

            return await Task.FromResult<IList<BillingInfo>>(result);
        }

        public async Task<IList<BillingInfo>> Search(string query)
        {
            var repository = _objectContainer.Get<IBillingManagementRepository>();

            var data = await repository.Search(query);

            var result = data.Select(x => new BillingInfo(x)).ToList();

            return await Task.FromResult(result);
        }
    }
}
