// Functions for the comment widget
function TelerikCommentWidget(editorFrameId, username, userId, elementKeys, validationRules) {
    this.editorFrameId = editorFrameId;
    this.elementKeys = elementKeys;
    this.username = username;
    this.userId = userId;
    this.commentIndex = 0;
    this.timeStampDifference = 0;
    this.validationRules = validationRules;
    this.maxLengthInCommentTitle = 25;
    this.maxLengthInUsername = 16;
    this.isMarkupOff = false;
    this.reviewers = [];
    this.filteredReviewers = [];
    this.comments = [];
}
// Get content frame
TelerikCommentWidget.prototype.getContentFrame = function() {
    return $("#" +this.editorFrameId)[0];
};
// Get target window
TelerikCommentWidget.prototype.getTargetWindow = function() {
    return this.getContentFrame().contentWindow;
};
// Insert comment
TelerikCommentWidget.prototype.insertComment = function (selectedText) {
    var commentEntry = this.createComment("", "", this.username, this.userid, "", this.getCommentCaption(selectedText), false);
    $("#" + this.elementKeys.displayComments).prepend(commentEntry);

    var contentFrame = this.getContentFrame();
    this.setSelectedRange(contentFrame);
        
    // Require a delay when setting a focus outside the editor
    window.setTimeout(function (selector) { $(selector).eq(0).focus() }, 500, "textarea." + this.elementKeys.commentEntryCommentClass);
};
// Insert comment action
TelerikCommentWidget.prototype.insertCommentAction = function (uid, comment, author, timestamp) {
    // Handle empty string
    if(comment.length > 0) {
        var contentFrame = this.getContentFrame();
        var targetWindow = this.getTargetWindow();
        
        var htmlContent = this.getSelectionHtml();
        if (htmlContent.length > 0) {
            this.deleteRange();
            var el = this.createCommentToEditor(uid, targetWindow, htmlContent, comment, author, timestamp);
            this.insertNodeAtRange(contentFrame, el);
        }
    }
};
// Create span comment element
TelerikCommentWidget.prototype.createCommentToEditor = function (uid, targetWindow, htmlContent, comment, author, timestamp) {
    var el = targetWindow.document.createElement("span");
    el.setAttribute("title", this.getCommentTitle(comment, author));
    el.setAttribute("comment", comment);
    el.setAttribute("author", author);
    el.setAttribute("userid", this.userId);
    el.setAttribute("timestamp", timestamp);
    // The UID should be unique
    el.setAttribute("uid", uid);
    
    EDITOR.setCommentMarkupStatus($(el), true);
    this.commentIndex++;
    el.innerHTML = htmlContent;
    return el;
};
// Get comment title
TelerikCommentWidget.prototype.getCommentTitle = function (comment, author) {
    var commentTitle;
    if (this.validationRules.maxCommentLengthInTooltip === -1 || comment.length <= this.validationRules.maxCommentLengthInTooltip) {
        commentTitle = author + " (" + this.userId + "): " + comment;
    } else {
        commentTitle = author + " (" +this.userId + "): " + comment.substring(0, this.validationRules.maxCommentLengthInTooltip) + "...";
    }
    return commentTitle;
};
// Detect if the comment is already included in a given array
TelerikCommentWidget.prototype.isCommentInArray = function (commentArray, uid) {
    var exists = false;
    var i = 0;
    while (!exists && i < commentArray.length) {
        if (commentArray[i].uid == uid) {
            exists = true;
        }
        i++;
    }
    return exists;
};
// Keyboard shortcuts
TelerikCommentWidget.prototype.jumpToCommentPanel = function () {
    $(document).find('.k-input').focus();
};
TelerikCommentWidget.prototype.jumpToHtml = function () {
    $(document).find('.reMode_html').focus();
};
TelerikCommentWidget.prototype.jumpToToolbar = function () {
    $(document).find('#radEditor1Top ul:nth-child(1) a').focus();
};
TelerikCommentWidget.prototype.jumpToFirstComment = function () {
    window.parent.firstComment();
};
TelerikCommentWidget.prototype.jumpToClose = function () {
    window.parent.widgetClosed();
};
TelerikCommentWidget.prototype.jumpToModify = function () {
    $(document).find('#viewContentMode').focus();
};
// Set comments
TelerikCommentWidget.prototype.setComments = function () {
    var commentArray = [];
    var $this = this;
    // Track change objects don't have comment attributes
    var targetWindow = this.getTargetWindow();
    $(targetWindow.document).find("[comment]").each(function () {
        if (!$this.isCommentInArray(commentArray, $(this).attr("uid"))) {
            commentArray.push({ comment: $(this).attr("comment"), author: $(this).attr("author"), userid: $(this).attr("userid"),
                timestamp: parseInt($(this).attr("timestamp")), uid: $(this).attr("uid"), isHidden: $(this).attr("data-hide") == "1"
            });
        }
    });
    this.comments = commentArray;
};
// Initialize reviewers
TelerikCommentWidget.prototype.initializeReviewers = function () {
    this.setReviewers();
    if ($.inArray(this.username, this.reviewers) == -1) {
        this.reviewers.push(this.username);
        this.filteredReviewers.push(this.username);
    }
};
// Set reviewers
TelerikCommentWidget.prototype.setReviewers = function () {
    var targetWindow = this.getTargetWindow();
    var reviewersJson = EDITOR.getReviewersJson($(targetWindow.document));
    this.reviewers = reviewersJson.reviewers;
    this.filteredReviewers = reviewersJson.filteredReviewers;
};
// Filter markup
TelerikCommentWidget.prototype.filterMarkup = function () {
    var targetWindow = this.getTargetWindow();
    var displayHiddenComments = $("#" + this.elementKeys.toggleComments).prop("checked");
    EDITOR.filterMarkup($(targetWindow.document), this.filteredReviewers, displayHiddenComments);
};
// Display comments
TelerikCommentWidget.prototype.displayComments = function () {
    $("#" + this.elementKeys.displayComments).empty();
    for (var i = 0; i < this.comments.length; i++) {
        var commentEntry = this.createComment(this.comments[i].uid, this.comments[i].comment, this.comments[i].author,
            this.comments[i].userid, this.comments[i].timestamp, null, this.comments[i].isHidden);
        $("#" + this.elementKeys.displayComments).append(commentEntry);
    }
    $("#" + this.elementKeys.displayComments).show();
};
// Create a comment jQuery object
TelerikCommentWidget.prototype.createComment = function (uid, comment, author, userId, timestamp, selectedText, isHidden) {

    var commentEntry = $("#" + this.elementKeys.commentEntryTemplate).clone();
    commentEntry.removeAttr("id");
    commentEntry.removeClass("hide");
    commentEntry.attr("data-uid", uid);
    commentEntry.attr("data-userid", userId);
    var targetWindow = this.getTargetWindow();
    var croppedSelectedText = selectedText;
    if (uid) {
        var el = $(targetWindow.document).find("[uid=" + uid + "]");
        selectedText = el.text();
        croppedSelectedText = this.getCommentCaption(selectedText);
    }
    // Add caption
    commentEntry.find("." + this.elementKeys.commentEntryCaptionClass).html(croppedSelectedText).attr("title", selectedText);
    // Encode and then display
    commentEntry.find("." + this.elementKeys.commentEntryCommentClass).html($.htmlEncode(comment));
    var croppedAuthor = author;
    if (author.length > this.maxLengthInUsername) {
        croppedAuthor = author.substring(0, this.maxLengthInUsername) + "...";
    }
    commentEntry.find("." + this.elementKeys.commentEntryAuthorClass).html(croppedAuthor).attr("title", author);
    if (timestamp) {
        commentEntry.find("." + this.elementKeys.commentEntryPublishedDateClass).html(new Date(timestamp).toString('MM/dd/yy hh:mm tt'));
        commentEntry.find("." + this.elementKeys.commentEntryEditModeClass).hide();
        // Hide or unhide
        if (isHidden) {
            commentEntry.attr("data-hide", "1");
            if ($("#" + this.elementKeys.toggleComments).prop("checked")) {
                commentEntry.find("." + this.elementKeys.commentEntryActionsClass).hide();
                commentEntry.find("." + this.elementKeys.commentEntryActionsHiddenClass).show();
            } else {
                commentEntry.hide();
            }
        } else {
            commentEntry.find("." + this.elementKeys.commentEntryActionsClass).show();
            commentEntry.find("." + this.elementKeys.commentEntryActionsHiddenClass).hide();
        }
        commentEntry.find("." + this.elementKeys.commentEntryViewModeClass).show();
        if (userId !== this.userId) {
            commentEntry.find("." +this.elementKeys.commentEntryEditLinkClass).hide();
        }
    } else {
        commentEntry.find("." + this.elementKeys.commentEntryViewModeClass).hide();
        commentEntry.find("." + this.elementKeys.commentEntryEditModeClass).show();
    }
    return commentEntry;
};
// Get comment's caption to be put on top of the comment
TelerikCommentWidget.prototype.getCommentCaption = function (selectedHtml) {
    var contentEl = $("<div />").append(selectedHtml);
    contentEl.find("del").each(function () {
        $(this).remove();
    });
    var selectedText = contentEl.text();
    if (selectedText.length > this.maxLengthInCommentTitle) {
        selectedText = selectedText.substring(0, this.maxLengthInCommentTitle) + "...";
    }
    return selectedText;
};

