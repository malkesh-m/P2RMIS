$(function () {    
    $(document).on('change', 'input.alternateContactPrimaryFlag:radio', function () {
        // Remove preferred text first
        $(".prefAlternateContactTag").text($("#SecondaryText").val());
        // Add it back for the current control
        $(this).parent().find('.prefAlternateContactTag').text($("#PreferredText").val());

        // Set value of all hidden fields to false
        $("input.alternateContactPrimaryFlagHidden:hidden").val('False');
        // Set value of associated hidden field to true for form binding
        $(this).parent().find("input.alternateContactPrimaryFlagHidden:hidden").val('True');
    });

    // Add alternate contact
    $("#alternateContactAdd").click(function (e) {
        e.preventDefault();
        var alternateContactsVisible = $(".alternateContact:visible");
        var alternateContactsVisibleLength = alternateContactsVisible.length;
        var alternateContacts = $(".alternateContact");
        var alternateContactsLength = alternateContacts.length;
        if (alternateContactsVisibleLength < 2) {
            // Clone the first element

            var template = alternateContacts.first();
            var newElement = template.clone();

            var alternateContactId = newElement.find(".alternateContactId");
            var alternateContactPrimaryFlag = newElement.find(".alternateContactPrimaryFlag");
            var alternateContactPrimaryFlagHidden = newElement.find(".alternateContactPrimaryFlagHidden");
            var alternateContactTypeId = newElement.find(".alternateContactTypeId");
            var alternateContactFirstName = newElement.find(".alternateContactFirstName");
            var alternateContactLastName = newElement.find(".alternateContactLastName");
            var alternateContactPhoneTypeId = newElement.find("ul li:nth-child(1) .alternateContactPhoneTypeId");
            var alternateContactPhoneNumber = newElement.find("ul li:nth-child(1) .alternateContactPhoneNumber");
            var alternateContactPhoneExtension = newElement.find("ul li:nth-child(1) .alternateContactPhoneExtension");
            var alternateContactPhonePrimaryFlag = newElement.find("ul li:nth-child(1) .alternateContactPhonePrimaryFlag");
            var alternateContactPhonePrimaryFlagHidden = newElement.find("ul li:nth-child(1) .alternateContactPhonePrimaryFlagHidden");
            var alternateContactPhoneInternational = newElement.find("ul li:nth-child(1) .alternateContactPhoneInternational");
            var alternateContactPhoneTypeIdTwo = newElement.find("ul li:nth-child(2) .alternateContactPhoneTypeId");
            var alternateContactPhoneNumberTwo = newElement.find("ul li:nth-child(2) .alternateContactPhoneNumber");
            var alternateContactPhoneExtensionTwo = newElement.find("ul li:nth-child(2) .alternateContactPhoneExtension");
            var alternateContactPhonePrimaryFlagTwo = newElement.find("ul li:nth-child(2) .alternateContactPhonePrimaryFlag");
            var alternateContactPhonePrimaryFlagHiddenTwo = newElement.find("ul li:nth-child(2) .alternateContactPhonePrimaryFlagHidden");
            var alternateContactPhoneInternationalTwo = newElement.find("ul li:nth-child(2) .alternateContactPhoneInternational");
            var alternateContactEmail = newElement.find(".alternateContactEmail");
            var alternateContactTitle = newElement.find(".alternateContactTitle");
            var alternateContactPrefTag = newElement.find(".prefAlternateContactTag");
            var alternateContactPhonePrefTag = newElement.find(".prefAlternateContactPhoneTag");
            var alternateContactIsDeletable = newElement.find(".alternateContactIsDeletable");
            // Set the new id and name

            $.setElementArrayIndex(alternateContactId, alternateContactsLength);
            $.setElementArrayIndex(alternateContactPrimaryFlag, alternateContactsLength);
            $.setElementArrayIndex(alternateContactPrimaryFlagHidden, alternateContactsLength);
            $.setElementArrayIndex(alternateContactTypeId, alternateContactsLength);
            $.setElementArrayIndex(alternateContactFirstName, alternateContactsLength);
            $.setElementArrayIndex(alternateContactLastName, alternateContactsLength);
            $.setElementArrayIndex(alternateContactPhoneTypeId, alternateContactsLength);
            $.setElementArrayIndex(alternateContactPhoneNumber, alternateContactsLength);
            $.setElementArrayIndex(alternateContactPhoneExtension, alternateContactsLength);
            $.setElementArrayIndex(alternateContactPhonePrimaryFlag, alternateContactsLength);
            $.setElementArrayIndex(alternateContactPhonePrimaryFlagHidden, alternateContactsLength);
            alternateContactPhonePrimaryFlag.closest('div').find('label').attr('for', alternateContactPhonePrimaryFlag.attr("id"));
            alternateContactPhoneInternational.closest('div').find('label:last-child').attr('for', alternateContactPhoneInternational.attr("id"));
            $.setElementArrayIndex(alternateContactPhoneTypeIdTwo, alternateContactsLength);
            $.setElementArrayIndex(alternateContactPhoneNumberTwo, alternateContactsLength);
            $.setElementArrayIndex(alternateContactPhoneExtensionTwo, alternateContactsLength);
            $.setElementArrayIndex(alternateContactPhonePrimaryFlagTwo, alternateContactsLength);
            $.setElementArrayIndex(alternateContactPhonePrimaryFlagHiddenTwo, alternateContactsLength);
            alternateContactPhonePrimaryFlagTwo.closest('div').find('label').attr('for', alternateContactPhonePrimaryFlagTwo.attr("id"));
            alternateContactPhoneInternationalTwo.closest('div').find('label:last-child').attr('for', alternateContactPhoneInternationalTwo.attr("id"));
            alternateContactEmail.attr('id', 'AlternateContactPersons_1__Email');
            alternateContactEmail.closest('div').find('label').attr('for', alternateContactEmail.attr("id"));
            $.setElementArrayIndex(alternateContactIsDeletable, alternateContactsLength);
            // Reset the values
            alternateContactId.val("0");
            alternateContactPrimaryFlag.prop("checked", false);
            alternateContactPrimaryFlagHidden.val("False");
            alternateContactTypeId.val("");
            alternateContactFirstName.val("");
            alternateContactLastName.val("");
            alternateContactPhoneTypeId.val("");
            alternateContactPhoneNumber.val("");
            alternateContactPhoneExtension.val("");
            alternateContactPhonePrimaryFlag.prop("checked", false);
            alternateContactPhonePrimaryFlag.prop("disabled", false);
            alternateContactPhonePrimaryFlag.eq(0).prop("checked", true);
            alternateContactPhoneTypeIdTwo.val("");
            alternateContactPhoneNumberTwo.val("");
            alternateContactPhoneExtensionTwo.val("");
            alternateContactPhonePrimaryFlagTwo.prop("checked", false);
            alternateContactPhonePrimaryFlagTwo.prop("disabled", false);
            alternateContactPhonePrimaryFlagTwo.eq(0).prop("checked", false);
            alternateContactPrefTag.text($("#SecondaryText").val());
            alternateContactPhonePrefTag.text($("#SecondaryText").val());
            alternateContactPhonePrefTag.eq(0).text($("#PreferredText").val());
            alternateContactPhoneInternational.prop("checked", false);
            alternateContactPhoneInternationalTwo.prop("checked", false);
            alternateContactPhoneNumber.removeClass('IntlPhone');
            alternateContactPhoneNumberTwo.removeClass('IntlPhone');
            alternateContactTitle.html($("#SecondaryText").val());
            alternateContactIsDeletable.val("false");
            // Insert after the template
            newElement.insertAfter(alternateContacts.last());
            alternateContactTypeId.focus();
            maskPhone();

            //
            // Now just in case they have deleted all elements, the cloned element
            // will be hidden.  In which case one needs to show it.
            //
            newElement.show();
            //
            // Count the number of visible contacts.  If there is n 2
            // then  the link should be disabled.
            //
            var elements = $(".alternateContact:visible");
            if (elements.length == 2) {
                $("#alternateContactAdd").attr('src', '/Content/img/plus_disabled.png');
                $("#alternateContactAdd").parent().addClass("disabled");
                $("#alternateContactAdd").addClass("disabled");
                $("#alternateContactAdd").attr('title', 'Add Alternate Disabled');
            }
        }
    });

    $(document).on('click', '.alternateContactDelete', function (e) {
        e.preventDefault();
        var currentElement = $(this).closest(".alternateContact");
        var alternateContactId = currentElement.find(".alternateContactId");
        var modalTitle = '<span class="modalSmallCaption modalWarningCaption">Warning</span>';
        $('.modal-footer').remove();        
        p2rims.modalFramework.displayModalNoEvent($("#AlternateContactDeletionDialog").html(), p2rims.modalFramework.customModalSizes.medium, modalTitle);
        $(".modal-footer").append(p2rims.modalFramework.defaultModalFooters.cancelOkProfileFooter);
        $('#modal-confirm-alternateContact-deletion-button').attr('elementId', alternateContactId.attr('id'));
        $("a[id='closeModal'], button[id='closeDialogBtn']").click(function () { $('.ui-dialog-titlebar-close').click(); });
    });

    // Remove/Delete alternate contact
    $(document).on('click', '#modal-confirm-alternateContact-deletion-button', function (e) {
        e.preventDefault();
        var elements = $(".alternateContact:visible");
        var elementLength = elements.length;
        if (elementLength > 0) {
            var elementId = $(this).attr('elementId');
            var currentElement = $('#' + elementId).parent().parent();
            var IsDeletable = currentElement.find(".alternateContactIsDeletable");
            // Set the delete flag
            IsDeletable.val(Boolean(true));

            // Hide element
            currentElement.hide();

            // Find the remaining visible element
            var visibleElement = $(".alternateContact:visible").eq(0);
            visibleElement.find('input.alternateContactPrimaryFlag:radio').prop('checked', true);
            visibleElement.find('.prefAlternateContactTag').text($("#PreferredText").val());
            visibleElement.find('.prefAlternateContactTag').removeClass("hidden");
            visibleElement.find("input.alternateContactPrimaryFlagHidden:hidden").val('True');
        }
        $(this).closest(".ui-dialog .ui-dialog-content").dialog("close");
        //
        // Count the number of visible contacts.  If there is less than 2
        // then we can add more so the link should be enabled.
        //
        elements = $(".alternateContact:visible");
        if (elements.length < 2) {
            $("#alternateContactAdd").parent().removeClass("disabled");
            $("#alternateContactAdd").removeClass("disabled");
            $("#alternateContactAdd").attr('src', '/Content/img/plus.png');
            $("#alternateContactAdd").attr('title', 'Add Alternate');
        }
    });
    // Enable/disable mask if IntlPhone checkbox is changed
    $(document).on('change', 'input.alternateContactPhoneInternational:checkbox', function () {

        if ($(this).is(":checked"))
            $(this).parent().find('.alternateContactPhoneNumber').addClass("IntlPhone");
        else
            $(this).parent().find('.alternateContactPhoneNumber').removeClass("IntlPhone");
        maskPhone();
    });

    $('input[name=AlternateContactPhones]:radio').change(function () {
        // Hide all first
        $(".prefAlternateContactPhoneTag").hide();
        // Show current one
        $(this).parent().find('.prefAlternateContactPhoneTag').show();
    });
    
    $(document).on('change', 'input.phonePreferredType:radio', function () {
        // Remove preferred text first
        $(this).closest("ul").find(".prefAlternateContactPhoneTag").text($("#SecondaryText").val());
        // Add it back for the current control
        $(this).parent().find('.prefAlternateContactPhoneTag').text($("#PreferredText").val());
        // Set value of all hidden fields to false
        $(this).closest("ul").find("input.alternateContactPhonePrimaryFlagHidden:hidden").val('False');
        // Set value of associated hidden field to true for form binding
        $(this).parent().find("input.alternateContactPhonePrimaryFlagHidden:hidden").val('True');
    });
});