// Selectors
var failureMessage = $('#failureMessage'),
    successMessage = $('#successMessage'),
    validationMessage = $('#validationMessage'),
    uploadData = $('.uploadData'),
    preText = $('#preText'),
    mmtable = $('#mm-table'),
    uploadButton = $('#uploadButton');
    fileTemplateDownloadLink = $('#fileTemplateDownloadLink');

// Page Calls
var mmManagementCalls = {
    kendoUpload:
        function () {
            // File upload
            $("#fileBase").kendoUpload({
                async: {
                    saveUrl: "ProcessExcelFile",
                    removeUrl: "remove",
                    autoUpload: true,
                },
                localization: {
                    select: "Browse"
                },
                multiple: false,
                select: mmManagementCalls.onSelect,
                upload: mmManagementCalls.onUpload,
                success: mmManagementCalls.onSuccess,
                remove: function (e) {
                    fileUidToRemove = e.files[0].uid;
                    e.preventDefault();
                    $(".k-upload-files li").each(function (i, item) {
                        var dataId = $(this).attr("data-uid");
                        if (fileUidToRemove == dataId) {
                            $(this).remove();
                        }
                    });
                    uploadButton.attr('disabled', true);
                    mmtable.removeClass('displayBlock');
                    uploadData.removeClass('displayBlock');
                    $('.mm-main-upload_uploadMMData').removeClass('displayBlock');
                    preText.addClass('displayNone');
                    validationMessage.empty();
                    $('.k-upload-status.k-upload-status-total').addClass('displayNone');
                    $("#successMessage").html("").hide();
                    $("#failureMessage").html("").hide();
                }
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
            if (fileContentType.toLowerCase() == "xlsx") {
                $(this).removeClass('input-validation-error');
                $('.k-upload-files').removeClass('displayNone');
            } else {
                setTimeout(function () {
                    $('.k-button.k-upload-button').removeClass('k-state-focused');
                }, 200);
                $('#failureMessage').text(INVALID_FILE_FORMAT);
                mmtable.removeClass('displayBlock');
                uploadData.removeClass('displayBlock');
                $('.k-upload-files').addClass('displayNone');
                $(this).addClass('input-validation-error');
                e.preventDefault();
            }
        },
    onUpload:
        // Kendo Upload
        function (e) {
            var fileBase = $('input.mm-main-upload_filterBox_table_file_input')[0].files[0];

            $.ajax({
                cache: false,
                type: "POST",
                data: fileBase,
                contentType: false,
                processData: false
            });
        },
    onSuccess:
        // Kendo Success
        function (e) {
            $('#mm-table tbody').empty();
            var createdRows = JSON.parse(e.response);
            $(createdRows).each(function (i, value) {
                $("#mm-table tbody").append("<tr><td>" + (i + 1) + "</td></tr>");
                $(value).each(function (e, newValue) {
                    $('#mm-table tbody').find('tr:nth-child(' + (i + 1) + ')').append('<td>' + newValue + '</td>');
                })
            })
            mmtable.addClass('displayBlock');
            uploadData.addClass('displayBlock');
            uploadButton.attr('disabled', false);
            $('.mm-main-upload_uploadMMData').addClass('displayBlock');
            $('.k-button.k-upload-button').removeClass('k-state-focused');
            $('.k-upload-status.k-upload-status-total, .k-upload-pct').addClass('displayNone');
            mmManagementCalls.failureMessageCall();
        },
    onMMUploadButton:
        function () {
            // Upload excel file
            uploadButton.on('click', function () {
                validationMessage.empty();
                $("#successMessage").html("").hide();
                $("#failureMessage").html("").hide();
                preText.text('');
                var flights = [];
                $('#mm-table tbody tr').each(function (e, value) {
                    var PanelUserAssignmentId = this.cells[1].textContent,
                        LastName = this.cells[2].textContent,
                        FirstName = this.cells[3].textContent,
                        Reservation = this.cells[4].textContent,
                        CarrierName = this.cells[5].textContent,
                        Flight = this.cells[6].textContent,
                        DepCity = this.cells[7].textContent,
                        DepDate = this.cells[8].textContent,
                        DepTime = this.cells[9].textContent,
                        ArrCity = this.cells[10].textContent,
                        ArrDate = this.cells[11].textContent,
                        ArrTime = this.cells[12].textContent;
                        Fare = this.cells[13].textContent;

                    flight = {
                        PanelUserAssignmentId : PanelUserAssignmentId,
                        LastName : LastName,
                        FirstName : FirstName,
                        ReservationCode : Reservation,
                        CarrierName : CarrierName,
                        FlightNumber : Flight,
                        DepartureCity : DepCity,
                        DepartureDate : DepDate,
                        DepartureTime : DepTime,
                        ArrivalCity : ArrCity,
                        ArrivalDate : ArrDate,
                        ArrivalTime: ArrTime,
                        Fare: Fare,
                    }
                    flights.push(flight);
                })

                $.ajax({
                    type: 'POST',
                    url: '/MeetingManagement/UploadMMTravel',
                    data: { flights: flights },
                success: function(results) {
                    console.log("success");
                }
                }).done(function (results) {
                    var errorTotal = "";
                    var rowValidated = "";
                    var rowValidatedTotal = '';
                    if (results.results.length != 0) {
                        $(results.results).each(function (i, value) {
                            var errorMade = results.results[i];
                            rowValidated = '';
                            var htmlCreated = "<span>" + errorMade + "</span>";
                            errorTotal += htmlCreated;
                            rowValidated += errorMade.slice(0, 6);
                            rowValidatedTotal += (rowValidatedTotal.indexOf(rowValidated) >= 0) ? "" : rowValidated + ", ";
                        });
                        if (results.length > 10) {
                            var slicedTotal = rowValidatedTotal.slice(0, -2);
                            validationMessage.text('More than 10 errors were encountered while processing your file. Please fix the errors on the following rows and try again.');
                            validationMessage.append('<span>' + slicedTotal + '</span');
                        } else {
                            preText.text('Please correct the following errors and try again:').removeClass('displayNone');
                            validationMessage.html(errorTotal).addClass('displayBlock');
                        }
                    } else {
                        var fileName = sessionStorage.getItem('fileName');
                        successMessage.text(fileName + ' uploaded successfully.').show();
                    }
                })
            })
        },
    onTemplateFileDownload:
        function () {
            // Download file template
            fileTemplateDownloadLink.on('click', function () {
                showFileDownloadWarning(undefined, undefined, "/Content/World_Travel_Data_Upload_Sample.xlsx");
            });
        },
    failureMessageCall:
        function () {
            var failureMade = failureMessage.length;
            if (failureMade > 0) {
                if (failureMessage[0].innerHTML != "") {
                    failureMessage.text('').hide();
                    successMessage.text('').hide();
                    validationMessage.text('');
                    preText.text('');
                    validationMessage.empty();
                }
            }
        },
    allMMFunctions:
        function () {
            mmManagementCalls.kendoUpload();
            mmManagementCalls.onMMUploadButton();
            mmManagementCalls.onTemplateFileDownload();
        }
}
// Call all functions
$(document).ready(mmManagementCalls.allMMFunctions);

