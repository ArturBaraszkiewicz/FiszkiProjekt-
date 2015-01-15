using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Fiszki.Startup))]
namespace Fiszki
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
