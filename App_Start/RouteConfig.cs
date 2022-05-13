using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace CinemaSharpAuth
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");
            
            //this enables custom attribute routing in controllers
            routes.MapMvcAttributeRoutes();

            //Custom route to get movies by release date, this method is not commonly used nowdays since
            //in order to update a route, you'll have to update it as well in the controller.
            //routes.MapRoute(
            //    name: "MoviesByReleaseDate",
            //    url: "movies/release/{year}/{month}",
            //    new { controller = "movies", action = "ByReleaseDate" },
            //    new { year = @"\d{4}", month = @"\d{2}" }
            //);

            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}
