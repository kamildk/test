using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using PagedList;
using recenzent.Data;
using recenzent.Data.Interface;
using recenzent.Data.Model;
using recenzent.Data.Service;
using recenzent.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using recenzent.Data.Service;
using System.Web;
using System.Web.Mvc;

namespace recenzent.Controllers
{
    [AllowAnonymous]
    public class AdminPanelController : Controller
    {
        private DataContext ctx = new DataContext();

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
        public List<string> GetRoleList()
        {
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
            using (var context = new DataContext())
            {
                //List<string> users = new List<string>();
                //var UserManager = new UserManager<User>(new UserStore<User>(context));
                var userlist = context.Users.ToList();
                //foreach (var User in UserManager.Users)
                //{
                //    users.Add(User.Name.ToString());
                //}

                return View(userlist);
            }
        }

        //get: List of users that need registration attention
        [HttpGet]
        public ViewResult RegistrationRequests()
        {
            RegistrationRequestViewModel regvm = new RegistrationRequestViewModel();
            regvm.UserList = new List<Data.Model.User>();
            UserService serv = new UserService();
            var list = serv.GetOwinUsersList();
            foreach(User user in list)
            {
                if (user.wantToBeAuthor || user.wantToBeReviewer)
                {
                    regvm.UserList.Add(user);
                }
            }
            regvm.UserCount = regvm.UserList.Count();
            regvm.Reviewer = new bool[regvm.UserCount];
            regvm.Author = new bool[regvm.UserCount];
            return View(regvm); 
        }
        [HttpPost]
        public ActionResult RegistrationRequests(RegistrationRequestViewModel regvm)
        {
            UserService userService = new UserService();
            
            for (int i=0; i<regvm.UserCount;i++)
            {
                User _user = userService.GetDBUser(regvm.UserList[i].Id);
                bool check = false;
                if (regvm.Author[i])
                {
                    userService.AddToRole(regvm.UserList[i].Id, "Author");
                    _user.wantToBeAuthor = false;
                    check = true;

                }
                if (regvm.Reviewer[i])
                {
                    userService.AddToRole(regvm.UserList[i].Id, "Reviewer");
                    _user.wantToBeReviewer = false;
                    check = true;

                }
                if (check)userService.ChangeUser(_user);
            }
            return Redirect("Index");
        }


        public ActionResult PublicationList(int? page)
        {
            using (var context = new DataContext())
            {
                var pubSort = (from Publication pub in context.Publications orderby pub.ShareDate descending select pub).ToList();

                int pageSize = 5;
                int pageNumber = (page ?? 1);
                return View(pubSort.ToPagedList(pageNumber, pageSize));
            }
        }

        public ActionResult LatestPublicationsPartial()
        {
            using (var context = new DataContext())
            {
                var latestPub = (from Publication pub in context.Publications orderby pub.ShareDate descending select pub).ToList().Take(5);

                return PartialView("LatestPublicationPartial", latestPub);
            }
        }
        [HttpGet]
        public ViewResult PublicationDetails(int? id)
        {
            if (!id.HasValue)
            {
                RedirectToAction("Index");
            }

            using (DataContext context = new DataContext())
            {
                int _id = (int)id;
                var data = new PublicationEditViewModel();
                var publication = context.Publications.FirstOrDefault(i => i.PublicationId == _id);
                data.Id = _id;
                data.Category = publication.Category.Name;
                data.Description = publication.Description;
                //data.File= publication.Files.FirstOrDefault();
                //data.Tags = publication.PublicationTags.ToString();
                data.Title = publication.Title;
                return View(data);
            }
        }
        [HttpGet]
        public ActionResult OrderReview(int publicationId = -1)
        {
            var data = new OrderReviewViewModel();
            UserService service = new UserService();
            var _validUsers = service.GetUsersInRole("Reviwer");
            if (_validUsers != null)
                data.ValidUsers = _validUsers;
            data.pubId = publicationId;
            return View(data);
        }
        [HttpPost]
        public ActionResult OrderReview(OrderReviewViewModel model)
        {
            if (model.pubId > 0)
            {
                using (DataContext ctx = new DataContext())
                {
                    Review review = new Review();
                    ReviewStateHistory rsh = new ReviewStateHistory();
                    
                    //TODO:
                    IUserService userService = new UserService();
                    string userId = User.Identity.GetUserId();
                    User currentUser = ctx.Users.Where(u => u.Id == userId).FirstOrDefault();

                    var state = ctx.ReviewStates.First(i => i.Name == "Przydzielony");


                    review.User = currentUser;
                    DateTime date = DateTime.UtcNow;
                    review.Creation_date = date;
                    review.Expiration_date = date.AddYears(2);
                    review.PublicationId = model.pubId;
                    review.CurrentState = state;
                    currentUser.Reviews.Add(review);
                    ctx.Reviews.Add(review);
                    
                    ctx.SaveChanges();
                    rsh.Review = review;
                    rsh.StateId = state.Id;
                    rsh.State = state;
                    rsh.ChangeDate = DateTime.UtcNow;
                    ctx.ReviewStateHistory.Add(rsh);
                    ctx.SaveChanges();
                    return RedirectToAction("Index");
                }
            }

            return View(model);
        }
    }
}