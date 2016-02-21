using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ThanhHuongSolution.Common.Infrastrucure;
using ThanhHuongSolution.Common.MongoDBDataAccess.Interface;
using ThanhHuongSolution.Common.MongoDBDataAccess.Services;

namespace ThanhHuongSolution.Common.MongoDBDataAccess.CompositionRoot
{
    public class DIRegister
    {
        public static void RegisterMongoDBDataAccessService(IObjectContainer objectContainer)
        {
            objectContainer.BindTo<ITrackingNumberGenerator, TrackingNumberGenerator>();
            objectContainer.BindTo<IUserManagementRepository, UserManagementService>();
        }
    }
}
