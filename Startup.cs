using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(CinemaSharpAuth.Startup))]
namespace CinemaSharpAuth
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
