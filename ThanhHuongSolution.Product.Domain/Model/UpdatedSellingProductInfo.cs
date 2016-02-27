using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ThanhHuongSolution.Product.Domain.Model
{
    public class UpdatedSellingProductInfo
    {
        public string ProductTrackingNumber { get; set; }

        public long ProductRemainingNumber { get; set; }

        public UpdatedSellingProductInfo() { }

        public UpdatedSellingProductInfo(string productTrackingNumber, long productRemainingNumber)
        {
            ProductTrackingNumber = productTrackingNumber;
            ProductRemainingNumber = productRemainingNumber;
        }
    }
}
