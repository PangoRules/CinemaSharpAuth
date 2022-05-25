using System.Web;
using System.Web.Mvc;

namespace CinemaSharpAuth
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
            filters.Add(new AuthorizeAttribute());
            //This prevents none https to have access to the page
            //filters.Add(new RequireHttpsAttribute());
        }
    }
}
