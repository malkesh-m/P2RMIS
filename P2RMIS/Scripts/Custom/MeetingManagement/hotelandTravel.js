// Back to Meeting Management page
$('#backtoStaff').on('click', function () {
    window.location.href = "/MeetingManagement/Index";
});

$('#btn-clear-meetings').on('click',
    function() {
        clearDropdowns();
    });

$(document).keypress(function (event) {
    
    var keycode = event.keyCode || event.which;
    if (keycode == '13') {
        var inputIn = $('#reviewerSearch').is(':focus');
        if (inputIn) {
            $('#btn-find-meetings').click();
        }
    }
});

// Initialize the saved variables
var getSavedFiscalYear = sessionStorage.getItem('savedFiscalYearId'),
    getSavedProgramYearId = sessionStorage.getItem('savedProgramYearId'),
    getSavedMeetingId = sessionStorage.getItem('savedMeetingId'),
    getSavedSessionId = sessionStorage.getItem('savedSessionId'),
    getSavedPanelId = sessionStorage.getItem('savedPanelId'),
    getReviewerName = sessionStorage.getItem('reviewerName'),
    getProgramDropdown = sessionStorage.getItem('savedProgramDropdown'),
    getMeetingDropdown = sessionStorage.getItem('savedMeetingDropdown'),
    getSessionDropdown = sessionStorage.getItem('savedSessionDropdown'),
    getPanelDropdown = sessionStorage.getItem('savedPanelDropdown');

var nonReviewerOnly = $("#nonReviewerOnly").val(); 



// Page on load set select dropdowns
$(document).ready(function () {
    $('#successMessage').html("").hide();
    var path = window.location.pathname;
    if (path.indexOf('Index') >= 0 || path.indexOf('NonReviewerAttendees') >= 0) {
        // Go through each saved variable to see what is saved and what isn't
        if (getReviewerName !== null && getReviewerName !== 'undefined' && getReviewerName !== "") {
            $("#reviewerSearch").val(getReviewerName);
        }
        if (getSavedFiscalYear !== null && getSavedFiscalYear !== 'undefined' && getSavedFiscalYear !== "") {
            $('#fiscalYearId').val(getSavedFiscalYear);
        }
        // Program and Meeting
        if (getProgramDropdown !== null && getProgramDropdown !== 'undefined' && getProgramDropdown !== "") {
            $('#programYearId').replaceWith(getProgramDropdown);
            if (getSavedProgramYearId != "") {
                $('#programYearId').val(getSavedProgramYearId);
            }
        }
        if (getMeetingDropdown !== null && getMeetingDropdown !== 'undefined' && getMeetingDropdown !== "") {
            $('#meetingId').replaceWith(getMeetingDropdown);
            if (getSavedMeetingId != "") {
                $('#meetingId').val(getSavedMeetingId);
            }
        }
        if (getSessionDropdown !== null && getSessionDropdown !== 'undefined' && getSessionDropdown !== "") {
            $('#sessionId').replaceWith(getSessionDropdown);
            if (getSavedSessionId != "") {
                $('#sessionId').val(getSavedSessionId);
            }
        }
        if (getPanelDropdown !== null && getPanelDropdown !== 'undefined' && getPanelDropdown !== "") {
            $('#panelId').replaceWith(getPanelDropdown);
            if (getSavedPanelId != "") {
                $('#panelId').val(getSavedPanelId);
            }
        }
        if (checkForEnoughSearchParams().indexOf(true) >= 0) {
            setTimeout(function () {
                    $('#btn-find-meetings').attr('disabled', false);
                    $('#btn-clear-meetings').attr('disabled', false);
                    $('#btn-find-meetings').click();
                },
                500
            );
        }
    }
});

