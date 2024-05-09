using System.IO;
using System.Web.Mvc;

namespace Sra.P2rmis.Web.Controllers
{
    public static class VersionedFiles
    {
        public static MvcHtmlString IncludeVersionedFiles(this HtmlHelper helper, string path) {
            var actualPath = System.Web.Hosting.HostingEnvironment.ApplicationPhysicalPath;
            FileInfo file = new FileInfo(actualPath + path);
            var getLastWrite = file.LastWriteTime.ToString("MMddHHmmss");
            var versionExt = file.Extension;

            // Js Files
            if(versionExt == ".js")
            {
                return MvcHtmlString.Create("<script type='text/javascript' src='"+ path + "?" + getLastWrite + "'></script>");
            }
            // CSS Files
            else if (versionExt == ".css")
            {
                return MvcHtmlString.Create("<link href='"+ path + "?" + getLastWrite + "' type='text/css' rel='stylesheet'></script>");
            }          
            // No html added to this path on return
            return MvcHtmlString.Create("");
        }  
    }
}