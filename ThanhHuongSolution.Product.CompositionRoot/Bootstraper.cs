﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ThanhHuongSolution.Common.Infrastrucure;
using ThanhHuongSolution.Common.Infrastrucure.CompositionRoot;

namespace ThanhHuongSolution.Product.CompositionRoot
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
