using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ThanhHuongSolution.Product.Domain.Entity;

namespace ThanhHuongSolution.Product.Domain.Interfaces
{
    public interface IProductManagementRepository
    {
        Task<bool> CreateCustomer(MDProduct customer);
    }
}
