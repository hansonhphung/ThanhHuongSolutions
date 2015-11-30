using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ninject.Modules;
using Ninject;

namespace ThanhHuongSolution.Common.Infrastrucure.Utilities
{
    public static class DIHelper
    {
        public static IObjectContainer CreateObjectContainer()
        {
            var kernel = new StandardKernel();
            return new ObjectContainer(kernel);
        }
    }
}