// Fiscal year drop-down change event handler
$(document).on("change", "#fiscalYearId", function () {
    var setYear = $('#fiscalYearId').val();
    if (setYear != "") {
        fiscalYearChange();
        $('#btn-clear-meetings').attr('disabled', false);
    } else {
        $('#programYearId, #meetingId, #sessionId, #panelId').attr('disabled', 'disabled').find("option:gt(0)").remove();
        if ($.trim($("#reviewerSearch").val()) == "") {
            $('#btn-find-meetings').attr('disabled', 'disabled');
        }
        $('#btn-clear-meetings').attr('disabled', 'disabled');
    }
});
function fiscalYearChange() {
    var fiscalYear = $("#fiscalYearId").val();
    $('#programYearId, #meetingId, #sessionId, #panelId').attr('disabled', 'disabled').find("option:gt(0)").remove();
    if ($("#reviewerSearch").val() == "") {
        $('#btn-find-meetings').attr('disabled', 'disabled');
    }
    $.ajax({
        url: '/MeetingManagement/GetProgramYearsJson',
        data: {
            fiscalYear: fiscalYear
        }
    }).done(function (results) {
        $("#programYearId").find("option:gt(0)").remove();
        $('#programYearId').attr('disabled', false);
        $.each(results, function (i, item) {
            $("#programYearId").append($("<option/>")
                .attr("value", item.Index).html(item.Value));
        });

        $('#btn-clear-meetings').attr('disabled', false);

    });
    $.ajax({
        url: '/MeetingManagement/GetMeetingsFromYearJson',
        data: {
            fiscalYear: fiscalYear,
            onSiteOnly:
                (nonReviewerOnly == 'True')
                    ? false
                    : true // if non reviewer only page, do not restrict meetings to on site.
        }
    }).done(function(results) {
        $("#meetingId").find("option:gt(0)").remove();
        $('#meetingId').attr('disabled', false);
        $.each(results,
            function(i, item) {
                $("#meetingId").append($("<option/>")
                    .attr("value", item.Index).html(item.Value));
            });
    });
}
// Program year drop-down change event handler
$(document).on("change", "#programYearId", function () {
    var setProgram = $('#programYearId').val();

    if (checkForEnoughSearchParams().indexOf(true) >= 0) {
        $('#btn-find-meetings').attr('disabled', false);
    } else {
        $('#btn-find-meetings').attr('disabled', true);
    }
    if (setProgram != "") {
        $('#sessionId, #panelId').attr('disabled', false).find("option:gt(0)").remove();
        programChange();
    } else {
        $('#meetingId, #sessionId, #panelId').attr('disabled', 'disabled').find("option:gt(0)").remove();
        var fiscalYear = $("#fiscalYearId").val();
        $.ajax({
            url: '/MeetingManagement/GetMeetingsFromYearJson',
            data: {
                fiscalYear: fiscalYear,
                onSiteOnly:
                    (nonReviewerOnly == 'True')
                        ? false
                        : true // if non reviewer only page, do not restrict meetings to on site.
            }
        }).done(function(results) {
            $("#meetingId").find("option:gt(0)").remove();
            $('#meetingId').attr('disabled', false);
            $.each(results,
                function(i, item) {
                    $("#meetingId").append($("<option/>")
                        .attr("value", item.Index).html(item.Value));
                });
        });
        var clientMeetingId = $("#meetingId").val();
        $.ajax({
            url: "/MeetingManagement/GetSessionsByMeetingProgramJson",
            data: { clientMeetingId: clientMeetingId, programYearId: programYearId }
        }).done(function(results) {
            $("#sessionId").find("option:gt(0)").remove();
            $('#sessionId').attr('disabled', false);
            $.each(results,
                function(i, item) {
                    $("#sessionId").append($("<option/>")
                        .attr("value", item.Key).html(item.Value));
                });

            $.ajax({
                url: "/MeetingManagement/GetPanelsByMeetingProgramJson",
                data: { clientMeetingId: clientMeetingId, programYearId: programYearId }
            }).done(function(newResults) {
                $("#panelId").find("option:gt(0)").remove();
                $('#panelId').attr('disabled', false);
                $.each(newResults,
                    function(i, item) {
                        $("#panelId").append($("<option/>")
                            .attr("value", item.Key).html(item.Value));
                    });
            });
        });
    } 
});

