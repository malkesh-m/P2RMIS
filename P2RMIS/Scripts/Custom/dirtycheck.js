$(document).ready(function() {
    var unsaved = false;
    //On change flag unsaved flag
    $(":input:not(:button):not([type=hidden])").change(function() {
        unsaved = true;
    });
    //Reset unsaved flag if submit button is used
    $('input[type = "submit"], button[type = "submit"]').click(function() {
        unsaved = false;
    });
    // reset unsaved flag if class is reset
    $(".reset").click(function () {
        unsaved = false;
    });

    //Before page is unloaded, call function to determine if ditry data exists
    window.addEventListener("beforeunload", function (e) {
        if (unsaved) {
            (e || window.event).returnValue = "xxYou have unsaved changes on this page. Do you want to leave this page and discard your changes or stay on this page?";
            return "You have unsaved changes on this page. Do you want to leave this page and discard your changes or stay on this page?";
        }
    });
});