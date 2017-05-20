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


namespace recenzent.Controllers
{
    [Authorize(Roles = "Reviewer")]
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
        public ActionResult AddReview( int pubId = -1)
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
            
                if (ModelState.IsValid && model.File != null && model.pubId>0)
                {
                using (DataContext ctx = new DataContext())
                {
                    Review review = new Review();

                    IUserService userService = new UserService();
                    string userId = User.Identity.GetUserId();
                    User currentUser = ctx.Users.Where(u => u.Id == userId).FirstOrDefault();

                    ReviewState state = ctx.ReviewStates.Where(s => s.Name == "Przydzielony").FirstOrDefault();
                    review.CurrentState = state;

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

                    Publication pub = ctx.Publications.Find(model.pubId);
                    ctx.Files.Add(file);

                    review.User = currentUser;
                    DateTime date = DateTime.Today;
                    review.Creation_date = date;
                    date = date.AddDays(14);
                    review.Expiration_date = date;
                    //review.PublicationId = model.pubId;
                    review.Publication = pub;
                    review.Files.Add(file);

                    currentUser.Reviews.Add(review);

                    ctx.Reviews.Add(review);

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
                              where pub.AuthoId == userId && pub.IsShared == false
                              select pub;

                return View(pubList.ToList());
            }
        }

        public ViewResult PublicationDetails(int id = 6)
        {
            using (var ctx = new DataContext())
            {
                Publication pub = ctx.Publications.Where(p => p.PublicationId == id).FirstOrDefault();
                return View(pub);
            }
        }
    }
}