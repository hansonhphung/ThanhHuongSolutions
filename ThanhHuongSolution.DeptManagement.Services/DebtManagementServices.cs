using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ThanhHuongSolution.Common.Infrastrucure;
using ThanhHuongSolution.Common.Infrastrucure.Model;
using ThanhHuongSolution.Common.LocResources;
using ThanhHuongSolution.DeptManagement.Domain.Factory;
using ThanhHuongSolution.DeptManagement.Domain.Interfaces;
using ThanhHuongSolution.DeptManagement.Domain.Model;

namespace ThanhHuongSolution.DeptManagement.Services
{
    public class DebtManagementServices : IDebtManagementServices
    {
        public IObjectContainer _objectContainer;

        public DebtManagementServices(IObjectContainer objectContainer)
        {
            _objectContainer = objectContainer;
        }

        public async Task<bool> CreateDebt(BaseDebtModel debt)
        {
            var repository = _objectContainer.Get<IDebtManagementRepository>();

            var visitor = await debt.Visit(new GetEntityVisitor());

            var mdDebt = visitor.MDBaseDebt;

            var oldDebt = await repository.GetDebtByTrackingNumber(debt.TrackingNumber);

            Check.ThrowExceptionIfNotNull(oldDebt, BillManagementResources.DEBT_EXIST);

            var result = await repository.CreateDebt(mdDebt);

            return await Task.FromResult(result);
        }

        public async Task<BaseDebtModel> GetDebtById(string debtId)
        {
            var repository = _objectContainer.Get<IDebtManagementRepository>();

            var data = await repository.GetDebtById(debtId);

            var visitor = await data.Visit(new GetModelVisitor());

            var debt = visitor.Debt;

            return await Task.FromResult(debt);
        }

        public async Task<BaseDebtModel> GetDebtByTrackingNumber(string trackingNumber)
        {
            var repository = _objectContainer.Get<IDebtManagementRepository>();

            var data = await repository.GetDebtById(trackingNumber);

            var visitor = await data.Visit(new GetModelVisitor());

            var debt = visitor.Debt;

            return await Task.FromResult(debt);
        }

        public async Task<SearchDebtResponse> Search(string customerId, string query, Pagination pagination)
        {
            var repository = _objectContainer.Get<IDebtManagementRepository>();

            var data = await repository.Search(customerId, query, pagination);

            var result = new List<BaseDebtModel>();

            foreach (var mdDebt in data)
            {
                var debt = await mdDebt.Visit(new GetModelVisitor());

                result.Add(debt.Debt);
            }

            var totalItem = await repository.Count(customerId, query);

            return await Task.FromResult(new SearchDebtResponse(totalItem, result));
        }
    }
}
