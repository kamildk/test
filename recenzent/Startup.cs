using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(recenzent.Startup))]
namespace recenzent
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
            
        }
    }
}
