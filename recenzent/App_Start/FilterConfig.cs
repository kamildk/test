using System.Web;
using System.Web.Mvc;

namespace recenzent
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
            //filters.Add(new System.Web.Mvc.AuthorizeAttribute()); TODO: do właczenie jak wszytko bedzie smigać
            filters.Add(new RequireHttpsAttribute());
        }
    }
}
