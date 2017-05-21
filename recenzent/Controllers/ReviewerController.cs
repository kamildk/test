using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.IO;
using recenzent.Data;
using recenzent.Data.Model;
using System.Diagnostics;

using recenzent.Data.Interface;
using recenzent.Data.Service;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.AspNet.Identity.EntityFramework;
using recenzent.Models;
using System.Data.Entity.Infrastructure;
using System.Data.Entity;


namespace recenzent.Controllers
{
    //[Authorize(Roles = "Reviewer")]
    public class ReviewerController : Controller
    {
        // GET: Reviewer
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult DownloadPub()
        {
            return View();
        }

        /*
        [HttpPost]
        public ActionResult UploadRev(HttpPostedFileBase file, string title)
        {
            if (file != null)
            {
                string path = Server.MapPath("~/Uploads/");
                if (!Directory.Exists(path))
                {
                    Directory.CreateDirectory(path);
                }

                file.SaveAs(path + Path.GetFileName(file.FileName));
                ViewBag.Message = "Plik został przesłany";
            }

            return View();
        }
        */

        //-------------------------------
        //[HttpGet]
        //public ActionResult AddReview()
        //{

        //    using (var ctx = new DataContext())
        //    {
        //        ViewBag.Categories = ctx.Publication_Categories.Select(c => c.Name).ToList();
        //    }

        //    return View();
        //}

        [HttpGet]
        public ActionResult AddReview(int pubId = -1)
        {
            using (var ctx = new DataContext())
            {
                ViewBag.Categories = ctx.Publication_Categories.Select(c => c.Name).ToList();

                Publication pub = ctx.Publications.Where(p => p.PublicationId == pubId).FirstOrDefault();
                ReviewViewModel _model = new ReviewViewModel();
                _model.pubId = pub.PublicationId;
                return View(_model);
            }
        }
        [HttpPost]
        public ActionResult AddReview(ReviewViewModel model)
        {
            if (ModelState.IsValid && model.File != null && model.pubId > 0)
            {
                using (DataContext ctx = new DataContext())
                {
                    //Review review = new Review();

                    IUserService userService = new UserService();
                    string userId = User.Identity.GetUserId();
                    User currentUser = ctx.Users.Where(u => u.Id == userId).FirstOrDefault();

                    var revId = from Review rev in ctx.Reviews
                                where rev.PublicationId == model.pubId && rev.UserId == userId
                                select rev.ReviewId;
                    int reviewId = revId.First();

                    Review review = ctx.Reviews.Find(reviewId);
                    Publication pub = ctx.Publications.Find(model.pubId);

                    //ReviewState state = ctx.ReviewStates.Where(s => s.Name == "Przydzielony").FirstOrDefault();
                    //review.CurrentState = state;

                    //File
                    string filePath = Server.MapPath("~/Reviews/");
                    if (!Directory.Exists(filePath))
                    {
                        Directory.CreateDirectory(filePath);
                    }

                    string fileName = model.File.FileName;
                    fileName = fileName.Substring(fileName.LastIndexOf("\\") + 1);

                    model.File.SaveAs(filePath + fileName);

                    Data.Model.File file = new Data.Model.File()
                    {
                        Name = fileName,
                        Link_source = filePath + fileName,
                        IsCurrent = true,
                        Review = review
                    };
                    ctx.Files.Add(file);

                    //var original = ctx.Reviews.Find(review);

                    if (review != null)
                    {
                        //review.User = currentUser;
                        DateTime date = DateTime.Now;
                        //review.Creation_date = date;
                        date = date.AddDays(14);
                        review.Expiration_date = date;
                        //review.PublicationId = model.pubId;
                        //review.Publication = pub;
                        review.Files.Add(file);
                        review.CurrentStateId = 2;
                        //ctx.SaveChanges();
                    }

                    //currentUser.Reviews.Add(review);

                    //ctx.Reviews.Add(review);
                    var fId = from Data.Model.File f in ctx.Files
                              where f.ReviewId == reviewId
                              select f.FileId;
                    int fileId = fId.First();

                    Data.Model.File fileCheck = ctx.Files.Find(fileId);
                    if (fileCheck != null)
                    {
                        ReviewStateHistory stateHistory = new ReviewStateHistory();
                        stateHistory.ChangeDate = DateTime.Now;
                        stateHistory.StateId = review.CurrentStateId;
                        stateHistory.ReviewId = reviewId;
                        ctx.ReviewStateHistory.Add(stateHistory);
                    }

                    review.Publication = pub;
                    pub.Reviews.Add(review); //Tu moze byc problem
                    ctx.Entry(pub).State = EntityState.Modified;
                    ctx.Entry(pub.Reviews).State = EntityState.Modified;
                    ctx.SaveChanges();
                    return RedirectToAction("Index");
                }
            }
            return View(model);
        }

        public ActionResult PublicationList()
        {

            using (var context = new Data.DataContext())
            {
                string userId = User.Identity.GetUserId();
                var pubList = from Publication pub in context.Publications
                              where pub.AuthoId == userId && pub.IsShared == true
                              select pub;

                return View(pubList.ToList());
            }
        }

        public ActionResult PublicationsToReview()
        {

            using (var context = new Data.DataContext())
            {
                string userId = User.Identity.GetUserId();

                var pubList = from Publication pub in context.Publications
                              from Review rev in context.Reviews
                              where rev.UserId == userId && pub.PublicationId == rev.PublicationId && rev.CurrentStateId == 1
                              select pub;
                return View(pubList.ToList());
            }
        }

        public ActionResult ReviewsAccepted()
        {
            using (var context = new Data.DataContext())
            {
                string userId = User.Identity.GetUserId();
                var pubList = from Publication pub in context.Publications
                              from Review rev in context.Reviews
                              where rev.UserId == userId && pub.PublicationId == rev.PublicationId && rev.CurrentStateId == 2
                              select pub;

                return View(pubList.ToList());
            }
        }

        public ActionResult ReviewsWaiting()
        {
            using (var context = new Data.DataContext())
            {
                string userId = User.Identity.GetUserId();
                var pubList = from Publication pub in context.Publications
                              from Review rev in context.Reviews
                              where rev.UserId == userId && pub.PublicationId == rev.PublicationId && rev.CurrentStateId == 3
                              select pub;

                return View(pubList.ToList());
            }
        }

        public ViewResult PublicationDetails(int id)
        { 
            using (var ctx = new DataContext())
            {
                Publication pub = ctx.Publications.Where(p => p.PublicationId == id).FirstOrDefault();
                return View(pub);
            }
        }

        public ActionResult DownloadPublication(ReviewViewModel model)
        {
            using (DataContext ctx = new DataContext())
            {
                var physicalFilePath = (from Data.Model.File f in ctx.Files where f.PublicationId == model.pubId select f.Link_source).FirstOrDefault();

                if (physicalFilePath != null)
                {
                    return File(physicalFilePath, "File");
                }
                else
                    return View("Error");
            }
        }
    }
}