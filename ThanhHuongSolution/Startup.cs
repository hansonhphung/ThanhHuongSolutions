using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(ThanhHuongSolution.Startup))]
namespace ThanhHuongSolution
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
