'use strict';

var storedText = [];

function submitComment() {
    var comment = $("#discussionBoardComment").val();
    var applicationStageStepEntityId = $("#applicationStageStepEntityId").val();
    var applicationDiscussionEntityId = $("#applicationStageStepDiscussionEntityId").val();
    $.ajax({
        cache: false,
        type: 'POST',
        url: "/PanelManagement/SaveModComment",
        data: {
            "applicationStageStepEntityId": applicationStageStepEntityId,
            "applicationDiscussionEntityId": applicationDiscussionEntityId,
            "comment": comment
        }
    }).success(function () {
        refreshviewCommentsGrid();
    }).fail(function () {
        alert($.defaultFailureMessage);
    });
}

function refreshviewCommentsGrid() {
    //
    // retrieve the ApplicationStageStep entity id
    //
    var applicationStageStepEntityId = $("#applicationStageStepEntityId").val();
    var searchResults;
    //
    // Now we go to the server and retrieve the comments
    //
    $.ajax({
        url: '/PanelManagement/RefreshComments',
        data: { applicationStageStepEntityId: applicationStageStepEntityId },
        success: function (result) {
            searchResults = result;
        }
    }).done(function (results) {
        if (results != "") {
            var grid = $("#viewCommentsGrid").data("kendoGrid");
            var ds = populateCommentDatasource(JSON.parse(results));
            ds.read();
            grid.dataSource.data(ds.data());
            grid.setDataSource(grid.dataSource);
            grid.refresh();

            var textCount = 0;
            // Check for large comments
            $('.ellipsis-comment').each(function () {
                textCount++;
                var $ellipsisText = $(this);

                if (($ellipsisText.outerHeight(true) > 70)) {
                    $ellipsisText.closest('td').append('<div class="read-more" style="float: right; color: #08c; font-size: 10px; padding-right: 10px; cursor: pointer;">Read More</div>');
                    storedText.push({ index: textCount, containerText: $(this).text() })
                }
                while ($ellipsisText.outerHeight(true) > 70) {
                    $ellipsisText.text(function (index, text) {
                        return text.replace(/\W*\s(\S)*$/, '...');
                    });
                }
            });

            // Total data in Grid
            var sourcedata = ds.data()
            $('#totalCount span').text(sourcedata.length);
        }
    });
}

$(document).on('click', '.read-more', function (e) {
    var getText = $(this).text();
    var getIndex = $(e.target).closest("tr").index();

    if (getText == "Read More") {
        $(storedText).each(function (i, value) {
            if (getIndex == value.index - 1) {
                $('#viewCommentsGrid tbody tr:nth-child(' + (getIndex + 1) + ' ) .ellipsis-comment').css('height', '100%').text(value.containerText);
                $('#viewCommentsGrid tbody tr:nth-child(' + (getIndex + 1) + ' ) .read-more').text('Read Less');
            }
        })
    } else {
        $(storedText).each(function (i, value) {
            if (getIndex == value.index - 1) {
                var getContainer = $('#viewCommentsGrid tbody tr:nth-child(' + (getIndex + 1) + ') .ellipsis-comment');
                getContainer.css('height', 'unset')
                $('#viewCommentsGrid tbody tr:nth-child(' + (getIndex + 1) + ' ) .read-more').text('Read More');
                while (getContainer.height() > 70) {
                    getContainer.text(function (index, text) {
                        return text.replace(/\W*\s(\S)*$/, '...');
                    });
                }
            }
        })
    }

});

//
// Function refreshes the comments grid.
//
function openAddDBCommentModal() {
    var dialogTitle = "<span class='modalSmallCaption modalNotificationCaption'>Comment</span>";
    $.get("/PanelManagement/AddDBComment", function (responseText) {
        p2rims.modalFramework.displayModalNoEvent(responseText, p2rims.modalFramework.customModalSizes.medium, dialogTitle);
        $(".ui-dialog .modal-footer").remove();
        $(".ui-dialog").append('<div class="modal-footer"></div>')
        $(".modal-footer").append(p2rims.modalFramework.defaultModalFooters.cancelSaveFooter);
        $("a[id='closeModal'], button[id='closeDialogBtn']").click(function () { $('.ui-dialog-titlebar-close').click(); });
        $("button[id='saveDialogChanges']").click(function () {
            //
            //add comment submit code here
            //
            submitComment();
            $('.ui-dialog-titlebar-close').click();
        });
        $('.ui-dialog-content').keypress(function (e) {
            if (e.keyCode === 13 && !$('#discussionBoardComment').is(":focus")) {
                $("button[id='saveDialogChanges']").click();
            }
        });
    });
}

