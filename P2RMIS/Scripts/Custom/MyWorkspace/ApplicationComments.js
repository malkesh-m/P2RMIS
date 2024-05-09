// Script for general comments
// Assign id and class names
var UserId = parseInt($("#checkoutUserId").val());
var CanModifyGeneralComments = $.toBoolean($("#canModifyGeneralComments").val());
var OneCommentPerUser = true;
var ElementKeys = {
    displayComments: "displayGeneralComments",
    saveCommentSection: "saveGeneralCommentSection",
    saveCommentSectionId: "generalCommentId",
    saveCommentSectionComment: "generalComment",
    addCommentTitle: "addGeneralCommentTitle",
    editCommentTitle: "editGeneralCommentTitle",
    cancelSaveCommentButton: "cancelSaveGeneralComment",
    commentEntryTemplate: "commentEntryTemplate",
    commentEntrySectionClass: "commentEntry",
    commentEntryCommentClass: "commentEntryComment",
    commentEntryAuthorClass: "commentEntryAuthor",
    commentEntryPublishedDateClass: "commentEntryPublishedDate",
    commentEntryEditLinkClass: "commentEntryEdit",
    commentEntryDeleteLinkClass: "commentEntryDelete",
};
var ValidationRules = {
    maxCommentLength: GetGeneralCommentMaxCharLimit(), // -1 means disabled
    canModify: CanModifyGeneralComments,
    noCommentsMessage: "No application comments have been submitted.",
    invalidCommentLengthMessage: "You have exceeded the maximum character limit of " + GetGeneralCommentMaxCharLimit() +
            ". Please reduce the number of characters to " + GetGeneralCommentMaxCharLimit() + "."
};
//TODO: get names from Routing.cs
var AjaxUrls = {
    viewComments: '/MyWorkspace/ViewComments',
    addComment: '/MyWorkspace/AddComment',
    editComment: '/MyWorkspace/EditComment',
    deleteComment: '/MyWorkspace/DeleteComment'
};
// Initialize comment widget
var commentWidget = new CommentWidget(UserId, ElementKeys, ValidationRules, OneCommentPerUser);

// Get max char limit
function GetGeneralCommentMaxCharLimit() {
    return parseInt($("#generalCommentMaxCharLimit").val());
}

// Script for general comments
$('#generalCommentsModal').dialog({
    autoOpen: false,
    width: 990,
    height: 555,
    modal: true,
    resizable: false,
    title: "Application Comments"
});
// Add general comment button in tool bar
$("#addGeneralComment").on("click", function (e) {
    e.preventDefault();
    fnLoadGeneralComments();
});

