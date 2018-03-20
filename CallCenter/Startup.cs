using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(CallCenter.Startup))]
namespace CallCenter
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
