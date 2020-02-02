using System.Web;
using System.Web.Optimization;

namespace Geyser
{
    public class BundleConfig
    {
        // For more information on bundling, visit https://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
               "~/Scripts/jquery-{version}.js"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
                        "~/Scripts/jquery.validate*"));

            // Use the development version of Modernizr to develop with and learn from. Then, when you're
            // ready for production, use the build tool at https://modernizr.com to pick only the tests you need.
            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                        "~/Scripts/modernizr-*"));

            bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
                      "~/Scripts/bootstrap.js",
                      "~/Scripts/respond.js"));

            bundles.Add(new StyleBundle("~/Content/css").Include(
                      "~/Content/bootstrap.css",
                      "~/Content/site.css"));
            bundles.Add(new StyleBundle("~/Content/Display/Style/css").Include(
               "~/Content/Display/style/default.css",
  "~/Content/Display/style/default_Rs.css",
   "~/Content/Display/Css/baogia.css",
  "~/Content/Display/Css/Baogia_Rs.css",
   "~/Content/Display/style/font-awesome.css",
  "~/Content/Display/style/font-awesome.min.css",
  "~/Content/Display/style/news.css",
     "~/Content/Display/style/news_Rs.css",
 "~/Content/Display/style/nivo-slider.css",
  "~/Content/Display/style/owl.carousel.min.css",
  "~/Content/Display/style/owl.theme.default.min.css",
 "~/Content/Display/style/product.css",
  "~/Content/Display/style/product_Rs.css",
 "~/Content/Display/style/Order.css",
  "~/Content/Display/style/Order_Rs.css",
    "~/Content/Display/style/contact.css",
  "~/Content/Display/style/contact_Rs.css",
      "~/Content/Display/style/baogia.css",
  "~/Content/Display/style/baogia_rs.css",
    "~/Content/Display/style/feedback.css",
     "~/Content/Display/style/jquery.mmenu.all.css"
              ));
            BundleTable.EnableOptimizations = true;
        }
    }
}
