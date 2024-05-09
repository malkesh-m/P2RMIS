// Global namespace reference
var p2rims = p2rims || {};

///**
// * function: check for empty, null, undefined, false, the number 0 or NaN
// * @param {Obj} val 
// * @return {boolean} true if obj is empty / false otherwise
// */
p2rims.isEmpty = function (val) {
    return !val;
}

///**
// * Custom function to determine if library was already loaded
// * @param {Obj} namesp target namespace
// * @param {String} jsFile name of target javascript library
// */
p2rims.isFirstLoad = function (namesp, jsFile) {
    var isFirst = namesp.firstLoad === undefined;
    namesp.firstLoad = false;

    if (!isFirst) {
        console.log("Warning: Javascript file is already included: " + jsFile);
    }

    return isFirst;
}

/**
 * Custom function to determine if the browser is IE or not
 * @return {integer} if > 0, this is IE version; otherwise, 0 indicates browser is not IE
 * Example (usage): 
 *  if (GetIEVersion() > 0) 
 *      alert("This is IE " + GetIEVersion());
 *  else 
 *      alert("This is not IE.");
 */
p2rims.getIEVersion = function() {
    var sAgent = window.navigator.userAgent;
    var Idx = sAgent.indexOf("MSIE");

    // If IE, return version number.
    if (Idx > 0)
        return parseInt(sAgent.substring(Idx + 5, sAgent.indexOf(".", Idx)));

        // If IE 11 then look for Updated user agent string.
    else if (!!navigator.userAgent.match(/Trident\/7\./))
        return 11;

    else
        return 0; //It is not IE
}

/**
 * function: check to determine if the browser is chrome
 * @return {boolean} true if chrome; otherwise false
 */
p2rims.isChrome = function () {
    var is_chrome = /chrome/.test(navigator.userAgent.toLowerCase());
    return is_chrome;
}

$(window).resize(function () {
    if ($('#ModalDialog').dialog('isOpen')) {
        var winHeight = $(window).height();
        if (winHeight > 800) {
            if ($('#ModalDialog').hasClass('largeModal')) {
                $('.innerModalContainer').css('max-height', '700px');
                $('.ui-draggable').css('height', 'auto');
                $("#ModalDialog").dialog({ position: { my: "top center", at: "top center", of: window } });
            } else {
                if ($('#ModalDialog').hasClass('mediumModal')) {
                    $('.innerModalContainer').css('max-height', '601px');
                    $('.ui-draggable').css('height', 'auto');
                    $("#ModalDialog").dialog("option", "position", "center");
                } else {
                    if ($('#ModalDialog').hasClass('smallModal')) {
                        $("#ModalDialog").dialog({ position: { my: "top center", at: "top center", of: window } });
                    }
                }
            }
        } else {
            if ($('#ModalDialog').hasClass('largeModal')) {
                $('.innerModalContainer').css('max-height', '401px');
                $('.ui-draggable').css('height', 'auto');
                $("#ModalDialog").dialog("option", "position", "center");
            } else {
                if ($('#ModalDialog').hasClass('mediumModal')) {
                    $('.innerModalContainer').css('max-height', '401px');
                    $('.ui-draggable').css('height', 'auto');
                    $("#ModalDialog").dialog("option", "position", "center");
                } else {
                    if ($('#ModalDialog').hasClass('smallModal')) {
                        $("#ModalDialog").dialog({ position: { my: "top center", at: "top center", of: window } });
                    }
                }
            }
        }
    }
});

// Kendo Scroll to top
$(document).on('click', '.k-pager-wrap a', function (element) {
    var currentGrid = $(element)[0].currentTarget.closest('.k-grid').id;
    if (currentGrid == "preAssignmentGrid" || currentGrid == "postAssignmentGrid" || currentGrid == "gridChair") {
        return true;
    } else {
        $('.k-grid-content').animate({ scrollTop: 0 }, 'slow');
    }
})

