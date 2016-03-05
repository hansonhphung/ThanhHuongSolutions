using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ThanhHuongSolution.Common.Infrastrucure;
using ThanhHuongSolution.DeptManagement.Handler;
using ThanhHuongSolution.DeptManagement.MongoDataAccess;
using ThanhHuongSolution.DeptManagement.Services;

namespace ThanhHuongSolution.DeptManagement.CompositionRoot
{
    public class DIRegister
    {
        public static void RegisterRepositories(IObjectContainer objectContainer)
        {
            objectContainer.BindFromAssemblyContainingEndsWith<DebtManagementRepository>("Repository");
        }

        public static void RegisterServices(IObjectContainer objectContainer)
        {
            objectContainer.BindFromAssemblyContainingEndsWith<DebtManagementServices>("Services");
        }

        public static void RegisterAPIs(IObjectContainer objectContainer)
        {
            objectContainer.BindFromAssemblyContainingEndsWith<DebtManagementAPI>("API");
        }
    }
}
