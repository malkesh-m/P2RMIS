<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ReportViewer.aspx.cs" Inherits="Sra.P2rmis.Web.Report.ReportViewer" %>
<%@ Register assembly="Microsoft.ReportViewer.WebForms, Version=15.0.0.0, Culture=neutral, PublicKeyToken=89845dcd8080cc91" namespace="Microsoft.Reporting.WebForms" tagprefix="rsweb" %>
<%@ import Namespace="System.Web.Optimization" %>
<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>P2RMIS Reports</title>
    <asp:PlaceHolder runat="server">
        <%: Scripts.Render("~/bundles/js/report-viewer") %>
        <%: Styles.Render("~/bundles/css/report-viewer") %>
    </asp:PlaceHolder>
</head>
<body>
    <form id="form1" runat="server">
        <style type="text/css">
            #error p,h3 {
                font-family: "Helvetica Neue",Helvetica,Arial,sans-serif;
            }
                        #reportViewer_ctl09 {
                overflow: unset !important;
                overflow: inherit !important;
            }
            .ToolBarButtonsCell {
                background-color: lightgrey;
            }
        </style>
    <div>
        <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
        <rsweb:ReportViewer ID="reportViewer" runat="server" width="100%" height="1060px" AsyncRendering="false" KeepSessionAlive="false"  ></rsweb:ReportViewer>
        <asp:Literal ID="errorMessage" runat="server">
            <div id="error">
                <h3>P2RMIS Reporting</h3>
                <p>We are sorry, the system is having trouble retrieving the report you specified.<br />
                    You may close this window.<br />  
                    You may try it again by adjusting the query parameters.<br />
                    Please contact helpdesk (<a href='mailto:help@p2rmis.com'>help@p2rmis.com</a>)  for more help if this issue persists.
                </p>
                <button onclick="window.close();">Close Window</button>
            </div>
        </asp:Literal>
    </div>
    </form>
</body>
</html>
