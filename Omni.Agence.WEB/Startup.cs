using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Omni.Agence.WEB.Startup))]
namespace Omni.Agence.WEB
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
