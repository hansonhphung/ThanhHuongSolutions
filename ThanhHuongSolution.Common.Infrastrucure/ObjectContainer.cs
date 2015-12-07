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

        public void BindFromAssemblyContainingEndsWith<T>(string endsWith)
        {
            Kernel.Bind(
                reg => reg.FromAssemblyContaining<T>()
                    .SelectAllClasses()
                    .Where(type => type.Name.EndsWith(endsWith))
                    .BindAllInterfaces()
                    .Configure(c =>
                    {
                        c.InTransientScope();
                    }));
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


        public void BindTo<TInterface, TClass>(bool bSingleton = true)
            where TInterface : class
            where TClass : class, TInterface
        {
            if (bSingleton)
                Kernel.Bind<TInterface>().To<TClass>().InSingletonScope();
            else
                Kernel.Bind<TInterface>().To<TClass>();
        }

        public T Get<T>()
        {
            return Kernel.Get<T>();
        }
    }
}