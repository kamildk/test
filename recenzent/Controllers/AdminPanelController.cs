using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using recenzent.Data;
using recenzent.Data.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using recenzent.Data.Service;

namespace recenzent.Controllers
{
    [AllowAnonymous]
    public class AdminPanelController : Controller
    {
        // GET: AdminPanel
        [Authorize(Users = "admin")]
        public ActionResult Index()
        {
            return View();
        }

        //Get: List of roles
        public ActionResult RoleList()
        {
            return View(GetRoleList());
        }
        public List<string> GetRoleList()        {
            var context = new DataContext();
            List<string> roles = new List<string>();
            var roleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(context));
            foreach (var role in roleManager.Roles)
            {
                roles.Add(role.Name.ToString());
            }

            return roles;
        }
        //Get: List of users
        public ViewResult UsersList()
        {
            UserService svc = new UserService();
            return View(svc.GetOwinUsersList());
        }
        
    }
}