// Selectors
var successMessage = $('#successMessage'),
    validationMessage = $('#uploadFeeScheduleAlert');

// Page Calls
var feeScheduleUploadCalls = {
    kendoUpload:
        function () {
            // File upload
            var gridData = $("#ss-grid-feeSchedule").data().kendoGrid.dataSource.data();
            var shouldOverwrite = gridData.length > 0;
                $("#feeSchedule").kendoUpload({
                    async: {
                        saveUrl: "UploadFeeScheduleFile?programYearId=" + $("#programYearId").val() + "&meetingTypeId=" + $("#meetingId").val() + "&sessionId=" + $("#sessionId").val() + "&overwrite=" + shouldOverwrite,
                        removeUrl: "remove",
                        autoUpload: false
                    },
                    localization: {
                        select: "Browse"
                    },
                    multiple: false,
                    select: feeScheduleUploadCalls.onSelect,
                    success: feeScheduleUploadCalls.onSuccess
                });
        },
    onSelect:
        // Kendo Select
        function (e) {
            // Validation for files
            var formData = e.files[0];
            var fileContentType = e.files[0].extension.substring(1);
            var fileName = e.files[0].name;
            sessionStorage.setItem('fileName', fileName);
            if (fileContentType.toLowerCase() === "xlsx") {
                $(this).removeClass('input-validation-error');
                $('.k-upload-files').removeClass('displayNone');
                $("#saveDialogChanges").prop("disabled", false);
            } else {
                // Show message
                var msgElement = $("<div/>").html(INVALID_FILE_FORMAT).addClass("message");
                $("#uploadFeeScheduleAlert").empty().append(msgElement);
                $("#saveDialogChanges").prop("disabled", true);
            }
        },
    onSuccess:
        // Kendo Success
        function (results) {
           if (results.response.flag) {
                    setFeeSchedulesGrid();
                    $('.ss-grid-feeSchedule').addClass('displayBlock');
                    $('.ui-dialog-titlebar-close').click();
                } else {
                    feeScheduleUploadCalls.onShowMessages(results.response.messages);
           }
        },
    onShowMessages:
        function (msgs) {
            $("#uploadFeeScheduleAlert").empty();
            if (msgs.length > 0) {
                for (var i = 0; i < msgs.length; i++) {
                    var msgElement = $("<div/>").html(msgs[i]).addClass("message");
                    if (i >= 2) {
                        msgElement.hide();
                    }
                    $("#uploadFeeScheduleAlert").append(msgElement);
                }
                if (msgs.length > 2) {
                    var moreDivLink = $("<a/>").attr("href", "#").html("Show More").addClass("more-message");
                    var moreDiv = $("<div/>").append(moreDivLink);
                    $("#uploadFeeScheduleAlert").append(moreDiv);
                }
            } else {
                var gmsgElement = $("<div/>").html("There was an error reading your file. Please check the format of your excel file and be sure it matches the provided template.").addClass("message");
                $("#uploadFeeScheduleAlert").append(gmsgElement);
            }
        },
    onSaveDialog:
        // Save dialog button handler
        function () {
            var sessionGridLength = $('#ss-grid-session').length;
            if (sessionGridLength > 0) {
                $("button[id='saveDialogChanges']").click(function (e) {
                    var gridData = $("#ss-grid-session").data().kendoGrid.dataSource.data();
                    if (gridData.length === 0 || $("#uploadFeeScheduleAlert").find(".message").length > 0) {
                        $(".k-upload-selected").click();
                    } else {
                        // Show message
                        var msg = "A fee schedule already exists for this " +
                            $("#meetingId > option:selected").text() + "/" + $("#fiscalYearId > option:selected").text() +
                            " and will be overwritten with the new fee schedule. " +
                            "This will not update the fee schedule for any contracts already signed. Please click 'Upload' to continue " +
                            "or click 'Cancel' to return to the previous page.";
                        var msgElement = $("<div/>").html(msg).addClass("message");
                        $("#uploadFeeScheduleAlert").empty().append(msgElement);
                    }
                });
            } else {
                $("button[id='saveDialogChanges']").click(function (e) {
                    var gridData = $("#ss-grid-feeSchedule").data().kendoGrid.dataSource.data();
                    if (gridData.length === 0 || $("#uploadFeeScheduleAlert").find(".message").length > 0) {
                        $(".k-upload-selected").click();
                    } else {
                        // Show message
                        var msg = "A fee schedule already exists for this " +
                            $("#programYearId > option:selected").text() + "/" + $("#fiscalYearId > option:selected").text() +
                            " and will be overwritten with the new fee schedule. " +
                            "This will not update the fee schedule for any contracts already signed. Please click 'Upload' to continue " +
                            "or click 'Cancel' to return to the previous page.";
                        var msgElement = $("<div/>").html(msg).addClass("message");
                        $("#uploadFeeScheduleAlert").empty().append(msgElement);
                    }
                });
            }            
        },
    onMoreMessage:
        // Show More/Less link handler
        function() {
            $(document).on("click", ".more-message", function(e) {
                e.preventDefault();
                if ($("#uploadFeeScheduleAlert").find(".message:visible").length <= 2) {
                    $("#uploadFeeScheduleAlert").find(".message").show();
                    $(this).html("Show Less");
                } else {
                    $("#uploadFeeScheduleAlert").find(".message:gt(1)").hide();
                    $(this).html("Show More");
                }
            });
        },
    allFeeScheduleUploadFunctions:
        function () {
            feeScheduleUploadCalls.kendoUpload();
            feeScheduleUploadCalls.onSaveDialog();
            feeScheduleUploadCalls.onMoreMessage();
        }
};

