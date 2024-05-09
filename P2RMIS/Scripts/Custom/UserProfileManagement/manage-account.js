$(function () {
    var manageAccountHtml;
    $('#manageAccountButton').click(function (e) {
        e.preventDefault();
        //
        // Depending upon the user's unlocked state disable the button.
        //
        if ($('#unLockUser').attr('data-locked') == 'false') {
            $('#unLockUser').prop("disabled", true);
        }
        DeactivateControlBehavior();
        //
        // Create the dialog modal window
        //
        var modalTitle = "Manage Account";
        if ($("#ManageAccount").length > 0) {
            manageAccountHtml = $("#ManageAccount").html();
        }
        p2rims.modalFramework.displayModalNoEvent(manageAccountHtml, p2rims.modalFramework.customModalSizes.large, modalTitle);
        $(".modal-footer").append(p2rims.modalFramework.defaultModalFooters.closeFooter);
        $("a[id='closeModal'], button[id='closeDialogBtn']").click(function () { $('.ui-dialog-titlebar-close').click() });
        $("#ManageAccount").remove();
    });

    // Click event for closing the modal window
    $(document).on('click', '#cancelManageAccount', function () {
        $('#saveMsg').text('');
        manageAccountHtml = $("#ManageAccountDialog").html();
        $(this).closest(".ui-dialog .ui-dialog-content").dialog("close");
    });
    //Script for sending credentials
    $('body').on('click', '#sendCredentials', function (e) {
        e.preventDefault();
        $(this).attr('disabled', 'disabled');
        //
        // Invoke the controller method to send credentials
        //
        $.ajax({
            cache: false,
            url: '/UserProfileManagement/SendCredentials',
            data: { "targetUserId": $("#UserId").val() }
        }).done(function (data) {
            var f = JSON.parse(data);
            $('#saveMsg').text(f.ActionSuccessMessage);
            if (f.Status)
            {
                //
                // Update the account status
                //
                $(selectorDeActivateDiv).attr(attributesAccountStatus, f.AccountStatusId);
                $(selectorDeActivateDiv).attr(attributeAccountStatusReason, f.AccountStatusReasonId);

                //
                // Update the Send Credentials section of the screen
                //
                $('#sentBy').text(f.SentByName);
                $('#sentDate').text(f.SentByDate);

                //
                // Update the Account Section
                setAccountStatus(f.ManageAccountAccountStatus);
                setAccountStatusDate(f.FormattedAccountStatusDate);

                // if the account was locked, set unlock info in the unlock section of the view
                if (f.IsLocked)
                {
                    $('#unLockedDate').text(f.SentByDate);
                    $('#unlockedBy').text(f.SentByName);
                }

                DeactivateControlBehavior();
            }
        }).fail(function (xhr, ajaxOptions, thrownError) {
            alert($.defaultFailureMessage);
        });
    });
    // Unlocking the user
    $('body').on('click', '#unLockUser', function (e) {
        e.preventDefault();
        //
        // Invoke the controller method to unlock the user
        //
        $.ajax({
            cache: false,
            url: '/UserProfileManagement/Unlock',
            data: { 'targetUserId': $("#UserId").val() }
        }).done(function (data) {
            var f = JSON.parse(data);
            $('#saveMsg').text('');
            if (f.Status)
            {
                //
                // Update the on screen values & disable the button
                //
                $('#unlockedBy').text(f.SentByName);
                $('#unLockUser').prop("disabled", true);
                $('#unLockUser').attr('data-locked', 'false');
                $('#lockedDate').text('');
                setUnlockedDate(f.SentByDate);
                //
                // Update the Account field set
                //
                setAccountStatusDate(f.FormattedAccountStatusDate);
                setAccountStatus(f.ManageAccountAccountStatus);

                $(selectorDeActivateDiv).attr(attributesAccountStatus, f.AccountStatusId);
                $(selectorDeActivateDiv).attr(attributeAccountStatusReason, f.AccountStatusReasonId);


                DeactivateControlBehavior();

            }
        }).fail(function (xhr, ajaxOptions, thrownError) {
            alert($.defaultFailureMessage);
        });
    });
    // Reactivating a user
    $('body').on('click', '#reActivateUser', function (e) {
        e.preventDefault();
        //
        // Invoke the controller method to reaqctivate the user
        //
        $.ajax({
            cache: false,
            url: '/UserProfileManagement/ReActivate',
            data: { 'targetUserId': $("#UserId").val() }
        }).done(function (data) {
            var f = JSON.parse(data);
            $('#saveMsg').text('');
            if (f.Status)
            {
                //
                // Update the on screen values & disable the button.
                // Also change the DIV's data values. (status & status reason)
                //
                $('#reActivateUser').attr('disabled', 'disabled');
                $(selectorDeActivateDiv).attr(attributesAccountStatus, f.AccountStatusId);
                $(selectorDeActivateDiv).attr(attributeAccountStatusReason, f.AccountStatusReasonId);
                setAccountStatus(f.ManageAccountAccountStatus);
                setSentByName(f.SentByName);
                setAccountUpdatedDate(f.FormatLastUpdateDateTime);
                setAccountStatusDate(f.FormattedAccountStatusDate);
            }
        }).fail(function (xhr, ajaxOptions, thrownError) {
            alert($.defaultFailureMessage);
        });
    });
    // DeActivating a user
    $('body').on('click', '#deActivateUser', function (e) {
        e.preventDefault();
        //
        // Invoke the controller method to deactivate
        //
        $.ajax({
            cache: false,
            url: '/UserProfileManagement/DeActivate',
            data: { 'targetUserId': $("#UserId").val(), 'accountStatusReasonId': $('#deactivateDropdownList').val() }
        }).done(function (data) {
            var f = JSON.parse(data);
            $('#saveMsg').text('');
            if (f.Status)
            {
                //
                // Update the on screen values & disable the button
                //
                $('#deActivateUser').hide();

                $(selectorDeActivateDiv).attr(attributeProfileType, f.ProfileTypeId);
                $(selectorDeActivateDiv).attr(attributesAccountStatus, f.AccountStatusId);
                $(selectorDeActivateDiv).attr(attributeAccountStatusReason, f.AccountStatusReasonId);
                $(selectorDeActivateDropdownList).attr('disabled', 'disabled');
                if (IsIneligible())
                {
                    $(selectorSendCredentials).attr('disabled', 'disabled');
                }
                else
                {
                    $(selectorSendCredentials).removeAttr('disabled');
                }
                if (IsAccountStatusInactive() || IsProfileMisconduct())
                {
                    DeactivateControlBehavior();
                }
                else
                {
                    $(selectorReActivateButton).attr('disabled', 'disabled');
                    $('#reActivateUser').show();
                }
                setAccountStatus(f.ManageAccountAccountStatus);
                setSentByName(f.SentByName);
                setAccountUpdatedDate(f.FormatLastUpdateDateTime);
            }
        }).fail(function (xhr, ajaxOptions, thrownError) {
            alert($.defaultFailureMessage);
        });
    });
    //
    // Event handler for Activate/Deactivate dropdown box
    //
    $('body').on('change', '#deactivateDropdownList', function (e) {
        if ($(this).val() == "")
        {
            $(selectorDeActivateButton).attr('disabled', 'disabled');
        }
        else
        {
            $(selectorDeActivateButton).removeAttr('disabled');
        }
    });
    //
    // Function to control visibility & behavior of drop down list and buttons in Deactivate field set
    //
    function DeactivateControlBehavior() {
        //
        // put the controls in a know state
        //
        $(selectorReActivateButton).hide();
        $(selectorDeActivateButton).hide();
        $(selectorActivateButton).hide();
        $(selectorDeActivateDropdownList).attr('disabled', 'disabled');

        $('#deactivateDropdownList').val(null);
        //
        // There are three cases that need to be dealt with:
        //   - user's profile type is Misconduct
        //   - user's account status is active & ...
        //   - user's account status is inactive with a reason of inactivity,, PWD expired, or account closed
        //
        //if (IsProfileMisconduct(IsAccountStatusInactive())) {
        if (IsProfileMisconduct()) {
            $(selectorActivateButton).show();
        } else if (IsAccountStatusActive()) {
            //
            // Activate the drop down & show the deactivate button but show it disabled because there is not
            // a selection made in the dropdown
            //
            $(selectorDeActivateDropdownList).removeAttr('disabled');
            $(selectorDeActivateButton).show();
            $(selectorDeActivateButton).attr('disabled', 'disabled');

        } else if (IsAccountStatusInactive()) {
            //
            // Show & enable the Activate button.  The dropdown list should stay disabled.
            //
            $(selectorReActivateButton).show();
            $(selectorReActivateButton).removeAttr('disabled');
        }

    }
    //
    // Tests if this user's profile time is Misconduct.
    //
    function IsProfileMisconduct() {
        var profileType = $(selectorDeActivateDiv).attr(attributeProfileType);
        var invalidProfileTYpes = $(selectorDeActivateDiv).attr(attributeInvalidProfileTypes);
        return (profileType == invalidProfileTYpes);
    }
    //
    // Tests if this user's status is Active 
    //
    function IsAccountStatusActive() {
        var userAccountStatus = $(selectorDeActivateDiv).attr(attributesAccountStatus);
        var activeStatus = $(selectorDeActivateDiv).attr(attributesActiveStatusIs);
        return (userAccountStatus == activeStatus);
    }
    //
    // Tests if this user's status is InActive with reasons of inactivity, PWD expired, or account closed
    //
    function IsAccountStatusInactive()
    {
        var userAccountStatus = $(selectorDeActivateDiv).attr(attributesAccountStatus);
        var userAccoutStatusReason = $(selectorDeActivateDiv).attr(attributeAccountStatusReason);

        var inactiveStatus = $(selectorDeActivateDiv).attr(attributesInactiveStatusIs);
        var listOfReasons = $(selectorDeActivateDiv).attr(attributeValidReasons);

        var result = -1;

        if (userAccountStatus == inactiveStatus)
        {
            var x = listOfReasons.split(",");
            result = (x.indexOf(userAccoutStatusReason));
        }
        return (result >= 0);
    }
    //
    // Tests if this user has been declared ineligible
    function IsIneligible()
    {
        var userAccountStatusReason = $(selectorDeActivateDiv).attr(attributeAccountStatusReason);
        var accountStatusReasonIneligible = $(selectorDeActivateDiv).attr(attributeAccountStatusReasonIneligible);

        return (userAccountStatusReason == accountStatusReasonIneligible);
    }
    ///
    /// Set the value of the user status
    ///
    function setAccountStatus(value) {
        $('#accountStatus').text(value);
    }
    ///
    /// Set the value of the sent by name
    ///
    function setSentByName(value) {
        $('#updatedByName').text(value)
    }
    ///
    /// Set the value of the AccountStatusUpdatedDate
    ///
    function setAccountUpdatedDate(value) {
        $('#accountStatusUpdatedDate').text(value);
    }
    ///
    /// Set the value of the AccountStatusDate
    ///
    function setAccountStatusDate(value) {
        $('#accountStatusDate').text(value);
    }
    ///
    /// Set the value of the AccountStatusDate
    ///
    function setUnlockedDate(value) {
        $('#unLockedDate').text(value);
    }
    //
    // Common selector values
    //
    var selectorReActivateButton = '#reActivateUser';
    var selectorDeActivateButton = '#deActivateUser';
    var selectorActivateButton = '#activateUser';
    var selectorDeActivateDropdownList = '#deactivateDropdownList';
    var selectorDeActivateDiv = "#deActivateButtons";
    var selectorSendCredentials = "#sendCredentials";
    //
    // Profile types
    //
    var attributeInvalidProfileTypes = "data-InvalidProfileTypes";
    var attributeProfileType = "data-profiletype";
    //
    // Account status & what is an active status
    var attributesAccountStatus = "data-accountstatus";
    var attributesActiveStatusIs = "data-activestatusis"
    //
    //
    //
    var attributeAccountStatusReason = "data-accountstatusreason"; // user reason id
    var attributesInactiveStatusIs = "data-inactivstatusis";
    var attributeValidReasons = "data-accountstatusreasonreason";
    var selectorInvalidProfileTypes = "#data-InvalidProfileTypes";
    var attributeAccountStatusReasonIneligible = "data-accountstatusreasonineligible"
});
