using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Flab2Fab.Startup))]
namespace Flab2Fab
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
