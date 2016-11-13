using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(JKLSite.Startup))]
namespace JKLSite
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
