using recenzent.Data;
using recenzent.Data.Model;
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
        public ActionResult Open(int id)
        {
            var publication = ctx.Publications.Find(id);
            if (publication != null)
                return View(publication);
            else
                return View("Error");
        }

        public ActionResult Download(int id)
        {
            var physicalFilePath = (from File f in ctx.Files where f.PublicationId == id select f.Link_source).First();

            if (physicalFilePath != null)
            {
                return File(physicalFilePath, "File");
            }
            else
                return View("Error");
        }

    }
}