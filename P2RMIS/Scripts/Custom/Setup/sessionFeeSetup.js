// Client drop-down change event handler
$("#clientId").on("change", function () {
    if ($('#clientId').val() != "") {
        $.ajax({
            url: '/Setup/GetFiscalYearsJson',
            data: { clientId: $("#clientId").val() }
        }).done(function (results) {
            fiscalYearsJson = results;
            var activeOnly = $("#activeFyFilter").prop("checked");
            setFiscalYears(activeOnly);
        });
    }
});

$("#fiscalYearId").on("change", function () {
    if ($('#fiscalYearId').val() != "") {
        $.ajax({
            url: '/Setup/GetFeeScheduleMeetingsJson',
            data: {
                clientId: $("#clientId").val(),
                fiscalYear: $("#fiscalYearId").val()
            }
        }).done(function (results) {
            meetingsJson = results;
            var activeOnly = $("#activeFyFilter").prop("checked");
            setFeeScheduleMeetings();
        });
    }
});

$("#meetingId").on("change", function () {
    if ($('#meetingId').val() != "Select Meeting") {
        $.ajax({
            url: '/Setup/GetFeeScheduleSessionsJson',
            data: {
                clientMeetingId: $("#meetingId").val()
            }
        }).done(function (results) {
            sessionsJson = results;
            var activeOnly = $("#activeFyFilter").prop("checked");
            setFeeScheduleSessions();
        });
    }
});

// Active fiscal year filter
$("#activeFyFilter").on("change", function () {
    var activeOnly = $(this).prop("checked");
    setFiscalYears(activeOnly);
    setFeeScheduleSessions();
    setFeeScheduleMeetings();
});

// Set fiscal years drop-down
function setFiscalYears(activeOnly) {
    $("#fiscalYearId").find("option:gt(0)").remove();
    $.each(fiscalYearsJson, function (i, item) {
        if (!activeOnly || (activeOnly && item.IsActive)) {
            $("#fiscalYearId").append($("<option/>")
                .attr("value", item.YearValue).html(item.YearText));
        }
    });
}

// Set fee schedule meeting drop-down
function setFeeScheduleMeetings() {
    $("#meetingId").find("option:gt(0)").remove();
    $.each(meetingsJson, function (i, item) {
        $("#meetingId").append($("<option/>")
            .attr("value", item.Key).html(item.Value));
    });
}
// Set fee schedule session drop-down
function setFeeScheduleSessions() {
    $("#sessionId").find("option:gt(0)").remove();
    $.each(sessionsJson, function (i, item) {
        $("#sessionId").append($("<option/>")
            .attr("value", item.Key).html(item.Value));
    });
}

// Filtered info
var clientSelection = $('#clientSelection option:selected').text();
var meetingSelection = $('#meetingSelection option:selected').text();

$('#sessionClient').text(clientSelection);
$('#sessionMtg').text(meetingSelection);

function filterActiveSessions(isActive) {
    var ds = $("#ss-grid-session").data("kendoGrid").dataSource;
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
        schema: {
            model: {
                id: "clientProgramId",
                fields: {
                    index: { type: "number" },
                    participantType: { type: "string" },
                    consultantFeeText: { type: "string" },
                    paid: { type: "string" },
                    fee: { type: "number" },
                    startDate: { type: "date" },
                    endDate: { type: "date" },
                    sraManagers: { type: "string" },
                    workDescription: { type: "command" }
                }
            }
        }
    });
    return dataSource;
}

