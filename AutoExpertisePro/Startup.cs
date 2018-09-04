using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(AutoExpertisePro.Startup))]
namespace AutoExpertisePro
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
