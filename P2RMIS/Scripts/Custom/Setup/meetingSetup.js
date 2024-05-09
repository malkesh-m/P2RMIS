// If from another page link
var windowUrl = window.location.href,
    filtered = windowUrl.indexOf('Abbr');

// Initialize
var clientsJson, fiscalYearsJson, programYearsJson;
var activeProgramCheckbox = $('#activeProgramCheckbox'),
    activeMeetingCheckbox = $('#activeMeetingCheckbox'),
    clientSelect = $('.ss-filterBox_meeting-table_clientSelect__select'),
    yearSelect = $('.ss-filterBox_meeting-table_yearSelect__select'),
    programSelect = $('.ss-filterBox_meeting-table_programSelect__select');

// Get clients
function getClients() {
    $.ajax({
        url: '/Setup/GetClientsJson'
    }).done(function (results) {
        clientsJson = results;        
        var activeOnly = activeProgramCheckbox.prop("checked");
        setClients(activeOnly);
    });
}
// Set clients drop-down menu
function setClients(activeOnly) {
    clientSelect.find("option:gt(0)").remove();
    yearSelect.find("option:gt(0)").remove();
    programSelect.find("option:gt(0)").remove();
    yearSelect.prop("disabled", true);
    programSelect.prop("disabled", true);
    $.each(clientsJson, function (i, item) {
        if (!activeOnly || (activeOnly && item.IsActive)) {
            clientSelect.append($("<option/>")
                .attr("value", item.ClientId).html(item.ClientName));
        }
    });
    // Get from session storage if available
    if (sessionStorage.getItem("SS_ClientId")) {
        var clientId = sessionStorage.getItem("SS_ClientId");
        if (clientSelect.find('option[value=' + clientId + ']').length != 0) {
            clientSelect.val(clientId);
            FillFiscalYear();
            $("#meetingSetupInstructions").hide();
        }
    }
}

function init() {
    if (sessionStorage.getItem("SS_ActiveProgram")) {
        activeProgramCheckbox.prop("checked", $.toBoolean(sessionStorage.getItem("SS_ActiveProgram")));
    } else {
        activeProgramCheckbox.prop("checked", true);
    }
    if (sessionStorage.getItem("SS_ActiveMeeting")) {
        activeMeetingCheckbox.prop("checked", $.toBoolean(sessionStorage.getItem("SS_ActiveMeeting")));
    } else {
        activeMeetingCheckbox.prop("checked", true);
    }
    // Get clients on load
    getClients();
}

// Initialize
init();

// Client drop-down change event handler
clientSelect.on("change", function () {
    if (clientSelect.val() !== "") {
        sessionStorage.setItem("SS_ClientId", clientSelect.val());
        $("#meetingSetupInstructions").hide();
    } else {
        sessionStorage.removeItem("SS_ClientId");
    }
    sessionStorage.removeItem("SS_FiscalYear");
    sessionStorage.removeItem("SS_ProgramYearId");
    yearSelect.find("option:gt(0)").remove();
    programSelect.find("option:gt(0)").remove();
    yearSelect.prop("disabled", true);
    programSelect.prop("disabled", true);
    FillFiscalYear();
});
// Fill fiscal year drop-down menu
function FillFiscalYear() {
    if (clientSelect.val() !== "") {
        $.ajax({
            url: '/Setup/GetFiscalYearsJson',
            data: { clientId: clientSelect.val() }
        }).done(function (results) {
            fiscalYearsJson = results;
            yearSelect.prop("disabled", false);
            var activeOnly = activeProgramCheckbox.prop("checked");
            setFiscalYears(activeOnly);
        });
    }
}
// Set fiscal years
function setFiscalYears(activeOnly) {
    $.each(fiscalYearsJson, function (i, item) {
        if (!activeOnly || (activeOnly && item.IsActive)) {
            yearSelect.append($("<option/>")
                .attr("value", item.YearValue).html(item.YearText));
        }
    });
    // Get from session storage if available
    if (sessionStorage.getItem("SS_FiscalYear")) {
        var fiscalYear = sessionStorage.getItem("SS_FiscalYear");
        if (yearSelect.find('option[value=' + fiscalYear + ']').length != 0) {
            yearSelect.val(fiscalYear);
            FillProgram();
        }
    }
}

