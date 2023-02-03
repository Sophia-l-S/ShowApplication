using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(ShowApplication.Startup))]
namespace ShowApplication
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