// Edit comment action
TelerikCommentWidget.prototype.editCommentAction = function (uid, comment, author, timestamp) {
    // Handle empty string
    if (comment.length > 0) {
        var targetWindow = this.getTargetWindow();
        var el = $(targetWindow.document).find("[uid=" + uid + "]");
        el.attr("title", this.getCommentTitle(comment, el.attr("author")));
        el.attr("comment", comment);
        el.attr("timestamp", timestamp);
    }
};

// Hide comment action
TelerikCommentWidget.prototype.hideCommentAction = function (uid, visible) {
    var targetWindow = this.getTargetWindow();
    var el = $(targetWindow.document).find("[uid=" + uid + "]");
    el.attr("data-hide", "1");
    if (!visible) {
        EDITOR.setCommentMarkupStatus(el, false);
    } else {
        EDITOR.setCommentMarkupStatus(el, true);
    }
};
// Unhide comment action
TelerikCommentWidget.prototype.unhideCommentAction = function (uid) {
    var targetWindow = this.getTargetWindow();
    var el = $(targetWindow.document).find("[uid=" + uid + "]");
    el.removeAttr("data-hide");
    EDITOR.setCommentMarkupStatus(el, true);
};
// Merge split elements
// The child elements of unwrapped element and their immediate previous/next elements are 
// candidates of split comments/tracked changes, and are required to evaluate to merge or keep as they are
TelerikCommentWidget.prototype.mergeSplitElements = function (elements) {
    // Merge with previous element if exists and they share the same identity
    if (elements.length > 0) {
        var currentElement = elements.first();
        var prevElement = currentElement.prev();

        while (currentElement.length > 0 && currentElement !== elements.last().next().next()) {
            if (this.areMergeCandidateElements(prevElement, currentElement)) {
                currentElement.prepend(prevElement.html()).prev().remove();
            }
            // Loop if the current element has children
            this.mergeSplitElements(currentElement.children());
            // Re-assign elements
            prevElement = currentElement;
            currentElement = currentElement.next();
        }
    }
};
// Evaluate if elements are candidates for merging
TelerikCommentWidget.prototype.areMergeCandidateElements = function (prevElement, currentElement) {
    if (prevElement.length > 0 && prevElement[0].nextSibling === currentElement[0]
                && currentElement.clone().empty()[0].outerHTML === prevElement.clone().empty()[0].outerHTML) {
        return true;
    } else {
        return false;
    }
};
// Hide comment
TelerikCommentWidget.prototype.hideComment = function ($this) {
    var entry = $($this).closest("." + this.elementKeys.commentEntrySectionClass);
    entry.attr("data-hide", "1");
    var uid = entry.attr("data-uid");
    if ($("#" + this.elementKeys.toggleComments).prop("checked")) {
        entry.find("." + this.elementKeys.commentEntryActionsClass).hide();
        entry.find("." + this.elementKeys.commentEntryActionsHiddenClass).show();
        entry.show();
        telerikCommentWidget.hideCommentAction(uid, true);
    } else {
        entry.hide();
        telerikCommentWidget.hideCommentAction(uid, false);
    }
};
// Unhide comment
TelerikCommentWidget.prototype.unhideComment = function ($this) {
    var entry = $($this).closest("." + this.elementKeys.commentEntrySectionClass);
    entry.removeAttr("data-hide");
    entry.find("." + this.elementKeys.commentEntryActionsClass).show();
    entry.find("." + this.elementKeys.commentEntryActionsHiddenClass).hide();
    entry.show();
    var uid = entry.attr("data-uid");
    this.unhideCommentAction(uid);
};
// Save comment
TelerikCommentWidget.prototype.saveComment = function ($this) {
    var entry = $($this).closest("." + this.elementKeys.commentEntrySectionClass);
    var uid = entry.attr("data-uid");
    var userId = entry.attr("data-userid");
    // Decode first and then display
    var comment = $.htmlDecode(entry.find("textarea." + this.elementKeys.commentEntryCommentClass).val());
    var author = this.username;
    //TODO: server datetime
    var timestamp = new Date().getTime();

    if (this.validationRules.maxCommentLength === 0 || comment.length <= this.validationRules.maxCommentLength) {
        var added = false;
        if (comment.length > 0) {
            if (uid === "") {
                uid = Math.uuid().toLowerCase();
                this.insertCommentAction(uid, comment, author, timestamp);
                added = true;
            } else {
                if (userId === this.userId) {
                    this.editCommentAction(uid, comment, author, timestamp);
                }
            }
        } else {
            if (userId === this.userId) {
                var targetWindow = this.getTargetWindow();
                var el = $(targetWindow.document).find("[uid=" + uid + "]");
                el.contents().unwrap();
                // Merge split elements
                var childrenElements = $(targetWindow.document).children();
                this.mergeSplitElements(childrenElements);
            }
        }
        this.setComments();
        this.setReviewers();
        this.setReviewerListDataSource();
        this.displayComments();
        // Set focus on the editor
        this.getContentFrame().focus();
    } else {
        alert(this.validationRules.invalidCommentLengthMessage);
    }
};
// Edit comment
TelerikCommentWidget.prototype.editComment = function($this) {
    var entry = $($this).closest("." + this.elementKeys.commentEntrySectionClass);
    var uid = entry.attr("data-uid");
    var userId = entry.attr("data-userid");
    // Decode first and then display
    var comment = $.htmlDecode(entry.find("." + this.elementKeys.commentEntryCommentClass).html());
    if (userId === this.userId) {
        entry.find("." + this.elementKeys.commentEntryCommentClass).val(comment);
        entry.find("." + this.elementKeys.commentEntryViewModeClass).hide();
        entry.find("." + this.elementKeys.commentEntryEditModeClass).show();
        entry.find("." + this.elementKeys.commentEntryCommentClass).focus();
    }
};
// Delete comment
TelerikCommentWidget.prototype.deleteComment = function ($this) {
    var entry = $($this).closest(".commentEntry");
    var uid = entry.attr("data-uid");
    var userId = entry.attr("data-userid");
    if (userId === this.userId) {
        var targetWindow = this.getTargetWindow();
        var el = $(targetWindow.document).find("[uid=" + uid + "]");
        el.contents().unwrap();
        // Merge split elements
        var childrenElements = $(targetWindow.document).children();
        this.mergeSplitElements(childrenElements);
        // Remove entry from comment panel
        entry.remove();
    }
};
// Get selection HTML
TelerikCommentWidget.prototype.getSelectionHtml = function () {
    if (this.selectedRange) {
        var content = this.selectedRange.extractContents();
        span = document.createElement('SPAN');
        span.appendChild(content);
        var htmlContent = span.innerHTML;
        return htmlContent;
    }
};
// Set selected range from the first range
TelerikCommentWidget.prototype.setSelectedRange = function (iframeEntity) {
    var sel = rangy.getSelection(iframeEntity);
    // Define selected range
    this.selectedRange = sel.rangeCount ? sel.getRangeAt(0) : null;
};
// Delete range
TelerikCommentWidget.prototype.deleteRange = function () {
    if (this.selectedRange) {
        this.selectedRange.deleteContents();
    }
};
// Insert node at range
TelerikCommentWidget.prototype.insertNodeAtRange = function (iframeEntity, el) {
    if (this.selectedRange) {
        this.selectedRange.insertNode(el);
        rangy.getSelection(iframeEntity).setSingleRange(this.selectedRange);
    }
};
// Set markup off
TelerikCommentWidget.prototype.setMarkupOff = function () {
    var targetWindow = this.getTargetWindow();
    var displayHiddenComments = $("#" + this.elementKeys.toggleComments).prop("checked");
    EDITOR.setMarkupStatus($(targetWindow.document), false, false);
    this.isMarkupOff = true;
};
// Set markup on
TelerikCommentWidget.prototype.setMarkupOn = function () {
    var targetWindow = this.getTargetWindow();
    var displayHiddenComments = $("#" + this.elementKeys.toggleComments).prop("checked");
    EDITOR.setMarkupStatus($(targetWindow.document), true, displayHiddenComments);
    this.isMarkupOff = false;
};
// Whether the comment is new or changed
TelerikCommentWidget.prototype.isCommentNewOrChanged = function (uid, comment) {
    var isNewOrChanged = true;
    for (var i = 0; i < this.comments.length; i++) {
        var commentEntry = this.comments[i];
        if (commentEntry.uid == uid && commentEntry.comment == comment) {
            isNewOrChanged = false; 
        }
    }
    return isNewOrChanged;
};
// Initialize reviewer multi-select list
TelerikCommentWidget.prototype.initReviewerList = function () {
    var $this = this;
    $("#" + this.elementKeys.reviewersList).kendoMultiSelect({
        change: function (e) {
            var value = this.value();
            $this.filteredReviewers = value;
            $this.filterMarkup();
        }
    });
};
// Set reviewer multi-select list data source
TelerikCommentWidget.prototype.setReviewerListDataSource = function () {
    var multiselect = $("#" + this.elementKeys.reviewersList).data("kendoMultiSelect");
    var dataSource = new kendo.data.DataSource({
        data: this.reviewers
    });
    multiselect.setDataSource(dataSource);
};
// Set reviewer multi-select list data
TelerikCommentWidget.prototype.setReviewerListData = function () {
    var multiselect = $("#" + this.elementKeys.reviewersList).data("kendoMultiSelect");
    multiselect.value(this.filteredReviewers);
};
// Set reviewer multi-select list status
TelerikCommentWidget.prototype.setReviewerListStatus = function (status) {
    var multiselect = $("#" + this.elementKeys.reviewersList).data("kendoMultiSelect");
    if (status != undefined) {
        multiselect.enable(status);
    } else {
        if (this.reviewers.length > 0) {
            multiselect.enable(true);
        } else {
            multiselect.enable(false);
        }
    }
};
// Add Markup mode annotation
TelerikCommentWidget.prototype.addMarkupModeAnnotation = function () {
    $(".reTool[title='Markup Mode']").css("width", "95px");
    var markupText = $("<span/>").html("Markup On");
    markupText.attr("class", "reToolAnnotation").css("width", "65px");
    $(".reTool[title='Markup Mode']").append(markupText);
};
// Set Markup mode annotation on/off
TelerikCommentWidget.prototype.setMarkupModeAnnotation = function (isMarkupOn) {
    if (isMarkupOn) {
        $(".reTool[title='Markup Mode'] > .reToolAnnotation").html("Markup On");
    } else {
        $(".reTool[title='Markup Mode'] > .reToolAnnotation").html("Markup Off");
    }
};
// Select all users
TelerikCommentWidget.prototype.selectAllUsers = function () {
    var multiselect = $("#" + this.elementKeys.reviewersList).data("kendoMultiSelect");
    var selectedValues = "";
    var strComma = "";
    for (var i = 0; i < multiselect.dataSource.data().length; i++) {
        var item = multiselect.dataSource.data()[i];
        selectedValues += strComma + item.value;
        strComma = ",";
    }
    multiselect.value(selectedValues.split(","));
};
// Show comment panel
TelerikCommentWidget.prototype.showCommentPanel = function () {
    $("#" + this.elementKeys.commentPanel).show();
};
// Hide comment panel
TelerikCommentWidget.prototype.hideCommentPanel = function () {
    $("#" + this.elementKeys.commentPanel).hide();
};
// Show markup filter
TelerikCommentWidget.prototype.showMarkupFilter = function () {
    $("#" + this.elementKeys.markupFilter).show();
};
// Hide markup filter
TelerikCommentWidget.prototype.hideMarkupFilter = function () {
    $("#" + this.elementKeys.markupFilter).hide();
};