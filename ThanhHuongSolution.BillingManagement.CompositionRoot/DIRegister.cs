using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ThanhHuongSolution.BillingManagement.Handler;
using ThanhHuongSolution.BillingManagement.MongoDBDataAccess;
using ThanhHuongSolution.BillingManagement.Services;
using ThanhHuongSolution.Common.Infrastrucure;

namespace ThanhHuongSolution.BillingManagement.CompositionRoot
{
    public class DIRegister
    {
        public static void RegisterRepositories(IObjectContainer objectContainer)
        {
            objectContainer.BindFromAssemblyContainingEndsWith<BillingManagementRepository>("Repository");
        }

        public static void RegisterServices(IObjectContainer objectContainer)
        {
            objectContainer.BindFromAssemblyContainingEndsWith<BillingManagementServices>("Services");
        }

        public static void RegisterAPIs(IObjectContainer objectContainer)
        {
            objectContainer.BindFromAssemblyContainingEndsWith<BillingManagementAPI>("API");
        }
    }
}
