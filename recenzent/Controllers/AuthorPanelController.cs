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
using System.Data.Entity.Migrations;

namespace recenzent.Controllers {
    [Authorize(Roles = "Author")]
    public class AuthorPanelController : Controller {
        // GET: AuthorPanel
        public ActionResult Index() {
            return View();
        }

        [HttpGet]
        public ActionResult EditPub(int id) {
            using (var ctx = new DataContext()) {
                ViewBag.Categories = ctx.Publication_Categories.Select(c => c.Name).ToList();

                Publication pub = ctx.Publications.Where(p => p.PublicationId == id).FirstOrDefault();

                PublicationViewModel vm = new PublicationViewModel() {
                    Id = id,
                    Title = pub.Title,
                    Description = pub.Description,
                    Category = "Fizyka"
                };

                var tags = (from PublicationTag tag in ctx.Publication_Tags
                            where tag.PublicationId == id
                            select tag.Tag.Name).ToList();
                string tagStr = tags[0];
                for (int i = 1; i < tags.Count; i++) {
                    tagStr += ", " + tags[i];
                }

                vm.Tags = tagStr;

                return View(vm);
            }
        }

        [HttpPost]
        public ActionResult EditPub(PublicationViewModel model) {
            if (ModelState.IsValid) {
                var ctx = new DataContext();
                Publication publication = ctx.Publications.Where(p => p.PublicationId == model.Id).FirstOrDefault();

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
                ICategoryService categoryService = new CategoryService();
                Publication_category category = categoryService.GetCategory(model.Category);

                #region Sources
                //sources
                //string[] sourcesSplited = model.Sources.Split('\n');
                //for (int i = 0; i < sourcesSplited.Length; i++) {
                //    sourcesSplited[i] = sourcesSplited[i].Trim();
                //}
                //var sourcesList = sourcesSplited.ToList();
                //sourcesList.RemoveAt(sourcesList.Count - 1);

                //ISourceService sourceService = new SourceService();
                //sourceService.AddSources(sourcesList);

                ////source position
                //List<SourcePosition> sourcePositions = new List<SourcePosition>();
                //for (int i = 0; i < sourcesSplited.Length - 1; i++) {
                //    string sourceName = sourcesSplited[i];
                //    Source source = ctx.Sources.Where(s => s.Name == sourceName).FirstOrDefault();
                //    SourcePosition position = new SourcePosition() {
                //        Source = source,
                //        Publication = publication
                //    };
                //    sourcePositions.Add(position);
                //}

                //ctx.SourcePositions.AddRange(sourcePositions);
                #endregion
                //File
                string filePath = Server.MapPath("~/Publications/");
                if (!Directory.Exists(filePath)) {
                    Directory.CreateDirectory(filePath);
                }

                string fileName = model.File.FileName;
                fileName = fileName.Substring(fileName.LastIndexOf("\\") + 1);

                model.File.SaveAs(filePath + fileName);

                Data.Model.File file = new Data.Model.File() {
                    Name = fileName,
                    Link_source = filePath + fileName,
                    IsCurrent = true,
                    Publication = publication
                };

                //IFileService fileService = new FileService();
                //fileService.AddFile(file);
                ctx.Files.Add(file);

                publication.Author = currentUser;
                publication.Title = model.Title;
                publication.PublicationTags = pubTags;
                publication.Description = model.Description;
                //publication.SourcePositions = sourcePositions;
                publication.Category = category;
                publication.Files.Add(file);

                currentUser.Publications.Add(publication);

                ctx.Publications.AddOrUpdate(publication);

                ctx.SaveChanges();
                return RedirectToAction("Index");
            }

            return View();
        }

        [HttpGet]
        public ActionResult AddPub() {

            using (var ctx = new DataContext()) {
                ViewBag.Categories = ctx.Publication_Categories.Select(c => c.Name).ToList();
            }

            return View();
        }


        [HttpPost]
        public ActionResult AddPub(PublicationViewModel model) {

            if (ModelState.IsValid) {
                Publication publication = new Publication();

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

                //category
                ICategoryService categoryService = new CategoryService();
                Publication_category category = categoryService.GetCategory(model.Category);

                #region Sources
                //sources
                //string[] sourcesSplited = model.Sources.Split('\n');
                //for (int i = 0; i < sourcesSplited.Length; i++) {
                //    sourcesSplited[i] = sourcesSplited[i].Trim();
                //}
                //var sourcesList = sourcesSplited.ToList();
                //sourcesList.RemoveAt(sourcesList.Count - 1);

                //ISourceService sourceService = new SourceService();
                //sourceService.AddSources(sourcesList);

                ////source position
                //List<SourcePosition> sourcePositions = new List<SourcePosition>();
                //for (int i = 0; i < sourcesSplited.Length - 1; i++) {
                //    string sourceName = sourcesSplited[i];
                //    Source source = ctx.Sources.Where(s => s.Name == sourceName).FirstOrDefault();
                //    SourcePosition position = new SourcePosition() {
                //        Source = source,
                //        Publication = publication
                //    };
                //    sourcePositions.Add(position);
                //}

                //ctx.SourcePositions.AddRange(sourcePositions);
                #endregion
                //File
                string filePath = Server.MapPath("~/Publications/");
                if (!Directory.Exists(filePath)) {
                    Directory.CreateDirectory(filePath);
                }

                string fileName = model.File.FileName;
                fileName = fileName.Substring(fileName.LastIndexOf("\\") + 1);

                model.File.SaveAs(filePath + fileName);

                Data.Model.File file = new Data.Model.File() {
                    Name = fileName,
                    Link_source = filePath + fileName,
                    IsCurrent = true,
                    Publication = publication
                };

                publication.AuthoId = User.Identity.GetUserId();
                publication.Title = model.Title;
                publication.PublicationTags = pubTags;
                publication.Description = model.Description;
                //publication.SourcePositions = sourcePositions;
                publication.CategoryId = category.Publication_categoryId;
                publication.Files.Add(file);

                IPublicationService pubService = new PublicationService();
                pubService.AddPublication(publication);

                return RedirectToAction("Index");
            }

            return View();
        }


