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
        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public ActionResult AddPub() {

            using(var ctx = new DataContext()) {
                ViewBag.Categories = ctx.Publication_Categories.Select(c => c.Name).ToList();
            }

            return View();
        }

        void AddSources(List<string> sources) {
            using (var ctx = new DataContext()) {
                foreach (var item in sources) {
                    if (!ctx.Sources.Any(t => t.Name == item)) {
                        Source source = new Source() { Name = item };
                        ctx.Sources.Add(source);
                    }
                }

                ctx.SaveChanges();
            }
        }

        [HttpPost]
        public ActionResult AddPub(PublicationViewModel model) {

            if (ModelState.IsValid) {
                var ctx = new DataContext();
                Publication publication = new Publication();

                IUserService userService = new UserService();
                string userId = User.Identity.GetUserId();
                User currentUser = ctx.Users.Where(u => u.Id == userId).FirstOrDefault();
                //User currentUser = userService.GetDBUser(User.Identity.GetUserId());

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

                //category
                Publication_category category = ctx.Publication_Categories.Where(c => c.Name == model.Category).FirstOrDefault();

                //sources
                string[] sourcesSplited = model.Sources.Split('\n');
                for (int i = 0; i < sourcesSplited.Length; i++) {
                    sourcesSplited[i] = sourcesSplited[i].Trim();
                }
                var l = sourcesSplited.ToList();
                l.RemoveAt(l.Count - 1);

                AddSources(l);

                //source position
                List<SourcePosition> sourcePositions = new List<SourcePosition>();
                for (int i = 0; i < sourcesSplited.Length-1; i++) {
                    string sourceName = sourcesSplited[i];
                    Source source = ctx.Sources.Where(s => s.Name == sourceName).FirstOrDefault();
                    SourcePosition position = new SourcePosition() {
                        Source = source,
                        Publication = publication
                    };
                    sourcePositions.Add(position);
                }

                ctx.SourcePositions.AddRange(sourcePositions);

                //File
                string filePath = Server.MapPath("~/Publications/");
                if (!Directory.Exists(filePath)) {
                    Directory.CreateDirectory(filePath);
                }

                string fileName = model.File.FileName;
                model.File.SaveAs(filePath + fileName);

                Data.Model.File file = new Data.Model.File() {
                    Name = fileName,
                    Link_source = filePath + fileName,
                    IsCurrent = true,
                    Publication = publication
                };

                ctx.Files.Add(file);

                publication.Author = currentUser;
                publication.Title = model.Title;
                publication.PublicationTags = pubTags;
                publication.Description = model.Description;
                publication.SourcePositions = sourcePositions;
                publication.Category = category;
                publication.Files.Add(file);

                currentUser.Publications.Add(publication);

                ctx.Publications.Add(publication);

                ctx.SaveChanges();
                return RedirectToAction("Index");
            }

            return View();
        }


        public ActionResult PublicationList() {

            using (var context = new Data.DataContext()) {
                string userId = User.Identity.GetUserId();
                var pubList = from Publication pub in context.Publications
                              where pub.AuthoId == userId && pub.IsShared == true
                              select pub;

                return View(pubList.ToList());
            }
        }

        public ActionResult PublicationInReviewList() {

            using (var context = new Data.DataContext()) {
                string userId = User.Identity.GetUserId();
                var pubList = from Publication pub in context.Publications
                              where pub.AuthoId == userId && pub.IsShared == false
                              select pub;

                return View(pubList.ToList());
            }
        }

        public ActionResult PublicationReview(int id = 6) {
            using (var ctx = new DataContext()) {
                Publication pub = ctx.Publications.Where(p => p.PublicationId == id).FirstOrDefault();
                
                return View(pub);
            }
        }
    }
}