using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ThanhHuongSolution.Common.Infrastrucure
{
    public class FrameworkParamInput<T> : IFrameworkParamInput
    {
        public T Request { get; private set; }

        public FrameworkParamInput(T request)
        {
            Check.NotNull(request, "request");

            Request = request;
        }
    }
}
