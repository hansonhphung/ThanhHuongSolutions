using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ThanhHuongSolution.Common.Infrastrucure.MongoDBDataAccess;

namespace ThanhHuongSolution.Common.Infrastrucure.CompositionRoot
{
    public class DIRegister
    {
        public static void RegisterDataContextFactory(IObjectContainer objectContainer)
        {
            var connectionStr = DatabaseConfiguration.MongoDbConnection;

            objectContainer.BindWithConstructorArgument<ReadDataContextFactory>(typeof(IReadDataContextFactory), connectionStr);

            objectContainer.BindWithConstructorArgument<WriteDataContextFactory>(typeof(IWriteDataContextFactory), connectionStr);
        }

        public static void RegisterObjectContainer(IObjectContainer objectContainer)
        {
            objectContainer.BindTo<IObjectContainer, ObjectContainer>();
        }
    }
}
