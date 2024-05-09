$(function () {
    //TODO:Add international phone and mask
    // Add us phone number
    $("#usPhoneAdd").click(function (e) {
        e.preventDefault();
        var ulParent = $("#userPhones");
        var liLength = ulParent.children().length;
        if (ulParent.children().length > 0) {
            // Clone the first element
            var template = ulParent.children().eq(0);
            var newLi = template.clone();
            var phoneTypeId = newLi.find(".userPhoneTypeDropDown");
            var phoneNumber = newLi.find(".userPhoneNumber");
            var prefPhone = newLi.find(".userPrefPhone");
            var phoneIdHidden = newLi.find("input[name$='PhoneId']");
            var prefPhoneHidden = newLi.find("input[name$='Primary']");
            var intlHidden = newLi.find("input[name$='International']");
            var prefPhoneSection = newLi.find(".prefPhoneTag");
            var intlPhoneSection = newLi.find(".intlPhoneTag");
            // Set the new id and name
            $.setElementArrayIndex(phoneTypeId, liLength);
            $.setElementArrayIndex(phoneNumber, liLength);
            prefPhone.attr("id", prefPhone.attr("id").replace("_0", "_" + liLength));
            prefPhone.closest('div').find('label').attr('for', prefPhone.attr("id").replace("_0", "_" + liLength));
            intlPhoneSection.attr("id", intlPhoneSection.attr("id").replace("_0", "_" + liLength));
            intlPhoneSection.closest('div').find('label:last-child').attr('for', intlPhoneSection.attr("id").replace("_0", "_" + liLength));
            $.setElementArrayIndex(prefPhoneHidden, liLength);
            $.setElementArrayIndex(intlHidden, liLength);
            $.setElementArrayIndex(phoneIdHidden, liLength);
            // Reset the values
            phoneTypeId.val("9");
            phoneIdHidden.val("0");
            phoneNumber.val("");
            prefPhone.val("False");
            prefPhone.prop("checked", false);
            prefPhone.prop("disabled", false);
            prefPhoneHidden.val("False");
            phoneNumber.prop("class", "userPhoneNumber UsPhone");
            intlHidden.val("False");
            intlPhoneSection.prop('checked', false);


            prefPhoneSection.text("Secondary");
            // Add to parent element
            ulParent.append(newLi);
            phoneTypeId.focus();

            maskPhone();
        }
    });
    $(document).on('change', '.userPrefPhone:radio', function () {
        var hiddenId = $(this)[0].id;
        hiddenId = hiddenId.replace("UserPhonePref", "UserPhones");
        hiddenId = hiddenId + "__Primary";
        // Replace preferred text with secondary
        $(".prefPhoneTag").text($("#SecondaryText").val());
        // Add it back for the current control
        $(this).parent().find('.prefPhoneTag').text($("#PreferredText").val());
        // Set value of all hidden fields to false
        $("input[name^='UserPhone'][name$='Primary']:hidden").val('False');
        // Set value of associated hidden field to true for form binding
        $("input[name^='UserPhone'][name$='Primary'][id='" + hiddenId + "']:hidden").val('True');
    });

    $(document).on('click', '.intlPhoneTag', function () {
        var upnField = $(this).parent().find(".userPhoneNumber");
        var upnHiddenIntl = $(this).parent().next().next("input[name$='International']");
        if ($(this).prop('checked')) {
            upnField.addClass('IntlPhone').removeClass('UsPhone UsPhoneDesk');
            upnHiddenIntl.val('True');
        }
        else {
            upnField.removeClass('IntlPhone').addClass('UsPhone');
            upnHiddenIntl.val('False');
        }
        maskPhone();
    });
});