// Fiscal year drop-down change event handler
yearSelect.on("change", function () {
    var fy = yearSelect.val();
    if (fy !== "") {
        sessionStorage.setItem("SS_FiscalYear", fy);
    } else {
        sessionStorage.removeItem("SS_FiscalYear");
    }
    sessionStorage.removeItem("SS_ProgramYearId");
    programSelect.find("option:gt(0)").remove();
    programSelect.prop("disabled", true);
    FillProgram();
});
// Fill program drop-down menu
function FillProgram() {
    if (yearSelect.val() != "") {
        $.ajax({
            url: '/Setup/GetProgramYearsJson',
            data: {
                clientId: clientSelect.val(),
                fiscalYear: yearSelect.val()
            }
        }).done(function (results) {
            programYearsJson = results;
            var activeOnly = activeProgramCheckbox.prop("checked");
            setProgramYears(activeOnly);
            programSelect.prop("disabled", false);
        });
    }
}
// Set program years drop-down
function setProgramYears(activeOnly) {
    programSelect.find("option:gt(0)").remove();
    $.each(programYearsJson, function (i, item) {
        if (!activeOnly || (activeOnly && item.IsActive)) {
            programSelect.append($("<option/>")
                .attr("value", item.ProgramYearId).html(item.ProgramAbbr)
                .attr("data-name", item.ProgramName));
        }
    });
    // Get from session storage if available
    if (sessionStorage.getItem("SS_ProgramYearId")) {
        var programYearId = sessionStorage.getItem("SS_ProgramYearId");
        if (programSelect.find('option[value=' + programYearId + ']').length != 0) {
            programSelect.val(programYearId);
            $('.ss-filterBox_meeting-table_buttonShell_buttonPrimary').click();
        }
    }
}

// Active program filter
activeProgramCheckbox.on("change", function () {
    sessionStorage.setItem("SS_ActiveProgram", $("#activeProgramCheckbox").prop("checked"));
    sessionStorage.removeItem("SS_ClientId");
    sessionStorage.removeItem("SS_FiscalYear");
    sessionStorage.removeItem("SS_ProgramYearId");
    var activeOnly = $(this).prop("checked");
    setClients(activeOnly);
    yearSelect.find("option:gt(0)").remove();
    programSelect.find("option:gt(0)").remove();
});

if (filtered >= 0) {
    // Get client abbr
    var setClientId = windowUrl.substring(windowUrl.indexOf("active")).split('=')[1];
    $('#clientSelection option').each(function (i, value) {
        var thisText = $(this).text();
        if (thisText == setClientId) {
            var setVal = $(this).val();
            $("#clientSelection").val(setVal);
        }
    });
    setTimeout(function () {
        $('#btn-find-meetings').click();
    }, 300);
}

// Add program
$('.ss-gridHeader_addMeetingButton').on('click', function (e) {  //add a click event listener on the delete button
    e.preventDefault();
    e.stopPropagation();
    var inputData = { clientId: $("#clientSelection").val() };
    var title = '<span class="modalSmallCaption modalNotificationCaption">Add Meeting</span>';

    $.get("/Setup/MeetingWizard", inputData, function (data) {
        p2rims.modalFramework.displayModalNoEvent(data, p2rims.modalFramework.customModalSizes.medium, title);
    });
});
var meetingSelect = $('.meetingSelect option:selected').text();

// Set meeting grid data
function setMeetingGrid() {
    $("#successMessage").html("").hide();
    $("#failureMessage").html("").hide();
    var clientId = $("#clientSelection").val();
    var fiscalYear = yearSelect.val();
    var programYearId = programSelect.val();
    if (clientId != "") {
        $.ajax({
            url: '/Setup/GetMeetingsJsonString',
            data: {
                clientId: clientId,
                fiscalYear: fiscalYear,
                programYearId: programYearId
            }
        }).done(function (results) {
            if (results != "") {
                var ds = populateMeetingDataSource(JSON.parse(results));
                var grid = $("#grid").data("kendoGrid");
                ds.read();
                grid.dataSource.data(ds.data());
                grid.setDataSource(grid.dataSource);
                var isActive = $("#activeMeetingCheckbox").prop("checked");
                filterActiveMeetings(isActive);
                setGridDisplay(isActive);
                // Total data in Grid
                $('#totalCount span').text($("#grid").data("kendoGrid").dataSource.total());
            }
        });
    }
}

