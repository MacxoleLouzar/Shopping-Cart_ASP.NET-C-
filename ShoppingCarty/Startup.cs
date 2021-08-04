using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(ShoppingCarty.Startup))]
namespace ShoppingCarty
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