// Program grid
$(function () {
    $("#ss-grid-session").kendoGrid({
        pageable: true,
        height: 550,
        resizable: true,
        toolbar: ["create"],
        columns: [
            { field: "index", filterable: false, title: "#", width: "30px" },
            { field: "participantType", filterable: { multi: true }, title: "Participant Type", width: "150px" },
            { field: "consultantFeeText", filterable: true, title: "Consultant Fee Text", width: "180px" },
            { field: "paid", filterable: { ui: yesFilter }, title: "Paid", width: "85px" },
            {
                field: "fee", format: "{0:c2}", filterable:
                    {
                        ui: function (element) {
                            element.kendoNumericTextBox({
                                format: 'c2',
                                decimals: 2,
                            });
                            var form = element.closest("form");
                            form.find(".k-filter-help-text:first").text("Show items with value that:");
                            form.find("select").remove();
                            $('.k-filter-help-text:first').text('Enter an amount:');
                        }
                    }, title: "Fee", width: "70px"
            },
            { field: "startDate", filterable: { ui: datepicker }, format: "{0: MM/dd/yyyy}", title: "Start Date", width: "110px" },
            { field: "endDate", filterable: { ui: datepicker }, format: "{0: MM/dd/yyyy}", title: "End Date", width: "110px" },
            { field: "sraManagers", filterable: false, title: "SRA Managers", width: "140px", template: $("#panelsTemplate").html() },
            { field: "workDescription", filterable: false, title: "Work Description", width: "160px" },
            { field: "clientId", hidden: true },
            { field: "programYearId", hidden: true }],
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
            pageSize: 20
        },
        dataBound: dataBound
    });
    // Panels Selection
    function dataBound() {
        $('.panelsSelection').on('click', function () {
            $.get("/Setup/UpdatePanel", function (data) {
                var title = '<span class="modalSmallCaption modalNotificationCaption">Update Panel</span>';
                p2rims.modalFramework.displayModalNoEvent(data, p2rims.modalFramework.customModalSizes.medium, title);
            });
        });
    }

    // Yes/No Drop down for Filter
    function yesFilter(element) {
        var form = element.closest("form");
        form.find(".k-filter-help-text:first").text("Select an item from the list:");
        form.find("select").remove();
        element.kendoDropDownList({
            dataSource: ["Yes", "No"]
        });
    }

    // Active program filter
    $(function () {
        filterActiveSessions(true);
        $("#activeCheckbox").on("change", function () {
            var checked = $(this).prop("checked");
            filterActiveSessions(checked);
            // Total data in Grid
            $('#totalRecords span').text($("#ss-grid-session").data("kendoGrid").dataSource.total());
        });
    })

    // Formated Datepicker for Filter
    function datepicker(element) {
        var form = element.closest("form");
        form.find(".k-filter-help-text:first").text("Select an item from the list:");
        form.find("select").remove();
        element.kendoDatePicker({
            format: "0: MM/dd/yyyy"
        })
    }

    // This is where we added the same toolbar thats on the bottom of the grid to the top
    var grid = $("#ss-grid-session").data("kendoGrid");
    var wrapper = $('<div class="k-pager-wrap k-grid-pager pagerTop"/>').insertBefore(grid.element.children(".k-grid-header"));
    grid.pagerTop = new kendo.ui.Pager(wrapper, $.extend({}, grid.options.pageable, { dataSource: grid.dataSource }));
    grid.element.height("").find(".pagerTop").css("border-width", "0 0 1px 0");

    // Refresh button handler
    $('a.k-pager-refresh').on('click', function (e) {
        e.preventDefault();
        setSessionGrid();
    });

    var meetingsJson;
    // Set meeting drop-down
    function setMeetings() {
        $("#meetingSelection").find("option:gt(0)").remove();
        $.each(meetingsJson, function (i, item) {
            $("#meetingSelection").append($("<option/>")
                .attr("value", item.Key).html(item.Value));
        });
    }
    // Client drop-down change event handler
    $("#clientSelection").on("change", function () {
        var clientId = $("#clientSelection").val();
        if (clientId != "") {
            $.ajax({
                url: '/Setup/GetMeetingsJson',
                data: { clientId: clientId }
            }).done(function (results) {
                meetingsJson = results;
                setMeetings();
            });
        }
    });

    // "Find" button event handler
    $(".ss-filterBox_session-table_buttonShell_buttonPrimary").on("click", function (e) {
        e.preventDefault();
        dropDownValidation();
        var validationErrorVis = !$('.input-validation-error').is(':visible');
        if (validationErrorVis) {
            setSessionGrid();
        }
    });
});

// Set session grid data
function setSessionGrid() {
    $.ajax({
        url: '/Setup/GetSessionFeeSchedulesJsonString',
        data: {
            meetingSessionId: $("#sessionId").val()
        }
    }).done(function (results) {
        if (results.length > 3) {
            var ds = populateSessionDataSource(JSON.parse(results));
            var grid = $("#ss-grid-session").data("kendoGrid");
            ds.read();
            grid.setDataSource(ds);
            // Total data in Grid
            $('#totalRecords span').text(ds.total());
            $('.ss-grid-session').addClass('displayBlock');
            $('.ss-grid-session_message').removeClass('displayBlock');
            $('#removeFeeSchedule').attr('disabled', false);
            $('#removeFeeSchedule').addClass('enabled');
            var uploadedBy = ds.options.data[0].uploadedBy;
            var uploadedDate = ds.options.data[0].uploadedDate;
            $('#uploadedBy').text(uploadedBy);
            $('#uploadedDate').text(uploadedDate);
        } else {
            $("#ss-grid-session").data('kendoGrid').dataSource.data([]);
            $('.ss-grid-session_message').addClass('displayBlock');
            $('.ss-grid-session').removeClass('displayBlock');
            $('#removeFeeSchedule').attr('disabled', 'disabled');
            $('#removeFeeSchedule').removeClass('enabled');
            $('#uploadedBy').text('');
            $('#uploadedDate').text('');
        }
    });
}

