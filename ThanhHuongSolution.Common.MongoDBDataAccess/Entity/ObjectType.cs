using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ThanhHuongSolution.Common.MongoDBDataAccess.Entity
{
    public enum ObjectType
    {
        [Description("Khách hàng")]
        KhachHang = 1,

        [Description("Phân bón")]
        PhanBon = 2,

        [Description("Gạo")]
        Gao = 3,

        [Description("Cà phê")]
        CaPhe = 4,
    }
}