// "Find" button event handler
$("#btn-find-meetings").on("click", function (e) {
    e.preventDefault();
    $('#grid').hide();
    $('.select-filter-selected ').show();
    $('.ss-gridHeader').hide();
    var meetingSelect = $('#clientSelection option:selected').text();
    if ($('#clientSelection option:selected').filter(':contains("Select")').length > 0 ){
        $('#clientSelection').addClass('input-validation-error');
        $('#selectedClient').text('');
    } else {
        $('#clientSelection').removeClass('input-validation-error');
        $('#selectedClient').text(meetingSelect);
    }

    if (programSelect.find("option:selected").val() === '') {
        $('#selectedProgram').text('');
    } else {
        $('#selectedProgram').text(yearSelect.find("option:selected").text() +
            " - " + programSelect.find("option:selected").text());
    }
    var isActive = $('#activeProgramCheckbox').attr('checked');
    (isActive) ? $('#selectedActive').text('Yes') : $('#selectedActive').text('No');
    // Set grid
    setMeetingGrid();
});

// Populate meeting data source
function populateMeetingDataSource(dataJson) {
    var dataSource = new kendo.data.DataSource({
        type: "json",
        data: dataJson,
        batch: true,
        pageSize: 20,
        schema: {
            model: {
                id: "clientProgramId",
                fields: {
                    index: { type: "number" },
                    meetingAbbr: { type: "string" },
                    meetingDesc: { type: "string" },
                    meetingName: { type: "string" },
                    startDate: { type: "date" },
                    endDate: { type: "date" },
                    hotel: { type: "string" },
                    meetingType: { type: "string" },
                    programCount: { type: "number" },
                    sessionCount: { type: "number" },
                    mtgs: { type: "number" },
                    panelCount: { type: "number" },
                    action: { type: "command" }
                }
            }
        }
    });
    return dataSource;
}

