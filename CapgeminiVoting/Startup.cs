using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(CapgeminiVoting.Startup))]
namespace CapgeminiVoting
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
