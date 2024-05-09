$(function () {
    if ($.toBoolean($("#ShowSendCredentialsModal").val())) {
        var modalTitle = "Notification";
        $(".modal-footer").remove();
        p2rims.modalFramework.displayModalNoEvent($("#notificationDialog").html(), p2rims.modalFramework.customModalSizes.medium, modalTitle);
        $(".modal-footer").append(p2rims.modalFramework.defaultModalFooters.notificationFooter);
        $("a[id='closeModal'], button[id='closeDialogBtn']").click(function () { $('.ui-dialog-titlebar-close').click(); });
    }

    $(document).on('click', '#modal-confirm-send-credential-button', function (e) {
        e.preventDefault();
        // Ajax call to initiate the 'SendCredentials' request
        $.ajax({
            cache: false,
            url: '/UserProfileManagement/SendCredentials',
            data: { "targetUserId": $("#UserId").val() }
        }).done(function (data) {
            var f = JSON.parse(data);
            if (f.Status) {
                //
                // Update the Send Credentials section of the screen
                //
                $('#sentBy').text(f.SentByName);
                $('#sentDate').text(f.SentByDate);

                $('#accountStatus').text(f.ManageAccountAccountStatus);
                $('#accountStatusDate').text(f.SentByDate);

            }
        }).fail(function (xhr, ajaxOptions, thrownError) {
            alert($.defaultFailureMessage);
        });
        $(this).closest(".ui-dialog .ui-dialog-content").dialog("close");
    });
});