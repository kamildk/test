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
            regvm.Lista = new List<UserForAdmin>();
            regvm.UserList = new List<User>();
            UserService serv = new UserService();
            var list = serv.GetOwinUsersList();
            foreach (User user in list)
            {
                if (user.wantToBeAuthor || user.wantToBeReviewer)
                {
                    regvm.UserList.Add(user);
                }
            }
            foreach (User user in regvm.UserList)
            {
                UserForAdmin _obj = new UserForAdmin();
                _obj.Id = user.Id;
                _obj.Reviewer = false;
                _obj.Author = false;
                regvm.Lista.Add(_obj);
            }
                regvm.UserCount = regvm.UserList.Count();
            //regvm.Reviewer = new bool[regvm.UserCount];
            //regvm.Author = new bool[regvm.UserCount];
            //for (int i = 0; i < regvm.UserCount; i++)
            //{
            //    regvm.Reviewer[i] = false;
            //    regvm.Author[i] = false;
            //}
            return View(regvm);
        }
        [HttpPost]
        public ActionResult RegistrationRequests(RegistrationRequestViewModel regvm)
        {
            UserService userService = new UserService();
 
            for (int i = 0; i < regvm.UserCount; i++)
            {
                User _user = userService.GetDBUser(regvm.UserList[i].Id);
                bool check = false;

              
                    if (regvm.Lista[i].Author==true)
                    {
                        userService.AddToRole(regvm.UserList[i].Id, "Author");
                        _user.wantToBeAuthor = false;
                        check = true;

                    }

                
                    if (regvm.Lista[i].Reviewer==true)
                    {
                        userService.AddToRole(regvm.UserList[i].Id, "Reviewer");
                        _user.wantToBeReviewer = false;
                        check = true;

                    }


                if (check) userService.ChangeUser(_user);
            }
            return Redirect("Index");
        }


        public ActionResult PublicationList(int? page)
        {
            IPublicationService pubService = new PublicationService();
            var pubList = pubService.GetAllPublicationsList();

            List<PublicationAdminListViewModel> pubVMList = new List<PublicationAdminListViewModel>();
            foreach (var item in pubList)
            {
                string categoryName;
                if (item.Category == null)
                {
                    categoryName = "";
                }
                else
                {
                    categoryName = item.Category.Name;
                }
                string pubDate;
                if (item.ShareDate == null)
                {
                    pubDate = "";
                }
                else
                {
                    pubDate = item.ShareDate.ToString();
                }

                pubVMList.Add(new PublicationAdminListViewModel()
                {
                    Id = item.PublicationId,
                    Title = item.Title,
                    Description = item.Description,
                    Category = categoryName,
                    AuthorName = item.Author.Name + " " + item.Author.Surname,
                    ShareDate = pubDate,
                    isShared = item.IsShared,

                    
                });


            }

            int pageSize = 40;
            int pageNumber = (page ?? 1);
            return View(pubVMList.ToPagedList(pageNumber, pageSize));
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

            IReviewService revServ = new ReviewService();
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
                data.AllReviews = revServ.GetPublicationReviews(_id).ToList();
                return View(data);
            }
        }
        [HttpGet]
        public ActionResult OrderReview(int publicationId = -1)
        {
            var viewModel = new OrderReviewViewModel();
            UserService service = new UserService();
            List<User> _validUsers = service.GetUsersInRole("Reviewer").ToList();
            if (_validUsers != null)
            {
                viewModel.ValidUsers = _validUsers;
                viewModel.ValidUsersCount = _validUsers.Count();
                viewModel.Message = "";
                viewModel.Rewiever = new bool[viewModel.ValidUsersCount];
            }
            else
            {
                viewModel.Message = Resources.AdminPanel.ResourceManager.GetString("no_reviewers");
            }
            viewModel.pubId = publicationId;
            return View(viewModel);
        }
        [HttpPost]
        public ActionResult OrderReview(OrderReviewViewModel model)
        {
            if (model.pubId > 0)
            {
                for (int i = 0; i < model.Rewiever.Count(); i++)
                {
                    if (model.Rewiever[i] == true)
                    {
                        Review review = new Review();
                        ReviewStateHistory rsh = new ReviewStateHistory();

                        DataContext cont = new DataContext();
                        IUserService userService = new UserService(cont);
                        IReviewService reviewService = new ReviewService(cont);
                        User targetUser = userService.GetOwinUser(model.ValidUsers[i].Id);
                        var state = reviewService.GetState("Przydzielony");
                        review.User = targetUser;
                        DateTime date = DateTime.UtcNow;
                        review.Creation_date = date;
                        review.PublicationId = model.pubId;
                        review.CurrentState = state;
                        targetUser.Reviews.Add(review);
                        reviewService.CreateReview(review, rsh, state);
                        return RedirectToAction("Index");
                    }
                }
            }

            return Redirect("PublicationList");
        }

        public ActionResult ChangePublishState(int Id)
        {
            IPublicationService pubService = new PublicationService();
            var pub = pubService.GetPublication(p => p.PublicationId == Id);
            if (pub.IsShared)
            {
                pub.IsShared = false;

            }
            else
            {
                pub.IsShared = true;
                pub.ShareDate = DateTime.UtcNow;
            }
            pubService.UpdatePublication(pub);
            return Redirect("PublicationList");
        }
        public ActionResult DeleteReview(int _revId)
        {
            IReviewService revServ = new ReviewService();
            var _review = revServ.GetReview(p => p.ReviewId == _revId);
            _review.Expiration_date = DateTime.UtcNow;
            revServ.UpdateReview(_review);
            return RedirectToAction("PublicationDetails", _review.PublicationId);
        }
    }
}