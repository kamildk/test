using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.Owin;
using Owin;
using recenzent.Data;
using recenzent.Data.Model;

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