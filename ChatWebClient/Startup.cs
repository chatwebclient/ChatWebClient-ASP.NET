using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(ChatWebClient.Startup))]
namespace ChatWebClient
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
