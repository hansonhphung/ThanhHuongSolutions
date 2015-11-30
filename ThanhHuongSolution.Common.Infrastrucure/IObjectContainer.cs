using Ninject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ThanhHuongSolution.Common.Infrastrucure
{
    public interface IObjectContainer
    {
        IKernel Kernel { get; }

        void BindFromAssemblyContainingConfigure<T>(string endsWith);

        void BindWithConstructorArgument<T>(Type factory, string connectionStr) where T : class;
    }
}
