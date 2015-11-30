using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ThanhHuongSolution.Common.Infrastrucure
{
    public class WebContainer
    {
        public static WebContainer Instance = null;

        static WebContainer()
        {
            if (Instance == null)
            {
                Instance = new WebContainer(RocketUncle.Common.Infrastructure.Utilities.DIHelper.CreateObjectContainer());
            }
        }

        private readonly IObjectContainer _objectContainer;

        private WebContainer(IObjectContainer objectContainer)
        {
            Check.NotNull(objectContainer, "objectContainer");

            _objectContainer = objectContainer;
        }

        public IObjectContainer GetObjectContainer()
        {
            return _objectContainer;
        }

        public T ResolveAPI<T>()
        {
            return _objectContainer.Get<T>();
        }
    }
}
