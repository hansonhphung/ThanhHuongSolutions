using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ThanhHuongSolution.Customer;

namespace ThanhHuongSolution.Common.Infrastrucure
{
    public class WebDIRegister
    {
        public static void Setup(IObjectContainer objectContainer)
        {
            Customer.CompositionRoot.Bootstraper.Load(objectContainer);

            CompositionRoot.Bootstraper.Load(objectContainer);
        }
    }
}
