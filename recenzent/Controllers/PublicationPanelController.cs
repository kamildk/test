using Microsoft.AspNet.Identity;
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
    public class PublicationPanelController : Controller
    {
        private DataContext ctx = new DataContext();

        [AllowAnonymous]
        public ActionResult Open(int id)
        {
            IRatingService ratingService = new RatingService() { };
            string userId = User.Identity.GetUserId();


            var pub = ctx.Publications.Find(id);
            var rating = ratingService.GetPublicationRating(pub);

            ViewBag.RateFromUser = ratingService.GetUserRateOnPub(pub,userId);
            ViewBag.Ratings = new List<int>() { 1, 2, 3, 4, 5 };

            if (pub != null)
            {
                return View(TransferToViewModel(pub, rating));
               
            }
            else
                return View("Error");
        }


        [HttpPost]
        [Authorize]
        public ActionResult RatePub(PublicationPanelViewModel pubCom)
        {
            IUserService userService = new UserService();
            string userId = User.Identity.GetUserId();
            User currentUser = ctx.Users.Where(u => u.Id == userId).FirstOrDefault();

            var pub = ctx.Publications.Find(pubCom.PublicationId);

            var ratePrevious = ctx.Ratings.Where(r => r.Publication.PublicationId == pubCom.PublicationId && r.User.Id == currentUser.Id).FirstOrDefault();
            var rate = new Rating() { };


            bool wasRatedBefore = false;

            if ((ratePrevious) != null)
            {
                ratePrevious.Date = DateTime.Now;
                ratePrevious.User = currentUser;
                ratePrevious.Value = pubCom.RateFromCurrUser;
                ratePrevious.Publication = ctx.Publications.Find(pubCom.PublicationId);

            }
            else
            {
                rate.Date = DateTime.Now;
                rate.User = currentUser;
                rate.Value = pubCom.RateFromCurrUser;
                rate.Publication = ctx.Publications.Find(pubCom.PublicationId);
                ctx.Ratings.Add(rate);
            }



            wasRatedBefore = true;

            if (wasRatedBefore)
            {
                ViewBag.RateMessage = "Zmień swoją ocenę Publikacji:";
            }
            else
                ViewBag.RateMessage = "Oceń tę publikację:";

            ctx.SaveChanges();
            return Redirect("Open/"+pubCom.PublicationId.ToString());
        }


        [Authorize]
        public ActionResult Download(int PublicationId)
        {
            var physicalFilePath = (from File f in ctx.Files where f.PublicationId == PublicationId select f.Link_source).First();

            if (physicalFilePath != null)
            {
                return File(physicalFilePath, "application/pdf");
            }
            else
                return View("Error");
        }

        [HttpPost]
        [Authorize]
        public ActionResult AddComment(PublicationPanelViewModel pubCom)
        {
            if (pubCom.NewCommentText.Length > 0)
            {
                ICommentService commentService = new CommentService();
                IUserService userService = new UserService();
                string userId = User.Identity.GetUserId();

                commentService.AddComment(pubCom.NewCommentText, pubCom.PublicationId, userId);

                return Redirect("Open/" + pubCom.PublicationId.ToString());
            }
            else
                return View("Open", pubCom);
        }

        [HttpPost]
        [Authorize]
        public ActionResult AddReply(string reply, int parentId)
        {
            if (reply.Length > 0)
            {
                ICommentService commentService = new CommentService();
                IUserService userService = new UserService();
                string userId = User.Identity.GetUserId();
                var pub = ctx.Publications.Where(p => p.PublicationId == ctx.Comments.Where(c => c.CommentId == parentId).FirstOrDefault().PublicationID).FirstOrDefault();

                commentService.AddReply(reply, parentId, userId);
                return Redirect("Open/" + pub.PublicationId.ToString());

            }
            else
                return View("Open");
        }

        private PublicationPanelViewModel TransferToViewModel(Publication pub, float rating)
        {
            ICommentService commentService = new CommentService() { };
            List<Comment> comments = commentService.GetPublicationComments(pub);

            List<CommentViewModel> commentVMList = new List<CommentViewModel>();
            List<CommentViewModel> repliesVMList = new List<CommentViewModel>();
            foreach (var item in comments)
            {
                string userName = (from User u in ctx.Users
                                   where u.Id == item.UserID
                                   select u.UserName).FirstOrDefault();

                List<CommentViewModel> tRepliesVMList = new List<CommentViewModel>();

                item.ChildComments = commentService.GetChildComments(item);

                foreach (var ccom in item.ChildComments)
                {
                    string CCUserName = (from User u in ctx.Users
                                         where u.Id == ccom.UserID
                                         select u.UserName).FirstOrDefault();
                    tRepliesVMList.Add(new CommentViewModel()
                    {
                        Body = ccom.Text,
                        Id = ccom.CommentId,
                        UserName = CCUserName,
                        AddDate = ccom.Date

                    });
                }

                repliesVMList = tRepliesVMList;

                commentVMList.Add(new CommentViewModel()
                {
                    Body = item.Text,
                    Id = item.CommentId,
                    UserName = userName,
                    ChildReplies = repliesVMList,
                    AddDate = item.Date
                });
            }

            var pubCom = new PublicationPanelViewModel()
            {
                Comments = commentVMList,
                Description = pub.Description,
                Title = pub.Title,
                PublicationId = pub.PublicationId,
                Rating = rating
            };
            return pubCom;
        }

    }
}