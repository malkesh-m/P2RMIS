var EDITOR = EDITOR || {};

// Get markup content
EDITOR.getMarkupContent = function (content, currentUserId, canViewAllChanges, canViewAllComments, removeCleanModeStyles) {
    var contentEl = $("<div />").append(content);
    contentEl.find("[title], [data-title]").each(function () {
        if ($(this).hasClass("reU1") || $(this).hasClass("reUX")) {
            var regExp = /\(([^)]+)\)/;
            var matches = regExp.exec($(this).attr("author"));
            if (canViewAllChanges || parseInt(matches[1]) === currentUserId) {
                $(this).removeClass("reUX").addClass("reU1")
                if (!!$(this).attr("title")) {
                    $(this).attr("title", $(this).attr("data-title")).removeAttr("data-title");
                }
                $(this).removeAttr("style");
            } else {
                $(this).removeClass("reU1").addClass("reUX");
                if (!!$(this).attr("data-title")) {
                    $(this).attr("data-title", $(this).attr("title")).removeAttr("title");
                }
                if ($(this).prop("tagName") === "DEL") {
                    $(this).attr("style", "display:none;");
                } else if ($(this).prop("tagName") === "INS") {
                    $(this).attr("style", "text-decoration:none;");
                }
            }
        }
        if ($(this).hasClass("reCustomComment") || $(this).hasClass("reCustomCommentX")) {
            if (canViewAllComments || parseInt($(this).attr("userid")) === CurrentUserId) {
                $(this).removeClass("reCustomCommentX").addClass("reCustomComment");
                if (!!$(this).attr("title")) {
                    $(this).attr("title", $(this).attr("data-title")).removeAttr("data-title");
                }
                $(this).removeAttr("style");
            } else {
                $(this).removeClass("reCustomComment").addClass("reCustomCommentX");
                if (!!$(this).attr("data-title")) {
                    $(this).attr("data-title", $(this).attr("title")).removeAttr("title");
                }
            }
        }
    });
    if (removeCleanModeStyles) {
        EDITOR.removeCleanModeStyles(contentEl);
    }
    return contentEl.html();
};
// Get preview/final content 
EDITOR.getFinalContent = function (content) {
    var contentEl = $("<div />").append(content);
    contentEl.find("[title], [data-title]").each(function () {
        if ($(this).hasClass("reCustomComment")) {
            $(this).removeClass("reCustomComment").addClass("reCustomCommentX");
        } else if ($(this).hasClass("reU1")) {
            $(this).removeClass("reU1").addClass("reUX");
        }
        if (!!$(this).attr("data-title")) {
            $(this).attr("data-title", $(this).attr("title")).removeAttr("title");
        }
        if ($(this).prop("tagName") === "DEL") {
            $(this).attr("style", "display:none;");
        } else if ($(this).prop("tagName") === "INS") {
            $(this).attr("style", "text-decoration:none;");
        }
    });
    return contentEl.html();
}
// Get content for storage
EDITOR.getStorableContent = function (content) {
    var contentEl = $("<div />").append(content);
    contentEl.find("[class='reCustomCommentX'][data-title]").each(function () {
        $(this).removeClass("reCustomCommentX").addClass("reCustomComment")
        $(this).attr("title", $(this).attr("data-title")).removeAttr("data-title");
        $(this).removeAttr("style");
    });
    contentEl.find("[class='reUX'][data-title]").each(function () {
        $(this).removeClass("reUX").addClass("reU1")
        $(this).attr("title", $(this).attr("data-title")).removeAttr("data-title");
        $(this).removeAttr("style");
    });
    EDITOR.removeCleanModeStyles(contentEl);
    return contentEl.html();
};
// Clean content with hidden comment
EDITOR.getCleanContentWithHiddenComments = function (content) {
    var contentEl = $("<div />").append(content);
    contentEl.find("[class^='re'][data-hide=1]").each(function () {
        $(this).removeClass("reCustomComment").addClass("reCustomCommentX");
    });
    return contentEl.html();
};

