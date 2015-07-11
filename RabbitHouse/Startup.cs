using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(RabbitHouse.Startup))]
namespace RabbitHouse
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