function programChange() {
    var programYearId = $("#programYearId").val();
    var clientMeetingId = $("#meetingId").val();
    if (programYearId != "") {
        $.ajax({
            url: '/MeetingManagement/GetMeetings',
            data: {
                programYearId: programYearId
            }
        }).done(function (results) {
            $("#meetingId").find("option:gt(0)").remove();
            $('#meetingId').attr('disabled', false);
            $.each(results, function (i, item) {
                $("#meetingId").append($("<option/>")
                    .attr("value", item.Index).html(item.Value));
            });
            if (clientMeetingId != "") {
                $("#meetingId").val(clientMeetingId);
            }
            $('#btn-find-meetings').attr('disabled', false);
            $.ajax({
                    url: "/MeetingManagement/GetSessionsByMeetingProgramJson",
                    data: { clientMeetingId: clientMeetingId, programYearId: programYearId }
                }).done(function(results) {
                    $("#sessionId").find("option:gt(0)").remove();
                    $('#sessionId').attr('disabled', false);
                    $.each(results,
                        function(i, item) {
                            $("#sessionId").append($("<option/>")
                                .attr("value", item.Key).html(item.Value));
                        });
                    $.ajax({
                        url: "/MeetingManagement/GetPanelsByMeetingProgramJson",
                        data: { clientMeetingId: clientMeetingId, programYearId: programYearId }
                    }).done(function(newResults) {
                        $("#panelId").find("option:gt(0)").remove();
                        $('#panelId').attr('disabled', false);
                        $.each(newResults,
                            function(i, item) {
                                $("#panelId").append($("<option/>")
                                    .attr("value", item.Key).html(item.Value));
                            });
                    });
                });
        });
        $.ajax({
            url: "/MeetingManagement/GetSessionsByMeetingProgramJson",
            data: { clientMeetingId: clientMeetingId, programYearId: programYearId }
        }).done(function(results) {
            $("#sessionId").find("option:gt(0)").remove();
            $('#sessionId').attr('disabled', false);
            $.each(results,
                function(i, item) {
                    $("#sessionId").append($("<option/>")
                        .attr("value", item.Key).html(item.Value));
                });
            $.ajax({
                url: "/MeetingManagement/GetPanelsByMeetingProgramJson",
                data: { clientMeetingId: clientMeetingId, programYearId: programYearId }
            }).done(function(newResults) {
                $("#panelId").find("option:gt(0)").remove();
                $('#panelId').attr('disabled', false);
                $.each(newResults,
                    function(i, item) {
                        $("#panelId").append($("<option/>")
                            .attr("value", item.Key).html(item.Value));
                    });
            });
        });
    } else {
        var fiscalYear = $('#fiscalYearId').val();
        $.ajax({
            url: '/MeetingManagement/GetMeetingsFromYearJson',
            data: {
                fiscalYear: fiscalYear,
                onSiteOnly:
                    (nonReviewerOnly == 'True')
                        ? false
                        : true // if non reviewer only page, do not restrict meetings to on site.
            }
        }).done(function(results) {
            $("#meetingId").find("option:gt(0)").remove();
            $('#meetingId').attr('disabled', false);
            $.each(results,
                function(i, item) {
                    $("#meetingId").append($("<option/>")
                        .attr("value", item.Index).html(item.Value));
                });
            var getId = $('#meetingId').val();
            clientMeetingId = (getSavedMeetingId != "") ? getSavedMeetingId : getId;
            $.ajax({
                url: "/MeetingManagement/GetSessionsByMeetingProgramJson",
                data: { clientMeetingId: clientMeetingId, programYearId: programYearId }
            }).done(function(results) {
                $("#sessionId").find("option:gt(0)").remove();
                $('#sessionId').attr('disabled', false);
                $.each(results,
                    function(i, item) {
                        $("#sessionId").append($("<option/>")
                            .attr("value", item.Key).html(item.Value));
                    });
                $.ajax({
                    url: "/MeetingManagement/GetPanelsByMeetingProgramJson",
                    data: { clientMeetingId: clientMeetingId, programYearId: programYearId }
                }).done(function(newResults) {
                    $("#panelId").find("option:gt(0)").remove();
                    $('#panelId').attr('disabled', false);
                    $.each(newResults,
                        function(i, item) {
                            $("#panelId").append($("<option/>")
                                .attr("value", item.Key).html(item.Value));
                        });
                });
            });
        });
    }
}
// Meeting drop-down change event handler
$(document).on("change", '#meetingId', function () {
    var setMeeting = $('#meetingId').val();
    var getProgram = $('#programYearId').val();
    var fiscalYear = $('#fiscalYearId').val();
    if (checkForEnoughSearchParams().indexOf(true) >= 0) {
        $('#btn-find-meetings').attr('disabled', false);
    } else {
        $('#btn-find-meetings').attr('disabled', true);
    }
    if (setMeeting != "") {
        meetingChange();
    } else if (getProgram != "") {
        $('#sessionId, #panelId').find("option:gt(0)").remove();
        meetingChange();
        sessionChange();        
    } else {
        $.ajax({
            url: '/MeetingManagement/GetProgramYearsJson',
            data: {
                fiscalYear: fiscalYear
            }
        }).done(function (results) {
            $("#programYearId").find("option:gt(0)").remove();
            $('#programYearId').attr('disabled', false);
            $.each(results, function (i, item) {
                $("#programYearId").append($("<option/>")
                    .attr("value", item.Index).html(item.Value));
            });
        });
        $('#sessionId, #panelId').attr('disabled', 'disabled').find("option:gt(0)").remove();
    }
});
function meetingChange() {
    var clientMeetingId = $("#meetingId").val();
    var programYearId = $('#programYearId').val();
    if (clientMeetingId != "") {
        $('#sessionId, #panelId').attr('disabled', 'disabled').find("option:gt(0)").remove();
            $.ajax({
            url: '/MeetingManagement/GetProgramsByMeeting',
            data: {
                clientMeetingId: clientMeetingId
            }
        }).done(function (results) {
            $("#programYearId").find("option:gt(0)").remove();
            $('#programYearId').attr('disabled', false);
            $.each(results, function (i, item) {
                $("#programYearId").append($("<option/>")
                    .attr("value", item.Index).html(item.Value));
            });
            if (programYearId != "") {
                $("#programYearId").val(programYearId);
            }
            $.ajax({
                url: '/MeetingManagement/GetSessionsByMeetingProgramJson',
                data: {
                    clientMeetingId: clientMeetingId, programYearId: programYearId
                }
            }).done(function (results) {
                $("#sessionId").find("option:gt(0)").remove();
                $('#sessionId').attr('disabled', false);
                $.each(results, function (i, item) {
                    $("#sessionId").append($("<option/>")
                        .attr("value", item.Key).html(item.Value));
                });
                $.ajax({
                    url: "/MeetingManagement/GetPanelsByMeetingProgramJson",
                    data: { clientMeetingId: clientMeetingId, programYearId: programYearId }
                }).done(function(newResults) {
                    $("#panelId").find("option:gt(0)").remove();
                    $('#panelId').attr('disabled', false);
                    $.each(newResults,
                        function(i, item) {
                            $("#panelId").append($("<option/>")
                                .attr("value", item.Key).html(item.Value));
                        });
                });
            });
            $.ajax({
                url: '/MeetingManagement/GetSessionsJson',
                data: {
                    clientMeetingId: clientMeetingId, programYearId: programYearId
                }
            }).done(function (results) {
                $("#sessionId").find("option:gt(0)").remove();
                $('#sessionId').attr('disabled', false);
                $.each(results, function (i, item) {
                    $("#sessionId").append($("<option/>")
                        .attr("value", item.Key).html(item.Value));
                });
                $.ajax({
                    url: "/MeetingManagement/GetPanelsByMeetingProgramJson",
                    data: { clientMeetingId: clientMeetingId, programYearId: programYearId }
                }).done(function(newResults) {
                    $("#panelId").find("option:gt(0)").remove();
                    $('#panelId').attr('disabled', false);
                    $.each(newResults,
                        function(i, item) {
                            $("#panelId").append($("<option/>")
                                .attr("value", item.Key).html(item.Value));
                        });
                });
            });
        });
    } else {
        $.ajax({
            url: "/MeetingManagement/GetSessionsByMeetingProgramJson",
            data: { clientMeetingId: clientMeetingId, programYearId: programYearId }
        }).done(function(results) {
            $("#sessionId").find("option:gt(0)").remove();
            $('#sessionId').attr('disabled', false);
            $.each(results,
                function(i, item) {
                    $("#sessionId").append($("<option/>")
                        .attr("value", item.Key).html(item.Value));
                });

            $.ajax({
                url: "/MeetingManagement/GetPanelsByMeetingProgramJson",
                data: { clientMeetingId: clientMeetingId, programYearId: programYearId }
            }).done(function(newResults) {
                $("#panelId").find("option:gt(0)").remove();
                $('#panelId').attr('disabled', false);
                $.each(newResults,
                    function(i, item) {
                        $("#panelId").append($("<option/>")
                            .attr("value", item.Key).html(item.Value));
                    });
            });
        });
    }
}

