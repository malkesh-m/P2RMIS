(function ($) {
    var oldHTML = $.fn.html;
    // Form html so it can return IDL state values instead of initial state values
    // When using the jQuery html() or text(), by default it returns the initial state of its children elements
    // This extended function can convert initial states to IDL (current) states in its children elements
    $.fn.formhtml = function () {
        if (arguments.length) return oldHTML.apply(this, arguments);
        $("input,button", this).each(function () {
            this.setAttribute('value', this.value);
        });
        $("textarea", this).each(function () {
            this.innerHTML = this.value;
        });
        $("input:radio,input:checkbox", this).each(function () {
            // im not really even sure you need to do this for "checked"
            // but what the heck, better safe than sorry
            if (this.checked) this.setAttribute('checked', 'checked');
            else this.removeAttribute('checked');
        });
        $("option", this).each(function () {
            // also not sure, but, better safe...
            if (this.selected) this.setAttribute('selected', 'selected');
            else this.removeAttribute('selected');
        });
        return oldHTML.apply(this);
    };
})(jQuery);
// Failure message
$.defaultFailureMessage = "Sorry, there was a problem processing your request.";
$.invalidOperation = "Sorry, it was an invalid operation.";

// Encode HTML
$.htmlEncode = function (value) {
    return $('<div/>').text(value).html();
};
// Decode HTML
$.htmlDecode = function (value) {
    return $('<div/>').html(value).text();
};
// Convert C# datetime to Javascript time stamp
$.toJsTimeStamp = function (cSharpTimeStamp) {
    var jsTimeStamp = parseInt(cSharpTimeStamp.replace(/\/Date\((-?\d+)\)\//, '$1'));
    return jsTimeStamp;
};
// Convert time stamp to display date
$.toDateDisplayFormat = function (timeStamp) {
    var date = new Date(timeStamp);
    return (date.getMonth() + 1) + '/' + date.getDate() + '/' + date.getFullYear();
};
// Convert time stamp to display date/time
$.toDateTimeDisplayFormat = function (timeStamp) {
    return new Date(timeStamp).toString('M/d/yyyy h:mm:ss tt');
};
// Convert boolean string to boolean type
$.toBoolean = function (d) {
    if (d !== undefined && d.toLowerCase() === "true")
        return true;
    else
        return false;
};
// Toggle visibility
$.toggleVisibility = function(id, caller) {
    var e = document.getElementById(id);
    var c = document.getElementById(caller);
    if (e.style.display === 'none') {
        e.style.display = 'block';
        c.innerHTML = c.innerHTML.replace("Show", "Hide");
    }
    else {
        e.style.display = 'none';
        c.innerHTML = c.innerHTML.replace("Hide", "Show");
    }
}
// Get URL parameter
$.getUrlParameter = function getUrlParameter(sParam) {
    var sPageURL = decodeURIComponent(window.location.search.substring(1)),
        sURLVariables = sPageURL.split('&'),
        sParameterName,
        i;
    for (i = 0; i < sURLVariables.length; i++) {
        sParameterName = sURLVariables[i].split('=');

        if (sParameterName[0] === sParam) {
            return sParameterName[1] === undefined ? true : sParameterName[1];
        }
    }
};
// Trim with ellipses
$.trimWithEllipses = function (name, maxLength) {
    if (name.length > maxLength) {
        name = name.substring(0, maxLength) + "...";
    }
    return name;
}
// Open pop-up window
$.openPopupWindow = function (url, width, height) {
    window.open(url, "_blank",
        "directories = no, titlebar = no, toolbar = no, location = no, status = no, menubar = no, scrollbars = no, resizable = no, width = " + width + ", height = " + height);
};
// Set element array index
$.setElementArrayIndex = function (element, newIndex) {
    for (var i = 0; i < element.length; i++) {
        element.eq(i).attr("id", element.eq(i).attr("id").replace("_0_", "_" + newIndex + "_"));
        element.eq(i).attr("name", element.eq(i).attr("name").replace("[0]", "[" + newIndex + "]"));
        var elementId = element.eq(i).attr("id");
        if (elementId.indexOf('Alternate') !== -1) {
            var setElement = element.closest('div').find('label')[0];
            $(setElement).attr('for', elementId);
        }else{
            element.closest('div').find('label').attr('for', element.eq(i).attr("id").replace("_0_", "_" + newIndex + "_"));
        }
    }
};

//Prevent ajax request caching
$.ajaxSetup({ cache: false });

$(document).on('click', '.dropdown-menu a.searchUser', function () {
    var newNullVar = '';
    sessionStorage.setItem('newValueSearch', newNullVar);
    sessionStorage.setItem('firstSearch', newNullVar);
    sessionStorage.setItem('lastSearch', newNullVar);
    sessionStorage.setItem('emailSearch', newNullVar);
    sessionStorage.setItem('usernameSearch', newNullVar);
    sessionStorage.setItem('userIdSearch', newNullVar);
    sessionStorage.setItem('vendorIdSearch', newNullVar);
})
// Back button
$(document).on('click', '#backArrow, #backButton', function () {
    window.history.back();
});

function isVisibleFocus(id) {
    var isVisible = $(id).is(':visible');
    if (isVisible) {
        $(id).focus();
    }
}
// Simple email validation
function ValidateEmail(email) {
    return /^\w+([\.-]?\w+)*@\w+([\.-]?\w+)*(\.\w{2,3})+$/.test(email);
}

// Masked phone validation
function ValidatePhone(phone) {
    return /((\(\d{3}\) ?)|(\d{3}-))?\d{3}-\d{4}/.test(phone);
}