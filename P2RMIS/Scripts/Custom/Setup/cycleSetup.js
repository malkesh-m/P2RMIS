$(function () {
    // Initialize
    var clientsJson, fiscalYearsJson, programYearsJson;
    var activeCheckbox = $('#activeCheckbox'),
        clientSelect = $('#ss-cycle-client'),
        yearSelect = $('#ss-cycle-fy'),
        programSelect = $('#ss-cycle-program');

    // Grabbing Values for Selected Box
    $('#ss-cycle-button').click(function () {
        var selectedOption = $('.p2FilterContainer_selects option:selected').text(),
            abbrValNext = $('#selectedClient'),
            yearValNext = $('#selectedProgram'),
            activeValNext = $('#selectedActive');

        if (selectedOption.indexOf('Select') >= 0) {
            ($('#ss-cycle-client option:selected').filter(':contains("Select")').length > 0) ? clientSelect.addClass('input-validation-error') : clientSelect.removeClass('input-validation-error');
            ($('#ss-cycle-fy option:selected').filter(':contains("Select")').length > 0) ? yearSelect.addClass('input-validation-error') : yearSelect.removeClass('input-validation-error');
            ($('#ss-cycle-program option:selected').filter(':contains("Select")').length > 0) ? programSelect.addClass('input-validation-error') : programSelect.removeClass('input-validation-error');
            $('#mainContentArea, .add-cycle').addClass('hidden');
            return false;
        } else {
            $('.ss-appliedBox_table select').removeClass('input-validation-error');
        }

        // Applied Filter Text Box
        $(abbrValNext).html($('#ss-cycle-client option:selected').text());
        $(yearValNext).text($('#ss-cycle-fy option:selected').text() + " - " + $('#ss-cycle-program option:selected').text());
        var isActive = $('#activeCheckbox').attr('checked');
        (isActive) ? activeValNext.text('Yes') : activeValNext.text('No');
        setCyclesGrid();
    });

    // Get clients
    function getClients() {
        $.ajax({
            url: '/Setup/GetClientsJson'
        }).done(function (results) {
            clientsJson = results;
            var windowUrl = window.location.href,
                filtered = windowUrl.indexOf('active');

            if (filtered >= 0) {
                // Active or not 
                var setClientId = windowUrl.substring(windowUrl.indexOf("active")).split('=')[1];
                (setClientId == "true") ? $('#activeCheckbox').attr('checked', 'checked') : $('#activeCheckbox').attr('checked', false);
                var activeOnly = activeCheckbox.prop("checked");
                setClients(activeOnly);

                // Client
                var setClientVar = windowUrl.match("clientAbbr=(.*)?fy")[1];
                var setClient = setClientVar.substring(0, setClientVar.length - 1);
                $('#ss-cycle-client option').each(function (i, value) {
                    var thisText = $(this).text();
                    if (thisText == setClient) {
                       clientSelect.val($(this).val());
                    }
                })
                $.ajax({
                    url: '/Setup/GetFiscalYearsJson',
                    data: { clientId: clientSelect.val() }
                }).done(function (results) {
                    fiscalYearsJson = results;
                    var activeOnly = activeCheckbox.prop("checked");
                    setFiscalYears(activeOnly);
                    // Fiscal Year
                    $.ajax({
                        url: '/Setup/GetProgramYearsJson',
                        data: {
                            clientId: clientSelect.val(),
                            fiscalYear: yearSelect.val()
                        }
                    }).done(function (results) {
                        programYearsJson = results;
                        var activeOnly = activeCheckbox.prop("checked");
                        // Program
                        setProgramYears(activeOnly);
                        $('.ss-filterBox_award-table_buttonShell_buttonPrimary').click();
                    });
                });
            } else {
                var activeOnly = activeCheckbox.prop("checked");
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
    }
    // Get clients on load
    getClients();

    // Set fiscal years drop-down
    function setFiscalYears(activeOnly) {
        yearSelect.find("option:gt(0)").remove();
        $.each(fiscalYearsJson, function (i, item) {
            if (!activeOnly || (activeOnly && item.IsActive)) {
                yearSelect.append($("<option/>")
                    .attr("value", item.YearValue).html(item.YearText));
            }
        });

        // If coming from another page
        var windowUrl = window.location.href,
            filtered = windowUrl.indexOf('fy');

        if (filtered >= 0) {
            var setFYVar = windowUrl.match("fy=(.*)?program")[1];
            var setFy = setFYVar.substring(0, setFYVar.length - 1);
            $('#ss-cycle-fy option').each(function (i, value) {
                var thisText = $(this).text();
                if (thisText == setFy) {
                    yearSelect.val($(this).val());
                }
            })
        }
    }
    // Client drop-down change event handler
    clientSelect.on("change", function () {
        if (clientSelect.val() != "Select Client") {
            $.ajax({
                url: '/Setup/GetFiscalYearsJson',
                data: { clientId: clientSelect.val() }
            }).done(function (results) {
                fiscalYearsJson = results;
                var activeOnly = activeCheckbox.prop("checked");
                yearSelect.attr('disabled', false);
                setFiscalYears(activeOnly);
                clientSelect.removeClass('input-validation-error');
                programSelect.find("option:gt(0)").remove();
            });
        } else {
            yearSelect.attr('disabled', 'disabled').find("option:gt(0)").remove();
            programSelect.attr('disabled', 'disabled').find("option:gt(0)").remove();
        }

    });
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

        // If coming from another page
        var windowUrl = window.location.href,
            filtered = windowUrl.indexOf('programYearId');

        if (filtered >= 0) {
            var setProgramVar = windowUrl.match("programYearId=(.*)?active")[1];
            var setProgram = setProgramVar.substring(0, setProgramVar.length - 1);
            programSelect.val(setProgram);
            $('.ss-filterBox_award-table_buttonShell_buttonPrimary').click();
        }
    }
    // Fiscal year drop-down change event handler
    yearSelect.on("change", function () {
        if (yearSelect.val() != "Select FY") {
            $.ajax({
                url: '/Setup/GetProgramYearsJson',
                data: {
                    clientId: clientSelect.val(),
                    fiscalYear: yearSelect.val()
                }
            }).done(function (results) {
                programYearsJson = results;
                yearSelect.removeClass('input-validation-error');
                var activeOnly = activeCheckbox.prop("checked");
                programSelect.attr('disabled', false);
                setProgramYears(activeOnly);
            });
        } else {
            programSelect.attr('disabled', 'disabled').find("option:gt(0)").remove();
        }
    });

    programSelect.on("change", function () {
        if (programSelect.val() != "Select Program") {
            programSelect.removeClass('input-validation-error');
        }
    });
    // Active filter
    activeCheckbox.on("change", function () {
        var activeOnly = $(this).prop("checked");
        setClients(activeOnly);
        yearSelect.find("option:gt(0)").remove();
        programSelect.find("option:gt(0)").remove();
    });
    // Popultate award data source for grid
    function populateCycleDataSource(dataJson) {
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
                        Index: { type: "number" },
                        Cycle: { type: "number" },
                        Award: { type: "string" },
                        StartDate: { type: "date" },
                        EndDate: { type: "date" },
                        Action: { type: "command" }
                    }
                },
                parse: function (response) {
                    return response;
                },
            },
        });
        return dataSource;
    }
    // Set up kendo grid
    $("#grid").kendoGrid({
        height: 550,
        resizable: true,
        toolbar: ["create"],
        columns: [
            { field: "Index", filterable: false, title: "#", width: "45px" },
            { field: "Cycle", filterable: { multi: true }, title: "Cycle", width: "65px" },
            { field: "StartDate", filterable: { ui: datepicker }, title: "Start" },
            { field: "EndDate", filterable: { ui: datepicker }, title: "End" },
            { field: "Award", filterable: true, title: "Award Mechanism" },
            {
                command: [
                    {
                        name: "editable",
                        text: "",
                        template: "<a class='k-button k-button-icontext k-grid-editable' href=''><img src='/Content/img/edit_20px_enabled.png' alt='Edit Icon' title='Edit Cycle'/></a>",
                        click: function (e) {  //add a click event listener on the button
                            e.preventDefault();
                            e.stopPropagation();                            
                            // ADD FUNCTION HERE
                        }
                    },
                    {
                        name: "Destroy",
                        text: "",
                        template: "<a class='k-button k-button-icontext k-grid-Destroy' href=''><img src='/Content/img/icon_remove_16x16.png' alt='Delete Icon' title='Delete Row'/></a>",
                        click: function (e) {  //add a click event listener on the button
                            e.preventDefault();
                            e.stopPropagation();
                            var title = '<span class="modalSmallCaption modalWarningCaption">Warning</span>',
                                grid = $("#grid").data("kendoGrid"),
                                idx = $(e.target).closest("tr").index() + 3,
                                dataItem = grid.dataItem("tr:eq(" + idx + ")");

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
                                        //url: 'Add Delete URL here',
                                        //data: { programMechanismId: dataItem.programMechanismId },
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
                                                $('a.k-pager-refresh').click();
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
                    
                ], title: "Action", width: "100px",
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
            pageSize: 20,
            pageSizes: true,
        }
    });
    // Set award grid data
    function setCyclesGrid() {
        $.ajax({
            url: '/Setup/GetAwardsJsonString',
            data: {
                programYearId: programSelect.val()
            }
        }).done(function (results) {
            if (results.length > 2) {
                var grid = $("#grid").data("kendoGrid");
                var ds = populateCycleDataSource(JSON.parse(results));
                ds.read();
                grid.dataSource.data(ds.data());
                grid.setDataSource(grid.dataSource);
                grid.refresh();
                // Total data in Grid
                var sourcedata = ds.data()
                $('#totalCount span').text(sourcedata.length);
                $('#mainContentArea').removeClass('hidden');
                $('.add-cycle, .afterComment').addClass('hidden');
            } else {
                $('#mainContentArea, .afterComment').addClass('hidden');
                $('.add-cycle').removeClass('hidden');
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
        setCyclesGrid();
    });   
});

// Export link event handler
$("#btnExport").click(function (e) {
    var grid = $("#grid").data("kendoGrid");
    grid.saveAsExcel();
});

// Formated Datepicker for Filter
function datepicker(element) {
    var form = element.closest("form");
    var startsWith = form.find('select option:nth-child(1)').html('');
    var isNot = form.find('select option:nth-child(3)').html('');
    $(startsWith).remove();
    $(isNot).remove();
    element.kendoDatePicker({
        format: "MM/dd/yyyy",
        parseFormats: ["MM-dd-yyyy"],
    })
}