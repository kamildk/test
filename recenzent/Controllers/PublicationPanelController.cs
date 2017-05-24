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
        // GET: PublicationPanel
        public ActionResult Index()
        {
            return View();
        }


        [AllowAnonymous]
        public ActionResult Open(int id = 1)
        {
            ViewBag.Ratings = new List<int>() { 1, 2, 3, 4, 5 };

            var pub = ctx.Publications.Find(id);

            var rates = (ctx.Ratings.Where(r => r.Publication.PublicationId == id)).ToList();
            int sum = 0;
            foreach (var item in rates)
            {
                sum += item.Value;
            }

            float rating;
            if (rates.Count() != 0)
                rating = (float)sum / rates.Count();
            else
                rating = 0;


            if (pub != null)
            {
                List<Comment> comments = (from Comment c in ctx.Comments
                                          where c.Publication.PublicationId == id && c.ParentComment==null
                                          select c).ToList();
                List<CommentViewModel> commentVMList = new List<CommentViewModel>();
                foreach (var item in comments) {
                    string userName = (from User u in ctx.Users
                                      where u.Id == item.UserID
                                      select u.UserName).FirstOrDefault();
                    //PROTIP
                    //W tym miejscu możesz odpytać jeszcze raz bazę o komentarze, które mają id rodzica równe item.commentID
                    //Następnie robisz kolejne ViewModele z nich i przypisujesz ją do tego dodawanego niżej:

                    commentVMList.Add(new CommentViewModel() {
                        Body = item.Text,
                        Id = item.CommentId,
                        UserName = userName,
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
                return View(pubCom);
            }
            else
                return View("Error");
        }


        [HttpPost]
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


            //rate.Date = DateTime.Now;
            //rate.User = currentUser;
            //rate.Value = pubCom.rateFromCurrUser;
            //rate.Publication = ctx.Publications.Find(pubCom.PublicationId);
            //ctx.Ratings.Add(rate);
            wasRatedBefore = true;

            if (wasRatedBefore)
            {
                ViewBag.RateMessage = "Zmień swoją ocenę Publikacji:";
            }
            else
                ViewBag.RateMessage = "Oceń tę publikację:";

            //currentUser.Ratings.Add(rate);
            //  pub.Ratings.Add(rate);
            ctx.SaveChanges();
            return RedirectToAction("Open");
        }

        public ActionResult Download(int PublicationId)
        {
            var physicalFilePath = (from File f in ctx.Files where f.PublicationId == PublicationId select f.Link_source).First();

            if (physicalFilePath != null)
            {
                return File(physicalFilePath, "File");
            }
            else
                return View("Error");
        }

        [HttpPost]
        public ActionResult AddComment(PublicationPanelViewModel pubCom)
        {
            if (pubCom.NewCommentText.Length > 0)
            {
                IUserService userService = new UserService();
                string userId = User.Identity.GetUserId();
                User currentUser = ctx.Users.Where(u => u.Id == userId).FirstOrDefault();

                var pub = ctx.Publications.Find(pubCom.PublicationId);


                Comment comment = new Comment();

                comment.Text = pubCom.NewCommentText;
                comment.Date = DateTime.Now;
                comment.User = currentUser;
                comment.Publication = ctx.Publications.Where(p => p.PublicationId == pubCom.PublicationId).First();
                pub.Comments.Add(comment);
                ctx.Comments.Add(comment);
                currentUser.Comments.Add(comment);


                ctx.SaveChanges();
                return RedirectToAction("Open", "PublicationPanel");
            }
            else
                return View("Open", pubCom);
        }

        //BEEEE
        //public List<Comment> GetCommentReplies(Comment com)
        //{
        //    var replies = ctx.Comments.Where(c => c.ParentComment.CommentId == com.CommentId).ToList();
        //    replies.OrderBy(c => c.Date);
        //    return replies;
        //}



    }
}