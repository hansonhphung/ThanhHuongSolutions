using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ThanhHuongSolution.Common.Infrastrucure.Interfaces
{
    public interface IFrameworkParamOutput<out T>
    {
        T Result { get; }
    }
}
