// Initialize
var clientsJson, fiscalYearsJson, meetingTypesJson, meetingListJson, programYearsJson, sessionJson;
var clientSelect = $('#clientId'),
    yearSelect = $('.ss-filterBox_session-table_yearSelect__select'),
    meetingSelect = $('.ss-filterBox_fee-table_meetingSelect_select'),
    programSelect = $('.ss-filterBox_session-table_programSelect__select');

$('#uploadFeeSchedule').attr('disabled', true);
$('#removeFeeSchedule').attr('disabled', true);

// Client drop-down change event handler
$("#clientId").on("change", function () {
    if ($('#clientId').val() !== "") {
        $.ajax({
            url: '/Setup/GetFiscalYearsJson',
            data: { clientId: $("#clientId").val() }
        }).done(function (results) {
            fiscalYearsJson = results;
            var activeOnly = $("#activeFyFilter").prop("checked");
            setFiscalYears(activeOnly);
        });
    }
    clearDropdowns("clientId");
});

// Fiscal year drop-down change event handler
$("#fiscalYearId").on("change", function () {
    $.ajax({
        url: '/Setup/GetProgramYearsJson',              
        data: {
            clientId: $("#clientId").val(),
            fiscalYear: $("#fiscalYearId").val()
        }
    }).done(function (results) {
        programYearsJson = results;
        clearDropdowns("fiscalYearId");
        var activeOnly = $("#activeFyFilter").prop("checked");
        setProgramYears(activeOnly);
    });
});


// Program drop-down change event handler
$("#programYearId").on("change", function () {
    $.ajax({
        url: '/Setup/GetMeetingTypesJson',                       
        data: {
            clientId: $("#clientId").val(),
            fiscalYear: $("#fiscalYearId").val()
        }
    }).done(function (results) {
        meetingTypesJson = results;
        meetingListJson = null;
        sessionJson = null; 
        clearDropdowns("programYearId");
        setMeetingTypeList();
    });
});

// Meeting type drop-down change event
$("#meetingId").on("change", function () {
    if ($('#meetingId').val() !== "") {
        $.ajax({
            url: '/Setup/GetFeeScheduleMeetingsJson',
            data: {
                clientId: $("#clientId").val(),
                fiscalYear: $("#fiscalYearId").val(),
                programYearId: $("#programYearId").val(),
                meetingTypeId: $("#meetingId").val()
            }
        }).done(function (results) {
            meetingListJson = results;
            $('#uploadFeeSchedule').attr('disabled', false);
            $('#uploadFeeSchedule').addClass('enabled');
            $('#uploadFeeSchedule').css("color", "blue");
             $.each(results, function (i, item) {
                $("#meetingNameId").append($("<option/>")
                    .attr("value", item.Key).html(item.Value));
            });

        });
    }
    // clear the remaining filter fields
    clearDropdowns("meetingId");
    $("#sessionId").find("option:gt(0)").remove();
    $("#meetingNameId").find("option:gt(0)").remove();

});


// Meeting drop-down change event
$("#meetingNameId").on("change", function () {
    if ($('#meetingNameId option:selected').filter(':contains("Select")').length === 0) {
        $.ajax({
            url: '/Setup/GetSessionsFromMeetingJson',
            data: {
                clientMeetingId: $("#meetingNameId").val(),
                programYearId: $("#programYearId").val()
        }
        }).done(function (results) {
           sessionJson = results;
            $("#sessionId").find("option:gt(0)").remove();
            $.each(results, function (i, item) {
            $("#sessionId").append($("<option/>")
                .attr("value", item.Key).html(item.Value));
        });

        });
    }
    clearDropdowns("meetingNameId");
});


