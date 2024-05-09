$(function () {
    // Update address titles
    function updateAddressTitles(e) {
        var organizationAddressTypeId = $("#OrganizationAddressTypeId").val();
        var getTarget = $(e.target);
        var findContainer = getTarget.closest('.upm-profile-section-alternate-container_section').attr('data-section');
        var findOrg = getTarget.closest('.address').find('.upm-profile-section-sub_container-sub')[1];
        var findDept = getTarget.closest('.address').find('.upm-profile-section-sub_container-sub')[2];
        var findAddOne = getTarget.closest('.address').find('.upm-profile-section-sub_container-sub')[3];
        var findAddTwo = getTarget.closest('.address').find('.upm-profile-section-sub_container-sub')[4];

        if ($(findOrg).find('label').is(':visible')) {
            $(findAddOne).show().find('input').val('');
            $(findAddOne).find('input').attr({ 'id': 'Addresses_' + findContainer + '__Address1', 'name': 'Addresses[' + findContainer + '].Address1' });
            $(findAddOne).find('label').attr('for', 'Addresses_' + findContainer + '__Address1');
            $(findAddOne).find('input').removeClass('address3').addClass('address1');

            $(findAddTwo).show().find('input').val('');
            $(findAddTwo).find('input').attr({ 'id': 'Addresses_' + findContainer + '__Address2', 'name': 'Addresses[' + findContainer + '].Address2' });
            $(findAddTwo).find('label').attr('for', 'Addresses_' + findContainer + '__Address2');
            $(findAddTwo).find('input').removeClass('address4').addClass('address2');

            $(findOrg).hide();
            $(findDept).hide();
        }
        else {
            $(findOrg).show().find('input').val('');
            $(findOrg).find('input').attr({ 'id': 'Addresses_' + findContainer + '__Address1', 'name': 'Addresses[' + findContainer + '].Address1' });
            $(findOrg).find('label').attr('for', 'Addresses_' + findContainer + '__Address1');
            $(findOrg).find('input').removeClass('address5').addClass('address1');

            $(findDept).show().find('input').val('');
            $(findDept).find('input').attr({ 'id': 'Addresses_' + findContainer + '__Address2', 'name': 'Addresses[' + findContainer + '].Address2' });
            $(findDept).find('input').removeClass('address6').addClass('address2');
            $(findDept).find('label').attr('for', 'Addresses_' + findContainer + '__Address2');

            $(findAddOne).show().find('input').val('');
            $(findAddOne).find('input').attr({ 'id': 'Addresses_' + findContainer + '__Address3', 'name': 'Addresses[' + findContainer + '].Address3' });
            $(findAddOne).find('input').removeClass('address1').addClass('address3');
            $(findAddOne).find('label').attr('for', 'Addresses_' + findContainer + '__Address3');

            $(findAddTwo).show().find('input').val('');
            $(findAddTwo).find('input').attr({ 'id': 'Addresses_' + findContainer + '__Address4', 'name': 'Addresses[' + findContainer + '].Address4' });
            $(findAddTwo).find('input').removeClass('address2').addClass('address4');
            $(findAddTwo).find('label').attr('for', 'Addresses_' + findContainer + '__Address4');
        }       
    };

    $(document).on('click', '.addressDelete', function (e) {
        e.preventDefault();
        var currentElement = $(this).closest(".address");
        var addressId = currentElement.find(".addressId");
        var modalTitle = '<span class="modalSmallCaption modalWarningCaption">Warning</span>';
        p2rims.modalFramework.displayModalNoEvent($("#AddressDeletionDialog").html(), p2rims.modalFramework.customModalSizes.medium, modalTitle);
        $(".modal-footer").append(p2rims.modalFramework.defaultModalFooters.cancelOkFooter);
        $('#ModalDialog #saveDialogChanges').attr('elementId', addressId.attr('id'));
        $("a[id='closeModal'], button[id='closeDialogBtn']").click(function () { $('.ui-dialog-titlebar-close').click(); });
    });


    //// Remove/Delete address
    $(document).on('click', '#saveDialogChanges', function (e) {
        e.preventDefault();
        var elements = $(".address:visible");
        var elementLength = elements.length;
        if (elementLength > 0) {
            var elementId = $(this).attr('elementId');
            var currentElement = $('#' + elementId).parent().parent();

            // Hide element
            currentElement.addClass("hidden");

            // Set the delete flag
            currentElement.find(".addressIsDeletable").val(Boolean(true));
            //currentElement.find(".addressTypeId").val("");
            $("#addressAdd").attr('src', '/Content/img/plus.png');

            var visableElement = $(".address:visible").first();
            if (visableElement.length > 0 && currentElement.find(".addressType").is(':checked')) {
                var addressPrefTag = visableElement.find(".prefAddressTag");
                addressPrefTag.text($("#PreferredText").val());

                var addressType = visableElement.find(".addressType");
                addressType.prop("checked", true);
            }
        }
        $(this).closest(".ui-dialog .ui-dialog-content").dialog("close");
    });

    // Add address
    $("#addressAdd").click(function (e) {
        e.preventDefault();
        var addresses = $(".address");
        var visibleAddresses = $(".address:visible");
        var numberVisibleAddress = visibleAddresses.length;
        var addressLength = addresses.length;

        var template = addresses.first();
        var newElement = template.clone();
        newElement.attr('data-section', '1');

        var addressId = newElement.find(".addressId");
        var address1 = newElement.find(".address1");
        var address2 = newElement.find(".address2");
        var address3 = newElement.find(".address3");
        var address4 = newElement.find(".address4");
        var addressCity = newElement.find(".addressCity");
        var addressState = newElement.find(".addressState");
        var addressZip = newElement.find(".addressZip");
        var addressCountry = newElement.find(".addressCountry");
        var addressType = newElement.find(".addressType");
        var addressTypeId = newElement.find(".addressTypeId")
        var addressPrefTag = newElement.find(".prefAddressTag");
        var addressIsDeletable = newElement.find(".addressIsDeletable");

        // set the new id and name
        $.setElementArrayIndex(addressId, addressLength);
        $.setElementArrayIndex(address1, addressLength);
        $.setElementArrayIndex(address2, addressLength);
        $.setElementArrayIndex(address3, addressLength);
        $.setElementArrayIndex(address4, addressLength);
        $.setElementArrayIndex(addressCity, addressLength);
        $.setElementArrayIndex(addressState, addressLength);
        $.setElementArrayIndex(addressZip, addressLength);
        $.setElementArrayIndex(addressCountry, addressLength);
        $.setElementArrayIndex(addressType, addressLength);
        $.setElementArrayIndex(addressTypeId, addressLength);
        $.setElementArrayIndex(addressIsDeletable, addressLength);

        // Reset the values
        addressId.val("0");
        address1.val("");
        address2.val("");
        address3.val("");
        address4.val("");
        addressCity.val("");
        addressState.val("");
        addressZip.val("").removeClass("IntlZip").addClass("UsZip");
        addressCountry.val($("#UsCountryValue").val());

        // If this address is the only address, default to perferred address
        if (numberVisibleAddress < 1) {
            addressType.prop("checked", true);
            addressPrefTag.html($("#PreferredText").val());
        }
        else {
            addressType.removeAttr("checked");
            addressPrefTag.html($("#SecondaryText").val());
        }

        addressIsDeletable.val(Boolean(false));
        // Add to parent element

        newElement.removeClass("hidden");
        newElement.insertAfter(addresses.last());
        address1.focus();
        maskZip();

        $(newElement.find('.upm-profile-section-sub_container-sub')).each(function (i, value) {
            var findInput = $(this).find('input').attr('id');
            var findSelect = $(this).find('select').attr('id');
            if (typeof findSelect === "undefined") {
                $(this).find('label').attr('for', findInput);
            } else {
                $(this).find('label').attr('for', findSelect);
            }
        });
    });

    $(document).on('click', 'input.addressType:radio', function (e) {
        $('input.addressType:radio').not(this).prop('checked', false);
        // Replace preferred text with secondary
        $(".prefAddressTag").text($("#SecondaryText").val());
        // Add it back for the current control
        $(this).parent().find('.prefAddressTag').text($("#PreferredText").val());
    });

    // Mask swap for Zipcode
    $(document).on("change", ".addressCountry", function () {
        var usZip = $("#UsCountryValue").val();
        var zipInput = $(this).parent().parent().parent().parent().find('.addressZip');
        //Bind the correct css mask class based on selection
        if ($(this).val() == usZip && zipInput.hasClass("IntlZip")) {
            zipInput.removeClass('IntlZip').addClass('UsZip');
        } else if (zipInput.hasClass("UsZip")) {
            zipInput.removeClass('UsZip').addClass('IntlZip');
        }
        maskZip();
    });

    // Handles when address type is changed
    $(document).on("change", ".addressTypeId", function (e) {
        updateAddressTitles(e);
    });
});