using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ThanhHuongSolution.Customer.Domain.Entity;

namespace ThanhHuongSolution.Customer.MongoDBDataAccess
{
    public interface ICustomerRepository
    {
        Task<bool> CreateCustomer(MDCustomer customer);

        Task<IList<MDCustomer>> GetAllCustomer();

        Task<MDCustomer> GetCustomerById(string id);

        Task<MDCustomer> GetCustomerByTrackingNumber(string trackingNumber);

        Task<IList<MDCustomer>> Search(string query);
    }
}