// Session drop-down change event
$('#sessionId').on("change", function () {
    $('.ss-filterBox_fee-table_sessionSelect select').removeClass('input-validation-error');
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

// Set program years drop-down
function setProgramYears(activeOnly) {
    $("#programYearId").find("option:gt(0)").remove();
    $.each(programYearsJson, function (i, item) {
        if (!activeOnly || (activeOnly && item.IsActive)) {
            $("#programYearId").append($("<option/>")
                .attr("value", item.ProgramYearId).html(item.ProgramAbbr)
                .attr("data-name", item.ProgramName));
        }
    });
}

// Set fee schedule meeting drop-down
function setMeetingList() {
    $("#meetingNameId").find("option:gt(0)").remove();
    $.each(meetingListJson, function (i, item) {
        $("#meetingNameId").append($("<option/>")
            .attr("value", item.Index).html(item.Value));
    });
}

function setMeetingTypeList() {
    $("#meetingId").find("option:gt(0)").remove();
    $.each(meetingTypesJson, function (i, item) {
        $("#meetingId").append($("<option/>")
            .attr("value", item.Key).html(item.Value));
    });
}


function setSessionList() {
    $("#sessionId").find("option:gt(0)").remove();
    $.each(sessionJson, function (i, item) {
        $("#sessionId").append($("<option/>")
            .attr("value", item.Index).html(item.Value));
    });
}


// Populate fee schedule data source
function populateFeeScheduleDataSource(dataJson) {
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
                    particpantType: { type: "string" },
                    consultantFeeText: { type: "string" },
                    paid: { type: "string" },
                    fee: { type: "number" },
                    startDate: { type: "date" },
                    endDate: { type: "date" },
                    sraManagers: { type: "string" },
                    workDescription: { type: "string" }
                }
            }
        }
    });
    return dataSource;
}

// Program grid
$(function () {
    $("#ss-grid-feeSchedule").kendoGrid({
        height: 550,
        resizable: true,
        toolbar: ["create"],
        columns: [
            { field: "index", filterable: false, title: "#", width: "30px" },
            { field: "participantType", filterable: { multi: true }, title: "Participant Type", width: "150px" },
            { field: "consultantFeeText", filterable: true, title: "Consultant Fee Text", width: "180px" },
            { field: "paid", filterable: { multi: true }, title: "Paid", width: "85px" },
            {
                field: "fee", format: "{0:c2}", filterable:
                    {
                        ui: function (element) {
                            element.kendoNumericTextBox({
                                format: 'c2'
                            });
                            var form = element.closest("form");
                            form.find(".k-filter-help-text:first").text("Enter an amount:");
                            form.find("select").remove();
                         }
                    }, title: "Fee", width: "70px"
            },
            { field: "startDate", filterable: { ui: "datepicker" }, format: "{0: MM/dd/yyyy}", title: "Start Date", width: "110px" },
            { field: "endDate", filterable: { ui: "datepicker" }, format: "{0: MM/dd/yyyy}", title: "End Date", width: "110px" },
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
            responsive: false,
            pageSize: 20
        }
    });

    // Active fiscal year filter
    $("#activeFyFilter").on("change", function () {
        var activeOnly = $(this).prop("checked");
        setFiscalYears(activeOnly);
        setProgramYears(activeOnly);
        setMeetingTypeList();
        setMeetingList();
        setSessionList();
    });

    // Formated Datepicker for Filter
    function datepicker(element) {
        var form = element.closest("form");
        form.find(".k-filter-help-text:first").text("Select an item from the list:");
        form.find("select").remove();
        element.kendoDatePicker({
            format: "0: MM/dd/yyyy"
        });
    }

    // Yes/No Drop down for Filter
    //function yesFilter(element) {
    //    var form = element.closest("form");
    //    form.find(".k-filter-help-text:first").text("Select an item from the list:");
    //    form.find("select").remove();
    //    element.kendoDropDownList({
    //        dataSource: ["Yes", "No"]
    //    });
    //}

    // This is where we added the same toolbar thats on the bottom of the grid to the top
    var grid = $("#ss-grid-feeSchedule").data("kendoGrid");
    var wrapper = $('<div class="k-pager-wrap k-grid-pager pagerTop"/>').insertBefore(grid.element.children(".k-grid-header"));
    wrapper.kendoPager($.extend({}, grid.options.pageable, { dataSource: grid.dataSource }));
    grid.element.height("").find(".pagerTop").css("border-width", "0 0 1px 0");

    // Refresh button handler
    $('a.k-pager-refresh').on('click', function (e) {
        e.preventDefault();
        setFeeSchedulesGrid();
    });

    // "Find" button for session fee schedule grid
    $(".ss-filterBox_award-table_buttonShell_buttonPrimary").on("click", function (e) {
        e.preventDefault();
        dropDownValidation();

        var savedFiscalYear = $("#fiscalYearId").val();
        var savedSessionId = $("#sessionId").val();
        sessionStorage.setItem('savedFiscalYear', savedFiscalYear);
        sessionStorage.setItem('savedSessionId', savedSessionId);

        var validationErrorVis = !$('.input-validation-error').is(':visible');
        if (validationErrorVis) {
            setFeeSchedulesGrid();
        }
        else {
            // clean up here
            $('.ss-grid-feeSchedule_message').removeClass('displayBlock');
            $("#ss-grid-feeSchedule").data('kendoGrid').dataSource.data([]);
            $('.ss-grid-feeSchedule').removeClass('displayBlock');
        }
    });

});

