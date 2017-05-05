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
    }
}