using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ThanhHuongSolution.DeptManagement.CompositionRoot
{
    public class Bootstraper
    {
        public static void Load(IObjectContainer objectContainer)
        {
            DIRegister.RegisterRepositories(objectContainer);

            DIRegister.RegisterServices(objectContainer);

            DIRegister.RegisterAPIs(objectContainer);
        }
    }
}
