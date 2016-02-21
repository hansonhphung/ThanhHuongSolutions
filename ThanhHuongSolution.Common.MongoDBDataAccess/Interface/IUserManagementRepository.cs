using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ThanhHuongSolution.Common.MongoDBDataAccess.Interface
{
    public interface IUserManagementRepository
    {
        Task<bool> Login(string username, string password);
    }
}
