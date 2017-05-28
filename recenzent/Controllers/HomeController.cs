using System.Linq;
using System.Web.Mvc;
using recenzent.Data;
using recenzent.Data.Model;
using PagedList;
using System;
using System.Threading;
using System.Globalization;
using System.Web;
using recenzent.Data.Interface;
using recenzent.Data.Service;
using recenzent.Models;
using System.Collections.Generic;

namespace recenzent.Controllers
{
    public class HomeController : Controller
    {

        public ActionResult Index(int? page)
        {
            IPublicationService pubService = new PublicationService();
            var pubList = pubService.GetPublicationListOrderedByDate(p => p.IsShared == false, false);

            List<PublicationHomeViewModel> pubVMList = new List<PublicationHomeViewModel>();
            foreach (var item in pubList)
            {
                string categoryName;
                if (item.Category == null)
                {
                    categoryName = "";
                }
                else
                {
                    categoryName = item.Category.Name;
                }
                string pubDate;
                if (item.ShareDate == null)
                {
                    pubDate = "";
                }
                else
                {
                    pubDate = item.ShareDate.ToString();
                }

                pubVMList.Add(new PublicationHomeViewModel()
                {
                    Id = item.PublicationId,
                    Title = item.Title,
                    Description = item.Description,
                    Category = categoryName,
                    AuthorName = item.Author.Name + " " + item.Author.Surname,
                    ShareDate = pubDate
                });
            }

            int pageSize = 5;
            int pageNumber = (page ?? 1);
            return View(pubVMList.ToPagedList(pageNumber, pageSize));
        }

        public ActionResult LatestPublicationsPartial()
        {
            IPublicationService pubService = new PublicationService();
            var pubList = pubService.GetPublicationListOrderedByDate(p => p.IsShared == false, false).Take(5);
            return PartialView("LatestPublicationPartial", pubList);
        }

        public ActionResult ChangeLanguage(string lang)
        {
            if(lang != null)
            {
                Thread.CurrentThread.CurrentCulture = CultureInfo.CreateSpecificCulture(lang);
                Thread.CurrentThread.CurrentUICulture = new CultureInfo(lang);
            }

            HttpCookie cookie = new HttpCookie("Language");
            cookie.Value = lang;
            Response.Cookies.Add(cookie);

            return RedirectToAction("Index");
        }
    }
}