// Program grid
$(function () {
    $("#grid").kendoGrid({
        height: 550,
        resizable: true,
        toolbar: ["create"],
        columns: [
            { field: "index", filterable: false, title: "#", width: "45px" },
            { field: "meetingAbbr", filterable: { multi: true }, title: "Meeting Abbr", width: "150px" },
            { field: "meetingDesc", filterable: true, title: "Meeting Name", width: "175px" },
            { field: "startDate", filterable: { ui: datepicker }, format: "{0: MM/dd/yyyy}", title: "Start", width: "100px" },
            { field: "endDate", filterable: { ui: datepicker }, format: "{0: MM/dd/yyyy}", title: "End", width: "100px" },
            { field: "hotel", filterable: true, title: "Hotel", width: "150px" },
            { field: "meetingType", filterable: { multi: true }, title: "Mtg Type", width: "125px" },
            { field: "programCount", filterable: false, title: "Programs", width: "95px", template: "<a href='\\#' class='clickPrograms' data-clientMeetingId='${clientMeetingId}' id='clickPrograms'>${programCount}</a>" },
            { field: "sessionCount", filterable: false, title: "Sessions", width: "95px", template: "<a href='\\#' onclick='sessionLink(${clientId}, ${clientMeetingId})' class='sessionLink'>${sessionCount}</a>" },
            { field: "panelCount", filterable: false, title: "Panels", width: "75px", template: "<span class='panelCount'>${panelCount}</span>" },
            { field: "clientId", hidden: true },
            { field: "active", hidden: true },
            { filed: "meetingName", hidden: true },
            { field: "clientMeetingId", hidden: true },
            { field: "programYearId", hidden: true },
                {
                    command: [
                    {
                        name: "assign",
                        text: "",
                        template: "<a class='k-button k-button-icontext k-grid-assign' href=''><img src='/Content/img/panelicon.png' title='Assign Programs' alt='Assign Icon' /></a>",
                        click: function (e) {  //add a click event listener on the delete button
                            e.preventDefault();
                            e.stopPropagation();
                            var title = '<span class="modalSmallCaption modalNotificationCaption">Assign Programs</span>';
                            var grid = $("#grid").data("kendoGrid");
                            var gridData = grid.dataSource.view();
                            var i = $(e.target).closest("tr").index();
                            var currentUid = gridData[i].uid;
                            var sessionClientMId = gridData[i].clientMeetingId;

                            sessionStorage.setItem('sessionClientMId', sessionClientMId);

                            var clientMeetingId = gridData[i].clientMeetingId,
                                meetingName = gridData[i].meetingName,
                                meetingDesc = gridData[i].meetingDesc;
                            var client = $('#selectedClient').text();
                            var inputDate = {
                                clientId: $('#clientSelection').val(), clientMeetingId: clientMeetingId
                            };

                            var el = $("#grid");
                            var row = el.find("tbody>tr");
                            if ($(row).hasClass("gridHighlight")) {
                                $(row).removeClass("gridHighlight");
                            };

                            var uniqueIDRow = el.find("tbody>tr[data-uid=" + currentUid + "]");
                            $(uniqueIDRow).addClass('gridHighlight');

                            $.get("/Setup/AssignPrograms", inputDate, function (data) {
                                p2rims.modalFramework.displayModalNoEvent(data, p2rims.modalFramework.customModalSizes.medium, title);
                                $('#clientName').text(client);
                                $('#mtgName').text(meetingDesc);
                            });
                        }

                    },
                    {
                        name: "editable",
                        text: "",
                        template: "<a class='k-button k-button-icontext k-grid-editable' href=''><img src='/Content/img/edit_20px_enabled.png' title='Edit Row' alt='edit icon' /></a>",
                        click: function (e) {  //add a click event listener on the delete button
                            e.preventDefault();
                            e.stopPropagation();
                            var title = '<span class="modalSmallCaption modalNotificationCaption">Edit Meeting</span>';

                            var grid = $("#grid").data("kendoGrid");
                            var gridData = grid.dataSource.view();
                            var i = $(e.target).closest("tr").index();
                            var currentUid = gridData[i].uid;
                            var clientMeetingId = gridData[i].clientMeetingId;

                            var meetingAbbr = gridData[i].meetingAbbr,
                                meetingName = gridData[i].meetingName,
                                start = gridData[i].startDate,
                                end = gridData[i].endDate,
                                hotel = gridData[i].hotel,
                                mtgType = gridData[i].mtgType,
                                dateStart = kendo.parseDate(start),
                                dateEnd = kendo.parseDate(end),
                                dateStartString = kendo.toString(dateStart, "MM/dd/yyyy"),
                                dateEndString = kendo.toString(dateEnd, "MM/dd/yyyy"),
                                currentUid = gridData[i].uid;

                                sessionStorage.setItem('currentUid', currentUid);
                                sessionStorage.setItem('clientMeetingId', clientMeetingId);

                            var inputData = { clientId: $("#clientSelection").val(), clientMeetingId: clientMeetingId };

                            var el = $("#grid");
                            var row = el.find("tbody>tr");
                            if ($(row).hasClass("gridHighlight")) {
                                $(row).removeClass("gridHighlight");
                            };

                            var uniqueIDRow = el.find("tbody>tr[data-uid=" + currentUid + "]");
                            $(uniqueIDRow).addClass('gridHighlight');

                            $.get("/Setup/MeetingWizard", inputData, function (data) {
                                p2rims.modalFramework.displayModalNoEvent(data, p2rims.modalFramework.customModalSizes.medium, title);
                                $("a[id='closeModal'], button[id='closeDialogBtn']").click(function () { $('.ui-dialog-titlebar-close').click(); });
                                hotelSetup();
                            });
                        }
                    },
                    {
                        name: "Destroy",
                        text: "",
                        template: "<a class='k-button k-button-icontext k-grid-Destroy' href=''><img src='/Content/img/icon_remove_16x16.png' title='Delete Row' alt='edit icon' /></a>",
                        click: function (e) {  //add a click event listener on the delete button
                            e.preventDefault();
                            e.stopPropagation();
                            var grid = $("#grid").data("kendoGrid");
                            var gridData = grid.dataSource.view();
                            var i = $(e.target).closest("tr").index();
                            var clientMeetingId = gridData[i].clientMeetingId,
                                sessionCount = gridData[i].sessionCount,
                                meetingName = gridData[i].meetingName;
                            if (sessionCount == 0) {
                                var title = '<span class="modalSmallCaption modalWarningCaption">Warning</span>';
                                $.get("/Setup/RemoveWarning", function (data) {
                                    p2rims.modalFramework.displayModalNoEvent(data, p2rims.modalFramework.customModalSizes.small, title);
                                    var made = "You have selected " + meetingName + " to be removed from the list. Any programs assigned to the meeting will be automatically removed. " +
                                        "Please click CONFIRM to permanently remove it from the list.";
                                    $('.row-fluid p').append(made);
                                    $("a[id='closeModal'], button[id='closeDialogBtn']").click(function () { $('.ui-dialog-titlebar-close').click(); });
                                    $("button[id='saveDialogChanges']").click(function () {
                                        $('#saveDialogChanges').prop("disabled", true);
                                        $.ajax({
                                            cache: false,
                                            type: "POST",
                                            url: '/Setup/DeleteMeeting',
                                            data: { clientMeetingId: gridData[i].clientMeetingId },
                                            success: function (data) {
                                                if (!data.flag) {
                                                    for (var i = 0; i < data.messages.length; i++) {
                                                        $("#warningAlert").append($("<div/>").html(data.messages[i]));
                                                        $('#saveDialogChanges').prop("disabled", true);
                                                    }
                                                } else {
                                                    repopulateHighlightParentGrid();
                                                    $("#successMessage").text("Meeting " + meetingName + " deleted successfully.").show();
                                                    $('.ui-dialog-titlebar-close').click();
                                                }
                                            },
                                            error: function (xhr, ajaxOptions, thrownError) {
                                                $("#warningAlert").html("Failed to delete the program.");
                                            },
                                            complete: function (data) {
                                                $('#submitDialog').prop("disabled", true);
                                            }
                                        });
                                    });
                                });
                            } else {
                                var title = '<span class="modalSmallCaption modalWarningCaption">Warning</span>';
                                $.get("/Setup/RemoveDisallowed", function (data) {
                                    p2rims.modalFramework.displayModalNoEvent(data, p2rims.modalFramework.customModalSizes.small, title);
                                    $("a[id='closeModal'], button[id='closeDialogBtn']").click(function () { $('.ui-dialog-titlebar-close').click(); });
                                    var made = "You may not remove a meeting once sessions have been assigned.";
                                    $('.row-fluid p').append(made);
                                });
                            }
                        }
                    }
                    ], title: "Action",
                }],
            editable: { mode: "popup" },
            scrollable: true,
            groupable: true,
            sortable: true,
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
            pageable: {
                input: true,
                numeric: false,
                refresh: true,
                pageSizes: true,
                responsive: false,
                pageSize: 20
            }
        });


    // This is where we added the same toolbar thats on the bottom of the grid to the top
    var grid = $("#grid").data("kendoGrid");
    var wrapper = $('<div class="k-pager-wrap pagerTop"/>').insertBefore(grid.element.children(".k-grid-header"));
    wrapper.kendoPager($.extend({}, grid.options.pageable, { dataSource: grid.dataSource }));
    grid.element.height("").find(".pagerTop").css("border-width", "0 0 1px 0");

    // Refresh button handler
    $('a.k-pager-refresh').on('click', function (e) {
        e.preventDefault();
        setMeetingGrid();
    });

    function datepicker(element) {
        element.kendoDatePicker({
            format: "MM/dd/yyyy"
        });
    }
});

