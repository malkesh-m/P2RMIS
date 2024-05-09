function showFileDownloadWarning(fun, param, url) {
    $.get('/Home/DownloadWarning', function (data) {
        var dialogTitle = 'Data Security Terms & Conditions';
        $('.modal-footer').remove();
        p2rims.modalFramework.displayModalNoEvent(data, p2rims.modalFramework.customModalSizes.medium, dialogTitle);
        $(".modal-footer").append(p2rims.modalFramework.defaultModalFooters.cancelAcceptFooter);
        $("a[id='closeModal'], button[id='closeDialogBtn']").click(function () { $('.ui-dialog-titlebar-close').click(); });

        //close dialog and call function
        if (fun !== undefined && param !== undefined) {
            $("#modal-confirm-reset-to-edit-button").click(function () { $('.ui-dialog-titlebar-close').click(); fun(param); });
        }
        //close dialog and call execute url
        else if (url !== undefined) {
            $("#modal-confirm-reset-to-edit-button").wrap("<a onclick=\"$('.ui-dialog-titlebar-close').click()\"; href=" + encodeURI(url) + "></a>")            
        }
    });
}