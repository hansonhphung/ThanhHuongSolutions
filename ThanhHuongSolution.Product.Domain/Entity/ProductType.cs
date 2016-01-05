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
        [Description("Lương thực")]
        LuongThuc = 1,

        [Description("Phân bón")]
        PhanBon = 2
    }
}
