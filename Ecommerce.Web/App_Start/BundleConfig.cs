using System.Web.Optimization;

namespace Ecommerce.Web
{
    public class BundleConfig
    {
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/js/jquery").Include(
                "~/Assets/vendor/jquery/jquery.min.js"
            ));

            bundles.Add(new ScriptBundle("~/js/core").Include(
                "~/Assets/vendor/jquery.appear/jquery.appear.min.js",
                "~/Assets/vendor/jquery.easing/jquery.easing.min.js",
                "~/Assets/vendor/jquery-cookie/jquery-cookie.min.js",
                "~/Assets/vendor/bootstrap/js/bootstrap.min.js",
                "~/Assets/vendor/common/common.min.js",
                "~/Assets/vendor/jquery.validation/jquery.validation.min.js",
                "~/Assets/vendor/jquery.easy-pie-chart/jquery.easy-pie-chart.min.js",
                "~/Assets/vendor/jquery.gmap/jquery.gmap.min.js",
                "~/Assets/vendor/jquery.lazyload/jquery.lazyload.min.js",
                "~/Assets/vendor/isotope/jquery.isotope.min.js",
                "~/Assets/vendor/owl.carousel/owl.carousel.min.js",
                "~/Assets/vendor/magnific-popup/jquery.magnific-popup.min.js",
                "~/Assets/vendor/vide/vide.min.js",
                "~/Assets/js/theme.js",
                "~/Assets/js/views/view.contact.js",
                "~/Assets/js/demos/demo-shop-12.js",
                "~/Assets/js/custom.js",
                "~/Assets/js/theme.init.js",
                "~/Assets/js/jquery-migrate-3.0.0.min.js",
                "~/Assets/js/jquery-ui.min.js",
                "~/Assets/js/custom-search-autocomplete.js",
                "~/Assets/js/mustache.min.js",
                "~/Assets/js/cart.js",
                "~/Assets/js/numeral.js",
                "~/Assets/js/jquery.validate.min.js",
                "~/Assets/js/additional-methods.min.js"
            ));

            bundles.Add(new StyleBundle("~/css/base")
                .Include("~/Assets/vendor/bootstrap/css/bootstrap.min.css", new CssRewriteUrlTransform())
                .Include("~/Assets/vendor/font-awesome/css/font-awesome.min.css", new CssRewriteUrlTransform())
                .Include("~/Assets/vendor/animate/animate.min.css", new CssRewriteUrlTransform())
                .Include("~/Assets/vendor/simple-line-icons/css/simple-line-icons.min.css", new CssRewriteUrlTransform())
                .Include("~/Assets/vendor/owl.carousel/assets/owl.carousel.min.css", new CssRewriteUrlTransform())
                .Include("~/Assets/vendor/owl.carousel/assets/owl.theme.default.min.css", new CssRewriteUrlTransform())
                .Include("~/Assets/vendor/magnific-popup/magnific-popup.min.css", new CssRewriteUrlTransform())
                .Include("~/Assets/css/theme.css", new CssRewriteUrlTransform())
                .Include("~/Assets/css/theme-elements.css", new CssRewriteUrlTransform())
                .Include("~/Assets/css/theme-blog.css", new CssRewriteUrlTransform())
                .Include("~/Assets/css/theme-shop.css", new CssRewriteUrlTransform())
                .Include("~/Assets/vendor/rs-plugin/css/settings.css", new CssRewriteUrlTransform())
                .Include("~/Assets/vendor/rs-plugin/css/layers.css", new CssRewriteUrlTransform())
                .Include("~/Assets/vendor/rs-plugin/css/navigation.css", new CssRewriteUrlTransform())
                .Include("~/Assets/css/skins/skin-shop-12.css", new CssRewriteUrlTransform())
                .Include("~/Assets/css/demos/demo-shop-12.css", new CssRewriteUrlTransform())
                .Include("~/Assets/css/jquery-ui.css", new CssRewriteUrlTransform())
                .Include("~/Assets/css/custom.css", new CssRewriteUrlTransform())
            );

            bundles.Add(new ScriptBundle("~/js/modernizr").Include(
                "~/Assets/vendor/modernizr/modernizr.min.js"
            ));

            BundleTable.EnableOptimizations = true;
        }
    }
}