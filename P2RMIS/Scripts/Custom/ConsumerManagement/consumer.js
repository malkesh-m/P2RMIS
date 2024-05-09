$(document).ready(function () {
    var editing = $("#editing").val() == "True";
    $("#nomineeTypeId").prop("disabled", !editing);
    $('#nomineeDob').kendoDatePicker();
    $(".phone-number").mask("?(999) 999-9999");
 
    // round-about way to remove Chrome autofill
    var tmpNominatingOrg = $('#nominatingOrganization');
    if (!tmpNominatingOrg.val().trim()) 
        $('#nominatingOrganization').val(' ');
    else {
        if (!$("#nominatingOrganizationId").val().trim()) {
              document.getElementById('lblNomOrgWarning').style.display = 'block';
        }
    }
 
    $("#nominatingOrganization").focus(function () {
        $(this).attr('autocomplete', 'new_nomOrg');

    }); 

    var isSelected = false;

    // for ajax call
    $("#nominatingOrganization").autocomplete({
        create: function (e) {
            e.preventDefault();
            // For autofill now remove all temp spaces
            if (!tmpNominatingOrg.val().trim()) {
                $('#nominatingOrganization').val('');
            }
        },
        focus: function (e) { $(e.toElement).parents().show(); },
        source: function (request, response) {
            $.ajax({
                url: "/ConsumerManagement/GetNominatingOrganizations",
                type: "POST",
                dataType: "json",
                data: { inprefix: request.term, findtype: "startswith" },
                success: function (data) {
                    response($.map(data, function (item) {
                        return { label: item.Value, value: item.Value, id: item.Key };
                    }));
                }
            });
        },
        change: function(e, ui) {
            isSelected = false;
        },
        select: function (e, ui) {
            $("#nominatingOrganizationId").val(ui.item.id);
            this.value = ui.item.value;
            isSelected = true;
        },
        minLength: 3
    });

    $("#nominatingOrganization").change(function () {
         var nomOrgId = $('#nominatingOrganizationId').val();
         if (!isSelected || !nomOrgId.trim()) {
             document.getElementById('lblNomOrgWarning').style.display = 'block';
            $("#nominatingOrganizationId").val('');
       } else {
            document.getElementById('lblNomOrgWarning').style.display = 'none';
        }

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
    // Check if email is used by another consumer
    var nomineeEmailIsUnavailable = function () {
        var existsFlag = false;
        $.ajax({
            type: "POST",
            data: $("#nomineeId,#nomineeEmail").serialize(),
            url: "/ConsumerManagement/EmailIsUnavailable",
            async: false
        }).success(function (status) {
            existsFlag = status;
        }).fail(function (jqXHR) {
            alert(jqXHR.responseText);
        });
        return existsFlag;
    };
    function validateNomineePhone() {
        var valid = true;
        var nomineePhone1 = $("#nomineePhone1").val();
        var nomineePhone2 = $("#nomineePhone2").val();
        if (nomineePhone2 == "" && nomineePhone1 != "") {
            valid = ValidatePhone(nomineePhone1);
        } else if (nomineePhone1 == "" && nomineePhone2 != "") {
            valid = ValidatePhone(nomineePhone2);
        }
        return valid;
    }
    function validateNominatorPhone() {
        var valid = true;
        var nominatorPhone1 = $("#nominatorPhone1").val();
        var nominatorPhone2 = $("#nominatorPhone2").val();
        if (nominatorPhone2 == "" && nominatorPhone1 != "") {
            valid = ValidatePhone(nominatorPhone1);
        } else if (nominatorPhone1 == "" && nominatorPhone2 != "") {
            valid = ValidatePhone(nominatorPhone2);
        }
        return valid;
    }
    function getConsumerProgramYears() {
        var clientProgramId = $("#nomineeProgram").val();
        if (clientProgramId != "") {
            $.ajax({
                url: "/ConsumerManagement/GetConsumerProgramYears",
                data: { clientProgramId: clientProgramId, editing: editing }
            })
                .done(function (programYears) {
                    $("#nomineeProgramYear").empty();
                    $.each(programYears, function (index, value) {
                        $("#nomineeProgramYear").append("<option value='" + value.Key + "'>" + value.Value + "</option>");
                    });
                })
                .fail(function (jqXHR, textStatus, errorThrown) {
                    alert($.defaultFailureMessage);
                });
        }
    }
    function changeProgramDropdownTooltip() {
        var selectedProgramName = $("#nomineeProgram option:selected").data("program-name");
        $("#nomineeProgram").attr("title", selectedProgramName);
    }
    changeProgramDropdownTooltip();
    // Nominee email validataion
    $("#nomineeEmail").blur(function () {
        $("#nomineeEmailMessage").html("");
        $("#nomineeEmailMessageContainer").addClass("hidden");
        var email = $.trim($("#nomineeEmail").val());
        if (email != "") {
            if (nomineeEmailIsUnavailable()) {
                $("#nomineeEmailMessage").html("This email address is associated with another consumer account. Please use a different email address.");
                $("#nomineeEmailMessageContainer").removeClass("hidden");
            } else if (!ValidateEmail(email)) {
                $("#nomineeEmailMessage").html("This email address is in an invalid format.");
                $("#nomineeEmailMessageContainer").removeClass("hidden");
            }
        }
    });
    // Nominator email validataion
    $("#nominatorEmail").blur(function () {
        $("#nominatorEmailMessage").html("");
        $("#nominatorEmailMessageContainer").addClass("hidden");
        var email = $.trim($("#nominatorEmail").val());
        if (email != "" && !ValidateEmail(email)) {
            $("#nominatorEmailMessage").html("This email address is in an invalid format.");
            $("#nominatorEmailMessageContainer").removeClass("hidden");
        }
    });
    // Nominee phone number validation
    $("#nomineePhone1,#nomineePhone2").blur(function () {
        $("#nomineePhoneMessage").html("");
        $("#nomineePhoneMessageContainer").addClass("hidden");
        if (!validateNomineePhone()) {
            $("#nomineePhoneMessage").html("This phone number is in an invalid format.");
            $("#nomineePhoneMessageContainer").removeClass("hidden");
        }
    });
    // Nominator phone number validation
    $("#nominatorPhone1,#nominatorPhone2").blur(function () {
        $("#nominatorPhoneMessage").html("");
        $("#nominatorPhoneMessageContainer").addClass("hidden");
        if (!validateNominatorPhone()) {
            $("#nominatorPhoneMessage").html("This phone number is in an invalid format.");
            $("#nominatorPhoneMessageContainer").removeClass("hidden");
        }
    });
    $("#nomineeProgram").change(function () {
        getConsumerProgramYears();
        changeProgramDropdownTooltip();
    });
    // Reset input border style
    $("input, select").on("input change", function () {
        $(this).css("border", "");
    });
    // Save consumer
    $("#btnSaveConsumer").on("click", function (e) {
        e.preventDefault();
        $("#nomineeError").empty();
        var validationMsgs = [];
        var validationElements = [];
        $("#successMessage").empty();
        $("#nomineeError").empty();

        // Nominee
        var firstName = $.trim($("#nomineeFirstName").val());
        if (firstName === "") {
            validationMsgs.push("First name is required.");
            validationElements.push("#nomineeFirstName");
        }
        var lastName = $.trim($("#nomineeLastName").val());
        if (lastName === "") {
            validationMsgs.push("Last name is required.");
            validationElements.push("#nomineeLastName");
        }
        var address1 = $.trim($("#nomineeAddress1").val());
        if (address1 === "") {
            validationMsgs.push("Address 1 is required.");
            validationElements.push("#nomineeAddress1");
        }
        var city = $.trim($("#nomineeCity").val());
        if (city === "") {
            validationMsgs.push("City is required.");
            validationElements.push("#nomineeCity");
        }
        var stateId = $("#nomineeState").val();
        if (stateId === "") {
            validationMsgs.push("State is required.");
            validationElements.push("#nomineeState");
        }
        var zip = $.trim($("#nomineeZip").val());
        if (zip === "") {
            validationMsgs.push("Zip code is required.");
            validationElements.push("#nomineeZip");
        }
        var countryId = $("#nomineeCountry").val();
        if (countryId === "") {
            validationMsgs.push("Country is required.");
            validationElements.push("#nomineeCountry");n
        }
        var email = $.trim($("#nomineeEmail").val());
        if (email == "") {
            validationMsgs.push("Email is required.");
            validationElements.push("#nomineeEmail");
        } else if (nomineeEmailIsUnavailable()) {
            validationMsgs.push("Nominee email address is associated with another consumer account. Please use a different email address.");
            validationElements.push("#nomineeEmail");
        } else if (!ValidateEmail(email)) {
            validationMsgs.push("Email address is in an invalid format.");
            validationElements.push("#nomineeEmail");
        }
        var phoneType1 = $("#nomineePhoneType1").val();
        var phoneNumber1 = $.trim($("#nomineePhone1").val());
        var phoneType2 = $("#nomineePhoneType2").val();
        var phoneNumber2 = $.trim($("#nomineePhone2").val());
        if (phoneNumber1 === "" && phoneNumber2 === "") {
            validationMsgs.push("At least 1 phone entry is required.");
            validationElements.push("#nomineePhone1");
        } else if ((phoneType1 === "" && phoneNumber1 !== "") || (phoneType2 === "" && phoneNumber2 !== "")) {
            validationMsgs.push("Phone type is required.");
            if (phoneType1 === "" && phoneNumber1 !== "") {
                validationElements.push("#nomineePhoneType1");
            } else {
                validationElements.push("#nomineePhoneType2");
            }
        } else if (!validateNomineePhone()) {
            validationMsgs.push("Phone number is in an invalid format.");
            validationElements.push("#nomineePhone1");
        }  
        // Program
        var clientProgramId = $("#nomineeProgram").val();
        if (clientProgramId === "") {
            validationMsgs.push("Nominee program is required.");
            validationElements.push("#nomineeProgram");
        }
        var fiscalYear = $("#nomineeProgramYear").val();
        if (fiscalYear === "") {
            validationMsgs.push("Nominee program year is required.");
            validationElements.push("#nomineeProgramYear");
        }
        var diseaseSite = $.trim($("#nomineeProgramDiseaseSite").val());
        if (diseaseSite === "") {
            validationMsgs.push("Disease site is required.");
            validationElements.push("#nomineeProgramDiseaseSite");
        }
        var score = $.trim($("#nomineeProgramScore").val());
        if (score === "") {
            validationMsgs.push("Score is required.");
            validationElements.push("#nomineeProgramScore");
        }     
        // Sponsor
        var nominatingOrganization = $.trim($("#nominatingOrganization").val());
        if (nominatingOrganization === "") {
            validationMsgs.push("Nominating organization is required.");
            validationElements.push("#nominatingOrganization");
        }
        var nominatorAddress1 = $.trim($("#nominatingOrganizationAddress1").val());
        if (nominatorAddress1 === "") {
            validationMsgs.push("Nominating organization address 1 is required.");
            validationElements.push("#nominatingOrganizationAddress1");
        }
        var nominatorCity = $.trim($("#nominatingOrganizationCity").val());
        if (nominatorCity === "") {
            validationMsgs.push("Nominating organization city is required.");
            validationElements.push("#nominatingOrganizationCity");
        }
        var nominatorStateId = $("#nominatingOrganizationState").val();
        if (nominatorStateId === "") {
            validationMsgs.push("Nominating organization state is required.");
            validationElements.push("#nominatingOrganizationState");
        }
        var nominatorZip = $.trim($("#nominatingOrganizationZip").val());
        if (nominatorZip === "") {
            validationMsgs.push("Nominating organization zip code is required.");
            validationElements.push("#nominatingOrganizationZip");
        }
        var nominatorCountryId = $("#nominatingOrganizationCountry").val();
        if (nominatorCountryId === "") {
            validationMsgs.push("Nominating organization country is required.");
            validationElements.push("#nominatingOrganizationCountry");
        }
        var nominatorFirstName = $.trim($("#nominatorFirstName").val());
        if (nominatorFirstName === "") {
            validationMsgs.push("Nominator first name is required.");
            validationElements.push("#nominatorFirstName");
        }
        var nominatorLastName = $.trim($("#nominatorLastName").val());
        if (nominatorLastName === "") {
            validationMsgs.push("Nominator last name is required.");
            validationElements.push("#nominatorLastName");
        }
        var nominatorEmail = $.trim($("#nominatorEmail").val());
        if (nominatorEmail === "") {
            validationMsgs.push("Nominator email is required.");
            validationElements.push("#nominatorEmail");
        } else if (!ValidateEmail(nominatorEmail)) {
            validationMsgs.push("Nominator email address is in an invalid format.");
            validationElements.push("#nominatorEmail");
        }
        var nominatorPhoneType1 = $("#nominatorPhoneType1").val();
        var nominatorPhoneNumber1 = $.trim($("#nominatorPhone1").val());
        var nominatorPhoneNumber2 = $.trim($("#nominatorPhone2").val());
        if (nominatorPhoneNumber1 === "" && nominatorPhoneNumber2 === "") {
            validationMsgs.push("At least 1 phone number for the nominator is required.");
            validationElements.push("#nominatorPhone1");
        } else if ((nominatorPhoneType1 === "" && nominatorPhoneNumber1 !== "") || (nominatorPhoneType1 === "" && nominatorPhoneNumber1 !== "")) {
            validationMsgs.push("Phone type for the nominator is required.");
            if (nominatorPhoneType1 === "" && nominatorPhoneNumber1 !== "") {
                validationElements.push("#nominatorPhoneType1");
            } else {
                validationElements.push("#nominatorPhoneType2");
            }
        } else if (!validateNominatorPhone()) {
            validationMsgs.push("Nominator phone number is in an invalid format.");
            validationElements.push("#nominatorPhone1");
        }      
        // Save nominee if there are no errors
        if (validationMsgs.length === 0) {     
            $.ajax({
                cache: false,
                type: 'POST',
                url: "/ConsumerManagement/SaveNominee",
                data: $("#consumerForm").serialize() + "&NomineeProgramUpdateModel.NomineeTypeId=" + $("#nomineeTypeId option:selected").val()  //nomineeTypeId is outside form, adding to query string manually
            }).success(function (data) {
                var msg = data.StatusMessage;
                if (msg == "Email in Use") {
                    var dialogTitle = "<span class='modalSmallCaption modalWarningCaption'>Warning</span>";
                    var statusObject = data.StatusObject;
                    var userProfileLink = "<a style='color: #0000EE;' target='_blank' href='/UserProfileManagement/ViewUser?userInfoId=" + statusObject.UserInfoId + "'>" + statusObject.UserProfileName + "</a>";
                    $("#userProfileName").html(userProfileLink);
                    $("#userEmail").html(statusObject.UserEmail);
                    p2rims.modalFramework.displayModalNoEvent($("#userProfileExistsDialog").html(), p2rims.modalFramework.customModalSizes.medium, dialogTitle);
                    $(".modal-footer").append(p2rims.modalFramework.defaultModalFooters.confirmFooter);
                    $("a[id='closeModal'], button[id='closeDialogBtn']").click(function () { $('.ui-dialog-titlebar-close').click(); });
                    $("button[id='saveDialogChanges']").click(function () {
                        $("#nomineeUserId").val(statusObject.UserId);
                        $("#btnSaveConsumer").click();
                    });
                } else {
                    if ($("#addAnotherNominee").prop("checked")) {
                        $("input[type=text], select:enabled").val("");
                        document.getElementById('lblNomOrgWarning').style.display = 'none';
                        // Show success message
                        $("#successMessage").html(msg).show().delay(20000).fadeOut();
                    } else {
                        localStorage.setItem("carryOverMessage", msg);
                        window.location.href = "/ConsumerManagement/Index";
                    }
                }
            }).fail(function (jqXHR, textStatus, errorThrown) {
                alert($.defaultFailureMessage);
            });
        } else {
            // Show error messages
            $("input").css("border", "");
            var headerElement = $("<div/>").html("Please correct the following errors:");
            $("#nomineeError").append(headerElement);
            for (var i = 0; i < validationMsgs.length; i++) {
                $(validationElements[i]).css("border", "1px solid red");
                var msgElement = $("<div/>").html(validationMsgs[i]);
                if (i >= 10) {
                    msgElement.addClass("hidden");
                }
                $("#nomineeError").append(msgElement);
            }
            if (validationMsgs.length > 10) {
                var linkElement = $("<a/>").attr("href", "#").html("More").addClass("more-link");
                $("#nomineeError").append($("<div/>").append(linkElement));
            }
        }
        $(window).scrollTop(0);
    });
    // More link handler
    $(document).on("click", ".more-link", function () {
        $("#nomineeError").find(".hidden").removeClass("hidden");
        $("#nomineeError").find("a.more-link").parent().remove();
        var linkElement = $("<a/>").attr("href", "#").html("Less").addClass("less-link");
        $("#nomineeError").append($("<div/>").append(linkElement));
    });
    // Less link handler
    $(document).on("click", ".less-link", function () {
        $("#nomineeError").find("div:gt(10)").addClass("hidden");
        $("#nomineeError").find("a.less-link").parent().remove();
        var linkElement = $("<a/>").attr("href", "#").html("More").addClass("more-link");
        $("#nomineeError").append($("<div/>").append(linkElement));
    });
    // Applies score max rule
    $("#nomineeProgramScore").on("keyup mouseup", function () {
        var value = $(this).val();
        var intValue = parseInt(value);
        var intMin = parseInt($(this).attr("min"));
        var intMax = parseInt($(this).attr("max"));
        if (isNaN(intValue) || intValue < intMin) {
            $(this).val("");
            $(this).select();
        } else if (intValue != value || intValue > intMax) {
            $(this).val(value.substring(0, value.length - 1));
            $(this).select();
        } else {
            $(this).val(intValue.toString());
        }
    });
    // cancel consumer changes
    $(".cancel-consumer-changes").on("click", function (e) {
        e.preventDefault();
        p2rims.modalFramework.displayModalNoEvent($("#cancelConsumerChangesDialog").html(),
            p2rims.modalFramework.customModalSizes.small,
            '<span class="modalSmallCaption modalWarningCaption">Warning</span>');
        $(".modal-footer").append(p2rims.modalFramework.defaultModalFooters.confirmFooter);
        $("a[id='closeModal'], button[id='closeDialogBtn']").click(function () { $('.ui-dialog-titlebar-close').click(); });
        $("button[id='saveDialogChanges']").click(function () {
            window.location.reload();
        });
    });
});