// Initialize
var selectedContractType, selectedContractTypeId;

$("#saveDialogChanges").css("display", "none");

$("div").on("change", function () {
    $("#errorMessage").text("");
    $("#errorMessage").hide();
});

// Contract staus drop-down change event handler
$("#contractStatusDropdown").on("change", function () {
    selectedContractType = $(this).find(':selected').text().trim();
    selectedContractTypeId = $(this).find(':selected').val();
    var canAddAddendum = $("#canAddAddendum").val().toLowerCase() === 'true';
    // first hide/disable all of them just in case
    $("[id^='divContractStatusId']").hide().find('input').prop('disabled', true);
    //show selected div
    $("#divContractStatusId" + selectedContractTypeId).show().find('input').prop('disabled', false);
    //toggle saveDialog display
    //hidden completely
    if (selectedContractTypeId === "" || (selectedContractTypeId === "2" && !canAddAddendum)) {
        $("#saveDialogChanges").css("display", "none");
        $("#saveDialogChanges").prop("disabled", true);
        $("#wordLink").css("display", "none");
    }
    //disabled
    else if (selectedContractTypeId === "2" || selectedContractTypeId === "5") {
        $("#saveDialogChanges").css("display", "inline-block");
        $("#saveDialogChanges").prop("disabled", true);
        $("#wordLink").css("display", "inline-block");
        uploader.uploadKendo();
    }
    //enabled
    else {
        $("#saveDialogChanges").css("display", "inline-block");
        $("#saveDialogChanges").prop("disabled", false);
        $("#wordLink").css("display", "none");
    }
    ModalDialogReposition();
});
$("#feeAmount,#contractFile").on("change", function () {
    $("#saveDialogChanges").prop("disabled", false);
});

function ValidateCustomizeContractModal() {

    switch (selectedContractTypeId) {
        case "3":
            if ($("#bypassReason").val() === "" || $("#bypassReason").val().length < 3 ) {
                $("#errorMessage").text("Please enter at least 3 characters for your comment");
                $("#errorMessage").show();
                return false;
            }
            break;
        case "2": case "5":
            //here we use name instead of Id since kendo is adding an extra
            if ($("input[name='CustomContractFile']:enabled")[0].value.toLowerCase().replace(/^.*\./, '') !== "pdf") {
                $("#errorMessage").text("File must be in PDF format.");
                $("#errorMessage").show();
                return false;
            }
            if ($("#feeAmount:enabled").val().length === 0 || !($.isNumeric($("#feeAmount:enabled").val())) || $("#feeAmount:enabled").val() < 0) {
                $("#errorMessage").text("Consultant Fee is required.");
                $("#errorMessage").show();
                return false;
            }
            break;

        default:
    }

    return true;
}

function ModalDialogReposition() {
    if ($('#ModalDialog').dialog('isOpen')) {
        var winHeight = $('#ModalDialog').height();
        if (winHeight > 400) {
            $("#ModalDialog").dialog({ position: { my: "top center", at: "top center", of: window } });
        }
    }
}
function showWordTemplateDownloadWarning(panelUserAssignmentId) {
    customizedContractReasonIndex = $('#contractStatusDropdown').val();
    showFileDownloadWarning(showCustomizedContactModalAgain, panelUserAssignmentId);
}
//kendo upload functionality
var uploader = {
    uploadKendo:
        function () {
            // File upload
            $("#contractFile:enabled").kendoUpload({
                localization: {
                    select: "Browse"
                },
                multiple: false,
                select: function (e) {
                    $(".field-validation-error").empty();
                    for (var i = 0; i < e.files.length; i++) {
                        var file = e.files[i];
                        if (file.extension != ".pdf") {
                            $(".field-validation-error").append($("<div/>").html(INVALID_FILE_FORMAT).addClass("redColor"));
                            e.preventDefault();
                            break;
                        }
                    }
                }
            });

        }
};