// Get un-styled content for comparison 
EDITOR.getUnstyledContent = function (content) {
    var contentEl = $("<div />").append(content);
    // Remove class/title attributes
    contentEl.find("[class^='re'][title],[class^='re'][data-title]").each(function () {
        $(this).removeAttr("class");
        $(this).removeAttr("title").removeAttr("data-title");
        $(this).removeAttr("style");
    });
    return contentEl.html();
};
// Remove clean mode styles
EDITOR.removeCleanModeStyles = function (ele) {
    var commentElements = ele.find(".reCustomCommentX").filter(":not([data-hide=1])");
    commentElements.each(function () {
        $(this).removeClass("reCustomCommentX").addClass("reCustomComment");
    });
    var changeTrackingElements = ele.find(".reUX");
    changeTrackingElements.each(function () {
        $(this).removeClass("reUX").addClass("reU1");
    });
};
// Set change tracking markup on or off for closest changed element
EDITOR.setChangeTrackingMarkupStatusForClosestElement = function (container, flag) {
    if (flag) {
        EDITOR.setChangeTrackingMarkupStatus(container.closest(".reUX"), flag);
    } else {
        EDITOR.setChangeTrackingMarkupStatus(container.closest(".reU1"), flag);
    }
}
// Set change tracking markup on or off
EDITOR.setChangeTrackingMarkupStatus = function ($this, flag) {
    if (flag) {
        $this.removeClass("reUX").addClass("reU1");
        if ($this.hasClass("reFormatX")) {
            $this.removeClass("reFormatX").addClass("reFormat");
        }
    } else {
        $this.removeClass("reU1").addClass("reUX");
        if ($this.hasClass("reFormat")) {
            $this.removeClass("reFormat").addClass("reFormatX");
        }
    }
};
// Set comment markup on or off
EDITOR.setCommentMarkupStatus = function ($this, flag) {
    if (flag) {
        $this.removeClass("reCustomCommentX").addClass("reCustomComment");
    } else {
        $this.removeClass("reCustomComment").addClass("reCustomCommentX");
    }
};
// Filter markup
EDITOR.filterMarkup = function ($doc, reviewerList, displayHiddenComments) {
    $doc.find(".reCustomComment,.reCustomCommentX").each(function () {
        if (displayHiddenComments || $(this).is(":not([data-hide=1])")) {
            EDITOR.setCommentMarkupStatus($(this), true);
        } else {
            EDITOR.setCommentMarkupStatus($(this), false);
        } 
    });
    $doc.find(".reU1,.reUX").each(function() {
        var author = $(this).attr("author").replace(/\s*\(.*?\)\s*/g, "");
        if ($.inArray(author, reviewerList) == -1) {
            EDITOR.setChangeTrackingMarkupStatus($(this), false);
        } else {
            EDITOR.setChangeTrackingMarkupStatus($(this), true);
        }
    });
};
// Set markup status
EDITOR.setMarkupStatus = function ($doc, isMarkupOn, displayHiddenComments) {
    if (isMarkupOn) {
        var commentElements = $doc.find(".reCustomCommentX");
        if (!displayHiddenComments) {
            commentElements = commentElements.filter(":not([data-hide=1])");
        }
        commentElements.each(function () {
            EDITOR.setCommentMarkupStatus($(this), true);
        });
        var changeTrackingElements = $doc.find(".reUX");
        changeTrackingElements.each(function () {
            EDITOR.setChangeTrackingMarkupStatus($(this), true);
        });
    } else {
        var changeTrackingElements = $doc.find(".reU1");
        changeTrackingElements.each(function () {
            EDITOR.setChangeTrackingMarkupStatus($(this), false);
        });
    }
};

EDITOR.getReviewersJson = function ($doc) {
    var reviewersJson = {};
    var reviewerArray = [];
    var filteredReviewerArray = [];
    // Get reviewers for the filter
    $doc.find(".reCustomComment, .reU1").each(function () {
        if ($(this).attr("author")) {
            var author = $(this).attr("author").replace(/\s*\(.*?\)\s*/g, "");
            if ($.inArray(author, reviewerArray) == -1) {
                reviewerArray.push(author);
                filteredReviewerArray.push(author);
            }
        }
    });
    $doc.find(".reCustomCommentX, .reUX").each(function () {
        if ($(this).attr("author")) {
            var author = $(this).attr("author").replace(/\s*\(.*?\)\s*/g, "");
            if ($.inArray(author, reviewerArray) == -1) {
                reviewerArray.push(author);
            }
        }
    });
    reviewersJson.reviewers = reviewerArray;
    reviewersJson.filteredReviewers = filteredReviewerArray;
    return reviewersJson;
};