function hotelSetup() {
    if ($('#meetingType select :selected').filter(':contains("Onsite")').length > 0) {
        $('#hotelType select').attr('disabled', false);
    } else {
        $('#hotelType select').attr('disabled', 'disabled');
    }
}
// Programs link click event handler
$(document).on('click', '.clickPrograms', function (e) {
    var clientId = $("#clientSelection").val();
    var title = '<span class="modalSmallCaption modalNotificationCaption">UnAssign Programs</span>';
    var clientMeetingId = e.currentTarget;
    var id = $(clientMeetingId).attr('data-clientMeetingId');
    var inputData = { clientId: clientId, clientMeetingId: id };
    $.get("/Setup/UnassignPrograms", inputData, function (data) {
        p2rims.modalFramework.displayModalNoEvent(data, p2rims.modalFramework.customModalSizes.medium, title);
        var client = $('#selectedClient').text();
        $('#clientName').text(client);
    });
});
//Session data link
function sessionLink(clientId, clientMeetingId) {
    sessionStorage.setItem("SS_ClientId", clientId);
    sessionStorage.setItem("SS_ClientMeetingId", clientMeetingId);
    sessionStorage.setItem("SS_ActiveMeeting", $("#activeMeetingCheckbox").prop("checked"));
    window.location = "/Setup/SessionSetup";
}

