using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(HelloCloudAspx.Startup))]
namespace HelloCloudAspx
{
    public partial class Startup {
        public void Configuration(IAppBuilder app) {
            ConfigureAuth(app);
        }
    }
}
