using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ThanhHuongSolution.Common.Infrastrucure;

namespace ThanhHuongSolution.Customer.CompositionRoot
{
    public class Bootstraper
    {
        public static void Load(IObjectContainer objectContainer)
        {
            DIRegister.RegisterForObjectContainer(objectContainer);

            DIRegister.RegisterRepositories(objectContainer);

            DIRegister.RegisterServices(objectContainer);

            DIRegister.RegisterAPIs(objectContainer);
        }
    }
}
