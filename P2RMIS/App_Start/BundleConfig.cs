using System.Web.Optimization;

namespace Sra.P2rmis.Web
{
    public class BundleConfig
    {
        // For more information on Bundling, visit http://go.microsoft.com/fwlink/?LinkId=254725
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/js/report-viewer")
                        .NonOrdering()
                        .Include(
                            "~/Scripts/jquery/jquery-{version}.js",
                            "~/Scripts/jquery-ui/jquery-ui-{version}.js",
                            "~/Scripts/common/common-globalnamespace.js",
                            "~/Scripts/jquery-custom-modal/js/jquery-custom-modal.js",
                            "~/Scripts/Custom/fileDownload.js",
                            "~/Scripts/report-viewer/report-viewer.js"
                        ));

            bundles.Add(new StyleBundle("~/bundles/css/report-viewer")
                        .NonOrdering()
                        .Include(
                            "~/Content/Site.css",
                            "~/Content/themes/base/jquery-ui-{version}.smoothness.min.css",
                            "~/Content/bootstrap.min.css"
                        ));
          
            bundles.Add(new ScriptBundle("~/bundles/js/pdfjs-viewer")
                       .NonOrdering()
                       .Include(
                           "~/Scripts/jquery/jquery-{version}.js",
                           "~/Scripts/jquery-ui/jquery-ui-{version}.js",
                           "~/Scripts/common/common-globalnamespace.js",
                           "~/Scripts/jquery-custom-modal/js/jquery-custom-modal.js",
                           "~/Scripts/Custom/fileDownload.js"
                       ));

            bundles.Add(new StyleBundle("~/bundles/css/pdfjs-viewer")
                        .NonOrdering()
                        .Include(
                            "~/Content/themes/base/jquery-ui-{version}.smoothness.min.css",
                            "~/Content/bootstrap.min.css",
                            "~/Content/Site.css"
                        ));

            BundleTable.EnableOptimizations = true;

        }
    }
}