function filterActiveMeetings(isActive) {
    var ds = $("#grid").data("kendoGrid").dataSource;
    // Get dataSource's array of filters
    var curr_filters = ds.filter() != undefined ? ds.filter().filters : [];
    if (curr_filters.length > 0) {
        // Remove existing "active" filters if any
        var removedFieldIndex = -1;
        var i = 0;
        while (removedFieldIndex === -1 && i < curr_filters.length) {
            if (curr_filters[i].field == "active") {
                removedFieldIndex = i;
            }
            i++;
        }
        if (removedFieldIndex !== -1) {
            curr_filters.splice(removedFieldIndex, 1);
        }
    }
    if (isActive) {
        // Create new filter object
        var new_filter = { field: "active", operator: "eq", value: "true" };
        // Add new_filter to filters
        curr_filters.push(new_filter);
    }
    // Apply the filters
    ds.filter(curr_filters);
}

function setGridDisplay(isActive) {
    var grid = $("#grid").data("kendoGrid");
    grid.dataSource.data();
    var count = grid.dataSource.total();
    if (count === 0) {
        $(".ss-gridHeader").hide();
        $("#grid").hide();
        if (isActive) {
            $("#noActiveMeeting").show();
            $("#noMeeting").hide();
        } else {
            $("#noActiveMeeting").hide();
            $("#noMeeting").show();
        }
    } else {
        $(".ss-gridHeader").show();
        $("#grid").show();
        $("#noMeeting").hide();
        $("#noActiveMeeting").hide();
    }
    $("#meetingSetupInstructions").hide();
}
// Reset program/year dropdown values
function resetProgramAndYear() {
    yearSelect.val("");
    programSelect.val("");
}

// Active filter
activeMeetingCheckbox.on("change", function () {
    sessionStorage.setItem("SS_ActiveMeeting", $("#activeMeetingCheckbox").prop("checked"));
    var isActive = $(this).prop("checked");
    if ($('#grid').is(":visible") || $("#noMeeting").is(":visible") || $("#noActiveMeeting").is(":visible")) {
        filterActiveMeetings(isActive);
        setGridDisplay(isActive);
    }
    // Total data in Grid
    $('#totalCount span').text($("#grid").data("kendoGrid").dataSource.total());
});

function filterActivePrograms(isActive) {
    var ds = $("#grid").data("kendoGrid").dataSource;
    // Get dataSource's array of filters
    var curr_filters = ds.filter() != undefined ? ds.filter().filters : [];
    if (curr_filters.length > 0) {
        // Remove existing "active" filters if any
        var removedFieldIndex = -1;
        var i = 0;
        while (removedFieldIndex == -1 && i < curr_filters.length) {
            if (curr_filters[i].field == "active") {
                removedFieldIndex = i;
            }
            i++;
        }
        if (removedFieldIndex != -1) {
            curr_filters.splice(removedFieldIndex, 1);
        }
    }
    if (isActive) {
        // Create new filter object
        var new_filter = { field: "active", operator: "eq", value: "Yes" };
        // Add new_filter to filters
        curr_filters.push(new_filter);
    }
    // Apply the filters
    ds.filter(curr_filters)
}

