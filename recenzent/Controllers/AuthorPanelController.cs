using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.IO;
using recenzent.Data;
using recenzent.Data.Model;
using System.Diagnostics;

namespace recenzent.Controllers
{
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

        [HttpPost]
        public ActionResult AddPub(HttpPostedFileBase file, string title, string tags) {

            if(file != null) {
                string path = Server.MapPath("~/Uploads/");
                if (!Directory.Exists(path)) {
                    Directory.CreateDirectory(path);
                }

                file.SaveAs(path + Path.GetFileName(file.FileName));
                ViewBag.Message = "Plik został przesłany";
            }

            string[] tagsSplited = tags.Split(',');
            for (int i = 0; i < tagsSplited.Length; i++) {
                tagsSplited[i] = tagsSplited[i].Trim();
                Debug.WriteLine(tagsSplited[i]);
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