using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System;


namespace recenzent.Data.Migrations
{
    using Model;
    using System;
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<recenzent.Data.DataContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
            AutomaticMigrationDataLossAllowed = true;
        }

        protected override void Seed(recenzent.Data.DataContext context)
        {

            try
            {
                var testAffiliation = new Affiliation() {
                    Name = "brak"
                };

                if(!context.Affliations.Any(a => a.Name == testAffiliation.Name))
                    context.Affliations.AddOrUpdate(testAffiliation);

                var roleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(context));
                var userManager = new UserManager<User>(new UserStore<User>(context));

                if (!roleManager.RoleExists("Admin"))
                {
                    var role = new IdentityRole();
                    role.Name = "Admin";
                    roleManager.Create(role);
                }

                if (!roleManager.RoleExists("Author"))
                {
                    var role = new IdentityRole();
                    role.Name = "Author";
                    roleManager.Create(role);

                }

                if (!roleManager.RoleExists("Reviewer"))
                {
                    var role = new IdentityRole();
                    role.Name = "Reviewer";
                    roleManager.Create(role);
                }

                //var user = new User();
                //user.UserName = "admin";
                //user.Email = "test@test.pl";
                //user.Name = "Wojciech";
                //user.Surname = "Sendera";
                //user.RegistrationDate = DateTime.UtcNow;
                //user.Affiliation = testAffiliation;
                //string userPassword = "Test_1234";

                //var chkUser = UserManager.Create(user, userPassword);
                //if (chkUser.Succeeded) {
                //    var result1 = UserManager.AddToRole(user.Id, "Admin");
                //}

                //user = new User();
                //user.UserName = "author";
                //user.Email = "test2@test.pl";
                //user.Name = "Wojciech2";
                //user.Surname = "Sendera2";
                //user.RegistrationDate = DateTime.UtcNow;
                //user.Affiliation = testAffiliation;

                //var chkUser2 = UserManager.Create(user, userPassword);
                //if (chkUser2.Succeeded) {
                //    var result1 = UserManager.AddToRole(user.Id, "Author");
                //}

                //user = new User();
                //user.UserName = "reviewer";
                //user.Email = "test3@test.pl";
                //user.Name = "Wojciech3";
                //user.Surname = "Sendera3";
                //user.RegistrationDate = DateTime.UtcNow;
                //user.Affiliation = testAffiliation;
                //user.EmailConfirmed = true;

                //var chkUser3 = UserManager.Create(user, userPassword);
                //if (chkUser3.Succeeded) {
                //    var result1 = UserManager.AddToRole(user.Id, "Reviewer");
                //}


            }

            catch (System.Data.Entity.Validation.DbEntityValidationException e)
            {
                foreach (var eve in e.EntityValidationErrors)
                {
                    Console.WriteLine("Entity of type \"{0}\" in state \"{1}\" has the following validation errors:",
                        eve.Entry.Entity.GetType().Name, eve.Entry.State);
                    foreach (var ve in eve.ValidationErrors)
                    {
                        Console.WriteLine("- Property: \"{0}\", Value: \"{1}\", Error: \"{2}\"",
                            ve.PropertyName,
                            eve.Entry.CurrentValues.GetValue<object>(ve.PropertyName),
                            ve.ErrorMessage);
                    }
                }
                throw;

            }
            
            context.SaveChanges();

        }
    }
}
