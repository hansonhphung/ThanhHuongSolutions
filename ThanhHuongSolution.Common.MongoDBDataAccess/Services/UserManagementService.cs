using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ThanhHuongSolution.Common.Infrastrucure;
using ThanhHuongSolution.Common.Infrastrucure.MongoDBDataAccess;
using ThanhHuongSolution.Common.Infrastrucure.MongoDBDataAccess.Entity;
using ThanhHuongSolution.Common.LocResources;
using ThanhHuongSolution.Common.MongoDBDataAccess.Entity;
using ThanhHuongSolution.Common.MongoDBDataAccess.Interface;

namespace ThanhHuongSolution.Common.MongoDBDataAccess.Services
{
    public class UserManagementService : IUserManagementRepository
    {
        private readonly IReadDataContextFactory _readDataContectFactory;
        private readonly IWriteDataContextFactory _writeDataContextFactory;

        public UserManagementService(IReadDataContextFactory readDataContextFactory, IWriteDataContextFactory writeDataContextFactory)
        {
            Check.NotNull(readDataContextFactory, "readDataContextFactory");
            Check.NotNull(writeDataContextFactory, "writeDataContextFactory");

            _readDataContectFactory = readDataContextFactory;
            _writeDataContextFactory = writeDataContextFactory;
        }

        public async Task<bool> Login(string username, string password)
        {
            try
            {
                var dbContext = _readDataContectFactory.CreateMongoDBReadContext();

                var collection = dbContext.GetCollection<MDUser>(MongoDBEntityNames.UserCollection.TableName);

                var user = await collection.Find(x => x.Username == username).FirstOrDefaultAsync();

                Check.ThrowExceptionIfNull(user, UserManagementResources.USERNAME_PASS_INCORECT);

                if (!user.Password.Equals(password))
                    Check.ThrowException(UserManagementResources.USERNAME_PASS_INCORECT);

                return await Task.FromResult(true);
            }
            catch (Exception ex)
            {
                Check.ThrowException(ex.Message);
                return await Task.FromResult(false);
            }
        }
    }
}