// Session drop-down change event handler
$(document).on("change", "#sessionId", function () {
    var setSession = $('#sessionId').val();
    if (setSession) {
        sessionChange();
    } else {
        $('#panelId').find("option:gt(0)").remove();
        var clientMeetingId = $("#meetingId").val();
        var programYearId = $('#programYearId').val();
        $.ajax({
            url: "/MeetingManagement/GetPanelsByMeetingProgramJson",
            data: { clientMeetingId: clientMeetingId, programYearId: programYearId }
        }).done(function(newResults) {
            $("#panelId").find("option:gt(0)").remove();
            $('#panelId').attr('disabled', false);
            $.each(newResults,
                function(i, item) {
                    $("#panelId").append($("<option/>")
                        .attr("value", item.Key).html(item.Value));
                });
        });
    }
});

// Retrieve panel information for the panelId dropdown upon user selecting a session
function sessionChange() {
    var clientSessionId = $("#sessionId").val();

    if (clientSessionId != "") {
        $('#panelId').attr('disabled', 'disabled').find("option:gt(0)").remove();
        var programYearId = $("#programYearId").val();
        $.ajax({
            url: '/MeetingManagement/GetPanelsJson',
            data: {
                sessionId: clientSessionId, programYearId: (programYearId > 0) ? programYearId : null
            }
        }).done(function (results) {
            $("#panelId").find("option:gt(0)").remove();
            $('#panelId').attr('disabled', false);
            $.each(results, function (i, item) {
                $("#panelId").append($("<option/>")
                    .attr("value", item.Key).html(item.Value));
            });
        });
    }
}

