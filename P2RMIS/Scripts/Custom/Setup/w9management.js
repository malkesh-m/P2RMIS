// Selectors
var failureMessage = $('#failureMessage'),
    successMessage = $('#successMessage'),
    validationMessage = $('#failureMessage'),
    uploadData = $('.uploadData'),
    preText = $('#preText'),
    w9Table = $('#w9Table'),
    uploadButton = $('#uploadButton');

// Page Calls
var w9ManagementCalls = {
    backButton:
        function () {
            // Back button
            $('#backButton').on('click', function () {
                window.history.back();
            });
        },
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
                select: w9ManagementCalls.onSelect,
                upload: w9ManagementCalls.onUpload,
                success: w9ManagementCalls.onSuccess,
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
                    w9Table.removeClass('displayBlock');
                    uploadData.removeClass('displayBlock');
                    preText.addClass('displayNone');
                    validationMessage.empty();
                    $('.k-upload-status.k-upload-status-total').addClass('displayNone');
                    successMessage.hide();
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
                w9Table.removeClass('displayBlock');
                uploadData.removeClass('displayBlock');
                $('.k-upload-files').addClass('displayNone');
                $(this).addClass('input-validation-error');
                e.preventDefault();
            }
        },
    onUpload:
        // Kendo Upload
        function (e) {
            var fileBase = $('input#fileBase')[0].files[0];

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
            $('#w9Table tbody').empty();
            var createdRows = JSON.parse(e.response);
            $(createdRows).each(function (i, value) {
                $("#w9Table tbody").append("<tr><td>" + (i + 1) + "</td></tr>");
                $(value).each(function (e, newValue) {
                    $('#w9Table tbody').find('tr:nth-child(' + (i + 1) + ')').append('<td>' + newValue + '</td>');
                })
            })
            w9Table.addClass('displayBlock');
            uploadData.addClass('displayBlock');
            uploadButton.attr('disabled', false);
            $('.k-button.k-upload-button').removeClass('k-state-focused');
            $('.k-upload-status.k-upload-status-total, .k-upload-pct').addClass('displayNone');
            w9ManagementCalls.failureMessageCall();
        },
    onW9UploadButton:
        function(){
            // Upload excel file
            uploadButton.on('click', function () {
                validationMessage.empty();
                preText.text('');
                var addresses = [];
                var innerAddresses = [];
                $('#w9Table tbody tr').each(function (e, value) {
                    var innerAddress = {};
                    var ReviewerName = this.cells[1].textContent,
                        VendorName = this.cells[4].textContent,
                        UserId = this.cells[6].textContent,
                        VendorId = this.cells[3].textContent,
                        InstVendorId = this.cells[2].textContent,
                        AddressTypeId = this.cells[5].textContent,
                        UserAddressId = null,
                        Address1 = this.cells[8].textContent,
                        Address2 = this.cells[9].textContent,
                        Address3 = this.cells[10].textContent,
                        Address4 = null,
                        City = this.cells[11].textContent,
                        State = this.cells[12].textContent,
                        StateId = null,
                        Zip = this.cells[14].textContent,
                        Country = this.cells[13].textContent,
                        CountryId = null;

                    innerAddresses = {
                        ReviewerName: ReviewerName,
                        VendorName: VendorName,
                        UserId: UserId,
                        VendorId: VendorId,
                        InstVendorId: InstVendorId,
                        AddressTypeId: AddressTypeId,
                        UserAddressId: null,
                        Address1: Address1,
                        Address2: Address2,
                        Address3: Address3,
                        Address4: Address4,
                        City: City,
                        State: State,
                        StateId: null,
                        Zip: Zip,
                        Country: Country,
                        CountryId: null
                    }
                    addresses.push(innerAddresses);
                    innerAddresses = [];
                })

                $.ajax({
                    type: 'POST',
                    url: '/UserProfileManagement/UploadW9Addresses',
                    data: { addresses: addresses }
                }).done(function (results) {
                    var errorTotal = "";
                    var rowValidated = "";
                    var rowValidatedTotal = '';
                    if (results.results.length > 0) {
                        $(results.results).each(function (i, value) {
                            var errorMade = results.results[i];
                            rowValidated = '';
                            var htmlCreated = "<span>" + errorMade + ', ' + "</span>";
                            errorTotal += htmlCreated + '<br/>';
                            rowValidated += errorMade.slice(0, 6);
                            rowValidatedTotal += (rowValidatedTotal.indexOf(rowValidated) >= 0) ? "" : rowValidated + ", ";
                        });
                        if (results.results.length > 10) {
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
    allW9Functions:
        function () {
            w9ManagementCalls.backButton();
            w9ManagementCalls.kendoUpload();
            w9ManagementCalls.onW9UploadButton();
        }
}
// Call all functions
$(document).ready(w9ManagementCalls.allW9Functions);

