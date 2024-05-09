// Opens admin note model
$('body').on('click', '.admin-note-count', function (e) {
    e.preventDefault();
    var logNumber = $(this).attr("data-lognumber");
    var applicationId = $(this).attr("data-applicationId");
    var dialogTitle = "<span class='modalLargeCaption modalNotificationCaption'>Admin Note for " + logNumber + "</span>";
    $.get('/ManageApplicationScoring/AdminNotes',
        { applicationId: applicationId },
        function (responseText, textStatus, XMLHttpRequest) {
            p2rims.modalFramework.displayModalNoEvent(responseText, p2rims.modalFramework.customModalSizes.medium, dialogTitle);
            $('.ui-dialog.ui-widget .modal-footer').remove();
            $('.ui-dialog.ui-widget').append('<div class="modal-footer"></div>');
            $(".modal-footer").append(p2rims.modalFramework.defaultModalFooters.cancelSaveFooter);
            $("a[id='closeModal'], button[id='closeDialogBtn']").click(function () { $('.ui-dialog-titlebar-close').click() });
            $('button#saveDialogChanges').on('click', function (e) {
                console.log('test');
                e.preventDefault();
                if ($(this).attr('id') == 'saveDialogChanges') {
                    var note = $.trim($("#adminNote").val());
                    $.ajax({
                        cache: false,
                        type: 'POST',
                        url: '/ManageApplicationScoring/SaveAdminNote',
                        data: {
                            "applicationBudgetId": $("#applicationBudgetId").val(),
                            "applicationId": $("#applicationId").val(),
                            "note": note
                        }
                    }).success(function (data) {
                        console.log(data);
                        if (data && typeof setAdminNoteCount === "function") {
                            var cnt = note.length > 0 ? 1 : 0;
                            setAdminNoteCount($("#applicationId").val(), cnt);
                        }
                        $('.ui-dialog-titlebar-close').click();
                        $('.refresh-grid').click();
                    });


                }

            });
        }
    );
});
// Set admin note count
function setAdminNoteCount(applicationId, count) {
    if (count === 1) {
        $(".admin-note-count[data-applicationid='" + applicationId + "']").text("Edit");
    } else {
        $(".admin-note-count[data-applicationid='" + applicationId + "']").text("Add");
    }
}
