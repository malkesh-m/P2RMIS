$(document).ready(function () {
    $("#program-dropdown, #meeting-dropdown").attr("disabled", true);
    CreateMultiSelectWidget();
    $("#successMessage").html("").hide();
    $("#failureMessage").html("").hide();
});


function CreateMultiSelectWidget() {
    $('#unassignedSessions').multiselect2side({
        selectedPosition: 'right',
        moveOptions: false,
        labelsx: '(0) Unassigned Session(s)',
        labeldx: '(0) Assigned Session(s)',
        autoSort: true,
        autoSortAvailable: true
    });
    
    $('#unassignedSessions').closest('ul').addClass('ulNone');
}

$("#per-managementmodal-fy").on("change", function (e) {
    var fiscalYear = $("#per-managementmodal-fy").val();
    var userId = $("#ReviewerUserId").val();
    if (fiscalYear != "Select Year") {
        $.ajax({
            url: '/MeetingManagement/GetProgramYearsJson',
            data: {
                fiscalYear: fiscalYear
            }
        }).done(function (results) {

            $("#program-dropdown").find("option:gt(0)").remove();
            $("#program-dropdown").attr("disabled", false);
            $.each(results, function (i, item) {
                $("#program-dropdown").append($("<option/>")
                    .attr("value", item.Index).html(item.Value));
            });
            $("#meeting-dropdown").find("option:gt(0)").remove()
            $("#meeting-dropdown").attr("disabled", true);
            $("#unassignedSessions").empty().multiselect2side('destroy');
            $('#unassignedSessions').multiselect2side({
                labelsx: '(0) Unassigned Session(s)',
                labeldx: '(0) Assigned Session(s)'
            });
            $(".ms2side__updown").hide();

        });
    } else {
        $("#program-dropdown").attr("disabled", true);
    }
    $("#nonRevAssignmentSave").attr("disabled", true);
});

$("#program-dropdown").on("change", function (e) {

    var thisProgram = $("#program-dropdown").val();
    if (thisProgram != "Select Program") {
        $.ajax({
            url: '/MeetingManagement/GetMeetings',
            data: {
                programYearId: thisProgram
            }
        }).done(function (results) {
            $("#meeting-dropdown").attr("disabled", false);
            $("#meeting-dropdown").find("option:gt(0)").remove();
            $.each(results, function (i, item) {
                $("#meeting-dropdown").append($("<option/>")
                    .attr("value", item.Index).html(item.Value));
            });
            $("#unassignedSessions").empty().multiselect2side('destroy');
            $('#unassignedSessions').multiselect2side({
                labelsx: '(0) Unassigned Session(s)',
                labeldx: '(0) Assigned Session(s)'
            });
            $(".ms2side__updown").hide();
        });
    } else {
        $("#meeting-dropdown").attr("disabled", true);
    }
    $("#nonRevAssignmentSave").attr("disabled", true);
});

$("#meeting-dropdown").on("change", function (e) {

    var thisMeeting = $("#meeting-dropdown").val();
    var userId = $("#ReviewerUserId").val();

    if (thisMeeting != "Select Meeting") {
        $.ajax({
            url: '/MeetingManagement/GetSessionsWithAssignmentsJson',
            data: {
                clientMeetingId: thisMeeting, userId: userId
            }
        }).done(function (results) {
            
            $("#unassignedSessions").empty().multiselect2side('destroy');
            $('#unassignedSessions').multiselect2side({
                labelsx: '(0) Unassigned Session(s)',
                labeldx: '(0) Assigned Session(s)'
            });
            $(".ms2side__updown").hide();
            $.each(results.Sessions, function (i, item) {
                var isAssigned = results.Assignments.indexOf(item.Key) >= 0;
                $('#unassignedSessions').multiselect2side('addOption', { name: item.Value, value: item.Key, selected: isAssigned });
            });

            var updatedCounts = UpdateSelectCounts();

            $("#sessionCountLeft").val(updatedCounts[0]);
            $("#sessionCountRight").val(updatedCounts[1]);
        });
    }
});

$(document).on('click', 'button', function () {
    var updatedCounts = UpdateSelectCounts();
    var savedCount = $("#sessionCountRight").val();
    if (updatedCounts[1] != savedCount) {
        $("#nonRevAssignmentSave").attr("disabled", false);
    } else {
        $("#nonRevAssignmentSave").attr("disabled", true);
    }
});

// Updates counts on multiselect2side control
function UpdateSelectCounts() {
    var unselectedCount = $('#unassignedSessionsms2side__sx').children('option').length;
    var selectedCount = $('#unassignedSessionsms2side__dx').children('option').length;
    $("#unassignedSessionsms2side__sx").siblings("div").text('(' + unselectedCount + ') Unassigned Session(s)');
    $("#unassignedSessionsms2side__dx").siblings("div").text('(' + selectedCount + ') Assigned Session(s)');

    return [unselectedCount, selectedCount];
    
}


