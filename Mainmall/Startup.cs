using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Mainmall.Startup))]
namespace Mainmall
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
