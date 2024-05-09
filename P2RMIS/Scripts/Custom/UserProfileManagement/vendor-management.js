// Selectors and Variables
var userInfoId = $('#UserInfoId').val(),
    thisText = '',
    idClicked = '',
    assignVendor = $('#assignVendor'),
    assignVendorInst = $('#assignVendorInst'),
    individualVendor = $('#individualVendor'),
    individualVendorId = $('#individualVendorId'),
    institutionalVendor = $('#institutionalVendor'),
    institutionalVendorId = $('#institutionalVendorId'),
    currentVendor = $('#currentVendor'),
    currentVendorClass = $('.currentVendorClass'),
    autoVendor = $('.autoVendor'),
    activeMessage = $('#activeMessage'),
    manualInput = $('#manualInput'),
    userVendorAdd = $('#userVendorAdd'),
    failureMessage = $('#failureMessage'),
    vendorHidden = $('#vendorHidden'),
    activeVendor = $('#activeVendor'),
    saveButton = $('button#saveVendorChanges');

// Page Calls
var vendorManagement = {
    getVendorInfo:
        function () {
            var vendorHidden = $('#vendorHidden');

            if ($('#institutionalVendorId').val() == "" && $('#institutionalVendor').text() == "N/A") {
                $('<span class="alignRight"><a href="#" class="vendorDelete"><img src="/Content/img/cross.png" class="icon-cancel close-x" alt="Delete Institutional Vendor"></a></span>').insertBefore('.removeInstitution');
                vendorHidden.removeClass('displayBlock').addClass('displayNone');
            } else {
                vendorHidden.addClass('displayBlock');
            }
        }, 
    addInstitutionalVendor:
        function () {
            // Add Institutional Vendor
            userVendorAdd.on('click', function () {
                vendorHidden.addClass('displayBlock');
                $('<span class="alignRight"><a href="#" class="vendorDelete"><img src="/Content/img/cross.png" class="icon-cancel close-x" alt="Delete Institutional Vendor"></a></span>').insertBefore('.removeInstitution');
                userVendorAdd.removeClass('inlineBlock').addClass('displayNone');
                // Scroll to top
                $('html, body').animate({
                    scrollTop: $(document).height()
                }, 1500);
            })
        },
    removeInstitutionalVendor:
        function () {
            // Remove Institutional Vendor
            $(document).on('click', '.vendorDelete', function () {
                vendorHidden.addClass('displayNone').removeClass('displayBlock');
                userVendorAdd.addClass('inlineBlock').removeClass('displayNone');
                $('#institutionalVendorId').val('');
                $(this).remove();
                // Scroll to top
                $('html, body').animate({
                    scrollTop: $(document).height()
                }, 1500);
            })
        },
    enableSaveButton:
        function () {
            $(document).on('click', '#vendorIdAuto', function () {
                saveButton.attr('disabled', false);
            })

            $(document).ready(function () {
                $('.validation-summary-errors li').each(function (i, value) {
                    var message = value.textContent;
                    if (message.indexOf('individual') > 0) {
                        $('#individualVendorId').addClass('input-validation-error');
                        return false;
                    } else {
                        $('#individualVendorId').removeClass('input-validation-error');
                    }
                })
            })
			
			$(document).ready(function(){
			    $('.validation-summary-errors li').each(function (i, value) {
                    var message = value.textContent;
                    if (message.indexOf('institutional') > 0) {
                        $('#institutionalVendorId').addClass('input-validation-error');
                        return false;
                    } else {
                        $('#institutionalVendorId').removeClass('input-validation-error');
                    }
                })
			})
        },
    enterNumberInManual:
        function () {
            $(document).on('keydown', '#individualVendorId, #institutionalVendorId', function () {
                var charCode = (event.which) ? event.which : event.keyCode
                if (charCode < 48 || charCode > 90) {
                    return false;
                }
            })            
        },
    allFunctions:
        function () {
            vendorManagement.getVendorInfo(); // Get Vendor Info for the page
            //vendorManagement.assignModifyModal(); // Initializes the modal to assign or modify page
            //vendorManagement.manualInputFocus(); // If manual input is focused
            vendorManagement.addInstitutionalVendor(); // When the plus sign is clicked setting the Institutional Vendor area
            vendorManagement.removeInstitutionalVendor(); // When the 'X' is clicked for Institutional Vendor
            vendorManagement.enableSaveButton(); // This enables save button when auto-assign radio is clicked
        }
}

// Call all functions
$(document).ready(vendorManagement.allFunctions);