using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(NuGetVersion.Startup))]
namespace NuGetVersion
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
