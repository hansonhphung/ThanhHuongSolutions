using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ThanhHuongSolution.Common.Infrastrucure
{
    public class CustomException : System.Exception
    {
        public CustomException(string errorMessage)
            : base(errorMessage)
        {
        }

        public CustomException(Exception ex)
            : base("Error happened", ex)
        {
        }

        public CustomException(string errorMessage, System.Exception innerException)
            : base(errorMessage, innerException)
        {
        }
    }
}
