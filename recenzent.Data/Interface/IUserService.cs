using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using recenzent.Data.Model;

namespace recenzent.Data.Interface {
    public interface IUserService {
        User GetUser(string id);

        List<User> GetUsersList();
    }
}
