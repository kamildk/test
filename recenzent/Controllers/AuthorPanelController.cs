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
    [Authorize(Roles = "Author")]
    public class AuthorPanelController : Controller
    {
        // GET: AuthorPanel
        //[Authorize(Users = "author")]
        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public ActionResult AddPub() {
            return View();
        }

        //[HttpPost]
        //public ActionResult AddPub(HttpPostedFileBase file, string title, string tags) {

        //    //File
        //    if (file != null) {
        //        string path = Server.MapPath("~/Uploads/");
        //        if (!Directory.Exists(path)) {
        //            Directory.CreateDirectory(path);
        //        }

        //        file.SaveAs(path + Path.GetFileName(file.FileName));
        //    }

        //    //Tags
        //    string[] tagsSplited = tags.Split(',');
        //    for (int i = 0; i < tagsSplited.Length; i++) {
        //        tagsSplited[i] = tagsSplited[i].Trim();
        //    }

        //    ITagsService service = new TagsService();
        //    service.AddTags(tagsSplited.ToList());

        //    return View();
        //}

        [HttpPost]
        public ActionResult AddPub(PublicationViewModel model) {

            if (ModelState.IsValid) {
                var ctx = new DataContext();
                Publication publication = new Publication();

                //IUserService userService = new UserService();
                string userId = User.Identity.GetUserId();
                User currentUser = ctx.Users.Where(u => u.Id == userId).FirstOrDefault(); //userService.GetUser(User.Identity.GetUserId());

                //Tags
                string[] tagsSplited = model.Tags.Split(',');
                for (int i = 0; i < tagsSplited.Length; i++) {
                    tagsSplited[i] = tagsSplited[i].Trim();
                }

                ITagsService tagsService = new TagsService();
                tagsService.AddTags(tagsSplited.ToList());

                //Publication tags
                List<PublicationTag> pubTags = new List<PublicationTag>();
                for (int i = 0; i < tagsSplited.Length; i++) {
                    Tag tag = tagsService.GetTag(tagsSplited[i]);
                    if (tagsSplited != null) {
                        pubTags.Add(new PublicationTag() { Publication = publication, Tag = tag });
                    }
                }

                ctx.Publication_Tags.AddRange(pubTags);

                //File
                string filePath = Server.MapPath("~/Publications/");
                if (!Directory.Exists(filePath)) {
                    Directory.CreateDirectory(filePath);
                }

                string fileName = publication.PublicationId.ToString() + DateTime.Now.ToShortDateString();
                model.File.SaveAs(filePath + fileName);

                Data.Model.File file = new Data.Model.File() {
                    Name = fileName,
                    Link_source = filePath + fileName,
                    IsCurrent = true,
                    Publication = publication
                };

                ctx.Files.Add(file);

                //publication.Author = currentUser;
                publication.Title = model.Title;
                publication.PublicationTags = pubTags;
                publication.Description = model.Description;
                publication.Files.Add(file);
                //publication.ShareDate = DateTime.Now;

                currentUser.Publications.Add(publication);

                ctx.Publications.Add(publication);

                ctx.SaveChanges();
            }

            return View();
        }

        public ActionResult PublicationList() {

            using (var context = new Data.DataContext()) {

                var pubList = context.Publications.ToList();

                return View(pubList);
            }
        }
    }
}