// Load general comments
var fnLoadGeneralComments = function () {
    var applicationId = $("#applicationId").val();
    var panelApplicationId = $("#panelApplicationId").val();
    $.ajax({
        cache: false,
        type: 'GET',
        url: AjaxUrls.viewComments,
        data: { "applicationId": applicationId, "panelApplicationId": panelApplicationId }
    }).done(function (data) {
        // Display comments
        var commentJsonArray = [];
        for (var i = 0; i < data.Notes.length; i++) {
            var commentJson = {
                commentId: data.Notes[i].CommentID,
                comment: data.Notes[i].Comment,
                userId: data.Notes[i].UserId,
                firstName: data.Notes[i].FirstName,
                lastName: data.Notes[i].LastName,
                timeStamp: data.Notes[i].CommentDate
            };
            commentJsonArray.push(commentJson);
        }
        // Reset whether it can modify comments
        commentWidget.validationRules.canModify = data.CanAddEditComments;
        commentWidget.addCommentsToBody(commentJsonArray);
        if (!data.CanAddEditComments) {
            $('#saveGeneralCommentSection').css('display','none');
            $("#ui-id-2").text("Unassigned Reviewer Comments");
            $('#generalCommentsModal').dialog("open");
            if ($('.modal-footer').is(':visible')) {
                $('.modal-footer').remove();
            }
            $('.ui-dialog').append('<div class="modal-footer"></div>');
            $(".modal-footer").append(p2rims.modalFramework.defaultModalFooters.closeFooter);
            $("a[id='closeModal'], button[id='closeDialogBtn']").click(function () { $('.ui-dialog-titlebar-close').click() });
        } else {
            $('#generalCommentsModal').dialog("open");
            if ($('.modal-footer').is(':visible')) {
                $('.modal-footer').remove();
            }
            $('.ui-dialog').append('<div class="modal-footer"></div>');
            var commentShown = $('#displayGeneralComments div').length;
            if (commentShown > 0) {
                var comment = $('#generalComment').is(':visible');
                if (comment == true) {
                    $(".modal-footer").append(p2rims.modalFramework.defaultModalFooters.cancelSaveCommentFooter);
                    $("a[id='closeModal'], button[id='closeDialogBtn']").click(function () { $('.ui-dialog-titlebar-close').click() });
                } else {
                    $(".modal-footer").append(p2rims.modalFramework.defaultModalFooters.closeFooter);
                    $("a[id='closeModal'], button[id='closeDialogBtn']").click(function () { $('.ui-dialog-titlebar-close').click() });
                }
            } else {
                $(".modal-footer").append(p2rims.modalFramework.defaultModalFooters.cancelSaveCommentFooter);
                    $("a[id='closeModal'], button[id='closeDialogBtn']").click(function () { $('.ui-dialog-titlebar-close').click() });
            }
        }
        
    }).fail(function (xhr, ajaxOptions, thrownError) {
        alert($.defaultFailureMessage);
    });
};
// Save general comment button
$(document).on("click", "#saveGeneralComment", function (e) {
    e.preventDefault();
    var applicationId = $("#applicationId").val();
    var panelApplicationId = $("#panelApplicationId").val();
    var comment = $.trim($("#generalComment").val());
    var commentId = $.trim($("#generalCommentId").val());

    if (commentWidget.canSaveComment(comment)) {
        if (commentId === "") {
            $.ajax({
                cache: false,
                type: 'GET',
                url: AjaxUrls.addComment,
                data: { "comment": comment, "panelApplicationId": panelApplicationId }
            }).done(function (data) {
                // Re-load comments
                fnLoadGeneralComments();
            }).fail(function (xhr, ajaxOptions, thrownError) {
                alert($.defaultFailureMessage);
            });
        } else {
            $.ajax({
                cache: false,
                type: 'GET',
                url: AjaxUrls.editComment,
                data: { "comment": comment, "commentId": commentId }
            }).done(function (data) {
                // Re-load comments
                fnLoadGeneralComments();
            }).fail(function (xhr, ajaxOptions, thrownError) {
                alert($.defaultFailureMessage);
            });
        }
    }
});
// Cancel save general comment button
$(document).on("click", "#cancelSaveGeneralComment", function (e) {
    e.preventDefault();
    commentWidget.cancelSaveComment(this);
    // Re-load general comments
    fnLoadGeneralComments();
});
// Edit comment
$(document).on("click", ".commentEntryEdit", function (e) {
    e.preventDefault();
    commentWidget.editComment(this);
    if ($('.modal-footer').is(':visible')) {
        $('.modal-footer').remove();
    }
    $('.ui-dialog').append('<div class="modal-footer"></div>');
    $(".modal-footer").append(p2rims.modalFramework.defaultModalFooters.cancelSaveCommentFooter);
    $("a[id='closeModal'], button[id='closeDialogBtn']").click(function () { $('.ui-dialog-titlebar-close').click() });
});
// Delete comment
$(document).on("click", ".commentEntryDelete", function (e) {
    e.preventDefault();
    var entry = $(this).closest(".commentEntry");
    var commentId = parseInt(entry.attr("data-id"));

    if (commentId !== undefined && commentId > 0 && CanModifyGeneralComments) {
        $.ajax({
            cache: false,
            type: 'GET',
            url: AjaxUrls.deleteComment,
            data: { "commentId": commentId }
        }).done(function (data) {
            // Re-load comments
            fnLoadGeneralComments();
        }).fail(function (xhr, ajaxOptions, thrownError) {
            alert($.defaultFailureMessage);
        });
    }
});

// Trigger comment modal
$(document).on("click", ".application-comment", function (e) {
    e.preventDefault();
    var applicationId = $(this).attr("data-applicationid");
    $("#applicationId").val(applicationId);

    var panelApplicationId = $(this).attr("data-panelapplicationid");
    $("#panelApplicationId").val(panelApplicationId);

    fnLoadGeneralComments();
});

//function to perform submit operation
$('.newDisable').on('click', function (e) {
    e.preventDefault();
    console.log('hit it');
    if ($(this).attr('id') == 'submitDialog') {
        var applicationWorkflowId = $(".edit-critique").eq(0).closest(".mw-critiques_main_section").attr("data-workflowid");
        var elementId = $(".edit-critique").eq(0).closest(".mw-critiques_main_sub-section_critique").attr("data-elementid");
        var elementsToAbstain = [];
        $(".incomplete-critique-element").each(function () {
            elementsToAbstain.push($(this).attr("data-elementid"));
        });
        $.ajax({
            cache: false,
            type: 'POST',
            url: '@Url.Action(Routing.MyWorkspace.SetAbstains, Routing.P2rmisControllers.MyWorkspace)',
            data: {
                "applicationWorkflowId": applicationWorkflowId, "applicationWorkflowStepElementId": elementId, "elementsToAbstain": elementsToAbstain
            }
        }).done(function (result) {
            if (result.success) {
                // Populate data to parent window
                $.each(result.elementsAbstained, function (index) {
                    var critiqueSection = $(".mw-critiques_main_sub-section_critique[data-elementid=" + result.elementsAbstained[index].ApplicationWorkflowStepElementId + "]");
                    // Reset the contentId and abstain
                    critiqueSection.attr("data-contentid", result.elementsAbstained[index].ApplicationWorkflowStepElementContentId);
                    critiqueSection.attr("data-abstain", true);
                });

                // Create confirmation modal
                $.get("@Url.Action(Routing.MyWorkspace.GetNotificationOfSubmitModal, Routing.P2rmisControllers.MyWorkspace)", function (data) {
                    p2rims.modalFramework.displayModalNoEvent(data, p2rims.modalFramework.customModalSizes.small, '<span class="modalSmallCaption modalNotificationCaption">Notification</span>');
                });
            } else {

                alert($.defaultFailureMessage);
            }
        }).fail(function (xhr, ajaxOptions, thrownError) {
            alert($.defaultFailureMessage);
        });
    }
});