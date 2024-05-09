var clientsJson;
var activeProgramCheckbox = $('#activeProgramCheckbox'),
    activeMeetingCheckbox = $('#activeMeetingCheckbox'),
    clientSelect = $('.ss-filterBox_ss-table_clientSelect_select'),
    meetingSelect = $('.ss-filterBox_ss-table_meetingSelect_select');

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
    meetingSelect.find("option:gt(0)").remove();
    meetingSelect.prop("disabled", true);
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
            resetMeetings();
        }
    }
}

// Initialize
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
    if (sessionStorage.getItem("SS_ClientId")) {
        if ($("#clientSelection > option[value='" + sessionStorage.getItem("SS_ClientId") + "']").length > 0) {
            $("#clientSelection").val(sessionStorage.getItem("SS_ClientId"));
            resetMeetings();
        }
    }
    // Get clients on load
    getClients();
}

// Add Session
$('.ss-gridHeader_addSessionButton').on('click', function (e) {  //add a click event listener on the delete button
    e.preventDefault();
    e.stopPropagation();

    var title = '<span class="modalSmallCaption modalNotificationCaption">Add Session</span>';
    $.get("/Setup/SessionWizard", { clientMeetingId: $("#meetingSelection").val() }, function (data) {
        p2rims.modalFramework.displayModalNoEvent(data, p2rims.modalFramework.customModalSizes.large, title);
        //Filtered info
        var clientSelection = $('#clientSelection option:selected').text();
        var meetingSelection = $('#meetingSelection option:selected').text();
        var sessionMtgType = $('#sessionMtgType').text();
        var sessionDate = $('#sessionDate').text();

        $('#addSessionClient').text(clientSelection);
        $('#addSessionMtg').text(meetingSelection);
        $('#addSessionMtgType').text(sessionMtgType);
        $('#addSessionMtgDates').text(sessionDate);
    });
});

function filterActiveSessions(isActive) {
    var ds = $("#session-grid").data("kendoGrid").dataSource;
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
        var new_filter = { field: "active", operator: "eq", value: "true" };
        // Add new_filter to filters
        curr_filters.push(new_filter);
    }
    // Apply the filters
    ds.filter(curr_filters)
}

// Populate program data source
function populateSessionDataSource(dataJson) {
    var dataSource = new kendo.data.DataSource({
        type: "json",
        data: dataJson,
        batch: true,
        pageSize: 20,
        //sort: { field: "index", dir: "asc" },
        schema: {
            model: {
                id: "clientProgramId",
                fields: {
                    index: { type: "number" },
                    sessionAbbr: { type: "string" },
                    sessionName: { type: "string" },
                    startDate: { type: "date" },
                    endDate: { type: "date" },
                    action: { type: "command" },
                    clientId: { type: "string" },
                    programYearId: { type: "string" },
                    active: { type: "string" },
                    clientMeetingId: { type: "string" },
                    meetingSessionId: { type: "string" },
                    hasProgramMeetings: { type: "string" }
                }
            }
        }
    });
    return dataSource;
}

// Set meeting drop-down
function setMeetings() {
    $("#meetingSelection").find("option:gt(0)").remove();
    var isActiveProgram = $("#activeProgramCheckbox").is(":checked");
    $.each(meetingsJson, function (i, item) {
        if (!isActiveProgram || item.ActiveProgram) {
            $("#meetingSelection").append($("<option/>")
                .attr("value", item.ClientMeetingId).html(item.MeetingName));
        }
    });
    meetingSelect.prop("disabled", false);
    if ($("#meetingId").val() != "" && $("#meetingSelection > option[value='" +
        $("#meetingId").val() + "']").length > 0) {
        $("#meetingSelection").val($("#meetingId").val());
    }
    // Session storage
    if (sessionStorage.getItem("SS_ClientMeetingId")) {
        if ($("#meetingSelection > option[value='" + sessionStorage.getItem("SS_ClientMeetingId") + "']").length > 0) {
            $('#meetingSelection').val(sessionStorage.getItem("SS_ClientMeetingId"));
            $('.ss-filterBox_ss-table_buttonShell_buttonPrimary').click();
        }
    }
}
// Reset meetings data
function resetMeetings() {
    var clientId = $("#clientSelection").val();
    var isDateEnded = $('#activeMeetingCheckbox').is(':checked');
    if (clientId != "") {
        $.ajax({
            url: '/Setup/GetMeetingsJson',
            data: { clientId: clientId, isDateEnded: isDateEnded }
        }).done(function (results) {
            meetingsJson = results;
            setMeetings();
        });
    }
}

