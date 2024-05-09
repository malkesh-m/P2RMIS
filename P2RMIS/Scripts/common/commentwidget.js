// Functions for the comment widget
function CommentWidget(userId, elementKeys, validationRules, oneCommentPerUser) {
    this.userId = userId;
    this.elementKeys = elementKeys;
    this.oneCommentPerUser = oneCommentPerUser;
    this.validationRules = validationRules;
    // Initialize
    $("#" +this.elementKeys.saveCommentSectionId).val("");
    $("#" +this.elementKeys.saveCommentSectionComment).val("");
}
// Display comments
CommentWidget.prototype.addCommentsToBody = function (comments) {
    var hasCurrentUserComment = false;
    $("#" + this.elementKeys.displayComments).empty();
    if (comments.length > 0) {
        for (var i = 0; i < comments.length; i++) {
            var commentJson = comments[i];
            // Format time stamp
            var timeStamp = $.toJsTimeStamp(commentJson.timeStamp);
            // Create a comment jQuery object
            var commentEntry = $("#" + this.elementKeys.commentEntryTemplate).clone();
            commentEntry.removeAttr("id");
            commentEntry.removeClass("hide");
            commentEntry.attr("data-id", commentJson.commentId);
            commentEntry.attr("data-userid", commentJson.userId);
            // Encode and then display
            commentEntry.find("." + this.elementKeys.commentEntryCommentClass).html($.htmlEncode(commentJson.comment));
            commentEntry.find("." + this.elementKeys.commentEntryAuthorClass).html(commentJson.lastName + ", " + commentJson.firstName);
            commentEntry.find("." + this.elementKeys.commentEntryPublishedDateClass).html(new Date(timeStamp).toString('M/d/yyyy h:mm:ss tt'));
            if (parseInt(commentJson.userId) === this.userId && this.validationRules.canModify) {
                commentEntry.find("." + this.elementKeys.commentEntryEditLinkClass).show();
                commentEntry.find("." + this.elementKeys.commentEntryDeleteLinkClass).show();
                hasCurrentUserComment = true;
            } else {
                commentEntry.find("." + this.elementKeys.commentEntryEditLinkClass).hide();
                commentEntry.find("." + this.elementKeys.commentEntryDeleteLinkClass).hide();
            }
            $("#" + this.elementKeys.displayComments).append(commentEntry);
        }
    } else {
        if (!this.validationRules.canModify) {
            $("#" + this.elementKeys.displayComments).append($("<div/>").html(this.validationRules.noCommentsMessage));
        }
    }
    // Show display comment section
    $("#" + this.elementKeys.displayComments).show();
    // Hide add/edit comment section when necessary
    if ((hasCurrentUserComment && this.oneCommentPerUser) || !this.validationRules.canModify) {
        $("#" + this.elementKeys.saveCommentSection).hide();
    } else {
        $("#" + this.elementKeys.saveCommentSectionId).val("");
        $("#" + this.elementKeys.saveCommentSectionComment).val("");
        $("#" + this.elementKeys.addCommentTitle).show();
        $("#" + this.elementKeys.editCommentTitle).hide();
        $("#" + this.elementKeys.cancelSaveCommentButton).hide();
        $("#" + this.elementKeys.saveCommentSection).show();
    }
};
// Edit comment
CommentWidget.prototype.editComment = function ($this) {
    var entry = $($this).closest("." + this.elementKeys.commentEntrySectionClass);
    var commentId = entry.attr("data-id");
    var userId = entry.attr("data-userid");
    // Decode first and then display
    var comment = $.htmlDecode(entry.find("." + this.elementKeys.commentEntryCommentClass).html());
    if (parseInt(userId) === this.userId && this.validationRules.canModify) {
        $("#" + this.elementKeys.saveCommentSectionId).val(commentId);
        $("#" + this.elementKeys.saveCommentSectionComment).val(comment);
        $("#" + this.elementKeys.displayComments).hide();
        $("#" + this.elementKeys.saveCommentSection).show();
        $("#" + this.elementKeys.addCommentTitle).hide();
        $("#" + this.elementKeys.editCommentTitle).show();
        $("#" + this.elementKeys.cancelSaveCommentButton).show();
        $("#" + this.elementKeys.saveCommentSectionComment).focus();
    }
};
// Cancel save comment
CommentWidget.prototype.cancelSaveComment = function ($this) {
    $("#" + this.elementKeys.saveCommentSectionId).val("");
    $("#" + this.elementKeys.saveCommentSectionComment).val("");   
    $("#" + this.elementKeys.addCommentTitle).show();
    $("#" +this.elementKeys.editCommentTitle).hide();
    $("#" +this.elementKeys.cancelSaveCommentButton).hide();
};
// Whether the comment can be saved
CommentWidget.prototype.canSaveComment = function (comment) {
    if (this.validationRules.maxCommentLength === 0 || comment.length <= this.validationRules.maxCommentLength) {
        if (this.validationRules.canModify) {
            return true;
        } else {
            return false;
        }
    } else {
        alert(this.validationRules.invalidCommentLengthMessage);
        // Set focus on the comment box
        $("#" + this.elementKeys.saveCommentSectionComment).focus();
        return false;
    }
};