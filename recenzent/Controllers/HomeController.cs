using System.Linq;
using System.Web.Mvc;
using recenzent.Data;
using recenzent.Data.Model;
using PagedList;

namespace recenzent.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index(int? page)
        {
            ViewBag.Controller = "Home";
            ViewBag.Action = "Index";
            var ctx = new DataContext();
            var pubSort = (from Publication pub in ctx.Publications orderby pub.ShareDate descending select pub).ToList(); 

            int pageSize = 5;
            int pageNumber = (page ?? 1);
            return View(pubSort.ToPagedList(pageNumber, pageSize));
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}