function authorPartTypeBuild() {
    return "# if (authorPartType) { # (${authorPartType} ${authorOrderNo}) # } #";
}
function authorRoleBuild() {
    return "# if (authorRole) { # - ${authorRole} # } #";
}
function authorBuild() {
    return "# if(authorModerator){ # <b><span style='color:black' data-date='${authorDate}'> ${authorNameAndRole}" + "<br />${formatedAuthorDate}<br />Moderator</span></b> # }" +
        "else { # <span data-date='${authorDate}'>${authorNameAndRole}" + "<br />${formatedAuthorDate}</span> # } #";
}
function partListBuild() {
    return "# if(authorModerator){ # <b><span>${authorPartType}- ${authorFName} ${authorLName} <${authorPhone}></span><hr /></b> # }" +
        "else if (chairPerson) { # <b><span>Chairperson- ${authorFName} ${authorLName} </span></b> # }" +
        "else { # ${authorLName}, ${authorFName} " + authorPartTypeBuild() + authorRoleBuild() + " # } #";
}
//
// Provides the markup for a enabled 'Add Comment' icon
//
function addCommentLinkEnabled() {
    return "<span style='float:right'><a href='javascript:;' onclick='openAddDBCommentModal()'>" +
        "<img src='/Content/img/icon_add_comment_18x18.png' title='Add Comment' alt='Add Comment' style='border:0;width:22px' /></a></span>";
}

//
// Provides the markup for a disabled 'Add Comment' icon
//
function addCommentLinkDisabled() {
    return "<span style='float:right'><img src='/Content/img/icon_add_comment_18x18_disabled.png' style='border:0;width:22px' /></span>";
}
function formatCommentsGrid() {
    $(".k-header").each(function () { $(this).addClass('center-align'); });
    $("#commentsCount").html($("#viewCommentsGrid").data('kendoGrid').dataSource.total());
    $("#allCommentsCount").html($("#discussionCommentsCount").val());
}

//
// Populates the datasource for the kendo comment grid.  The kendo comment grid datasource
// is initially populated when the grid is loaded and populated when the grid is refreshed.
//
function populateCommentDatasource(results) {
    var datasource = new kendo.data.DataSource({
        type: "json",
        data: results,
        schema: {
            model: {
                fields: {
                    authorDate: { type: "date", parse: parseDate },
                    authorFName: { type: "string" },
                    authorLName: { type: "string" },
                    authorPartType: { type: "string" },
                    authorOrderNo: { type: "string" },
                    authorRole: { type: "string" },
                    formatedAuthorDate: { type: "string" },
                    authorPhone: { type: "string" },
                    authorModerator: { type: "boolean" },
                    comment: { type: "string" },
                }
            },
        },
        sort: { field: "authorDate", dir: "desc" }
    });
    return datasource
}

//
// Enables the Kendo datasource to be sorted by the comment date.
//
function parseDate(rawDate) {
    return Date.parse(rawDate);
}

$(function () {
    $("#viewCommentsGrid").kendoGrid({
        height: 400,
        pageable: false,
        columns: [{
            title: "Comment", field: "comment", width: "80%", filterable: false, template: "<div class='ellipsis-comment'>${comment}</div>"
        },
        {
            title: "Author", field: "authorNameAndRole", filterable: true, template: kendo.template(authorBuild())
        }],
        dataBound: formatCommentsGrid,
        filterable: { extra: false }
    });

    // Open model if there are no comments
    if (parseInt($("#discussionCommentsCount").val()) == 0) {
        var isDone = $('#isDisscussionDone').val();
        if (isDone == 'False') {
            openAddDBCommentModal();
        }
    }

    refreshviewCommentsGrid();
});