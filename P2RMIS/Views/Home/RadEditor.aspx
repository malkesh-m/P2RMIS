<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="RadEditor.aspx.cs" Inherits="Sra.P2rmis.Web.Views.Home.RadEditor" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Telerik Editor</title>
    <script src="/Scripts/jquery/jquery-1.12.1.min.js"></script>
</head>
<body class="margin0">
    <form id="form1" runat="server">
        <asp:ScriptManager runat="server"></asp:ScriptManager>
        <telerik:RadEditor ID="radEditor1" runat="server" Width="100%" Height="400px" OnClientLoad="OnClientLoad" OnClientPasteHtml="OnClientPasteHtml"
            ToolsFile="/Content/telerik/editortoolbar.basic.xml" EnableResize="false"
            SpellCheckSettings-AjaxUrl="/Telerik.Web.UI.SpellCheckHandler.axd"
            DialogHandlerUrl="/Telerik.Web.UI.DialogHandler.axd"
            StripFormattingOptions="AllExceptNewLines, MSWordRemoveAll"
            EditModes="Design"
            AccessKey="1">
            <Content>   
            Loading...
            </Content>
        </telerik:RadEditor>
    </form>
    <script type="text/javascript">   
        // Global variables
        var EditorFrameId = "radEditor1_contentIframe";
        var EditorId = GetParameterByName("id");
        var EditorHeight = GetParameterByName("height");
        var IsLoaded = false;

        // Get URL parameter value by name
        function GetParameterByName(name) {
            name = name.replace(/[\[]/, "\\[").replace(/[\]]/, "\\]");
            var regex = new RegExp("[\\?&]" + name + "=([^&#]*)"),
                results = regex.exec(location.search);
            return results === null ? "" : decodeURIComponent(results[1].replace(/\+/g, " "));
        }
        // Get HTML content in editor
        function GetEditorHtmlContent() {
            var editor = $find("<%=radEditor1.ClientID%>");
            var someContent = editor.get_html();
            var strippedContent = someContent.replace(/\<img .+?\>/ig, "");
           
            return someContent;
            //editor.EditModes = EditModes.Design;
        }
        // Set HTML content character limit
        function SetEditorCharLimit(limitNum) {
            var editor = $find("<%=radEditor1.ClientID%>");
            var validate = function () {
                var mainValue = editor.get_html(true); //get the HTML content
                if (mainValue.length > limitNum) {
                    editor.set_html(mainValue.substring(0, limitNum));
                    var mainLimit = mainValue.length;
                    var message = 'The word limit is ' + limitNum + ' characters! You have ' + mainLimit + '';
                    alert(message);
                }
            };
            editor.attachEventHandler("onkeyup", validate);
            editor.attachEventHandler("onkeydown", validate);
        }
        // clear editor content
        function clearEditorContent() {
            var editor = $find("<%=radEditor1.ClientID%>");
            editor.set_html("");
        }
        function OnClientPasteHtml(sender, args)
        {
            var commandName = args.get_commandName();
            //alert(commandName);
            var value = args.get_value();
            value = "<p>" + value + "</p>";
            //var results = value.replace("<br><br>", "</p><p>");
            for (var i = 0; i <= value.length; i++) {
                value = value.replace("<br>", "</p><p>");
            }
            
            //alert(value);
            args.set_value(value);

            //if (commandName == "Paste")
            //{
            //    //See if an img has an alt tag set 
            //    //var div = document.createElement("DIV"); 
      
            //    //Do not use div.innerHTML as in IE this would cause the image's src or the link's href to be converted to absolute path.
            //    //This is a severe IE quirk.
            //    //Telerik.Web.UI.Editor.Utils.setElementInnerHtml(div,value);
      
            //    //Now check if there is alt attribute
            //    //var addBreak = value.replace("<br/><br/>", "</p><br/><br/>");
            //    //alert(value);
            //    args.set_value(value);
            //    var img = div.firstChild; 
            //    if (!img.alt)
            //    { 
            //        var alt = prompt("No alt tag specified. Please specify an alt attribute for the image", "");
            //        img.setAttribute("alt", alt);
        
            //        //Set new content to be pasted into the editor 
            //        args.set_value(value);
            //    } 
            //}
        }
        // Load content on Editor Load
        function OnClientLoad(editor, args) {
            if (self !== top) {
                if (EditorId !== "") {
                    var content = parent.GetMarkupContent(EditorId);
                    editor.get_contentArea().innerHTML = content;
                    if (EditorHeight != "") {
                        editor.setSize("100%", EditorHeight);
                    } else {
                        editor.get_contentArea().innerHTML = "";
                    }
                } else {
                    if (parent.getCriterionDescription) {
                        var getDescription = parent.getCriterionDescription();
                        var main = parent.main();
                        var edit = "Edit";
                        var desc = "Description";
                        if (main.indexOf(edit) !== -1) {
                            var desContents = $("#radEditor1_contentIframe").contents().find("body").text().length;
                            editor.get_contentArea().innerHTML = getDescription;
                        } else if (main.indexOf(desc) !== -1) {
                            editor.get_contentArea().innerHTML = "";
                        } else {
                            $("#radEditor1_contentIframe").contents().find("body").text("Start typing instructions here...");
                        }
                    }
                }
            } else {
                var msg = "Please load this editor from an IFRAME window.";
                editor.get_contentArea().innerHTML = msg;
            }
            IsLoaded = true;
        }
        // Show content
        function ShowContent() {
            $("#radEditor1_contentIframe").contents().find("body").show();
        }
        // Hide content
        function HideContent() {
            $("#radEditor1_contentIframe").contents().find("body").hide();
        }
        // Toggle editing
        function ToggleEditing(toggle) {
            var editor = $find("<%=radEditor1.ClientID%>");
            editor.enableEditing(toggle);
            editor.set_editable(toggle);
            if (toggle == false) editor.get_document().body.style.backgroundColor = "lightgray";
            else editor.get_document().body.style.backgroundColor = "";
        }
        // Check Validation
        function checkValidation() {
            var mainValue = editor.get_html(true); //get the HTML content
            if (mainValue.length < 1) {
                alert('this');
            }
        }
        var result = 'test';
    </script>
</body>
</html>
