using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ThanhHuongSolution.Common.Infrastrucure;
using ThanhHuongSolution.Common.Infrastrucure.Utilities;

namespace ThanhHuongSolution.Customer.UnitTests
{
    public class UnitTestHelper
    {
        public static IObjectContainer SetupKernelBootstraper()
        {
            var objectContainer = DIHelper.CreateObjectContainer();

            CompositionRoot.Bootstraper.Load(objectContainer);

            Common.Infrastrucure.CompositionRoot.Bootstraper.Load(objectContainer);

            return objectContainer;
        }
    }
}
