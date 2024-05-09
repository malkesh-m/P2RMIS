<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="RadEditorPro.aspx.cs" Inherits="Sra.P2rmis.Web.Views.Home.RadEditorPro" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Telerik Editor Pro</title> 
    <link href="/Content/telerik/editor.markup.css" rel="stylesheet" type="text/css" />
    <script src="/Scripts/jquery/jquery-1.12.1.min.js"></script>
    <link href="/Content/telerik/2015.1.429/kendo.common.min.css" rel="stylesheet" type="text/css" />
    <link href="/Content/telerik/2015.1.429/kendo.default.min.css" rel="stylesheet" type="text/css" />
    <script src="/scripts/telerik/2015.1.429/kendo.all.min.js" type="text/javascript"></script>
</head>
<body class="margin0" style="margin: 0px;">
    <form id="form1" runat="server">
        <asp:ScriptManager runat="server"></asp:ScriptManager>
        <div id="editorPanel">
            <telerik:RadEditor ID="radEditor1" runat="server" Width="100%" Height="445px"
                    OnClientLoad="OnClientLoad" OnClientSelectionChange="OnClientSelectionChange"
                    OnClientPasteHtml="OnClientPasteHtml" OnClientDomChange="OnClientDomChange" OnClientCommandExecuting="OnClientCommandExecuting"
                    EnableTrackChanges="True" ToolsFile="/Content/telerik/editortoolbar.xml" EnableResize="false"
                    SpellCheckSettings-AjaxUrl="/Telerik.Web.UI.SpellCheckHandler.axd"
                    DialogHandlerUrl="/Telerik.Web.UI.DialogHandler.axd"
                    StripFormattingOptions="AllExceptNewLines, MSWordRemoveAll"
                    EditModes="Design"
                    ContentFilters="ConvertCharactersToEntities, ConvertToXhtml, FixEnclosingP">
                <TrackChangesSettings UserCssId="reU1"></TrackChangesSettings>
                <Content>   
                Loading...
                </Content>
            </telerik:RadEditor>
        </div>
        <div id="commentPanel">
            <div id="markupFilter" class="bordered">
                <div class="header-title">Show mark-up by</div>
                <select id="reviewers" multiple="multiple" data-placeholder="Select">
                </select>
            </div>
            <div id="commentInstruction">
                <div>
                    Comments are limited to <span id="maxCharLimitInstruction"></span> characters.
                </div>
                <hr />
                <div>
                    <input type="checkbox" id="toggleComments" />
                    Display all hidden comments
                </div>
            </div>

            <div id="displayComments"></div>
        </div>
        <div id="commentEntryTemplate" class="comment-entry">
            <div>
                <span class="comment-entry-author"></span>
                <span class="comment-entry-published-date"></span>
            </div>
            <div class="comment-entry-caption"></div>
            <div class="comment-entry-view-mode">
                <div class="comment-entry-comment"></div>
                <div class="comment-entry-actions">
                    <a class="comment-entry-edit" href="#">Edit</a> |
                    <a class="comment-entry-hide" href="#">Hide</a>
                </div>
                <div class="comment-entry-actions-hidden" style="display:none;">
                    <a class="comment-entry-unhide" href="#">Unhide</a>
                </div>
            </div>
            <div class="comment-entry-edit-mode" style="display:none;">
                <div class="comment-entry-warning alert-message" style="display:none;"></div>
                <textarea class="comment-entry-comment"></textarea>
                <div>
                    <a class="comment-entry-save" href="#">Save</a> |
                    <a class="comment-entry-cancel" href="#">Cancel</a>
                </div>
            </div>
        </div>
    </form>  
    <!-- Telerik Comment Widget -->
    <script type="text/javascript" src="/Scripts/rangy/rangy-core.js"></script>
    <script type="text/javascript" src="/Scripts/jquery.uuid.js"></script>
    <script type="text/javascript" src="/Scripts/datejs/date.js"></script>
    <script type="text/javascript" src="/Scripts/common/common.js"></script>
    <script type="text/javascript" src="/Scripts/common/editor.js"></script>
    <script type="text/javascript" src="/Scripts/telerik/telerikcommentwidget.js?201609261513"></script>
    <script type="text/javascript">   
        // Global variables
        var EditorFrameId = "radEditor1_contentIframe";
        var Username = GetParameterByName("name");
        var UserId = GetParameterByName("userid");
        var EditorIdx = GetParameterByName("idx");
        var SaveIntervalMilliSeconds = 15000;
        var SaveIntervalId;
        var IsLoaded = false;
		parent.fnHideLoadingAnimation(EditorIdx);
        // Assign id and class names
        var ElementKeys = {
            commentPanel: "commentPanel",
            displayComments: "displayComments",
            commentEntryTemplate: "commentEntryTemplate",
            commentEntrySectionClass: "comment-entry",
            commentEntryCaptionClass: "comment-entry-caption",
            commentEntryCommentClass: "comment-entry-comment",
            commentEntryAuthorClass: "comment-entry-author",
            commentEntryPublishedDateClass: "comment-entry-published-date",
            commentEntryEditLinkClass: "comment-entry-edit",
            commentEntryHideClass: "comment-entry-hide",
            commentEntryActionsClass: "comment-entry-actions",
            commentEntryActionsHiddenClass: "comment-entry-actions-hidden",
            commentEntryEditModeClass: "comment-entry-edit-mode",
            commentEntryViewModeClass: "comment-entry-view-mode",
            toggleComments: "toggleComments",
            reviewersList: "reviewers",
            markupFilter: "markupFilter"
        };
        var ValidationRules = {
            maxCommentLength: GetMaxCharLimit(), // -1 means disabled
            maxCommentLengthInTooltip: -1, // -1 means disabled
            invalidCommentLengthMessage: "You have exceeded the maximum character limit of  " + GetMaxCharLimit() +
                ". Please reduce the number of characters to " + GetMaxCharLimit() + "."
        };
        $("#maxCharLimitInstruction").html(ValidationRules.maxCommentLength);
        // Initialize telerik comment widget
        var telerikCommentWidget = new TelerikCommentWidget(EditorFrameId, Username, UserId, ElementKeys, ValidationRules);

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
            return someContent;
        }
        // Load content on Editor Load
        function OnClientLoad(editor, args) {
            if (self !== top) {
                if (EditorIdx !== "") {
                    var content = parent.GetMarkupContent(EditorIdx);
                    editor.get_contentArea().innerHTML = content;
                    var toolNamesToDisable = ["AcceptTrackChange", "RejectTrackChange",
                            "AcceptAllTrackChanges", "RejectAllTrackChanges"];
                    var canAcceptChanges = editor.get_canAcceptTrackChanges();
                    // Add commment
                    editor.getToolByName("InsertComment").setState(Telerik.Web.UI.Editor.CommandStates.Disabled);
                    if (!canAcceptChanges) {
                        for (var i = 0; i < toolNamesToDisable.length; i++) {
                            var toolName = toolNamesToDisable[i];
                            editor.getToolByName(toolName).setState(Telerik.Web.UI.Editor.CommandStates.Disabled);
                        }
                    }
                    // Markup mode
                    telerikCommentWidget.setMarkupOn();
                    // Display all comments
                    telerikCommentWidget.setComments();
                    telerikCommentWidget.initializeReviewers();
                    telerikCommentWidget.displayComments();
                    telerikCommentWidget.filterMarkup();
                    // Multi-select reviewers
                    telerikCommentWidget.initReviewerList();
                    telerikCommentWidget.setReviewerListDataSource();
                    telerikCommentWidget.setReviewerListData();
                    telerikCommentWidget.setReviewerListStatus();
                    // Icon states
                    editor.getToolByName("MarkupMode").setState(Telerik.Web.UI.Editor.CommandStates.On);
                    telerikCommentWidget.addMarkupModeAnnotation();
                    // Show comment panel
                    telerikCommentWidget.showCommentPanel();
                    // Start timer
                    SaveIntervalId = setInterval(function () {
                        parent.SaveEditorContent(editor.get_html(), EditorIdx);
                    }, SaveIntervalMilliSeconds);
                    // Handles TAB key
                    editor.attachEventHandler("onkeydown", function (e) {
                        if (e.keyCode === 9) {
                            editor.pasteHtml("&nbsp;&nbsp;&nbsp;&nbsp;");
                            if (!document.all) {
                                e.preventDefault();
                                e.stopPropagation();
                            }
                            else {
                                e.returnValue = false;
                                e.cancelBubble = true;
                            }
                        }
                        if (e.keyCode === 123 && e.ctrlKey) {
                            telerikCommentWidget.jumpToCommentPanel();
                        }
                        if (e.keyCode === 119) {
                            telerikCommentWidget.jumpToClose();
                        }
                        if (e.keyCode === 120) {
                            telerikCommentWidget.jumpToModify();
                        }
                        if (e.keyCode === 121) {
                            telerikCommentWidget.jumpToToolbar();
                        }
                        if (e.keyCode === 49 && e.ctrlKey && e.shiftKey) {
                            setTimeout(function () {
                                if ($('.rePopupButton').length) {
                                    $('.rePopupButton.ok').focus();
                                }
                                //$(top).find('.rePopupButton').focus();
                            }, 500);
                        }
                        if (e.keyCode === 50 && e.ctrlKey && e.shiftKey) {
                            setTimeout(function () {
                                if ($('.rePopupButton').length) {
                                    $('.rePopupButton.ok').focus();
                                }
                                //$(top).find('.rePopupButton').focus();
                            }, 500);
                        }
                    });
                    // Change markup as needed
                    $telerik.$(editor.get_contentArea()).on("keyup", function (e) {
                        alterClosestFormattedNode(editor);
                        if (!telerikCommentWidget.isMarkupOff) {
                            telerikCommentWidget.setComments();
                            telerikCommentWidget.setReviewers();
                            telerikCommentWidget.setReviewerListDataSource();
                            telerikCommentWidget.setReviewerListStatus();
                        }
                    })
                }
            } else {
                var msg = "Please load this editor from an IFRAME window.";
                editor.get_contentArea().innerHTML = msg;
            }
            IsLoaded = true;
        }
        // On client selection change
        function OnClientSelectionChange(editor, args) {
            // Reset InsertComment and ToggleComments status 
            fnResetInsertCommentState(editor);
            editor.get_toolAdapter().enableContextMenus(false);
        }
        // On client paste HTML
        function OnClientPasteHtml(editor, args) {
            alterClosestFormattedNodePostponed(editor);
        }
        // On cliennt DOM change
        function OnClientDomChange(editor, args) {
            alterClosestFormattedNodePostponed(editor);
        }
        // On client command executing
        function OnClientCommandExecuting(editor, args) {
            var commandName = args.get_commandName();
            var defineRuleToChangeFormatedNode = commandName != "LinkManager" &&
                                                 commandName != "MarkupMode";
            if (defineRuleToChangeFormatedNode) {
                alterClosestFormattedNodePostponed(editor);
            }
        }
        // Alert closest formatted node
        function alterClosestFormattedNode(editor) {
            var range = editor.getDomRange();
            var startContainer = range ? range.startContainer : null;
            if (telerikCommentWidget.isMarkupOff) {
                // markup is off or current user is not in filter
                telerikCommentWidget.setMarkupOff();
            } else {
                telerikCommentWidget.setMarkupOn();
                telerikCommentWidget.filterMarkup();
            }
        }
        // Alert closest formatted node postponed
        function alterClosestFormattedNodePostponed(editor) {
            window.setTimeout(function () {
                alterClosestFormattedNode(editor);
            }, 5);
        }
        // Reset InsertComment status
        function fnResetInsertCommentState(editor) {
            var hasSelectedContent = editor.getSelection().getText().length > 0;
            if (hasSelectedContent && !IsInEditMode()) {
                editor.getToolByName("InsertComment").setState(Telerik.Web.UI.Editor.CommandStates.Off);
            } else {
                editor.getToolByName("InsertComment").setState(Telerik.Web.UI.Editor.CommandStates.Disabled);
            }
        }
        // Get max char limit
        function GetMaxCharLimit() {
            if (parent.fnGetInlineCommentMaxCharLimit)
                return parent.fnGetInlineCommentMaxCharLimit();
            else
                return -1;
        }
        // Cancel auto-save
        function CancelAutoSave() {
            clearInterval(SaveIntervalId);
        };
        // Hide comment panel
        function HideCommentPanel() {
            telerikCommentWidget.hideCommentPanel();
        }
        // Has unsaved data
        function HasUnsavedData() {
            var hasUnsavedData = false;
            $(".comment-entry-edit-mode:visible > .comment-entry-comment").each(function () {
                var editMode = $(this).parent();
                var entry = $(this).closest(".comment-entry");
                var uid = entry.attr("data-uid");
                var comment = $(this).val();
                if (telerikCommentWidget.isCommentNewOrChanged(uid, comment)) {
                    hasUnsavedData = true;
                    entry.find(".comment-entry-warning").html("You have unsaved data.").show();
                }
            });
            return hasUnsavedData;
        }
        // Whether the comment panel is in edit mode
        function IsInEditMode() {
            var isInEditMode = $(".comment-entry-edit-mode:visible > .comment-entry-comment").length > 0;
            return isInEditMode;
        }
        // Save action
        Telerik.Web.UI.Editor.CommandList["Save"] = function (commandName, editor, oTool) {
            clearInterval(saveIntervalId);
            parent.SaveEditorContent(editor.get_html(), EditorIdx);
            SaveIntervalId = setInterval(function () {
                parent.SaveEditorContent(editor.get_html(), EditorIdx);
            }, SaveIntervalMilliSeconds);
        };
        // Insert comment
        Telerik.Web.UI.Editor.CommandList["InsertComment"] = function (commandName, editor, args) {
            var selectedContent = editor.getSelection().getText();
            if (selectedContent.length > 0) {
                telerikCommentWidget.displayComments();
                telerikCommentWidget.insertComment(selectedContent);
            }
            window.setTimeout(function () { editor.getToolByName("InsertComment").setState(Telerik.Web.UI.Editor.CommandStates.Disabled); }, 500);
        };
        // Markup mode
        Telerik.Web.UI.Editor.CommandList["MarkupMode"] = function (commandName, editor, args) {
            if (telerikCommentWidget.isMarkupOff) {
                telerikCommentWidget.setMarkupOn();
                telerikCommentWidget.filterMarkup();
                telerikCommentWidget.setMarkupModeAnnotation(true);
                telerikCommentWidget.showMarkupFilter();
                editor.getToolByName("MarkupMode").setState(Telerik.Web.UI.Editor.CommandStates.On);
            } else {
                telerikCommentWidget.setMarkupOff();
                telerikCommentWidget.setMarkupModeAnnotation(false);
                telerikCommentWidget.hideMarkupFilter();
                editor.getToolByName("MarkupMode").setState(Telerik.Web.UI.Editor.CommandStates.Off);
            }
        };
        // Edit comment
        $(document).on("click", ".comment-entry-edit", function (e) {
            e.preventDefault();
            telerikCommentWidget.editComment(this);
        });
        // Save comment
        $(document).on("click", ".comment-entry-save", function (e) {
            e.preventDefault();
            telerikCommentWidget.saveComment(this);
            // Multi-select reviewers
            telerikCommentWidget.setReviewerListDataSource();
            telerikCommentWidget.setReviewerListStatus();
            // Reset InsertComment status
            var editor = $find("<%=radEditor1.ClientID%>");
            fnResetInsertCommentState(editor);
        });
        // Cancel saving comment
        $(document).on("click", ".comment-entry-cancel", function (e) {
            e.preventDefault();
            telerikCommentWidget.displayComments();
            telerikCommentWidget.setReviewerListStatus();
            // Reset InsertComment status
            var editor = $find("<%=radEditor1.ClientID%>");
            fnResetInsertCommentState(editor);
        });
        // Comment changes
        $(document).on("input", ".comment-entry-comment", function (e) {
            e.preventDefault();
            var entry = $(this).closest(".comment-entry");
            var uid = entry.attr("data-uid");
            entry.find(".comment-entry-warning").hide();
            var comment = $(this).val();
            var status = !telerikCommentWidget.isCommentNewOrChanged(uid, comment)
            telerikCommentWidget.setReviewerListStatus(status);
            // Auto expand
            if ($(this).get(0).scrollHeight > $(this).get(0).clientHeight) {
                $(this).height($(this).height() + 60);
            }
        });
        // Hide comment
        $(document).on("click", ".comment-entry-hide", function (e) {
            e.preventDefault();
            var entry = $(this).closest(".comment-entry");
            telerikCommentWidget.hideComment(this);
        });
        // Unhide comment
        $(document).on("click", ".comment-entry-unhide", function (e) {
            e.preventDefault();
            telerikCommentWidget.unhideComment(this);
        });
        // Toggle comments
        $(document).on("change", "#toggleComments", function (e) {
            $(".comment-entry[data-hide=1]").each(function () {
                telerikCommentWidget.hideComment(this);
            });
        });
    </script>
</body>
</html>