// Re-populate parent grid after adding/updating/deleting
var repopulateHighlightParentGrid = function () {
    // Refresh button handler
    $.ajax({
        url: '/Setup/GetMeetingsJsonString',
        data: {
            clientId: $("#clientSelection").val(),
            fiscalYear: yearSelect.val(),
            programYearId: programSelect.val()
        }
    }).done(function (results) {
        if (results != "") {
            var ds = populateMeetingDataSource(JSON.parse(results));
            var grid = $("#grid").data("kendoGrid");
            var gridTotal = $("#grid").data("kendoGrid").dataSource._data;
            ds.read();
            grid.dataSource.data(ds.data());

            // Total data in Grid
            var sourcedata = ds.data()
            $('#totalCount span').text(sourcedata.length);
            var isActive = $("#activeMeetingCheckbox").prop("checked");
            filterActiveMeetings(isActive);
            setGridDisplay(isActive);

            var editUid = sessionStorage.getItem('currentUid');

            if ($('.ui-dialog-title:contains("Add Meeting")').length > 0) {
                // Gets total amount of data before added row
                var arrayTotal = [];
                for (i = 0; i < gridTotal.length; i++) {
                    arrayTotal.push(gridTotal[i].uid);
                }

                // Gets total amount of data after added row
                var gridTotalAfter = $("#grid").data("kendoGrid").dataSource._data;
                var newArrayTotal = [];
                for (i = 0; i < gridTotalAfter.length; i++) {
                    newArrayTotal.push(gridTotalAfter[i].uid);
                }

                // Grabs the newly added row's program year ID
                var difference = [];
                $.grep(newArrayTotal, function (el) {
                    if ($.inArray(el, arrayTotal) == -1) difference.push(el);
                });

                // Filters through list and if program year ID equals the object, then display the UID
                var obj = gridTotalAfter.filter(function (obj) {
                    if (difference == obj.uid) {
                        var rowID = obj.uid;
                        var el = $("#grid");
                        var row = el.find("tbody>tr[data-uid=" + rowID + "]");
                        $(row).addClass('gridHighlight');
                    }
                });
            } else {
                // Gets the reset data with the new UID's
                var gridTotalAfter = $("#grid").data("kendoGrid").dataSource._data;
                var getProgramYear = sessionStorage.getItem('currentUid');

                // Filters the program year ID to match the new data to highlight the row
                var obj = gridTotalAfter.filter(function (obj) {
                    var newVar = obj.uid;
                    var sameEvalName = obj.clientMeetingId;
                    var selectedName = $('#criteriaSelection option:selected').text();
                    if (getProgramYear == newVar) {
                        var rowID = obj.uid;
                        var el = $("#grid");
                        var row = el.find("tbody>tr[data-uid=" + rowID + "]");
                        $(row).addClass('gridHighlight');
                    }
                });
            }
        }
    });
};

function validateDate() {
    var todayDate = kendo.toString(kendo.parseDate(new Date()), 'MM/dd/yyyy');

    function startChange() {
        var startDate = start.value(),
        endDate = end.value();

        if (startDate) {
            startDate = new Date(startDate);
            startDate.setDate(startDate.getDate());
            end.min(startDate);
        } else if (endDate) {
            start.max(new Date(endDate));
        } else {
            endDate = new Date();
            start.max(endDate);
            end.min(endDate);
        }
    }

    function endChange() {
        var endDate = end.value(),
        startDate = start.value();

        if (endDate) {
            endDate = new Date(endDate);
            endDate.setDate(endDate.getDate());
            start.max(endDate);
        } else if (startDate) {
            end.min(new Date(startDate));
        } else {
            endDate = new Date();
            start.max(endDate);
            end.min(endDate);
        }
    }

    var start = $("#startDatePicker").kendoDatePicker({
        change: startChange,
    }).data("kendoDatePicker");

    var end = $("#endDatePicker").kendoDatePicker({
        change: endChange,
    }).data("kendoDatePicker");

    start.min(todayDate);
    start.max(end.value());
    end.min(start.value());
};