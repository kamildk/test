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
                Debug.WriteLine("Buahahahah" + model.Title);

            }
            else {
                Debug.WriteLine("RIP");
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