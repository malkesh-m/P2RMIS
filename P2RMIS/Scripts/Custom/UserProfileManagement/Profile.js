// Masks
function maskPhone() {
    $('.UsPhone').not('.IntlPhone').mask("?(999) 999-9999");
    $('.UsPhoneDesk').not('.IntlPhone').mask("?(999) 999-9999 x999999");
    $('.IntlPhone').unmask();
}
function maskZip() {
    $('.UsZip').mask("?99999-9999");
    $('.IntlZip').unmask();
}   
$(function () {
    // Clearable Input field
    function tog(v) { return v ? 'addClass' : 'removeClass'; }
    $(document).on('input', '.clearable', function () {
        $(this)[tog(this.value)]('x');
    }).on('mousemove', '.x', function (e) {
        $(this)[tog(this.offsetWidth - 18 < e.clientX - this.getBoundingClientRect().left)]('onX');
    }).on('click', '.onX', function () {
        $(this).removeClass('x onX').val('').change();
    });

    //closes a dialog when "No" button is clicked
    $("body").on("click", ".dialog-close", function () {
        $(".ui-dialog-titlebar-close").click();
    });    
    maskPhone();    
    maskZip();

    //Mask swap for Phones
    $(document).on("change", ".userPhoneTypeDropDown", function () {
        var deskPhoneTypeId = 3;
        var phoneInput = $(this).parent().next().find('.userPhoneNumber, .alternateContactPhoneNumber');
        var prefInput = $(this).parent().next().find('.userPrefPhone, .phonePreferredType');
        var prefInputHidden = $(this).parent().parent().find("input[name^='UserPhone'][name$='Primary']:hidden, input[name^='UserPhone'][name$='Primary']:hidden");
        //Bind the correct css mask class based on selection
        if ($(this).val() == deskPhoneTypeId && phoneInput.hasClass("UsPhone")) {
            phoneInput.removeClass('UsPhone').addClass('UsPhoneDesk');
        } else if (phoneInput.hasClass("UsPhoneDesk")) {
            phoneInput.removeClass('UsPhoneDesk').addClass('UsPhone');
        };
        //Enable/disable preferred fax as necessary
        if ($(this).val() == 2 || $(this).val() == 5) {
            //disable and make sure unchecked
            prefInput.prop("disabled", true);
            prefInput.prop("checked", false);
            prefInputHidden.val('false');
        } else {
            prefInput.prop("disabled", false);
        };
        maskPhone();
    });

    // Sticky footer scrolling
    function posSaveFooter() {
        var ageFooterTop = $("#footer").offset().top;
        var wh = $(window).height();
        $("#saveFooter").css((ageFooterTop <= (wh + $(window).scrollTop())) ? { 'position': 'absolute', 'top': ageFooterTop - 50 } : { 'position': 'fixed', 'top': wh - 50 });
    }

    // Sticky footer scrolling
    posSaveFooter();
    $(window).scroll(function () { posSaveFooter(); });

    //Apply styling to .pageColumnTwoSelect
    $(".pageColumnTwoSelect").each(function () {
        $(this).attr('data-placeholder', $(this).find('option').eq(0).text())
        $(this).find('option').eq(0).css('color', 'darkgrey').css('font-style', 'italic').attr('disabled', 'disabled')
    })
    $(".optionalSelect").each(function () {
        $(this).attr('data-placeholder', $(this).find('option').eq(0).text())
    })
    //$(".pageColumnTwoSelect").chosen({disable_search_threshold: 10});

    //Apply styling to .pageColumnMilitarySelect
    $(".pageColumnMilitarySelect").each(function () {
        $(this).attr('data-placeholder', $(this).find('option').eq(0).text())
    })

    // Script for showing the user clients section
    var fnShowOrHideUserClients = function (element) {
        var misconductProfileTypeId = $("#MisconductProfileTypeId").val();
        if (element.val() == misconductProfileTypeId) {
            $("#userClientsSection").hide();
        } else {
            $("#userClientsSection").show();
        }
    };
    $("#GeneralInfo_ProfileTypeId").each(function () {
        fnShowOrHideUserClients($(this));
    }).change(function () {
        fnShowOrHideUserClients($(this));
    });

    // set the enable disable of the add degree href based on degree na
    ToggleDegreeAddLink();

    //
    // Clear the Education/Degree drop downs when the N/A checkbox is checked
    //
    $("body").on("click", "#degreeCheckbox", function (e) {
        //
        // Search for all entries in the list, even ones that are deleted (which we
        // just delete again.
        //
        ToggleDegreeAddLink();
        $('#userDegrees li').each(function (i, li) {
            var currentElement = $(li);
            //
            // locate the degree objects (degree drop down selection, degree major text box & the IsDeletable flag
            //
            var degreeId = currentElement.find(".degreeId");
            var degreeMajor = currentElement.find(".degreeMajor");
            var IsDeletable = currentElement.find(".degreeIsDeletable");
            // disable or enable entries based on status of degree NA checkbox
            if ($("#degreeCheckbox").prop('checked') == true) {
                degreeId.attr("disabled", "disabled");
                degreeMajor.attr("disabled", "disabled");
                IsDeletable.val(Boolean(true));
            }
            else {
                degreeId.removeAttr('disabled');
                degreeMajor.removeAttr('disabled');
                IsDeletable.val(Boolean(false));
            }
        });
    });

    function ToggleDegreeAddLink() {
        if ($("#degreeCheckbox").prop('checked') == true) {
            $("#userDegreeAdd").addClass("disabled");
        } else {
            $("#userDegreeAdd").removeClass("disabled");
        }
    }
});
// reload the page for cancel
function refreshProfilePage() {
    window.location.reload();   
}

$(document).ready(function () {
    // Error messagesd
    var savedMessage = '#saveSuccessMessages';
    isVisibleFocus(savedMessage);

    $('.validation-summary-valid, .validation-summary-errors').attr({ 'role': 'alert', 'tabindex': '-1' });
    var validationErrors = '.validation-summary-errors';
    isVisibleFocus(validationErrors);
});

$('#saveProfileButton').on('click', function () {
    $('#saveSuccessMessages').hide();
    setTimeout(function () {
        var validationErrors = '.validation-summary-errors';
        isVisibleFocus(validationErrors);
    }, 500);
});


