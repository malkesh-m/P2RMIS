// Click event for heading column to show either file/url/video
$(document).on('click', '.linkPage', function (e) {
    var docId = $(e.target).attr('data-id');
    var link = $(e.target).attr('data-link');
    var video = $(e.target).attr('data-video');
    var path = $(e.target).attr('data-path');
    var newHeading = $(e.target).text();
    var newDescription = $(e.target).closest('tr').find('td:nth-child(2)').text();
    sessionStorage.setItem('videoPath', video);
    if (video.toLowerCase() == "true") {
        sessionStorage.setItem('videoLink', path);
        //sessionStorage.setItem('newHeading', newHeading);
        sessionStorage.setItem('newDescription', newDescription);
        $.get('/Setup/VimeoModal', { docId: docId }, function (data) {
            p2rims.modalFramework.displayModalNoEvent(data, p2rims.modalFramework.customModalSizes.large, newHeading);
            $(".modal-footer").append(p2rims.modalFramework.defaultModalFooters.closeFooter);
            $("a[id='closeModal'], button[id='closeDialogBtn']").click(function () { $('.ui-dialog-titlebar-close').click(); });
        })
    } else if (link.toLowerCase() == "true") {
        window.open("https://" + path, "_blank");
    } else {
        window.open("/Setup/ViewDocumentManagementFile?documentId=" + docId);
    }
})