// returns an array of bool showing whether contents of these minimum required parameters have a value
function checkForEnoughSearchParams() {
    var checkNameMeetingProgram = [$.trim($('#reviewerSearch').val()).length > 1, $("#meetingId").val() != "", $("#programYearId").val() != ""];
    return checkNameMeetingProgram;
}

// Reviewer Search
// either namesearch must have at least 2 characters, or meeting or program must have a value, to enable search button
$("#reviewerSearch").on('keydown', function () {
    setTimeout(function () {
        if (checkForEnoughSearchParams().indexOf(true) >= 0) {
            $('#btn-find-meetings, #btn-clear-meetings').attr('disabled', false);
        } else {
            $('#btn-find-meetings, #btn-clear-meetings').attr('disabled', true);
        }
    }, 500);
})

// ensure that reviewer search, meeting, and program are not all blank, otherwise disable the search button.
$("#reviewerSearch").focusout(function (e) {
    if (checkForEnoughSearchParams().indexOf(true) >= 0) {
        $("#btn-find-meetings").attr('disabled', false);
    } else {
        $("#btn-find-meetings").attr('disabled', true);
    }
});

$(function () {
    // Popultate Meeting data source for grid
    function populateMeetingDataSource(dataJson) {
        var dataSource = new kendo.data.DataSource({
            type: "json",
            data: dataJson,
            batch: true,
            serverPaging: true,
            schema: {
                model: {
                    id: "clientProgramId",
                    fields: {
                        lastName: { type: "string" },
                        firstName: { type: "string" },
                        partInfo: { type: "string" },
                        program: { type: "string" },
                        fiscalYear: { type: "string" },
                        panel: { type: "string" },
                        meeting: { type: "string" },
                        session: { type: "string" },
                        hotelUpdated: { type: "string" },
                        travelUpdated: { type: "string" },
                        comments: { type: "string" },
                        meetingRegistrationId: { type: "number" },
                        panelUserAssignmentId: { type: "number" },
                        sessionUserAssignmentId: { type: "number" },
                        participantType: {type: "string"},
                        action: { type: "command" }
                    }
                },
            },
        });
        return dataSource;
    }
    // Set up kendo grid
    $("#mm-main-grid").kendoGrid({
        pageable: false,
        height: 550,
        resizable: true,
        toolbar: ["create"],
        columns: [
            { field: "lastName", filterable: true, title: "Last<br> Name", template: "<div>${lastName}</div>" },
            { field: "firstName", filterable: true, title: "First<br> Name", width: "95px" },
            { field: "participantType", filterable: {multi: true}, title: "Part Type", width: "110px"},
            { field: "partInfo", filterable: { multi: true }, title: "Part<br> Info" },
            { field: "program", filterable: false, title: "Program" },
            { field: "fiscalYear", filterable: { multi: true }, title: "FY", width: "75px" },
            { field: "panel", filterable: { multi: true }, title: "Panel" },
            { field: "meeting", filterable: { multi: true }, title: "Meeting" },
            { field: "session", filterable: { multi: true }, title: "Session" },
            { field: "hotelUpdated", filterable: false, title: "Hotel<br> Updated", width: "85px" },
            { field: "travelUpdated", filterable: false, title: "Travel<br> Updated", width: "85px" },
            { field: "meetingRegistrationId", hidden: true },
            { field: "panelUserAssignmentId", hidden: true },
            { field: "sessionUserAssignmentId", hidden: true },
            {
                command: [
                    {
                        name: "editable",
                        text: "",
                        template: "<a class='k-button k-button-icontext k-grid-editable' href=''><img src='/Content/img/edit_20px_enabled.png' alt='Edit' title='Edit'/></a>",
                        click: function (e) {  //add a click event listener on the button
                            e.preventDefault();
                            e.stopPropagation();
                            row = $(e.currentTarget).closest("tr"),
                            grid = $("#mm-main-grid").data("kendoGrid"),
                            dataItem = grid.dataItem(row);

                            meetingRegistrationId = dataItem.meetingRegistrationId,
                            panelUserAssignmentId = dataItem.panelUserAssignmentId;
                            sessionUserAssignmentId = dataItem.sessionUserAssignmentId;

                            sessionStorage.setItem('setMeetingRegistrationId', meetingRegistrationId);
                            sessionStorage.setItem('setPanelUserAssignmentId', panelUserAssignmentId);
                            sessionStorage.setItem('setSessionUserAssignmentId', sessionUserAssignmentId);

                            if (panelUserAssignmentId != null) {
                                window.location.href = "/MeetingManagement/EditHotel?panelUserAssignmentId=" + panelUserAssignmentId;
                            } else {
                                window.location.href = "/MeetingManagement/EditHotel?sessionUserAssignmentId=" + sessionUserAssignmentId;
                            }

                        }
                    },
                ], title: "Action", width: "75px",
            }],
        editable: { mode: "popup" },
        scrollable: true,
        groupable: true,
        sortable: true,
        dataBound: function (e) {
            kendoUnload()
        },
        filterable: {
            extra: false,
            operators: {
                string: {
                    startswith: "Starts with",
                    eq: "Is equal to",
                    neq: "Is not equal to"
                }
            }
        },
        filterMenuInit: function (e) {
            if (e.field === "program" || e.field === "panel" || e.field === "meeting" || e.field === "session" || e.field === "lastName" || e.field === "firstName") {
                var filterMultiCheck = this.thead.find("[data-field=" + e.field + "]").data("kendoFilterMultiCheck")
                filterMultiCheck.container.empty();
                filterMultiCheck.checkSource.sort({ field: e.field, dir: "asc" });

                filterMultiCheck.checkSource.data(filterMultiCheck.checkSource.view().toJSON());
                filterMultiCheck.createCheckBoxes();
            }
            if (e.field === "fiscalYear") {
                var filterMultiCheck = this.thead.find("[data-field=" + e.field + "]").data("kendoFilterMultiCheck")
                filterMultiCheck.container.empty();
                filterMultiCheck.checkSource.sort({ field: e.field, dir: "desc" });

                filterMultiCheck.checkSource.data(filterMultiCheck.checkSource.view().toJSON());
                filterMultiCheck.createCheckBoxes();
            }
        },
    });
    // Populate result header
    function populateResultHeader(count) {
        var reviewerName = $.trim($("#reviewerSearch").val());
        var fiscalYear = $('#fiscalYearId').val();
        var programYearId = $('#programYearId').val();
        var dis = reviewerName != "" ? "\"" + reviewerName + "\"" : "";
        if (fiscalYear != "0" && programYearId != "") {
            if (dis != "") {
                dis += " - ";
            }
            if (fiscalYear != "") {
                dis += fiscalYear;
                if (programYearId != "") {
                    dis += " " + $('#programYearId > option[value=' + programYearId + ']').text();
                }
            } else {
                dis +=  $('#programYearId > option[value=' + programYearId + ']').text();
            }            
        }
        $("#searchCriteria").html(dis);
        $("#recordCount").html(count == 1 ? "( 1 record )" : "(" + count + " records )" + ((count > 100) ? "<span class='redColor'> Your search has returned over 100 records. Please refine your search.</span>" : ""));
    };
    // Button Search
    $('#btn-find-meetings').on('click', function () {
        var reviewerName = $.trim($("#reviewerSearch").val()),
            fiscalYear = ($('#fiscalYearId').val() == "0") ? "" : $('#fiscalYearId').val(),
            programYearId = $('#programYearId').val(),
            meetingId = $('#meetingId').val(),
            sessionId = $('#sessionId').val(),
            panelId = $('#panelId').val(),
            programDropdown = $('#programYearId').clone().val($('#programYearId').val()).wrap('<p>').parent().html(),
            meetingDropdown = $('#meetingId').clone().wrap('<p>').parent().html(),
            sessionDropdown = $('#sessionId').clone().wrap('<p>').parent().html(),
            panelDropdown = $('#panelId').clone().wrap('<p>').parent().html();

                // Select dropdowns
        sessionStorage.setItem('savedFiscalYearId', fiscalYear);
        sessionStorage.setItem('savedProgramYearId', programYearId);
        sessionStorage.setItem('savedMeetingId', meetingId);
        sessionStorage.setItem('savedSessionId', sessionId);
        sessionStorage.setItem('savedPanelId', panelId);
        sessionStorage.setItem('reviewerName', reviewerName);
        sessionStorage.setItem('savedProgramDropdown', programDropdown);
        sessionStorage.setItem('savedMeetingDropdown', meetingDropdown);
        sessionStorage.setItem('savedSessionDropdown', sessionDropdown);
        sessionStorage.setItem('savedPanelDropdown', panelDropdown);
        // Parse first name and last name out
        var firstName, lastName;
        var nameParts = reviewerName.split(',');
        if (nameParts.length <= 1) {
            lastName = $.trim(nameParts[0]);
        } else {
            lastName = $.trim(nameParts[0]);
            firstName = $.trim(nameParts[1]);
        }
        $('#btn-clear-meetings').attr('disabled', false);
        var url = (nonReviewerOnly == 'True') ? '/MeetingManagement/GetNonReviewerRegistrationAttendanceJsonString' : '/MeetingManagement/GetRegistrationAttendanceJsonString';

        // Get attendance list for the grid
        kendoLoad();
        $.ajax({
            url: url,
            data: {
                firstName: firstName,
                lastName: lastName,
                fiscalYear: fiscalYear,
                programYearId: programYearId,
                meetingId: meetingId,
                sessionId: sessionId,
                panelId: panelId,
            }
        }).done(function (results) {
            // If Hotel & Travel then show this grid else show Reviewer Grid
            var getTab = $('.nav-tabs .active a').text();
            if (results.length > 2) {
                var ds = (getTab == "Non-Reviewer Attendees") ? populateReviewerDataSource(JSON.parse(results)) : populateMeetingDataSource(JSON.parse(results));
                var grid = (getTab == "Non-Reviewer Attendees") ? $("#mm-reviewer-grid").data("kendoGrid") : $("#mm-main-grid").data("kendoGrid");
                ds.read();
                grid.dataSource.data(ds.data());
                grid.setDataSource(grid.dataSource);
                // Total data in Grid
                var sourcedata = ds.data();
                $('#totalCount span').text(sourcedata.length);
                $('.mm-main').removeClass('displayNone');
                $('#noResults').addClass('displayNone');
                (getTab == "Non-Reviewer Attendees") ? $(".mm-main, #mm-reviewer-grid").show() : $(".mm-main, #mm-main-grid").show();
                $(".mm-main_title").addClass('displayInline');
                $('.mm-main_text').addClass('displayNone');
                $('#main-text').hide();
                populateResultHeader(sourcedata.length);
            } else {
                $('#noResults').removeClass('displayNone');
                $('.mm-main').addClass('displayNone');
                var element = $('#loading');
                kendo.ui.progress(element, false);
                (getTab == "Non-Reviewer Attendees") ? $('#mm-reviewer-grid').hide() : $('#mm-main-grid').hide();
                $('#main-text').show();
            }
        });
    });
    // Popultate Meeting data source for grid
    function populateReviewerDataSource(dataJson) {
        var dataSource = new kendo.data.DataSource({
            type: "json",
            data: dataJson,
            batch: true,
            serverPaging: true,
            schema: {
                model: {
                    id: "clientProgramId",
                    fields: {
                        ReviewerUserId: {type: "number"},
                        Name: { type: "string" },
                        Organization: { type: "string" },
                        Email: { type: "string" },
                        Role: { type: "string" },
                        meetingRegistrationId: { type: "number" },
                        panelUserAssignmentId: { type: "number" },
                        action: { type: "command" }
                    }
                },
            },
        });
        return dataSource;
    }
    $("#mm-reviewer-grid").kendoGrid({
        pageable: false,
        height: 550,
        resizable: true,
        toolbar: ["create"],
        columns: [
            { field: "ReviewerUserId", hidden: true },
            { field: "Name", filterable: true, title: "Name", template: "<div>${lastName}, ${firstName}</div>" },
            { field: "Organization", filterable: true, title: "Organization" },
            { field: "Email", filterable: true, title: "Email" },
            { field: "Role", filterable: true, title: "Role" },
            { field: "meetingRegistrationId", hidden: true },
            { field: "panelUserAssignmentId", hidden: true },
            {
                command: [
                    {
                        name: "editable",
                        text: "",
                        template: "<a class='k-button k-button-icontext k-grid-editable' href=''><img src='/Content/img/edit_20px_enabled.png' alt='Update Assignment' title='Update Assignment'/></a>",
                        click: function (e) {  //add a click event listener on the button
                            e.preventDefault();
                            e.stopPropagation();
                            var grid = $("#mm-reviewer-grid").data("kendoGrid");
                            var gridData = grid.dataSource.view();
                            var i = $(e.target).closest("tr").index();
                            var getName = gridData[i].firstName + ' ' + gridData[i].lastName;
                            var getUserId = gridData[i].ReviewerUserId;

                            var title = "Update Assignment";
                            $.get("/MeetingManagement/UpdateAssignment", {},
                                function (data) {
                                    p2rims.modalFramework.displayModalNoEvent(data, p2rims.modalFramework.customModalSizes.medium, title);
                                    $('#attendeeName').text(getName); 
                                    $("#ReviewerUserId").val(getUserId);
                                    
                                    // Populate fiscal year select on modal
                                    $("#fiscalYearId > option").each(function () {
                                        $("#per-managementmodal-fy").append(new Option(this.text, this.value));
                                    });
                                })
                        }
                    },
                ], title: "Action", width: "75px",
            }],
        editable: { mode: "popup" },
        scrollable: true,
        groupable: true,
        sortable: true,
        dataBound: function (e) {
            kendoUnload()
        },
        filterable: {
            extra: false,
            operators: {
                string: {
                    startswith: "Starts with",
                    eq: "Is equal to",
                    neq: "Is not equal to"
                }
            }
        },
        filterMenuInit: function (e) {
            if (e.field === "program" || e.field === "panel" || e.field === "meeting" || e.field === "session" || e.field === "lastName" || e.field === "firstName") {
                var filterMultiCheck = this.thead.find("[data-field=" + e.field + "]").data("kendoFilterMultiCheck")
                filterMultiCheck.container.empty();
                filterMultiCheck.checkSource.sort({ field: e.field, dir: "asc" });

                filterMultiCheck.checkSource.data(filterMultiCheck.checkSource.view().toJSON());
                filterMultiCheck.createCheckBoxes();
            }
            if (e.field === "fiscalYear") {
                var filterMultiCheck = this.thead.find("[data-field=" + e.field + "]").data("kendoFilterMultiCheck");
                filterMultiCheck.container.empty();
                filterMultiCheck.checkSource.sort({ field: e.field, dir: "desc" });

                filterMultiCheck.checkSource.data(filterMultiCheck.checkSource.view().toJSON());
                filterMultiCheck.createCheckBoxes();
            }
        },
    });
})
window.kendoLoad = function () {
    var element = $('#loading');
    kendo.ui.progress(element, true);
};