        public ActionResult PublicationList() {

            //using (var context = new Data.DataContext()) {
            //    string userId = User.Identity.GetUserId();
            //    var pubList = from Publication pub in context.Publications
            //                  where pub.AuthoId == userId && pub.IsShared == true
            //                  select pub;

            //    return View(pubList.ToList());
            //}
            string userId = User.Identity.GetUserId();
            IPublicationService pubService = new PublicationService();
            var pubList = pubService.GetPublicationList(p => p.AuthoId == userId && p.IsShared == true);
            return View(pubList);
        }

        public ActionResult PublicationInReviewList() {

            //using (var context = new Data.DataContext()) {
            //    string userId = User.Identity.GetUserId();
            //    var pubList = from Publication pub in context.Publications
            //                  where pub.AuthoId == userId && pub.IsShared == false
            //                  select pub;

            //    return View(pubList.ToList());
            //}

            string userId = User.Identity.GetUserId();
            IPublicationService pubService = new PublicationService();
            var pubList = pubService.GetPublicationList(p => p.AuthoId == userId && p.IsShared == false);
            return View(pubList);
        }

        public ActionResult PublicationReview(int id) {
            //using (var ctx = new DataContext()) {
            //    Publication pub = ctx.Publications.Where(p => p.PublicationId == id).FirstOrDefault();

            //    var reviews = from Review r in ctx.Reviews
            //                  join ReviewState s in ctx.ReviewStates on r.CurrentStateId equals s.Id
            //                  where r.PublicationId == id
            //                  select new { Review = r, State = s };

            //    List<PublicationReviewViewModel> reviewList = new List<PublicationReviewViewModel>();
            //    foreach (var item in reviews) {
            //        reviewList.Add(new PublicationReviewViewModel() {
            //            Id = item.Review.ReviewId,
            //            AddDate = item.Review.Creation_date,
            //            State = item.State.Name
            //        });
            //    }

            //    PublicationReviewListViewModel vm = new PublicationReviewListViewModel() {
            //        Id = id,
            //        Title = pub.Title,
            //        Description = pub.Description,
            //        Category = pub.Category.Name
            //    };

            //    vm.Tags = (from PublicationTag tag in ctx.Publication_Tags
            //               where tag.PublicationId == pub.PublicationId select tag.Tag.Name).ToList();
            //    //vm.Sources = (from SourcePosition source in ctx.SourcePositions
            //    //              where source.PublicationId == pub.PublicationId select source.Source.Name).ToList();
            //    vm.Reviews = reviewList;

            //    return View(vm);
            //}
            IPublicationService pubService = new PublicationService();
            IPublicationTagsService pubsTagsService = new PublicationTagsService();
            ITagsService tagsService = new TagsService();
            //Tu będzie kiedyś ReviewService

            Publication pub = pubService.GetPublication(p => p.PublicationId == id);

            using (var ctx = new DataContext()) {
                var reviews = from Review r in ctx.Reviews
                              join ReviewState s in ctx.ReviewStates on r.CurrentStateId equals s.Id
                              where r.PublicationId == id
                              select new { Review = r, State = s };

                List<PublicationReviewViewModel> reviewList = new List<PublicationReviewViewModel>();
                foreach (var item in reviews) {
                    reviewList.Add(new PublicationReviewViewModel() {
                        Id = item.Review.ReviewId,
                        AddDate = item.Review.Creation_date,
                        State = item.State.Name
                    });
                }
            }

            PublicationReviewListViewModel vm = new PublicationReviewListViewModel() {
                Id = id,
                Title = pub.Title,
                Description = pub.Description,
                Category = pub.Category.Name
            };

            var pubTagsResult = pubsTagsService.GetPublicationTagsList(t => t.PublicationId == pub.PublicationId).ToList();
            List<string> tags = new List<string>();
            foreach (var item in pubTagsResult) {
                tags.Add(tagsService.GetTag(item.TagId).Name);
            }
            vm.Tags = tags;

            return View(vm);
        }

        public ActionResult DownloadReview(int id) {
            using (var ctx = new DataContext()) {
                var result = (from Review review in ctx.Reviews
                              where review.ReviewId == id select review).FirstOrDefault();
                var filePath = (from Data.Model.File file in ctx.Files
                                where file.IsCurrent == true && file.ReviewId == result.ReviewId
                                select file.Link_source).FirstOrDefault();

                if (filePath != null) {
                    return File(filePath, "application/pdf");
                }
                else {
                    return View("Error");
                }
            }
        }
    }
}