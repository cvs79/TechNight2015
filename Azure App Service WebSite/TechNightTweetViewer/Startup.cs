using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(TechNightTweetViewer.Startup))]
namespace TechNightTweetViewer
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);   
        }
    }
}
