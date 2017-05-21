using System.Linq;
using System.Web.Mvc;
using recenzent.Data;
using recenzent.Data.Model;
using PagedList;
using System;

namespace recenzent.Controllers
{
    public class HomeController : Controller
    {
        private DataContext ctx = new DataContext();

        public ActionResult Index(int? page)
        {  
            var pubSort = (from Publication pub in ctx.Publications /*where pub.IsShared == true*/ orderby pub.ShareDate descending select pub).ToList();
           
            int pageSize = 5;
            int pageNumber = (page ?? 1);
            return View(pubSort.ToPagedList(pageNumber, pageSize));
        }

        public ActionResult LatestPublicationsPartial()
        {
            var latestPub = (from Publication pub in ctx.Publications /*where pub.IsShared == true*/ orderby pub.ShareDate descending select pub).ToList().Take(5);
            
            return PartialView("LatestPublicationPartial", latestPub);
        }

    }
}