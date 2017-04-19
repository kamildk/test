using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.IO;

namespace recenzent.Controllers
{
    public class AuthorPanelController : Controller
    {
        // GET: AuthorPanel
        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public ActionResult AddPub() {
            return View();
        }

        [HttpPost]
        public ActionResult AddPub(HttpPostedFileBase file) {

            if(file != null) {
                string path = Server.MapPath("~/Uploads/");
                if (!Directory.Exists(path)) {
                    Directory.CreateDirectory(path);
                }

                file.SaveAs(path + Path.GetFileName(file.FileName));
                ViewBag.Message = "Plik przesłany";
            }

            return View();
        }
    }
}