using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Money.Startup))]
namespace Money
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);

        }
    }
}
