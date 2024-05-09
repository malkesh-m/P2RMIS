function emailAddressSeparator() { return ";"; }
function attachmentSeparator() { return ","; }
///
/// Main controlling method to update the view.
///
function updateModal()
{
    $(".field-validation-error").children().not(".file").remove();

    value = $(".field-validation-error").text();
    if (value == "") {
        $('#modalSendEmail').removeAttr("disabled", "disabled");
    }
    else {
        $('#modalSendEmail').attr("disabled", "disabled");
    }

    updateToLine();
    updateCCLine();
    updateBCCLine();
    updateFromLine();
    updateSubjectLine();
    updateMessageLine();
    updateAttachmentLine();


}
//
// Updates the modal To line
//
function updateToLine() {
    var result = "";
    $('#recipientEmail :selected').each(function (i, selected) {
        result += " " + $(selected).text() + emailAddressSeparator();
    });

    $('#modalTo').text(removeLast(result, emailAddressSeparator()));

    if (result == "") {
        $(".field-validation-error").append($("<div/>").html("To is required"));
        $('#modalSendEmail').attr("disabled", "disabled");
    }
}
//
// Updates the modal CC line
//
function updateCCLine() {
    var result = "";
    $('#ccEmail :selected').each(function (i, selected) {
        result += " " + $(selected).text() + emailAddressSeparator();
    });

    $('#modalCc').text(removeLast(result, emailAddressSeparator()));
}
//
// Updates the modal BCC line
//
function updateBCCLine() {
    value = $('#bcc').val();
    $('#modalBcc').text(value);
}
//
// Updates the modal From line
//
function updateFromLine() {
    value = $('#from').val();
    $('#modalFrom').text(value);

    if (value == "") {
        $(".field-validation-error").append($("<div/>").html("From is required"));
        $('#modalSendEmail').attr("disabled", "disabled");
    }
}
//
// Updates the modal Subject line
//
function updateSubjectLine()
{
    value = $('#subject').val();
    $('#modalSubject').text(value);

    if (value == "") {
        $(".field-validation-error").append($("<div/>").html("Subject is required"));
        $('#modalSendEmail').attr("disabled", "disabled");
    }
}
//
// Updates the modal Message line
//
function updateMessageLine() {
    value = $('#editor').val();
    msg = $('#modalMessage').html($.htmlDecode(value));

    if ($.trim(value) == "") {
        $(".field-validation-error").append($("<div/>").html("Message is required"));
        $('#modalSendEmail').attr("disabled", "disabled");
    }

}
//
// Updates the modal Attachment line
//
function updateAttachmentLine() {
    var result = "";
    $('.k-filename').each(function (i, selected) {
        console.info($(selected).text());
        result += " " + $(selected).text() + attachmentSeparator();
    });
    //
    // Now remove the last comma if any & update the node
    //
    $('#modalAttachments').text(removeLast(result, attachmentSeparator()));
}
//
// Remove the specified last character
//
function removeLast(result, target)
{
    x = result.lastIndexOf(target);
    if (x > 0) {
        result = result.slice(0, x);
    }
    return result;
}

$('#selectAllEmails').on('click', function () {
    var selectedEmails = !$(this).is('.k-state-selected');
    $('#recipientEmail_listbox li').each(function (index, value) {
        var doesHave = !$(value).hasClass('k-state-selected');
        if (doesHave) {
            $(this).click();
        }
    })
})
