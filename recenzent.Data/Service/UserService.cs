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

        public UserService() {
            context = new DataContext();
            userManager = new UserManager<User>(new UserStore<User>(context));
        }

        public User GetOwinUser(string id) {
            var result = userManager.Users.Where(u => u.Id == id).ToList();

            return result.Count > 0 ? result.FirstOrDefault() : null;
        }

        public User GetDBUser(string id) {
            var result = context.Users.Where(u => u.Id == id).ToList();

            return result.Count > 0 ? result.FirstOrDefault() : null;
        }

        public List<User> GetOwinUsersList() {
            return userManager.Users.ToList();
        }
    }
}
