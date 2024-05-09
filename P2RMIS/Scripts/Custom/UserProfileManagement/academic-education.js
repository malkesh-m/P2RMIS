$(function () {
    // Disable add icon if n/a is selected
    $("#degreeCheckbox").click(function () {
        if ($(this).prop("checked")) {
            $("#addDegreeIcon").attr('src', '/Content/img/plus_disabled.png')
        }
        else {
            $("#addDegreeIcon").attr('src', '/Content/img/plus.png')
        }
    });

    // Add user degree
    $("#userDegreeAdd").click(function (e) {
        e.preventDefault();
        if (!$("#degreeCheckbox").prop("checked")) {
            //
            // reset the checkox
            //
            //$("#degreeCheckbox").prop("checked", false);

            var userDegrees = $("#userDegrees");
            var userDegreeLength = userDegrees.children().length;
            if (userDegreeLength > 0) {
                // Clone the first element
                var template = userDegrees.children().first();
                var newElement = template.clone();
                newElement.removeClass("hidden");
                var degreeId = newElement.find(".degreeId");
                var degreeMajor = newElement.find(".degreeMajor");
                var userDegreeId = newElement.find("input[name$='UserDegreeId']");
                var IsDeletable = newElement.find(".degreeIsDeletable");
                // Set the new id and name
                $.setElementArrayIndex(degreeId, userDegreeLength);
                $.setElementArrayIndex(degreeMajor, userDegreeLength);
                $.setElementArrayIndex(userDegreeId, userDegreeLength);
                $.setElementArrayIndex(IsDeletable, userDegreeLength);
                // Reset the values
                degreeId.val("");
                degreeMajor.val("");
                userDegreeId.val("0");
                IsDeletable.val(Boolean(false));
                // Add to parent element
                userDegrees.append(newElement);
                //
                // Now just in case they have deleted all elements, the cloned element
                // will be hidden.  In which case one needs to show it.
                //
                newElement.show();
            }
        }
    });
    $(document).on('click', '.userDegreeDelete', function (e) {
        e.preventDefault();
        $('.modal-footer').remove();
        var currentElement = $(this).closest("li");
        var degreeId = currentElement.find(".degreeId");
        var modalTitle = "<span class='modalWarningCaption'>Warning</span>";
        p2rims.modalFramework.displayModalNoEvent($("#EducationDeletionDialog").html(), p2rims.modalFramework.customModalSizes.small, modalTitle);
        $(".modal-footer").append(p2rims.modalFramework.defaultModalFooters.cancelOkEduFooter);
        $('#education-confirm-deletion-button').attr('elementId', degreeId.attr('id'));
        $("a[id='closeModal'], button[id='closeDialogBtn']").click(function () { $('.ui-dialog-titlebar-close').click() });
    });
    $(document).on('click', '#education-confirm-deletion-button', function (e) {
        e.preventDefault();
        var elementId = $(this).attr('elementId');
        var currentElement = $('#' + elementId).parent();
        var degreeId = currentElement.find(".degreeId");
        var degreeMajor = currentElement.find(".degreeMajor");
        var IsDeletable = currentElement.find(".degreeIsDeletable");

        // Reset the values
        degreeId.val("");
        degreeMajor.val("");
        //
        // Set the delete flag
        //
        IsDeletable.val(Boolean(true));
        // Hide element
        currentElement.parent().hide();
        $(this).closest(".ui-dialog .ui-dialog-content").dialog("close");
    });
});