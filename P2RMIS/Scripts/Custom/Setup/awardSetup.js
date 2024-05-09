var programSetupPage_clientId = $("#programSetupPage_clientId").val();
var programSetupPage_fiscalYear = $("#programSetupPage_fiscalYear").val();
var programSetupPage_isActive = $("#programSetupPage_isActive").val();

if (sessionStorage.getItem("SS_ActiveProgram") !== null) {
    // Active or not 
    if ($.toBoolean(sessionStorage.getItem("SS_ActiveProgram"))) {
        $('#activeCheckbox').attr('checked', 'checked');
    } else {
        $('#activeCheckbox').attr('checked', false);
    }
}

    // Initialize
    var clientsJson, fiscalYearsJson, programYearsJson;
    var activeCheckbox = $('#activeCheckbox'),
        selectedClient = $('.ss-filterBox_award-table_clientSelect__select :selected'),
        selectedYear = $('.ss-filterBox_award-table_yearSelect__select :selected'),
        selectedProgram = $('.ss-filterBox_award-table_programSelect__select :selected'),
        clientSelect = $('.ss-filterBox_award-table_clientSelect__select'),
        yearSelect = $('.ss-filterBox_award-table_yearSelect__select'),
        programSelect = $('.ss-filterBox_award-table_programSelect__select');

    // Grabbing Values for Selected Box
    $('.ss-filterBox_award-table_buttonShell_buttonPrimary').click(function () {
        var selectedClient = $('.ss-filterBox_award-table_clientSelect__select :selected'),
            clientValNext = $('#selectedClient'),
            yearValNext = $('#selectedProgram'),
            activeValNext = $('#selectedActive'),
            selectedYear = $('.ss-filterBox_award-table_yearSelect__select :selected'),
            selectedProgram = $('.ss-filterBox_award-table_programSelect__select :selected');

        var selectedOption = $('.ss-filterBox_award-table option:selected').text();
        if (selectedOption.indexOf('Select') >= 0) {
            (selectedClient.filter(':contains("Select")').length > 0) ? clientSelect.addClass('input-validation-error') : clientSelect.removeClass('input-validation-error');
            (selectedProgram.filter(':contains("Select")').length > 0) ? programSelect.addClass('input-validation-error') : programSelect.removeClass('input-validation-error');
            (selectedYear.filter(':contains("Select")').length > 0) ? yearSelect.addClass('input-validation-error') : yearSelect.removeClass('input-validation-error');
            return false;
        } else {
            $('.ss-filterBox_award-table select').removeClass('input-validation-error');
        }

        $(clientValNext).html($(selectedClient).text());
        $(yearValNext).text(selectedYear.text() + " - " + selectedProgram.text());
        var isActive = $('#activeCheckbox').attr('checked');
        (isActive) ? activeValNext.text('Yes') : activeValNext.text('No');

        // Displays grid
        if ($('#selectedClient:contains("Select")').length > 0 || $('#selectedProgram:contains("Select")').length > 0 || $('#selectedActive:contains("Select")').length > 0) {
            $('.ss-gridHeader').hide();
            $('#grid').hide();
            $('.afterComment').show();
        } else {
            $('.ss-gridHeader').show();
            $('#grid').show();
            $('.afterComment').hide();
            setAwardGrid();
        }
    });

    // Add award
    $('.ss-gridHeader_addAwardButton').on('click', function (e) {  //add a click event listener on the delete button
        e.preventDefault();
        e.stopPropagation();
        var clientId = clientSelect.val(),
            inputData = { clientId: clientId },
            title = '<span class="modalSmallCaption modalNotificationCaption">Add Award</span>',
            el = $("#grid"),
            row = el.find("tbody>tr");

        if ($(row).hasClass("gridHighlight")) {
            $(row).removeClass("gridHighlight");
        }

        var title = '<span class="modalSmallCaption modalNotificationCaption">Assign Award</span>';
        $.get("/Setup/AwardWizard", { clientId: clientId }, function (data) {
            $('#addAwardPanel').show();
            p2rims.modalFramework.displayModalNoEvent(data, p2rims.modalFramework.customModalSizes.medium, title);
            $('#datePicker').kendoDatePicker();
            var clientValNext = $('#selectedClient').text(),
                programValNext = $('#selectedProgram').text(),
                activeValNext = $('#selectedActive').text();

            $('#clientAwardModal').html(clientValNext);
            $('#programAwardModal').html(programValNext);
            $('#activeAwardModal').html(activeValNext);
            $('#blindedID').val('No').prop('selected', true).change();
            $('#preApp').val('No').prop('selected', true).change();
        });
    });

    // Get clients
    function getClients() {
        $.ajax({
            url: '/Setup/GetClientsJson'
        }).done(function (results) {
            clientsJson = results;
            if (sessionStorage.getItem("SS_ActiveProgram") !== null) {
                // Active or not 
                if ($.toBoolean(sessionStorage.getItem("SS_ActiveProgram"))) {
                    $('#activeCheckbox').attr('checked', 'checked');
                } else {
                    $('#activeCheckbox').attr('checked', false);
                }
                var activeOnly = activeCheckbox.prop("checked");
                setClients(activeOnly);

                // Client
                $('.ss-filterBox_award-table_clientSelect__select option').each(function (i, value) {
                    if ($(this).val() == sessionStorage.getItem("SS_ClientId")) {
                        $('.ss-filterBox_award-table_clientSelect__select').val($(this).val());
                    }
                });
            } else {
                activeOnly = activeCheckbox.prop("checked");
                setClients(activeOnly);
            }
        });
    }

