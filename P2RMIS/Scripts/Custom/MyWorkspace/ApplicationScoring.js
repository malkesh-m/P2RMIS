'use strict';

// Application Scoring
var gSetSessionPanelUrl;
var gViewApplicationModalUrl;

$(function () {
    // Session panel change event handler
    $('#selectedSessionPanel').change(function (e) {
        e.preventDefault();
        var postMeeting = $('#postAssignmentSection h5').text().indexOf('Meeting');
        if (postMeeting > 0) {
            $('#showAssignedChkBx').attr('checked', false);
        }
        $('#preAssignmentSection').hide();
        $('#applicationScoringSection').show();
    });
});

// Disable or enable drop-down
$("#selectedSessionPanel").prop("disabled", $("#selectedSessionPanel").children().length > 1 ? false : true);
// Fix the drop-down value when needed
var sessionPanelId = $("#originalSessionPanelId").val();
if ($("#selectedSessionPanel").val() !== sessionPanelId) {
    $("#selectedSessionPanel").val(sessionPanelId);
}
// scripts for drop down menu
$("#selectedSessionPanel").on("change", function () {
    // Call ajax to set session
    var sessionPanelId = $(this).val();
    var postMeeting = $('#postAssignmentSection h5').text().indexOf('Meeting');
    if (postMeeting > 0) {
        $('#showAssignedChkBx').attr('checked', false);
    }
    if (sessionPanelId !== "") {
        $.ajax({
            cache: false,
            url: gSetSessionPanelUrl,
            data: { "sessionPanelId": sessionPanelId }
        }).done(function (data) {
            if (data) {
                document.location.reload();
            }
        }).fail(function (xhr, ajaxOptions, thrownError) {
            alert($.defaultFailureMessage);
        });
    }
});
//script for ajax calls to get the modal preview window
$('body').on('click', '.applicationLogNumber', function (e) {
    e.preventDefault();
    
    // Get the parameters to pass
    var logNo = $(this).text();
    var appId = $(this).attr("data-applicationid");
    var title = 'Application ' + logNo;

    // load the data via ajax
    $.get(gViewApplicationModalUrl, { "applicationId": appId },
        function (responseText, textStatus, XMLHttpRequest) {
            p2rims.modalFramework.displayModalNoEvent(responseText, p2rims.modalFramework.customModalSizes.large, title);
            $('.modal-footer').remove();
            $('.ui-dialog').append('<div class="modal-footer"></div>');
            $(".modal-footer").append(p2rims.modalFramework.defaultModalFooters.closeFooter);
            $("a[id='closeModal'], button[id='closeDialogBtn']").click(function () { $('.ui-dialog-titlebar-close').click(); });
        }
    );
});

if ($('#notRegistered').is(':visible')) {
    $('#filterTableOpts .critique-overdue').hide();
} 

if ($('#postAssignmentSection').is(':visible')){
    if ($('#postAssignmentSection h5').filter(':contains("Online")').length > 0 || $('#postAssignmentSection h5').filter(':contains("Revised")').length > 0 || $('#postAssignmentSection h5').filter(':contains("Preliminary")').length > 0 ) {
        $('#filterTableOpts .critique-overdue').show();
    } else {
        $('#filterTableOpts .critique-overdue').hide();
    }
} else if ($('#applicationScoringSection').is(':visible')) {
    if ($('#applicationScoringSection h5').filter(':contains("Meeting")').length > 0) {
        $('#filterTableOpts .critique-overdue').show();
    } else {
        $('#filterTableOpts .critique-overdue').hide();
    }
} else if ($('#preAssignmentSection').is(':visible')) {
    if ($('#preAssignmentSection h5').length > 0) {
        $('#filterTableOpts .critique-overdue').hide();
    } else {
        $('#filterTableOpts .critique-overdue').show();
    }
}

