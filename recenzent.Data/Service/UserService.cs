using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using recenzent.Data.Interface;
using recenzent.Data.Model;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace recenzent.Data.Service {
    public class UserService : IUserService {

        UserManager<User> userManager;
        DataContext context;

        public UserService(DataContext cont=null) {
            if (cont != null)
            {
                context = cont;
            }
            else
            {
                context = new DataContext();
            }
            userManager = new UserManager<User>(new UserStore<User>(context));
        }
        //~UserService()
        //{
        //    context.SaveChanges();
        //}

        public User GetOwinUser(string id) {
            var result = userManager.Users.Where(u => u.Id == id).ToList();

            return result.Count > 0 ? result.FirstOrDefault() : null;
        }

        public User GetDBUser(string id) {
            using (var ctx = new DataContext()) {
                var result = ctx.Users.Where(u => u.Id == id).ToList();

                return result.Count > 0 ? result.FirstOrDefault() : null;
            }
        }

        public List<User> GetOwinUsersList() {
            return userManager.Users.ToList();
            
        }
        public IQueryable<User> GetUsersInRole(string roleName)
        {
            if (context != null && roleName != null)
            {
                var roles = context.Roles.Where(r => r.Name == roleName);
                if (roles.Any())
                {
                    var roleId = roles.First().Id;
                    return from user in context.Users
                           where user.Roles.Any(r => r.RoleId == roleId)
                           select user;
                }
            }
            return null;
        }
        public UserManager<User> GetUserManager()
        {
            return(userManager);
        }
        public void AddToRole(string Id, string Role)
        {
            userManager.AddToRole(Id, Role);
            context.SaveChanges();
        }
        public void ChangeUser(User destination)
        {
            var entity = context.Users.Where(c => c.Id == destination.Id).AsQueryable().FirstOrDefault();
            context.Entry(entity).CurrentValues.SetValues(destination);
            //IdentityResult result = userManager.Update(destination);
            //if (result==IdentityResult.Success)
            //{
            //    Console.WriteLine("1");

            //}
            //{
            //    Console.WriteLine("2");
            //}
            context.SaveChanges();
        }
    }
}
