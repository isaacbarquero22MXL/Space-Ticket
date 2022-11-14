using System.Web;
using System.Web.Optimization;

namespace SIST_SpaceTicket
{
    public class BundleConfig
    {
        // Para obtener más información sobre las uniones, visite https://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            var scriptBundle = new ScriptBundle("~/Scripts/bundle");
            var styleBundle = new StyleBundle("~/Content/bundle");
            // jQuery
            scriptBundle
                .Include("~/Scripts/jquery-3.5.1.js");
            // Bootstrap
            scriptBundle
                .Include("~/Scripts/bootstrap.js");
            // Bootstrap
            styleBundle
                .Include("~/Content/bootstrap.css");
            // Custom site styles
            styleBundle
                .Include("~/Content/Site.css");
            bundles.Add(scriptBundle);
            bundles.Add(styleBundle);
#if !DEBUG
            BundleTable.EnableOptimizations = true;
#endif
        }

    }
}
