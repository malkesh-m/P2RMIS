//
// Event handler populates the system role drop down based on the selection in the profile type drop down
//
$(function () {
    $('body').on('change', '#profileTypeDropdownList', function (e) {
        var addressMessage = $('#addressRequired');
        addressMessage.html(" A <i>preferred address</i> is only required for <i>reviewers</i>.  If you choose to provide an address, please enter all required fields.");
        if ($('#profileTypeDropdownList').val() == $("#ProspectProfileTypeId").val() || $('#profileTypeDropdownList').val() == $("#MisconductProfileTypeId").val()) {
            $(".affiliate-required").hide();
        } else {
            $(".affiliate-required").show();
        }

        $.ajax({
            cache: false,
            url: "/UserProfileManagement/GetSystemRolesForProfileType",
            data: { "profileTypeId": $('#profileTypeDropdownList').val() }
        }).done(function (data) {
            var dropdown = $('#roleDropdownList');
            dropdown.empty();
            dropdown.prop("disabled", false);
            //
            // Add the "Select" only if there is more than a single entry in the dropdown
            //
            if (data.length != 1) {
                dropdown.append($('<option/>')
                            .attr('value', '')
                            .text('Select'));
            }
            //
            // Now there is a role list that has no entries (Misconduct).  If this
            // was selected, disable it.
            //
            if (data.length < 1) {
                dropdown.prop("disabled", true);
            }
            //
            // Now we add the roles
            //
            $.each(data, function (index, aBitOfData) {
                dropdown.append(
                    $('<option/>')
                        .attr('value', aBitOfData.Index)
                        .text(aBitOfData.Value)
                )
            });
        }).fail(function (xhr, ajaxOptions, thrownError) {
            alert($.defaultFailureMessage);
        });
    });
});