var meetingsJson;
// Program grid
$(function () {
    $("#session-grid").kendoGrid({
        height: 550,
        resizable: true,
        toolbar: ["create"],
        columns: [
            { field: "index", filterable: false, title: "#", width: "45px" },
            { field: "sessionAbbr", filterable: true, title: "Session Abbr", width: "150px" },
            { field: "sessionName", filterable: true, title: "Session Name", width: "290px" },
            { field: "startDate", filterable: { ui: datepicker }, format: "{0: MM/dd/yyyy hh:mm tt}", title: "Start Date", width: "155px" },
            { field: "endDate", filterable: { ui: datepicker }, format: "{0: MM/dd/yyyy hh:mm tt}", title: "End Date", width: "155px" },
            { field: "panels", filterable: false, title: "Panels", width: "300px", template: $("#panelsTemplate").html() },
            { field: "clientId", hidden: true },
            { field: "programYearId", hidden: true },
            { field: "active", hidden: true },
            { field: "clientMeetingId", hidden: true },
            { field: "meetingSessionId", hidden: true },
            { field: "hasProgramMeetings", hidden: true },
            {
                command: [
                {
                    name: "assign",
                    text: "",
                    template: "<a class='k-button k-button-icontext k-grid-assign' href=''><img src='/Content/img/panelicon.png' title='Add Panels' alt='Add Panel Icon' /></a>",
                    click: function (e) {  //add a click event listener on the delete button
                        e.preventDefault();
                        e.stopPropagation();
                        var title = '<span class="modalSmallCaption modalNotificationCaption">Add Panels</span>';
                        var gridData = grid.dataSource.view();
                        var i = $(e.target).closest("tr").index();
                        var currentUid = gridData[i].uid;
                        var sessionName = gridData[i].sessionName;
                        var sessionAbbr = gridData[i].sessionAbbr;
                        var panels = gridData[i].panels;
                        var meetingSessionId = gridData[i].meetingSessionId;
                        var clientMeetingId = gridData[i].clientMeetingId;
                        var hasProgramMeetings = gridData[i].hasProgramMeetings;
                        var startDate = $.toDateDisplayFormat(gridData[i].startDate);
                        var endDate = $.toDateDisplayFormat(gridData[i].endDate);

                        sessionStorage.setItem('clientMeetingId', clientMeetingId);
                        sessionStorage.setItem('newMeetingSessionId', meetingSessionId);

                        if (hasProgramMeetings) {
                            var inputData = { meetingSessionId: gridData[i].meetingSessionId };
                            $.get("/Setup/PanelWizard", inputData, function (data) {
                                p2rims.modalFramework.displayModalNoEvent(data, p2rims.modalFramework.customModalSizes.medium, title);
                                var clientSelection = $('#clientSelection option:selected').text();
                                var meetingSelection = $('#meetingSelection option:selected').text();

                                $('#clientAwardModal').text(clientSelection);
                                $('#programAwardModal').text(meetingSelection);
                                $('#activeAwardModal').text(sessionAbbr + " - " + startDate + " - " + endDate);
                            });
                        } else {
                            var inputData = { clientId: gridData[i].clientId };
                            var el = $("#session-grid");
                            var row = el.find("tbody>tr");
                            if ($(row).hasClass("newPanel")) {
                                $(row).removeClass("newPanel");
                            };
                            var uniqueIDRow = el.find("tbody>tr[data-uid=" + currentUid + "]");
                            $(uniqueIDRow).addClass('newPanel');

                            var title = '<span class="modalSmallCaption modalNotificationCaption">Assign Program</span>';
                            $.get("/Setup/AddPanelNoPrograms", inputData, function (data) {
                                p2rims.modalFramework.displayModalNoEvent(data, p2rims.modalFramework.customModalSizes.medium, title);
                                var clientSelection = $('#clientSelection option:selected').text();
                                var meetingSelection = $('#meetingSelection option:selected').text();

                                $('#clientName').text(clientSelection);
                                $('#mtgName').text(meetingSelection);
                            });
                        }
                    }
                },
                {
                    name: "editable",
                    text: "",
                    template: "<a class='k-button k-button-icontext k-grid-editable' href=''><img src='/Content/img/edit_20px_enabled.png' title='Edit Session' alt='edit icon' /></a>",
                    click: function (e) {  //add a click event listener on the delete button
                        e.preventDefault();
                        e.stopPropagation();
                        var title = '<span class="modalSmallCaption modalNotificationCaption">Edit Session</span>';
                        var grid = $("#session-grid").data("kendoGrid");
                        var gridData = grid.dataSource.view();
                        var i = $(e.target).closest("tr").index();
                        var currentUid = gridData[i].uid;
                        var sessionAbbr = gridData[i].sessionAbbr;
                        var sessionName = gridData[i].sessionName;
                        var startDate = gridData[i].startDate;
                        var endDate = gridData[i].endDate;
                        var meetingSessionId = gridData[i].meetingSessionId;
                    
                        var grid = $("#session-grid").data("kendoGrid");
                        var idx = $(e.target).closest('tr').find('.panelsSelection').text().toString();

                        var dateStart = kendo.parseDate(startDate),
                            dateEnd = kendo.parseDate(endDate),
                            dateStartString = kendo.toString(dateStart, "MM/dd/yyyy hh:mm tt"),
                            dateEndString = kendo.toString(dateEnd, "MM/dd/yyyy hh:mm tt");

                        var inputData = { clientMeetingId: gridData[i].clientMeetingId, meetingSessionId: gridData[i].meetingSessionId };
                        $.get("/Setup/SessionWizard", inputData, function (data) {
                            p2rims.modalFramework.displayModalNoEvent(data, p2rims.modalFramework.customModalSizes.large, title);
                            $("a[id='closeModal'], button[id='closeDialogBtn']").click(function () { $('.ui-dialog-titlebar-close').click(); });

                            var clientSelection = $('#clientSelection option:selected').text();
                            var meetingSelection = $('#meetingSelection option:selected').text();
                            var sessionMtgType = $('#sessionMtgType').text();
                            var sessionDate = $('#sessionDate').text();

                            $('#addSessionClient').text(clientSelection);
                            $('#addSessionMtg').text(meetingSelection);
                            $('#addSessionMtgType').text(sessionMtgType);
                            $('#addSessionMtgDates').text(sessionDate);
                            $('.sessionAbbr input').val(sessionAbbr);
                            $('.sessionName input').val(sessionName);
                            $('#panels').val(idx);

                            $('.startDate, .reopenDate').each(function () {
                                var dateStartString = $(this).val();
                                var dateStart = kendo.parseDate(dateStartString);
                                var finalDate = kendo.toString(dateStart, "MM/dd/yyyy hh:mm tt");
                                $(this).val(finalDate);
                            });

                            $('.endDate, .closeDate').each(function () {
                                var dateStartString = $(this).val();
                                var dateStart = kendo.parseDate(dateStartString);
                                var finalDate = kendo.toString(dateStart, "MM/dd/yyyy hh:mm tt");
                                $(this).val(finalDate);
                            });

                            if ($('#addSessionMtgType').filter(':contains("On-Line")').length > 0) {
                                    $('.preMeeting .datePickRow:nth-child(4)').css('display', 'table-row');
                            } else {
                                    $('.preMeeting .datePickRow:nth-child(4)').css('display', 'none');
                            }
                        });
                    }

                },
                {
                    name: "Destroy",
                    text: "",
                    template: "<a class='k-button k-button-icontext k-grid-Destroy' href=''><img src='/Content/img/icon_remove_16x16.png' title='Remove Session' alt='edit icon' /></a>",
                    click: function (e) {  //add a click event listener on the delete button
                        e.preventDefault();
                        e.stopPropagation();
                        $("#successMessage").hide();
                        var grid = $("#session-grid").data("kendoGrid");
                        var gridData = grid.dataSource.view();
                        var i = $(e.target).closest("tr").index();
                        var currentUid = gridData[i].uid;
                        var sessionName = gridData[i].sessionName;
                        var panels = gridData[i].panels;
                        var meetingSessionId = gridData[i].meetingSessionId;

                        if (panels.length == 0) {
                            var title = '<span class="modalSmallCaption modalWarningCaption">Warning</span>';
                            $.get("/Setup/RemoveWarning", function (data) {
                                p2rims.modalFramework.displayModalNoEvent(data, p2rims.modalFramework.customModalSizes.small, title);
                                var made = "You have selected session " + sessionName + " to be removed from the list. Please click CONFIRM to permanently remove it from the list.";
                                $('.row-fluid p').append(made);
                                $("a[id='closeModal'], button[id='closeDialogBtn']").click(function () { $('.ui-dialog-titlebar-close').click(); });
                                $("button[id='saveDialogChanges']").click(function () {
                                    $('#saveDialogChanges').prop("disabled", true);
                                    $.ajax({
                                        cache: false,
                                        type: "POST",
                                        url: '/Setup/DeleteSession',
                                        data: { meetingSessionId: meetingSessionId },
                                        success: function (data) {
                                            if (!data.flag) {
                                                for (var i = 0; i < data.messages.length; i++) {
                                                    $("#warningAlert").append($("<div/>").html(data.messages[i]));
                                                    $('#saveDialogChanges').prop("disabled", true);
                                                }
                                            } else {
                                                $('.k-icon.k-i-refresh').click();
                                                setSessionGrid();
                                                $("#successMessage").text("Session " + sessionName + " deleted successfully.").show();
                                                $('.ui-dialog-titlebar-close').click();
                                            }
                                        },
                                        error: function (xhr, ajaxOptions, thrownError) {
                                            $("#warningAlert").html("Failed to delete the session.");
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
                                var made = "You may not remove a session once panels are assigned.";
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
        },
        dataBound: dataBound
    });
    // Panels Selection
    function dataBound(){
        $('.panelsSelection').on('click', function (e) {
            e.preventDefault();
            var inputData = { sessionPanelId: $(this).attr("data-session-panel-id") };
            $.get("/Setup/UpdatePanel", inputData, function (data) {
                var title = '<span class="modalSmallCaption modalNotificationCaption">Update Panel</span>';
                var gridData = grid.dataSource.view();
                var i = $(e.target).closest("tr").index();
                var currentUid = gridData[i].uid;
                var sessionName = gridData[i].sessionName;
                var sessionAbbr = gridData[i].sessionAbbr;
                var startDate = gridData[i].startDate;
                var endDate = gridData[i].endDate;

                var dateStart = kendo.parseDate(startDate),
                    dateEnd = kendo.parseDate(endDate),
                    dateStartString = kendo.toString(dateStart, "MM/dd/yyyy"),
                    dateEndString = kendo.toString(dateEnd, "MM/dd/yyyy");

                p2rims.modalFramework.displayModalNoEvent(data, p2rims.modalFramework.customModalSizes.medium, title);
                var clientSelection = $('#clientSelection option:selected').text();
                var meetingSelection = $('#meetingSelection option:selected').text();

                $('#clientAwardModal').text(clientSelection);
                $('#programAwardModal').text(meetingSelection);
                $('#activeAwardModal').text(sessionAbbr + " " + dateStartString + " - " + dateEndString);
            });
        });
    }

    // Initialize
    init();

    // Program checkbox change event handler
    $("#activeProgramCheckbox").on("change", function () {
        sessionStorage.removeItem("SS_ClientId");
        sessionStorage.removeItem("SS_ClientMeetingId");
        sessionStorage.removeItem("SS_ActiveProgram");
        var activeOnly = activeProgramCheckbox.prop("checked");
        setClients(activeOnly);
        $('#clientSelection').val("");
        $('#meetingSelection').find("option:gt(0)").remove();
    });
    // Meeting checkbox change event handler
    $("#activeMeetingCheckbox").on("change", function () {
        sessionStorage.setItem("SS_ActiveMeeting", $("#activeMeetingCheckbox").prop("checked"));
        sessionStorage.removeItem("SS_ClientMeetingId");
        $('#meetingSelection').val("");
        // Reset meetings
        resetMeetings();
    });
    // Formated Datepicker for Filter
    function datepicker(element) {
        element.kendoDateTimePicker({
            format: "MM/dd/yyyy hh:mm tt",
        })
    }

    // This is where we added the same toolbar thats on the bottom of the grid to the top
    var grid = $("#session-grid").data("kendoGrid");
    var wrapper = $('<div class="k-pager-wrap k-grid-pager pagerTop"/>').insertBefore(grid.element.children(".k-grid-header"));
    wrapper.kendoPager($.extend({}, grid.options.pageable, { dataSource: grid.dataSource }));
    grid.element.height("").find(".pagerTop").css("border-width", "0 0 1px 0");

    // Refresh button handler
    $('a.k-pager-refresh').on('click', function (e) {
        e.preventDefault();
        setMeetings();
        setSessionGrid(); 
    });

    // Client drop-down change event handler
    $("#clientSelection").on("change", function () {
        if ($("#clientSelection").val() === "") {
            sessionStorage.removeItem("SS_ClientId");
        } else {
            sessionStorage.setItem("SS_ClientId", $("#clientSelection").val());
        }
        sessionStorage.removeItem("SS_ClientMeetingId");
        $('#meetingSelection').find("option:gt(0)").remove();
        resetMeetings();
    });
    // Meeting drop-down change event handler
    $("#meetingSelection").on("change", function () {
        if ($("#meetingSelection").val() === "") {
            sessionStorage.removeItem("SS_ClientMeetingId");
        } else {
            sessionStorage.setItem("SS_ClientMeetingId", $("#meetingSelection").val());
        }
    });

    // "Find" button event handler
    $(".ss-filterBox_ss-table_buttonShell_buttonPrimary").on("click", function (e) {
        e.preventDefault();

        dropDownValidation();
        var validationErrorVis = !$('.input-validation-error').is(':visible');
        if (validationErrorVis) {
            setSessionGrid();
            $('.ss-gridHeader').addClass('displayBlock');
            $('.ss-grid-sessionSetup').addClass('displayBlock');
        }
    });
});

// Set session grid data
function setSessionGrid(fnPostSetGrid) {
    $.ajax({
        url: '/Setup/GetSessionsJsonString',
        data: {
            clientMeetingId: $("#meetingSelection").val()
        }
    }).done(function (results) {
        if (results.length >= 3) {
            var grid = $("#session-grid").data("kendoGrid");
            var ds = populateSessionDataSource(JSON.parse(results));
            ds.read();
            grid.dataSource.data(ds.data());
            // Total data in Grid
            var sourcedata = ds.data();
            $('#totalCount span').text(sourcedata.length);
            $('#session-grid, .ss-gridHeader').addClass('displayBlock');
            $('.ss-gridHeader_no-records').hide();
        } else {
            $('.ss-gridHeader_no-records').show();
            $('#session-grid, .ss-gridHeader').removeClass('displayBlock');
        }
        $('.ss-gridHeader_instruction').hide();
        //Filtered info
        var clientSelection = $('#clientSelection option:selected').text();
        var clientMeetingId = $('#meetingSelection option:selected').val();
        var meetingSelection = $('#meetingSelection option:selected').text();
        $("#meetingId").val(clientMeetingId);

        $('#sessionClient').text(clientSelection);
        $('#sessionMtg').text(meetingSelection);
        // Set meeting type and start/end
        var found = false;
        var i = 0;
        while (!found && i < meetingsJson.length) {
            if (meetingsJson[i].ClientMeetingId == clientMeetingId) {
                $("#sessionMtgType").html(meetingsJson[i].MeetingType);
                $("#sessionDate").html(meetingsJson[i].StartDate + " - " + meetingsJson[i].EndDate);
                found = true;
            }
            i++;
        }
        // Post function
        if (fnPostSetGrid) {
            fnPostSetGrid();
        }
    });
}
// Drop down validations
var dropDownValidation = function () {
    // Validation for select fields
    ($('#clientSelection option:selected').filter(':contains("Select")').length > 0) ? $('.ss-filterBox_ss-table_clientSelect select').addClass('input-validation-error') : $('.ss-filterBox_ss-table_clientSelect select').removeClass('input-validation-error');
    ($('#meetingId option:selected').filter(':contains("Select")').length > 0) ? $('.ss-filterBox_ss-table_meetingSelect select').addClass('input-validation-error') : $('.ss-filterBox_ss-table_meetingSelect select').removeClass('input-validation-error');
};

var repopulateHighlightParentGrid = function () {
    $.ajax({
        url: '/Setup/GetSessionsJsonString',
        data: {
            clientMeetingId: $("#meetingSelection").val()
        }
    }).done(function (results) {
        if (results !== "") {
            var ds = populateSessionDataSource(JSON.parse(results));
            var grid = $("#session-grid").data("kendoGrid");
            ds.read();
            grid.dataSource.data(ds.data());
             // Total data in Grid
            $('#totalRecords span').text(ds.total());
 
            //Filtered info
            var clientSelection = $('#clientSelection option:selected').text();
            var clientMeetingId = $('#meetingSelection option:selected').val();
            var meetingSelection = $('#meetingSelection option:selected').text();
            $("#meetingId").val(clientMeetingId);

            $('#sessionClient').text(clientSelection);
            $('#sessionMtg').text(meetingSelection);
            // Set meeting type and start/end
            var found = false;
            var i = 0;
            while (!found && i < meetingsJson.length) {
                if (meetingsJson[i].ClientMeetingId == clientMeetingId) {
                    $("#sessionMtgType").html(meetingsJson[i].MeetingType);
                    $("#sessionDate").html(meetingsJson[i].StartDate + " - " + meetingsJson[i].EndDate);
                    found = true;
                }
                i++;
            }
        }
    });
};

var repopulateHighlightParentGridPanel = function () {
    $.ajax({
        url: '/Setup/GetSessionsJsonString',
        data: {
            clientMeetingId: $("#meetingSelection").val()
        }
    }).done(function (results) {
        if (results !== "") {
            var ds = populateSessionDataSource(JSON.parse(results));
            var grid = $("#session-grid").data("kendoGrid");
            ds.read();
            grid.dataSource.data(ds.data());
            // Total data in Grid
            $('#totalRecords span').text(ds.total());
 
            // Gets total amount of data after added row
            var gridTotalAfter = $("#session-grid").data("kendoGrid").dataSource._data;
            var newArrayTotal = [];
            for (i = 0; i < gridTotalAfter.length; i++) {
                newArrayTotal.push(gridTotalAfter[i].meetingSessionId);
            }

            var newId = sessionStorage.getItem('newMeetingSessionId');
            // Filters through list and if program year ID equals the object, then display the UID
            var obj = gridTotalAfter.filter(function (obj) {
                if (newId == obj.meetingSessionId) {
                    var rowID = obj.uid;
                    var el = $("#session-grid");
                    var row = el.find("tbody>tr[data-uid=" + rowID + "]");
                    $(row).addClass('newPanel');
                    $('.newPanel a.k-grid-assign').click();
                }
            });
        }
    });
}

// Function to open new panel modal
var openNewPanelModal = function () {
    // Gets total amount of data after added row
    var gridTotalAfter = $("#session-grid").data("kendoGrid").dataSource._data;
    var newId = sessionStorage.getItem('newMeetingSessionId');
    // Filters through list and if program year ID equals the object, then display the UID
    var obj = gridTotalAfter.filter(function (obj) {
        if (newId == obj.meetingSessionId) {
            var rowID = obj.uid;
            var el = $("#session-grid");
            var row = el.find("tbody>tr[data-uid=" + rowID + "]");
            $(row).addClass('newPanel');
            $('.newPanel a.k-grid-assign').click();
        }
    });
};