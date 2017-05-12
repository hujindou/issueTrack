using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(issueTrack.Startup))]
namespace issueTrack
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
