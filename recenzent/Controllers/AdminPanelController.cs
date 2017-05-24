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
using System.Web;
using System.Web.Mvc;

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