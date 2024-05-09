using System;
using System.Web.Mvc;
using Sra.P2rmis.Web.Models;
using Telerik.Web.UI;
using Sra.P2rmis.CrossCuttingServices.ConfigurationServices;

namespace Sra.P2rmis.Web.Views.Home
{
    public partial class RadEditorPro : ViewPage    
    {
        #region Page_Init
        protected void Page_Init(object sender, EventArgs e)
        {
            CustomIdentity ident = User.Identity as CustomIdentity;
            //make sure the user is authenticated before proceeding with processing report request
            if (ident == null || !ident.IsAuthenticated)
            {
                Response.Redirect("/ErrorPage/NoAccess");
            }
        }
        #endregion
        #region Page_Load
        protected void Page_Load(object sender, System.EventArgs e)
        {
            if (!string.IsNullOrEmpty(Request.QueryString["name"]) && !string.IsNullOrEmpty(Request.QueryString["userid"])) 
            {
                radEditor1.TrackChangesSettings.Author = Request.QueryString["name"] + " (" + Request.QueryString["userid"] + ")";
            }
            if (!string.IsNullOrEmpty(Request.QueryString["catc"]))
            {
                radEditor1.TrackChangesSettings.CanAcceptTrackChanges = Convert.ToBoolean(Request.QueryString["catc"].Trim());
            }
            if (!String.IsNullOrEmpty(ConfigManager.SpellCheckSettingsDictionaryPath))
            {
                radEditor1.SpellCheckSettings.DictionaryPath = ConfigManager.SpellCheckSettingsDictionaryPath;
            }
            radEditor1.DisableFilter(EditorFilters.FixUlBoldItalic);
        }
        #endregion
    }
}