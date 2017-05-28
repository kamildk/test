using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using recenzent.Data;
using recenzent.Data.Model;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.AspNet.Identity;
using PagedList;
using System.Data.Entity;

namespace recenzent.Controllers
{
    public class SearchController : Controller
    {
        private DataContext _ctx = new DataContext();

        // GET: Search
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost()]
        public ActionResult Submit(int? page)
        {
            string query = Request.Params.Get("query").ToLower();
            var pubSort = (from Publication pub in _ctx.Publications orderby pub.ShareDate descending select pub)
                .Include(pub => pub.Author)
                .Include(pub => pub.PublicationTags.Select(pt => pt.Tag))
                .ToList()
                .Where(pub => pub.Title.ToLower().Contains(query)
                || pub.Author.Surname.ToLower().Contains(query)
                || pub.PublicationTags.Where(pt => pt.Tag.Name.ToLower().Contains(query)).Count() > 0
                );

            int pageSize = 5;
            int pageNumber = (page ?? 1);
            return View(pubSort.ToPagedList(pageNumber, pageSize));
        }
    }
}