using System.Web.Mvc;
using System.Web.Routing;

namespace Ecommerce.Web
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");
            routes.IgnoreRoute("{*botdetect}", new { botdetect = @"(.*)BotDetectCaptcha\.ashx" });

            routes.MapRoute(
                name: "Home1",
                url: "index.html",
                defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional },
                namespaces: new string[] { "Ecommerce.Web.Controllers" }
            );

            routes.MapRoute(
                name: "Home2",
                url: "trang-chu.html",
                defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional },
                namespaces: new string[] { "Ecommerce.Web.Controllers" }
            );

            routes.MapRoute(
                name: "About",
                url: "gioi-thieu.html",
                defaults: new { controller = "About", action = "Index", id = UrlParameter.Optional },
                namespaces: new string[] { "Ecommerce.Web.Controllers" }
            );

            routes.MapRoute(
                name: "Login",
                url: "dang-nhap.html",
                defaults: new { controller = "Account", action = "Login", id = UrlParameter.Optional },
                namespaces: new string[] { "Ecommerce.Web.Controllers" }
            );

            routes.MapRoute(
                name: "Register",
                url: "dang-ky.html",
                defaults: new { controller = "Account", action = "Register", id = UrlParameter.Optional },
                namespaces: new string[] { "Ecommerce.Web.Controllers" }
            );

            routes.MapRoute(
                name: "Search",
                url: "tim-kiem.html",
                defaults: new { controller = "Product", action = "Search", id = UrlParameter.Optional },
                namespaces: new string[] { "Ecommerce.Web.Controllers" }
            );

            routes.MapRoute(
                name: "Product Category",
                url: "{alias}.pc-{id}.html",
                defaults: new { controller = "Product", action = "Category", id = UrlParameter.Optional },
                namespaces: new string[] { "Ecommerce.Web.Controllers" }
            );

            routes.MapRoute(
                name: "Product",
                url: "{alias}.p-{productID}.html",
                defaults: new { controller = "Product", action = "Detail", productID = UrlParameter.Optional },
                namespaces: new string[] { "Ecommerce.Web.Controllers" }
            );

            routes.MapRoute(
                name: "TagList",
                url: "tag/{tagid}.html",
                defaults: new { controller = "Product", action = "ListByTag", tagid = UrlParameter.Optional },
                namespaces: new string[] { "Ecommerce.Web.Controllers" }
            );

            routes.MapRoute(
                name: "Cart",
                url: "gio-hang.html",
                defaults: new { controller = "Cart", action = "Index", id = UrlParameter.Optional },
                namespaces: new string[] { "Ecommerce.Web.Controllers" }
            );

            routes.MapRoute(
                name: "Checkout",
                url: "thanh-toan.html",
                defaults: new { controller = "Cart", action = "Checkout", id = UrlParameter.Optional },
                namespaces: new string[] { "Ecommerce.Web.Controllers" }
            );

            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional },
                namespaces: new string[] { "Ecommerce.Web.Controllers" }
            );
        }
    }
}