$(function () {
    // Save the clientId and userClientId values when loaded
    // These values will be used to compared with re-loaded values to reset the userClientId
    // which is an indicator to add or edit the records
    var clientLoaded = [];
    $('.clientId').each(function () {
        var clientIdVal = $(this).val();
        var userClientIdVal = $(this).parent().find(".userClientId").val();
        if (parseInt(clientIdVal) > 0) {
            clientLoaded.push({ clientId: clientIdVal, userClientId: userClientIdVal });
        }
    });
    // Save the available clientId and clientName values when loaded
    // These values will be used to add title attributes when re-loading the main section
    var clientAvailable = [];
    $('.clientIdAvailable').each(function () {
        var clientIdVal = $(this).val();
        var clientNameVal = $(this).parent().find(".clientNameAvailable").val();
        var clientAbrvVal = $(this).parent().find(".clientAbrvAvailable").val();
        clientAvailable.push({ clientId: clientIdVal, clientName: clientNameVal, clientAbrv: clientAbrvVal });
    });
    // Create the multi-selection boxes
    $('#userClientsListBox').multiselect2side({
        selectedPosition: 'right',
        moveOptions: false,
        labelsx: 'Available',
        labeldx: 'Selected',
        autoSort: true,
        autoSortAvailable: true
    });
    // Create the dialog modal window
    $('#UserClientSelection').dialog({
        autoOpen: false,
        width: 750,
        modal: true,
        resizable: false
    });
    // Click event for the add icon
    $('#userClientAdd').click(function (e) {
        e.preventDefault();
        // Get selected clientIds
        var clientIdSelected = [];
        $("input.clientId").each(function () {
            var clientId = $(this).val();
            if (parseInt(clientId) > 0) {
                clientIdSelected.push(clientId);
            }
        });
        // Populate clientIds to selection boxes
        var source = $("#UserClientsSelectedms2side__sx");
        var destination = $("#UserClientsSelectedms2side__dx");
        var iSource = 0;
        while (iSource < source.find('option').length) {
            var theSourceOption = source.find('option').eq(iSource);
            if ($.inArray(theSourceOption.val(), clientIdSelected) >= 0) {
                // Move it to destination box
                var newDestinationOption = theSourceOption.clone();
                destination.append(newDestinationOption);
                theSourceOption.remove();
            } else {
                iSource++;
            }
        }
        var iDestination = 0;
        while (iDestination < destination.find('option').length) {
            var theDestinationOption = destination.find('option').eq(iDestination);
            if ($.inArray(theDestinationOption.val(), clientIdSelected) < 0) {
                // Move it to destination box
                var newSourceOption = theDestinationOption.clone();
                source.append(newSourceOption);
                theDestinationOption.remove();
            } else {
                iDestination++;
            }
        }
        $('#UserClientSelection').dialog('open');
    });
    // Add selected options to the main window
    var fnAddSelectedToMain = function (index, id, text) {
        var userClientTemplate = $(".userClientTemplate");
        var userClients = $("#userClients");
        // Clone the template
        var newElement = userClientTemplate.clone();
        // Get the element entities
        var newUserId = newElement.find(".userId");
        var newClientId = newElement.find(".clientId");
        var newUserClientId = newElement.find(".userClientId");
        var newClientAbrvId = newElement.find(".clientAbrvId");
        // Reset the values
        newUserId.val($("#UserId").val());
        newClientId.val(id);
        newUserClientId.val(fnGetUserClientId(id));
        newClientAbrvId.val(text);
        // Reset the id and name attributes
        newUserId.attr("id", "UserClients_" + index + "__UserId").attr("name", "UserClients[" + index + "].UserId");
        newClientId.attr("id", "UserClients_" + index + "__ClientId").attr("name", "UserClients[" + index + "].ClientId");
        newUserClientId.attr("id", "UserClients_" + index + "__UserClientId").attr("name", "UserClients[" + index + "].UserClientId");
        newClientAbrvId.attr("id", "UserClients_" + index + "__ClientAbrv").attr("name", "UserClients[" + index + "].ClientAbrv");
        // Format the style classes
        newElement.removeClass("userClientTemplate").addClass("userClient").removeClass("hide");
        // Append to the parent element
        userClients.append(newElement);
    };
    // Get UserClientId if exists
    var fnGetUserClientId = function (clientId) {
        var userClientId = 0;
        for (var i = 0; i < clientLoaded.length; i++) {
            if (clientLoaded[i].clientId === clientId) {
                userClientId = clientLoaded[i].userClientId;
            }
        }
        return userClientId;
    };
    // Get client's name
    var fnGetClientName = function (clientId) {
        var clientName = "";
        for (var i = 0; i < clientAvailable.length; i++) {
            if (clientAvailable[i].clientId === clientId) {
                clientName = clientAvailable[i].clientName;
            }
        }
        return clientName;
    };
    // Get client's abbreviation
    var fnGetClientAbrv = function (clientId) {
        var clientAbrv = "";
        for (var i = 0; i < clientAvailable.length; i++) {
            if (clientAvailable[i].clientId === clientId) {
                clientAbrv = clientAvailable[i].clientAbrv;
            }
        }
        return clientAbrv;
    };
    // Populate the abbreviation and name
    $(".clientAbrv").each(function () {
        var clientId = $(this).parent().find(".clientId").val();
        if ($(this).html() === "" && parseInt(clientId) > 0) {
            $(this).html(fnGetClientAbrv(clientId));
            $(this).attr("title", fnGetClientName(clientId));
        }
    });
    // Populate client list
    var populateClientList = function () {
        var abrvList = "";
        $('.clientAbrvId').not("#ClientAbrvIdTemplate").each(function (i, value) {
            var abrv = $(this).val();
            abrvList = (!abrvList) ? abrv : abrvList + ", " + abrv;
        });
        var element = $("#userClientsList");
        element.text(abrvList);
    };

    if ($('.validation-summary-errors ul li').filter(':contains("At least one client is required.")').length > 0) {
        $('.clientsClass').addClass('input-validation-error');
    } else {
        $('.clientsClass').removeClass('input-validation-error');
    }

    // Click event for copying the user clients to the main screen
    $('button#saveDialogChanges').on('click', function () {
        var selectedSide = $("#UserClientsSelectedms2side__dx");
        var optionLength = selectedSide.find('option').length;
        $(".userClient").remove();
        for (var i = 0; i < optionLength; i++) {
            fnAddSelectedToMain(i, selectedSide.find('option').eq(i).val(), selectedSide.find('option').eq(i).text());
        }
        // Populate client list
        populateClientList();
        var selectedLength = $('.clientAdded select option').length;
        if (selectedLength > 0) {
            $('.clientsClass').removeClass('input-validation-error');
        } else {
            $('.clientsClass').addClass('input-validation-error');
        }
        $('#UserClientSelection').dialog('close');
        $('#closeDialogBtn').hide();
    });
});