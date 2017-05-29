using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using recenzent.Data.Model;

namespace recenzent.Data.Interface {
    public interface IUserService {
        User GetOwinUser(string id);
        User GetDBUser(string id);
        List<User> GetOwinUsersList();
        void ChangeUser(User destination);
        void AddToRole(string Id, string Role);
        IQueryable<User> GetUsersInRole(string roleName);
    }
}