// Set clients drop-down
function setClients(activeOnly) {
    clientSelect.find("option:gt(0)").remove();
    $.each(clientsJson, function (i, item) {
        if (!activeOnly || (activeOnly && item.IsActive)) {
            clientSelect.append($("<option/>")
                .attr("value", item.ClientId).html(item.ClientName));
        }
    });
    clientSelect.val("");

    if (sessionStorage.getItem("SS_ClientId") != null) {
        clientSelect.val(sessionStorage.getItem("SS_ClientId"));
        FillFiscalYear();
    }
}
// Get clients on load
getClients();
    
// Set fiscal years drop-down
function setFiscalYears(activeOnly) {
    $.each(fiscalYearsJson, function (i, item) {
        if (!activeOnly || (activeOnly && item.IsActive)) {
            yearSelect.append($("<option/>")
                .attr("value", item.YearValue).html(item.YearText));
        }
    });

    var sessionYear = sessionStorage.getItem("SS_FiscalYear");
    if (sessionYear !== "" && sessionYear !== null ) {
        yearSelect.val(sessionYear);
        FillProgram();
    }

    // In case the value is in memory
    if (sessionStorage.getItem("SS_FiscalYear") !== null) {
        $('.ss-filterBox_award-table_yearSelect__select option').each(function (i, value) {
            if ($(this).val() == sessionStorage.getItem("SS_FiscalYear")) {
                $('.ss-filterBox_award-table_yearSelect__select').val($(this).val());
            }
        });
    }
}

// Client drop-down change event handler
clientSelect.on("change", function () {
    sessionStorage.setItem("SS_ClientId", clientSelect.val());
    sessionStorage.removeItem("SS_FiscalYear");
    sessionStorage.removeItem("SS_ProgramYearId");
    FillFiscalYear();
});

function FillFiscalYear() {
    yearSelect.find("option:gt(0)").remove();
    programSelect.find("option:gt(0)").remove();
    if (clientSelect.val() != "") {
        $.ajax({
            url: '/Setup/GetFiscalYearsJson',
            data: { clientId: clientSelect.val() }
        }).done(function (results) {
            fiscalYearsJson = results;
            var activeOnly = activeCheckbox.prop("checked");
            setFiscalYears(activeOnly);
        });
    }
}

// Set program years drop-down
function setProgramYears(activeOnly) {
    $.each(programYearsJson, function (i, item) {
        if (!activeOnly || (activeOnly && item.IsActive)) {
            programSelect.append($("<option/>")
                .attr("value", item.ProgramYearId).html(item.ProgramAbbr)
                .attr("data-name", item.ProgramName));
        }
    });

    // In case the value is in memory
    if (sessionStorage.getItem("SS_ProgramYearId") !== null) {
        $('.ss-filterBox_award-table_programSelect__select').val(sessionStorage.getItem("SS_ProgramYearId"));
        $('.ss-filterBox_award-table_buttonShell_buttonPrimary').click();
    }
}

// Fiscal year drop-down change event handler
yearSelect.on("change", function () {
    var fy = yearSelect.val();
    sessionStorage.setItem("SS_FiscalYear", fy);
    sessionStorage.removeItem("SS_ProgramYearId");
    if (fy !== "") {
        FillProgram();
    }
});