$('#uploadFeeSchedule').on('click', function () {
    dropDownValidation();
    var validationErrorVis = !$('.input-validation-error').is(':visible');
    if (validationErrorVis) {
        fnUploadFeeSchedule();
    }
});

var fnUploadFeeSchedule = function () {
    dropDownValidation();

    var validationErrorVis = !$('.input-validation-error').is(':visible');
    if (validationErrorVis) {
        var title = '<span class="modalSmallCaption modalNotificationCaption">Upload Fee Schedule</span>';
        $.get("/Setup/UploadFeeSchedule", function (data) {
            p2rims.modalFramework.displayModalNoEvent(data, p2rims.modalFramework.customModalSizes.large, title);
            //setFeeSchedulesGrid();
        });
    }
};

// Drop down validations
var dropDownValidation = function () {
    // Validation for select fields
    ($('#clientId option:selected').filter(':contains("Select")').length > 0) ? $('.ss-filterBox_session-table_clientSelect select').addClass('input-validation-error') : $('.ss-filterBox_session-table_clientSelect select').removeClass('input-validation-error');
    ($('#fiscalYearId option:selected').filter(':contains("Select")').length > 0) ? $('.ss-filterBox_session-table_yearSelect select').addClass('input-validation-error') : $('.ss-filterBox_session-table_yearSelect select').removeClass('input-validation-error');
    ($('#meetingId option:selected').filter(':contains("Meeting")').length > 0) ? $('.ss-filterBox_session-table_meetingSelect select').addClass('input-validation-error') : $('.ss-filterBox_session-table_meetingSelect select').removeClass('input-validation-error');
    ($('#sessionId option:selected').filter(':contains("Session")').length > 0) ? $('.ss-filterBox_session-table_sessionSelect select').addClass('input-validation-error') : $('.ss-filterBox_session-table_sessionSelect select').removeClass('input-validation-error');

}

// Remove Fee Schedule
$('#removeFeeSchedule').on('click', function () {
    dropDownValidation();
    var validationErrorVis = !$('.input-validation-error').is(':visible');
    if (validationErrorVis) {
        var title = '<span class="modalSmallCaption modalWarningCaption">Remove Fee Schedule</span>';
        $.get("/Setup/RemoveWarning", function (data) {
            p2rims.modalFramework.displayModalNoEvent(data, p2rims.modalFramework.customModalSizes.small, title);
            var made = "The fee schedules will be removed from the list. Please click CONFIRM to permanently remove them from the list.";
            $('.row-fluid p').append(made);
            $("a[id='closeModal'], button[id='closeDialogBtn']").click(function () { $('.ui-dialog-titlebar-close').click(); });
            $("button[id='saveDialogChanges']").click(function () {
                $('#saveDialogChanges').prop("disabled", true);
                var meetingSessionId = $('#sessionId').val();
                $.ajax({
                    cache: false,
                    type: "POST",
                    url: '/Setup/DeleteSessionFeeSchedule',
                    data: { meetingSessionId: meetingSessionId },
                    success: function (data) {
                        if (!data.flag) {
                            for (var i = 0; i < data.messages.length; i++) {
                                $("#warningAlert").append($("<div/>").html(data.messages[i]));
                                $('#saveDialogChanges').prop("disabled", true);
                                $('.ss-grid-session, .ss-grid-session_message').addClass('displayBlock');
                            }
                        } else {
                            $('.ui-dialog-titlebar-close').click();
                            $("#ss-grid-session").data('kendoGrid').dataSource.data([]);
                            $('.ss-grid-session').removeClass('displayBlock');
                            $('.ss-grid-session_message').addClass('displayBlock');
                            $('#removeFeeSchedule').attr('disabled', 'disabled');
                            $('#uploadedBy').text('');
                            $('#uploadedDate').text('');
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
    }
})

var savedFiscalYear = $("#fiscalYearId").val();
var savedSessionId = $("#sessionId").val();
sessionStorage.setItem('savedFiscalYear', savedFiscalYear);
sessionStorage.setItem('savedSessionId', savedSessionId);