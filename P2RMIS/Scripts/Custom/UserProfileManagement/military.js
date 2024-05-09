$(function () {
    // Disable rank and status if service is not selected
    if ($("#MilitaryServiceId").val() === "") {
        $("#MilitaryServiceAndRank_MilitaryRankId").prop("disabled", true);
    }
    // Disable status if rank is not selected
    if ($("#MilitaryServiceAndRank_MilitaryRankId").val() === "") {
        $("#MilitaryStatus_MilitaryStatusTypeId").prop("disabled", true);
    }
    // Populate rank if service is changed
    $("#MilitaryServiceId").change(function () {
        var serviceId = $("#MilitaryServiceId").val();
        var service = $("#MilitaryServiceId option:selected").text();
        if (serviceId !== "") {
            $.ajax({
                cache: false,
                type: 'GET',
                url: '/UserProfileManagement/PopulateMilitaryRankDropdown',
                data: { "service": service }
            }).done(function (data) {
                var jsonData = JSON.parse(data);
                var militaryRankDropdown = $("#MilitaryServiceAndRank_MilitaryRankId");
                militaryRankDropdown.empty();
                $("<option />", {
                    val: "",
                    text: "Select"
                }).appendTo(militaryRankDropdown);
                $(jsonData).each(function () {
                    $("<option />", {
                        val: this.Index,
                        text: this.Value
                    }).appendTo(militaryRankDropdown);
                });
                // Enable rank drop down
                $("#MilitaryServiceAndRank_MilitaryRankId").prop("disabled", false);
                // Disable status drop down
                $("#MilitaryStatus_MilitaryStatusTypeId").prop("disabled", true);
            }).fail(function (xhr, ajaxOptions, thrownError) {
                alert("Sorry, there was a problem processing your request.");
            });
        } else {
            // Disable rank and service
            $("#MilitaryServiceAndRank_MilitaryRankId").prop("disabled", true);
            $("#MilitaryStatus_MilitaryStatusTypeId").prop("disabled", true);
        }
        // Reset values
        $("#MilitaryServiceAndRank_MilitaryRankId").val("");
        $("#MilitaryStatus_MilitaryStatusTypeId").val("");
    });

    // Enable/disable status if rank is changed
    $("#MilitaryServiceAndRank_MilitaryRankId").change(function () {
        $("#MilitaryStatus_MilitaryStatusTypeId").val("");
        if ($("#MilitaryServiceAndRank_MilitaryRankId").val() !== "") {
            $("#MilitaryStatus_MilitaryStatusTypeId").prop("disabled", false);
        } else {
            $("#MilitaryStatus_MilitaryStatusTypeId").prop("disabled", true);
        }
    });
});