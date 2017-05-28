using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.Owin;
using Owin;
using recenzent.Data;
using recenzent.Data.Model;
using System.Linq;
using System.Security;


[assembly: OwinStartupAttribute(typeof(recenzent.Startup))]
namespace recenzent
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);

            //using (var ctx = new DataContext()) {

            //    var roleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(ctx));
            //    var userManager = new UserManager<User>(new UserStore<User>(ctx));

            //    var defaultAff = ctx.Affliations.FirstOrDefault();

            //    var testUser = new User() {
            //        UserName = "AdminTest",
            //        Email = "adminTest@test.pl",
            //        RegistrationDate = System.DateTime.UtcNow,
            //        Name = "Admin",
            //        Surname = "Zbigniew",
            //        Affiliation = defaultAff
            //    };

            //    if (!userManager.Users.Any(u => u.Email == testUser.Email)) {
            //        var result = userManager.Create(testUser, "6znakow");
            //        if (result.Succeeded) {
            //            var res1 = userManager.AddToRole(testUser.Id, "Admin");
            //            var res2 = userManager.AddToRole(testUser.Id, "Author");
            //            var res3 = userManager.AddToRole(testUser.Id, "Reviewer");

            //            if(res1.Succeeded && res2.Succeeded && res3.Succeeded)
            //                System.Diagnostics.Debug.WriteLine("Pykło");
            //        }
            //        else {
            //            System.Diagnostics.Debug.WriteLine("Nie pykło");
            //        }
            //    }
            //}

        }


    }
}