// Set fee schedule grid data
function setFeeSchedulesGrid() {
    $.ajax({
        url: '/Setup/GetClientFeeSchedulesJsonString',
        data: {
            programYearId: $("#programYearId").val(),
            meetingTypeId: $("#meetingId").val(),
            sessionId: $("#sessionId").val()
        }
    }).done(function (results) {
        if (results.length > 3) {
            var ds = populateFeeScheduleDataSource(JSON.parse(results));
            var grid = $("#ss-grid-feeSchedule").data("kendoGrid");
            ds.read();
            grid.dataSource.data(ds.data());
            grid.refresh();
    //        grid.setDataSource(ds);
            // Total data in Grid
            $('#totalRecords span').text(ds.total());
            $('.ss-grid-feeSchedule').addClass('displayBlock');
            $('.ss-grid-feeSchedule_message').removeClass('displayBlock');
            $('#removeFeeSchedule').attr('disabled', false);
            $('#removeFeeSchedule').addClass('enabled');
            $('#removeFeeSchedule').css("color", "blue");
            var uploadedBy = ds.options.data[0].uploadedBy;
            var uploadedDate = ds.options.data[0].uploadedDate;
            $('#uploadedBy').text(uploadedBy);
            $('#uploadedDate').text(uploadedDate);
        } else {
            $("#ss-grid-feeSchedule").data('kendoGrid').dataSource.data([]);
            $('.ss-grid-feeSchedule_message').addClass('displayBlock');
            $('.ss-grid-feeSchedule').removeClass('displayBlock');
            DisableRemoveFeeSchedule();
            $('#uploadedBy').text('');
            $('#uploadedDate').text('');
        }
    });
}

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
                var programYearId = $('#programYearId').val();
                var meetingTypeId = $('#meetingId').val();
                $.ajax({
                    cache: false,
                    type: "POST",
                    url: '/Setup/DeleteProgramFeeSchedule',
                    data: { programYearId: programYearId, meetingTypeId: meetingTypeId, sessionId: $('#sessionId').val() },
                    success: function (data) {
                        if (!data.flag) {
                            for (var i = 0; i < data.messages.length; i++) {
                                $("#warningAlert").append($("<div/>").html(data.messages[i]));
                                $('#saveDialogChanges').prop("disabled", true);
                                $('.ss-grid-feeSchedule, .ss-grid-feeSchedule_message').addClass('displayBlock');
                            }
                        } else {
                            $('.ui-dialog-titlebar-close').click();
                            $("#ss-grid-feeSchedule").data('kendoGrid').dataSource.data([]);
                            $('.ss-grid-feeSchedule').removeClass('displayBlock');
                            $('.ss-grid-feeSchedule_message').addClass('displayBlock');
                            DisableRemoveFeeSchedule();
                            //$('#removeFeeSchedule').attr('disabled', 'disabled');
                            //$('#removeFeeSchedule').css("color", "grey");
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

$('#uploadFeeSchedule').on('click', function () {
    dropDownValidation();
    var validationErrorVis = !$('.input-validation-error').is(':visible');
    if (validationErrorVis) {
        fnUploadFeeSchedule();
    }
});

// Drop down validations
var dropDownValidation = function () {
    // Validation for select fields Session Fee page
    ($('#clientId option:selected').filter(':contains("Select")').length > 0) ? $('.ss-filterBox_fee-table_clientSelect select').addClass('input-validation-error') : $('.ss-filterBox_fee-table_clientSelect select').removeClass('input-validation-error');
    ($('#fiscalYearId option:selected').filter(':contains("Select")').length > 0) ? $('.ss-filterBox_session-table_yearSelect select').addClass('input-validation-error') : $('.ss-filterBox_session-table_yearSelect select').removeClass('input-validation-error');
    ($('#programYearId option:selected').filter(':contains("Select")').length > 0) ? $('.ss-filterBox_session-table_programSelect select').addClass('input-validation-error') : $('.ss-filterBox_session-table_programSelect select').removeClass('input-validation-error');
    ($('#meetingId option:selected').filter(':contains("Select")').length > 0) ? $('.ss-filterBox_fee-table_meetingSelect select').addClass('input-validation-error') : $('.ss-filterBox_fee-table_meetingSelect select').removeClass('input-validation-error');
    if ($('#meetingNameId option:selected').filter(':contains("Select")').length === 0) {
       ($('#sessionId option:selected').filter(':contains("Select")').length > 0) ? $('.ss-filterBox_session-table_sessionSelect select').addClass('input-validation-error') : $('.ss-filterBox_session-table_sessionSelect select').removeClass('input-validation-error');
    }
  
};

// Clear drop downs
function clearDropdowns(elemName) {
    var activeOnly = $(this).prop("checked");
     DisableRemoveFeeSchedule();
    switch (elemName) {
        case "clientId":
            $('.ss-filterBox_fee-table_clientSelect select').removeClass('input-validation-error');
            $('.ss-filterBox_session-table_yearSelect select').removeClass('input-validation-error');
            $('.ss-filterBox_session-table_programSelect select').removeClass('input-validation-error');
            $('.ss-filterBox_fee-table_meetingSelect select').removeClass('input-validation-error');
            $('.ss-filterBox_fee-table_meetingNameSelect select').removeClass('input-validation-error');
            $('.ss-filterBox_session-table_sessionSelect select').removeClass('input-validation-error');
            setFiscalYears(activeOnly);
            setProgramYears(activeOnly);
            setMeetingTypeList();
            setMeetingList();
            setSessionList();
            DisableUploadFeeSchedule();
            break;
        case "fiscalYearId":
            $('.ss-filterBox_session-table_yearSelect select').removeClass('input-validation-error');
            $('.ss-filterBox_session-table_programSelect select').removeClass('input-validation-error');
            $('.ss-filterBox_fee-table_meetingSelect select').removeClass('input-validation-error');
            $('.ss-filterBox_fee-table_meetingNameSelect select').removeClass('input-validation-error');
            $('.ss-filterBox_session-table_sessionSelect select').removeClass('input-validation-error');
            setProgramYears(activeOnly);
            setMeetingTypeList();
            setMeetingList();
            setSessionList();
            DisableUploadFeeSchedule();
           break;
        case "programYearId":
            $('.ss-filterBox_session-table_programSelect select').removeClass('input-validation-error');
            $('.ss-filterBox_fee-table_meetingSelect select').removeClass('input-validation-error');
            $('.ss-filterBox_fee-table_meetingNameSelect select').removeClass('input-validation-error');
            $('.ss-filterBox_session-table_sessionSelect select').removeClass('input-validation-error');
            setMeetingTypeList();
            setMeetingList();
            setSessionList();
            DisableUploadFeeSchedule();
           break;
        case "meetingId":
            $('.ss-filterBox_fee-table_meetingSelect select').removeClass('input-validation-error');
            $('.ss-filterBox_fee-table_meetingNameSelect select').removeClass('input-validation-error');
            $('.ss-filterBox_session-table_sessionSelect select').removeClass('input-validation-error');
            setMeetingList();
            setSessionList();
           break;
        case "meetingNameId":
            $('.ss-filterBox_fee-table_meetingNameSelect select').removeClass('input-validation-error');
            $('.ss-filterBox_session-table_sessionSelect select').removeClass('input-validation-error');
            setSessionList();
            break;
        default:
            break;
    }
}

function DisableUploadFeeSchedule() {
    $('#uploadFeeSchedule').attr('disabled', true);
    $('#uploadFeeSchedule').removeClass('enabled');
    $('#uploadFeeSchedule').css("color", "grey");
}

function DisableRemoveFeeSchedule() {
    $('#removeFeeSchedule').attr('disabled', true);
    $('#removeFeeSchedule').removeClass('enabled');
    $('#removeFeeSchedule').css("color", "grey");
}




