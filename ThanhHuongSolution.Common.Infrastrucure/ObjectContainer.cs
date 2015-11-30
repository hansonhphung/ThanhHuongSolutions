using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ninject;
using Ninject.Extensions.Conventions;

namespace ThanhHuongSolution.Common.Infrastrucure
{
    public class ObjectContainer : IObjectContainer
    {
        public IKernel Kernel { get; private set; }

        public void BindFromAssemblyContainingConfigure<T>(string endsWith)
        {
            Kernel.Bind(reg => reg
                .FromAssemblyContaining<T>()
                .SelectAllClasses()
                .Where(type => type.Name.EndsWith(endsWith))
                .BindDefaultInterface()
                .Configure(b => b.InSingletonScope()));
        }

        public void BindWithConstructorArgument<T>(Type factory, string connectionStr) where T : class
        {
            Kernel.Bind(factory)
                .To<T>()
                .InSingletonScope()
                .WithConstructorArgument("connectionString", context => connectionStr);
        }

        public ObjectContainer(IKernel kernel)
        {
            Kernel = kernel;
        }
    }
}