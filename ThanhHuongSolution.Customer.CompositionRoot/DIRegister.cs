using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ThanhHuongSolution.Common.Infrastrucure;
using ThanhHuongSolution.Common.Infrastrucure.MongoDBDataAccess;
using ThanhHuongSolution.Customer.Handler;
using ThanhHuongSolution.Customer.MongoDBDataAccess;
using ThanhHuongSolution.Customer.Services;

namespace ThanhHuongSolution.Customer.CompositionRoot
{
    public class DIRegister
    {
        public static void RegisterRepositories(IObjectContainer objectContainer)
        {
            objectContainer.BindFromAssemblyContainingEndsWith<CustomerManagementRepository>("Repository");
        }

        public static void RegisterServices(IObjectContainer objectContainer)
        {
            objectContainer.BindFromAssemblyContainingEndsWith<CustomerManagementServices>("Services");
        }

        public static void RegisterAPIs(IObjectContainer objectContainer)
        {
            objectContainer.BindFromAssemblyContainingEndsWith<CustomerManagementAPI>("API");
        }

        public static void RegisterForObjectContainer(IObjectContainer objectContainer)
        {
            objectContainer.BindFromAssemblyContainingEndsWith<ObjectContainer>("bjectContainer");
            objectContainer.BindFromAssemblyContainingEndsWith<ReadDataContextFactory>("DataContextFactory");
            objectContainer.BindWithConstructorArgument<ReadDataContextFactory>(typeof(ReadDataContextFactory), "connectionString");
        }
    }
}
