using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Prospects.Backend.Startup))]
namespace Prospects.Backend
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
