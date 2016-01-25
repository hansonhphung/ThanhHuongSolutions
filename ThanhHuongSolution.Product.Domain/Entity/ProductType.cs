using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ThanhHuongSolution.Product.Domain.Entity
{
    public enum ProductType
    {
        [Description("Phân bón")]
        PhanBon = 1,

        [Description("Gạo")]
        Gao = 2,

        [Description("Cà Phê")]
        CaPhe = 3
    }
}
