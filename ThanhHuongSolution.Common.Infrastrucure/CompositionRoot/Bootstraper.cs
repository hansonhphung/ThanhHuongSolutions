using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ThanhHuongSolution.Common.Infrastrucure.CompositionRoot
{
    public class Bootstraper
    {
        public static void Load(IObjectContainer objectContainer)
        {
            DIRegister.RegisterDataContextFactory(objectContainer);

            DIRegister.RegisterObjectContainer(objectContainer);
        }
    }
}