function FillProgram() {
    programSelect.find("option:gt(0)").remove();
    $.ajax({
        url: '/Setup/GetProgramYearsJson',
        data: {
            clientId: clientSelect.val(),
            fiscalYear: yearSelect.val()
        }
    }).done(function (results) {
        programYearsJson = results;
        var activeOnly = activeCheckbox.prop("checked");
        setProgramYears(activeOnly);
    });
}

// Program drop-down change event handler
programSelect.on("change", function () {
    var programYearId = programSelect.val();
    sessionStorage.setItem("SS_ProgramYearId", programYearId);
});


// Active filter
activeCheckbox.on("change", function () {
    var activeOnly = $(this).prop("checked");
    // Memory values
    sessionStorage.setItem("SS_ActiveProgram", activeOnly);
    sessionStorage.removeItem("SS_ClientId");
    sessionStorage.removeItem("SS_FiscalYear");
    sessionStorage.removeItem("SS_ProgramYearId");
    setClients(activeOnly);
    yearSelect.find("option:gt(0)").remove();
    programSelect.find("option:gt(0)").remove();
    $(".input-validation-error").removeClass("input-validation-error");
});

// Export link event handler
$("#btnExport").click(function (e) {
    showFileDownloadWarning(function (param) {
        var grid = $("#grid").data("kendoGrid");
        grid.hideColumn("index");
        grid.showColumn("clientAbbreviation");
        grid.showColumn("fiscalYear");
        grid.showColumn("programAbbreviation");
        grid.showColumn("programYearId");
        grid.showColumn("awardDescription");
        grid.showColumn("legacyAwardTypeId");
        grid.showColumn("legacyAtmId");
        grid.saveAsExcel();
        grid.showColumn("index");
        grid.hideColumn("clientAbbreviation");
        grid.hideColumn("fiscalYear");
        grid.hideColumn("programAbbreviation");
        grid.hideColumn("programYearId");
        grid.hideColumn("awardDescription");
        grid.hideColumn("legacyAwardTypeId");
        grid.hideColumn("legacyAtmId");
    }, null);
});

    // Set up kendo grid
    $("#grid").kendoGrid({
        height: 550,
        resizable: true,
        toolbar: ["create"],
        columns: [
            { field: "index", filterable: false, title: "#", width: "45px" },
            { field: "clientAbbreviation", hidden: true, title: "Client" },
            { field: "fiscalYear", hidden: true, title: "FY" },
            { field: "programAbbreviation", hidden: true, title: "Program" },
            { field: "programYearId", hidden: true, title: "Program ID" },
            { field: "cycle", filterable: { multi: true }, title: "Cycle", width: "100px" },
            { field: "oppID", filterable: true, title: "Opp ID", width: "160px" },
            { field: "awardAbbr", filterable: true, title: "Award Mechanism", width: "200px", template: "${awardAbbr} - ${legacyAwardTypeId}" },
            { field: "awardDescription", hidden: true, title: "Award Description" },
            { field: "legacyAwardTypeId", hidden: true, title: "Award Type ID" },
            { field: "legacyAtmId", hidden: true, title: "ATMID" },
            { field: "receipt", format: "{0:MM/dd/yyyy}", parseFormats: ["MM-dd-yyyy"], filterable: { ui: "datepicker" }, title: "Receipt", width: "125px" },
            { field: "partnering", filterable: { ui: yesFilter, extra: false, operators: false }, title: "Partnering", width: "130px", template: "<div class='alignCenter'>${partnering}</div>" },
            { field: "blinded", filterable: { ui: yesFilter }, title: "Blinded", width: "110px", template: "<div class='alignCenter'>${blinded}</div>" },    
            { field: "preAppCycle", filterable: false, title: "Pre-App Cycle", width: "130px", template: "<div class='alignCenter'>${preAppCycle}</div>" },
            { field: "criteriaCount", filterable: false, title: "Criteria", width: "95px", template: "<a href='\\#' onclick='criterionLink(\"#= programMechanismId #\");return false;' class='criterionLink'>${criteriaCount}</a>" },
            { field: "programMechanismId", hidden: true },
            { field: "parentProgramMechanismId", hidden: true },
            { field: "isPreAppCycle", hidden: true },
            { field: "hasApplications", hidden: true },
            {
                command: [
                    {
                        name: "editable",
                        text: "",
                        template: "<a class='k-button k-button-icontext k-grid-editable' href=''><img src='/Content/img/edit_20px_enabled.png' alt='edit icon' title='Edit Row'/></a>",
                        click: function (e) {  //add a click event listener on the button
                            e.preventDefault();
                            e.stopPropagation();

                            var idx = $(e.target).closest("tr").index() + 3,
                                dataItem = grid.dataItem("tr:eq(" + idx + ")"),
                                receiptCycle = dataItem.cycle,
                                oppID = dataItem.oppID,
                                blinded = dataItem.blinded,
                                receipt = dataItem.receipt,
                                partnering = dataItem.partnering,
                                awardName = dataItem.awardName,
                                awardAbbr = dataItem.awardAbbr,
                                programMechanismId = dataItem.programMechanismId,
                                parentProgramMechanismId = dataItem.parentProgramMechanismId,
                                isPreAppCycle = dataItem.isPreAppCycle,
                                criteriaCount = dataItem.criteriaCount,
                                hasApplications = dataItem.hasApplications;
                                
                            sessionStorage.setItem('programMechanismId', programMechanismId);

                            var clientId = $('.ss-filterBox_award-table_clientSelect option:selected').val();
                            var programYearId = $(".ss-filterBox_award-table_programSelect option:selected").val();
                            var inputDataAward = {
                                clientId: clientId,
                                programYearId: programYearId,
                                programMechanismId: programMechanismId,
                            };

                            if (criteriaCount > 0) {
                                $.get("/Setup/RemoveDisallowed", function (data) {
                                    p2rims.modalFramework.displayModalNoEvent(data, p2rims.modalFramework.customModalSizes.small,
                                        '<span class="modalSmallCaption modalWarningCaption">Warning</span>');
                                    $("a[id='closeModal'], button[id='closeDialogBtn']").click(function () { $('.ui-dialog-titlebar-close').click(); });
                                    var made = "You may not edit an award or pre-application award once evaluation criteria is assigned to the award.";
                                    $('.row-fluid p').append(made);
                                });
                            } else if (hasApplications) {
                                $.get("/Setup/RemoveDisallowed", function (data) {
                                    p2rims.modalFramework.displayModalNoEvent(data, p2rims.modalFramework.customModalSizes.small,
                                        '<span class="modalSmallCaption modalWarningCaption">Warning</span>');
                                    $("a[id='closeModal'], button[id='closeDialogBtn']").click(function () { $('.ui-dialog-titlebar-close').click(); });
                                    var made = "You may not edit an award or pre-application award once applications are assigned to the award.";
                                    $('.row-fluid p').append(made);
                                });
                            } else {
                                var title = parentProgramMechanismId !== null ? "Edit Pre-Application Award" : "Edit Award";
                                $.get("/Setup/AwardWizard", inputDataAward, function (data) {
                                    p2rims.modalFramework.displayModalNoEvent(data, p2rims.modalFramework.customModalSizes.medium,
                                        '<span class="modalSmallCaption modalNotificationCaption">' + title + '</span>');
                                    var clientValNext = $('#selectedClient'),
                                        programValNext = $('#selectedProgram'),
                                        activeValNext = $('#selectedActive');

                                    var dateobj = kendo.parseDate(receipt);
                                    var datestring = kendo.toString(dateobj, "MM/dd/yyyy");

                                    $('#clientAwardModal').html(clientValNext.text());
                                    $('#programAwardModal').html(programValNext.text());
                                    $('#activeAwardModal').html(activeValNext.text());
                                    $('#receiptCycle').val(receiptCycle);
                                    $('#oppIdValue').val(oppID);
                                    $('#awardAbbrID').val(awardAbbr);
                                    $('#partneringPiAllowedFlag').val(partnering);
                                    $('#blindedID').val(blinded);
                                    $('#datePicker').val(datestring);

                                    var el = $("#grid");
                                    var row = el.find("tbody>tr");
                                    if ($(row).hasClass("gridHighlight")) {
                                        $(row).removeClass("gridHighlight");
                                    }
                                    var uniqueID = dataItem.uid;
                                    var uniqueIDRow = el.find("tbody>tr[data-uid=" + uniqueID + "]");
                                    $(uniqueIDRow).addClass('gridHighlight');

                                    $("a[id='closeModal'], button[id='closeDialogBtn']").click(function () { $('.ui-dialog-titlebar-close').click(); });
                                    $('a.k-pager-refresh').click();
                                });
                            }                            
                        }
                    },
                    {
                        name: "Destroy",
                        text: "",
                        template: "<a class='k-button k-button-icontext k-grid-Destroy' href=''><img src='/Content/img/icon_remove_16x16.png' alt='edit icon' title='Delete Row'/></a>",
                        click: function (e) {  //add a click event listener on the button
                            e.preventDefault();
                            e.stopPropagation();
                            var title = '<span class="modalSmallCaption modalWarningCaption">Warning</span>',
                                grid = $("#grid").data("kendoGrid"),
                                idx = $(e.target).closest("tr").index() + 3,
                                dataItem = grid.dataItem("tr:eq(" + idx + ")"),
                                award = dataItem.awardAbbr,
                                criteriaCount = dataItem.criteriaCount,
                                hasApplications = dataItem.hasApplications,
                                hasPreApp = !isNaN(parseInt(dataItem.preAppCycle));

                            if (criteriaCount > 0 || hasApplications) {
                                $.get("/Setup/RemoveDisallowed", function (data) {
                                    p2rims.modalFramework.displayModalNoEvent(data, p2rims.modalFramework.customModalSizes.small,
                                        '<span class="modalSmallCaption modalWarningCaption">Warning</span>');
                                    $("a[id='closeModal'], button[id='closeDialogBtn']").click(function () { $('.ui-dialog-titlebar-close').click(); });
                                    var made = "You may not remove award " + award + " that has associated evaluation criteria and/or application assignments.";
                                    $('.row-fluid p').append(made);
                                });
                            } else if (hasPreApp) {
                                $.get("/Setup/RemoveDisallowed", function (data) {
                                    p2rims.modalFramework.displayModalNoEvent(data, p2rims.modalFramework.customModalSizes.small,
                                        '<span class="modalSmallCaption modalWarningCaption">Warning</span>');
                                    $("a[id='closeModal'], button[id='closeDialogBtn']").click(function () { $('.ui-dialog-titlebar-close').click(); });
                                    var made = "You may not modify an award that has a pre app award without deleting the pre app award first.";
                                    $('.row-fluid p').append(made);
                                });
                            } else {
                                $.get("/Setup/RemoveWarning", function (data) {
                                    p2rims.modalFramework.displayModalNoEvent(data, p2rims.modalFramework.customModalSizes.small, title);
                                    var made = "You have selected " + award + " to be removed from the list. Please click CONFIRM to permanently remove it from the list.";
                                    $('.row-fluid p').append(made);
                                    $("a[id='closeModal'], button[id='closeDialogBtn']").click(function () { $('.ui-dialog-titlebar-close').click(); });
                                    $("button[id='saveDialogChanges']").click(function () {
                                        $('#saveDialogChanges').prop("disabled", true);
                                        $.ajax({
                                            cache: false,
                                            type: "POST",
                                            url: '/Setup/DeleteAward',
                                            data: { programMechanismId: dataItem.programMechanismId },
                                            success: function (data) {
                                                if (!data.flag) {
                                                    for (var i = 0; i < data.messages.length; i++) {
                                                        $("#warningAlert").append($("<div/>").html(data.messages[i]));
                                                        $(".p2rmis-disallowed_p").remove();
                                                        $('#saveDialogChanges').prop("disabled", true);
                                                    }
                                                } else {
                                                    $('.ui-dialog-titlebar-close').click();
                                                    setAwardGrid();
                                                    $("#successMessage").html("Award " + award + " deleted successfully!").show();
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
                        }
                    },
                    {
                        name: "setup",
                        text: "",
                        template: "<a class='k-grid-setup' href=''><img src='/Content/img/Critique_20px_enabled.png' alt='Setup Summary Statement' title='Setup Summary Statement' /></a>",
                        click: function (e) {
                            e.preventDefault();
                            e.stopPropagation();
                            row = $(e.currentTarget).closest("tr"),
                            dataItem = this.dataItem(row);
                            programMechId = dataItem.programMechanismId;
                            console.info("Link executing");
                            window.location.href = "/Setup/SummarySetup?programMechanismId=" + programMechId;
                        }
                    } 
                ], title: "Action", 
            }],
        editable: { mode: "popup" },
        scrollable: true,
        groupable: true,
        sortable: true,
        dataBound: function dataBound(e) {
            var grid = $("#grid").data("kendoGrid");
            var gridData = grid.dataSource.view();

            for (var i = 0; i < gridData.length; i++) {
                var currentUid = gridData[i].uid;
                if (gridData[i].isPreAppCycle) {
                    var currenRow = grid.table.find("tr[data-uid='" + currentUid + "']");
                    var editButton = $(currenRow).find(".k-grid-addPreAppAward");
                    editButton.hide();
                }
            }

            // Check to see if PreApp Icon needs to be disabled
            var gridTotal = $("#grid").data("kendoGrid").dataSource._data;
            var obj = gridTotal.filter(function (obj) {
                var rowID, el, row;
                if (obj.preAppCycle === "NA") {
                    rowID = obj.uid;
                    el = $("#grid");
                    row = el.find("tbody>tr[data-uid=" + rowID + "]");
                    $(row).find('.k-grid-addPreAppAward img').attr('src', '/Content/img/panelicon.png');
                } else {
                    rowID = obj.uid;
                    el = $("#grid");
                    row = el.find("tbody>tr[data-uid=" + rowID + "]");
                    $(row).find('.k-grid-addPreAppAward img').attr('src', '/Content/img/panelicon_disabled.png');
                    $(row).find('.k-grid-addPreAppAward').css('cursor', 'not-allowed');
                    $(row).find('.k-grid-addPreAppAward').css('pointer-events', 'none');
                }
            });
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
        pageable: {
            input: true,
            numeric: false,
            refresh: true,
            responsive: false,
            pageSize: 20,
            pageSizes: true,
        },
        excelExport: function (e) {
            var sheet = e.workbook.sheets[0];
            var row = sheet.rows[0];
            row.cells[10].value = "Receipt Deadline";
        }
    });
    // Set award grid data
function setAwardGrid() {
    $("#successMessage").html("").hide();
    $("#failureMessage").html("").hide();
    var isActive = $('#activeCheckbox').attr('checked');
    var activeValNext = (isActive) ? 'Yes' : 'No';
    $('#mainContentArea, .afterComment, .add-award').addClass('hidden');
    $.ajax({
        url: '/Setup/GetAwardsJsonString',
        data: {
            programYearId: programSelect.val(),
            clientId: $('#ss-award-client option:selected').val(),
            fiscalYear: $('#ss-award-fy option:selected').val(),
            isActive: activeValNext
        }
    }).done(function (results) {
        if (results !== "" && JSON.parse(results) !== null &&
            JSON.parse(results).length > 0) {     
            var grid = $("#grid").data("kendoGrid");
            var ds = populateAwardDataSource(JSON.parse(results));
            ds.read();
            grid.dataSource.data(ds.data());
        //    grid.setDataSource(grid.dataSource);
            grid.refresh();
            // Total data in Grid
            var sourcedata = ds.data()
            $('#totalCount span').text(sourcedata.length);
            $('#mainContentArea').removeClass('hidden');
        } else {
            $('.add-award').removeClass('hidden');
        }
    });
}

    // This is where we added the same toolbar thats on the bottom of the grid to the top
    var grid = $("#grid").data("kendoGrid");
    var wrapper = $('<div class="k-pager-wrap k-grid-pager pagerTop"/>').insertBefore(grid.element.children(".k-grid-header"));
    wrapper.kendoPager($.extend({}, grid.options.pageable, { dataSource: grid.dataSource }));
    grid.element.height("").find(".pagerTop").css("border-width", "0 0 1px 0");

    // Refresh button that sets page to 1 after refresh
    $('a.k-pager-refresh').on('click', function (e) {
        e.preventDefault();
        setAwardGrid();
    });

    // Program Page linked in
    var programSetClient = sessionStorage.getItem('programSetClient');
    if (programSetClient !== null) {
        var programSetYear = sessionStorage.getItem('programSetYear');
        var programSetAbbr = sessionStorage.getItem('programSetAbbr');
    }

// Popultate award data source for grid
function populateAwardDataSource(dataJson) {
    var dataSource = new kendo.data.DataSource({
        type: "json",
        data: dataJson,
        pageSize: 20,
        batch: true,
        serverPaging: true,
        schema: {
            model: {
                id: "clientProgramId",
                fields: {
                    index: { type: "number" },
                    cycle: { type: "number" },
                    oppID: { type: "string" },
                    awardAbbr: { type: "string" },
                    awardDescription: { type: "string" },
                    partnering: { type: "string" },
                    blinded: { type: "string" },
                    receipt: { type: "date" },
                    preAppCycle: { type: "string" },
                    criteriaCount: { type: "number" },
                    action: { type: "command" }
                }
            },
            parse: function (response) {
                return response;
            },
        },
    });
    return dataSource;
}

function filterActivePrograms(isActive) {
    var ds = $("#grid").data("kendoGrid").dataSource;
    // Get dataSource's array of filters
    var curr_filters = ds.filter() != undefined ? ds.filter().filters : [];
    if (curr_filters.length > 0) {
        // Remove existing "active" filters if any
        var removedFieldIndex = -1;
        var i = 0;
        while (removedFieldIndex === -1 && i < curr_filters.length) {
            if (curr_filters[i].field === "active") {
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
var repopulateHighlightParentGrid = function() {
    // Refresh button handler
    var el = $("#grid");
    var row = el.find("tbody>tr");
    if ($(row).hasClass("gridHighlight")) {
        $(row).removeClass("gridHighlight");
    };

    $.ajax({
        url: '/Setup/GetAwardsJsonString',
        data: {
            programYearId: $(".awardSelectProgram select").val()
        }
    }).done(function (results) {
        if (results != "") {
            var grid = $("#grid").data("kendoGrid");
            var gridTotal = $("#grid").data("kendoGrid").dataSource._data;
            var ds = populateAwardDataSource(JSON.parse(results));
            ds.read();
            grid.dataSource.data(ds.data());
            grid.setDataSource(ds);
            var checked = $("#activeCheckbox").prop("checked");
            filterActivePrograms(checked);

            if ($('.ui-dialog-title:contains("Add Award")').length > 0) {
                // Gets total amount of data before added row
                var arrayTotal = [];
                for (i = 0; i < gridTotal.length; i++) {
                    arrayTotal.push(gridTotal[i].programMechanismId);
                }

                // Gets total amount of data after added row
                var gridTotalAfter = $("#grid").data("kendoGrid").dataSource._data;
                var newArrayTotal = [];
                for (i = 0; i < gridTotalAfter.length; i++) {
                    newArrayTotal.push(gridTotalAfter[i].programMechanismId);
                }

                // Grabs the newly added row's program year ID
                var difference = [];
                $.grep(newArrayTotal, function (el) {
                    if ($.inArray(el, arrayTotal) == -1) difference.push(el);
                });

                // Filters through list and if program year ID equals the object, then display the UID
                var obj = gridTotalAfter.filter(function (obj) {
                    if (difference == obj.programMechanismId) {
                        var rowID = obj.uid;
                        var el = $("#grid");
                        var row = el.find("tbody>tr[data-uid=" + rowID + "]");
                        $(row).addClass('gridHighlight');
                    }
                });
            } else {
                var gridTotalAfter = $("#grid").data("kendoGrid").dataSource._data;
                var programMechanismId = sessionStorage.getItem('programMechanismId');

                // Filters the program year ID to match the new data to highlight the row
                var obj = gridTotalAfter.filter(function (obj) {
                    var newVar = obj.programMechanismId;
                    if (programMechanismId == newVar) {
                        var rowID = obj.uid;
                        var el = $("#grid");
                        var row = el.find("tbody>tr[data-uid=" + rowID + "]");
                        $(row).addClass('gridHighlight');
                    }
                });
            }
        }
    });
    //TODO: re-populate parent grid and highlight the changed record
};



//Criterion data link
function criterionLink(programMechanismId) {
    window.location = '/Setup/CriterionSetup?programMechanismId=' + programMechanismId;
}

// Formated Datepicker for Filter
function datepicker(element) {
    var form = element.closest("form");
    var startsWith = form.find('select option:nth-child(1)').html('');
    var isNot = form.find('select option:nth-child(3)').html('');
    $(startsWith).remove();
    $(isNot).remove();
    element.kendoDatePicker({
        format: "MM/dd/yyyy",
        parseFormats: ["MM-dd-yyyy"]
    });
}

// Yes/No Drop down for Filter
function yesFilter(element) {
    var form = element.closest("form");
    form.find(".k-filter-help-text:first").text("Select an item from the list:");
    form.find("select").remove();
    element.kendoDropDownList({
        dataSource: ["Yes", "No"],
        optionLabel: "Select"
    });
}