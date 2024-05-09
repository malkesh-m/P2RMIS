// Recruitment Status validation
var getUserInfo = $('#UserInfoId').val();
if (getUserInfo > 0) {
    $.ajax({
        cache: false,
        url: "/UserProfileManagement/GetUserClientBlocks",
        data: { 'userInfoId': $('#UserInfoId').val() },
    }).done(function (results) {
        var clientResults = results.clients.length;
        var totalClientsValue = " - ";
        if (clientResults > 0) {
            $('#openRecruitment').hide();
            $('#blockedClients, blockedClient').show();
            var totalClients = results.clients;
            $(totalClients).each(function (i, value) {
                totalClientsValue += value.Value + ', ';
            })
            totalClientsValue = totalClientsValue.slice(0, -2);
            $('#blockedClients').text(totalClientsValue);
        } else {
            $('#openRecruitment').show();
            $('#blockedClients, #blockedClient').hide();
        }
    })
}


// Recruitment Opening Modal
$('#openRecruitment, #blockedClient').on('click', function () {
    var title = "Manage Block";
    var userInfoId = $('#UserInfoId').val();
    $.get('/UserProfileManagement/ManageBlock', { userInfoId: userInfoId }, function (data) {
        var newCount = 0;
        p2rims.modalFramework.displayModalNoEvent(data, p2rims.modalFramework.customModalSizes.large, title);
        $(".modal-footer").append(p2rims.modalFramework.defaultModalFooters.cancelSaveBlockFooter);
        $('button#saveClientBlock').attr('disabled', 'disabled');
        $("a[id='closeModal'], button[id='closeDialogBtn']").click(function () { $('.ui-dialog-titlebar-close').click(); });
        $('#clientBlockedTable tr td:nth-child(1) input').each(function (i, value) {
            var isChecked = $(this).is(':checked');
            if (isChecked) {
                newCount++;
            }
        })
        if (newCount == 0) {
            $('#textareaModal').attr('disabled', 'disabled');
        } else {
            $('#textareaModal').attr('disabled', 'disabled');
        }
    })
})

var profileTypeId = $('#profileTypeId').val();
if (profileTypeId == 1 || profileTypeId == 2) {
    $('#recruitmentBox').show();
}