// End of Progress Bar
window.kendoUnload = function () {
    var element = $('#loading');
    kendo.ui.progress(element, false);
};

// Updates dropdown selects based on active checkbox
$('#endDateCheckbox').on('click',
    function() {
        clearDropdowns();
    });

function clearDropdowns() {
    $('#reviewerSearch').val('');
    $('#programYearId, #meetingId, #sessionId, #panelId').attr('disabled', 'disabled').find("option:gt(0)").remove();
    $('#btn-clear-meetings, #btn-find-meetings').attr('disabled', 'disabled');
    $('#main-text').show();
    $('.mm-main, #mm-main-grid, #mm-reviewer-grid').hide();
    // Clear saved variables
    sessionStorage.setItem('savedFiscalYearId', "");
    sessionStorage.setItem('savedProgramYearId', "");
    sessionStorage.setItem('savedMeetingId', "");
    sessionStorage.setItem('savedSessionId', "");
    sessionStorage.setItem('savedPanelId', "");
    sessionStorage.setItem('reviewerName', "");
    sessionStorage.setItem('savedProgramDropdown', "");
    sessionStorage.setItem('savedMeetingDropdown', "");
    sessionStorage.setItem('savedSessionDropdown', "");
    sessionStorage.setItem('savedPanelDropdown', "");
    $('#fiscalYearId').val('');
}