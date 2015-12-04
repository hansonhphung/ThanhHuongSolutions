using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ThanhHuongSolution.Common.Infrastrucure.Interfaces;

namespace ThanhHuongSolution.Common.Infrastrucure
{
    public class FrameworkParamOutput<T> : IFrameworkParamOutput<T>
    {
        public T Result { get; private set; }

        public FrameworkParamOutput(T result)
        {
            Result = result;
        }

        public FrameworkParamOutput()
        {
            Result = default(T);
        }
    }
}
