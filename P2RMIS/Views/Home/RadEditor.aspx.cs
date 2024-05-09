using System;
using System.Web.Mvc;
using System.IO;

namespace Sra.P2rmis.Web.Views.Home
{
    public partial class RadEditor : ViewPage    
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Request.QueryString["type"]?.ToLower() == "evaluation")
            {
                radEditor1.ToolsFile = "/Content/telerik/editortoolbar.evaluation.xml";
                radEditor1.Height = 250;
                radEditor1.Content = "Start typing instructions here...";
            }
        }
    }
}