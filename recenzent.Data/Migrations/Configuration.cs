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

               
                if (userManager.FindByName("Magik") == null)
                {
                    var user = new User() { UserName = "Admin", Name = "Wojciech", Surname = "Sendera", Email="wojteksendera@gmail.com", RegistrationDate=DateTime.UtcNow,  };
                    userManager.Create(user, "Admin123");
                    userManager.AddToRole(user.Id, "Admin");
                    userManager.AddToRole(user.Id, "Author");
                    userManager.AddToRole(user.Id, "Reviewer");

                }


                Publication_category category1 = new Publication_category() {
                    Name = "Informatyka"
                };

                Publication_category category2 = new Publication_category() {
                    Name = "Matematyka"
                };
                Publication_category category3 = new Publication_category() {
                    Name = "Fizyka"
                };

                if (!context.Publication_Categories.Any(c => c.Name == category1.Name))
                    context.Publication_Categories.Add(category1);
                if (!context.Publication_Categories.Any(c => c.Name == category2.Name))
                    context.Publication_Categories.Add(category2);
                if (!context.Publication_Categories.Any(c => c.Name == category3.Name))
                    context.Publication_Categories.Add(category3);

                ReviewState state1 = new ReviewState() {
                    Name = "Przydzielony"
                };
                ReviewState state2 = new ReviewState() {
                    Name = "Zaakceptowany"
                };
                ReviewState state3 = new ReviewState() {
                    Name = "Cofniêty do poprawy"
                };
                ReviewState state4 = new ReviewState() {
                    Name = "Odrzucony"
                };

                if(!context.ReviewStates.Any(s=>s.Name == state1.Name)) {
                    context.ReviewStates.Add(state1);
                }
                if (!context.ReviewStates.Any(s => s.Name == state2.Name)) {
                    context.ReviewStates.Add(state2);
                }
                if (!context.ReviewStates.Any(s => s.Name == state3.Name)) {
                    context.ReviewStates.Add(state3);
                }
                if (!context.ReviewStates.Any(s => s.Name == state4.Name)) {
                    context.ReviewStates.Add(state4);
                }
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
