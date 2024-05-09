$(document).ready(function () {
    $("#successMessage").html("").hide();
    $("#failureMessage").html("").hide();
});

// Show Grid on load
$('.ss-gridHeader, .ss-program-grid').show();

// Add program
$('.ss-gridHeader_addProgramButton').on('click', function (e) {  //add a click event listener on the delete button
    e.preventDefault();
    e.stopPropagation();
    var title = '<span class="modalSmallCaption modalNotificationCaption">Add Program</span>';
    $.get("/Setup/ProgramWizard", function (data) {
        p2rims.modalFramework.displayModalNoEvent(data, p2rims.modalFramework.customModalSizes.large, title);
        $('.k-combobox input').attr('maxlength', '75');
        $("#clientList").val($("#ss-program-client").val());
        $("#clientList").trigger("change");
    });
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

// Populate program data source
function populateProgramDataSource(dataJson) {
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
                    clientAbbr: { type: "string" },
                    cycles: { type: "number" },
                    awards: { type: "number" },
                    mtgs: { type: "number" },
                    action: { type: "command" }
                }
            },
            parse: function (response) {
                return response;
            },
        }
    });
    return dataSource;
}

// Program grid
$(function () {
    var clientsJson;
    var activeCheckbox = $('#activeCheckbox'),
        clientSelect = $('#ss-program-client'),
        yearSelect = $('#ss-program-fy');

    // Get clients
    function getClients() {
        $.ajax({
            url: '/Setup/GetClientsSortByFullNameJson'
        }).done(function (results) {
            clientsJson = results;
            var activeOnly = activeCheckbox.prop("checked");
            setClients(activeOnly);
        });
    }
    // Set clients drop-down
    function setClients(activeOnly) {
        clientSelect.find("option:gt(0)").remove();
        $.each(clientsJson, function (i, item) {
            if (!activeOnly || (activeOnly && item.IsActive)) {
                clientSelect.append($("<option/>")
                    .attr("value", item.ClientId).html(item.ClientFullName));
            }
        });
    }
    // Get clients on load
    getClients();

    // Client drop-down change event handler
    clientSelect.on("change", function () {
        if (clientSelect.val() !== "") {
            $.ajax({
                url: '/Setup/GetFiscalYearsJson',
                data: { clientId: clientSelect.val() }
            }).done(function (results) {
                fiscalYearsJson = results;
                var activeOnly = activeCheckbox.prop("checked");
                yearSelect.attr('disabled', false);
                setFiscalYears(activeOnly);
                clientSelect.removeClass('input-validation-error');
            });
        } else {
            yearSelect.attr('disabled', 'disabled').find("option:gt(0)").remove();
        }
    });
    // Set fiscal years drop-down
    function setFiscalYears(activeOnly) {
        yearSelect.find("option:gt(0)").remove();
        $.each(fiscalYearsJson, function (i, item) {
            if (!activeOnly || (activeOnly && item.IsActive)) {
                yearSelect.append($("<option/>")
                    .attr("value", item.YearValue).html(item.YearText));
            }
        });
        yearSelect.prop("disabled", false);
    }

    // Active filter
    activeCheckbox.on("change", function () {
        var activeOnly = $(this).prop("checked");
        setClients(activeOnly);
        yearSelect.find("option:gt(0)").remove();
    });

    $("#grid").kendoGrid({
        //dataSource: dataSource,
        height: 550,
        resizable: true,
        filterMenuInit: function (e) {
            if (e.field === "programAbbr") {
                var filterMultiCheck = this.thead.find("[data-field=" + e.field + "]").data("kendoFilterMultiCheck")
                filterMultiCheck.container.empty();
                filterMultiCheck.checkSource.sort({ field: e.field, dir: "asc" });

                filterMultiCheck.checkSource.data(filterMultiCheck.checkSource.view().toJSON());
                filterMultiCheck.createCheckBoxes();
            }
            if (e.field === "fy") {
                var filterMultiCheck = this.thead.find("[data-field=" + e.field + "]").data("kendoFilterMultiCheck")
                filterMultiCheck.container.empty();
                filterMultiCheck.checkSource.sort({ field: e.field, dir: "desc" });

                filterMultiCheck.checkSource.data(filterMultiCheck.checkSource.view().toJSON());
                filterMultiCheck.createCheckBoxes();
            }
        },
        toolbar: ["create"],
        columns: [
            { field: "index", filterable: false, title: "#", width: "45px" },
            { field: "fy", filterable: { multi: true }, title: "Fiscal Year", width: "150px" },
            { field: "programAbbr", filterable: { multi: true }, title: "Program Abbr", width: "150px" },
            { field: "programName", filterable: true, title: "Program Name", width: "250px" },
            { field: "active", filterable: false, title: "Active", width: "70px" },
            { field: "cycles", filterable: false, title: "Cycles", width: "85px", template: "<div style'text-align: right;'>${cycles}</div>" },
            { field: "awards", filterable: false, title: "Awards", width: "85px", template: "<div style'text-align: right;'><a href='\\#' onclick='awardLink(${clientId}, ${fy}, ${programYearId});return false;'>${awards}</a></div>" },
            { field: "mtgs", filterable: false, title: "Mtgs", width: "65px", template: "<div style'text-align: right;'><a href='\\#' onclick='meetingLink(${clientId}, ${fy}, ${programYearId});return false;'>${mtgs}</a></div>" },
            { field: "clientId", hidden: true },
            { field: "programYearId", hidden: true },
            { field: "clientProgramId", hidden: true },
            { field: "active", hidden: true },
            {
                command: [
                {
                    name: "editable",
                    text: "",
                    template: "<a class='k-button k-button-icontext k-grid-editable' href=''><img src='/Content/img/edit_20px_enabled.png' title='Edit Row' alt='edit icon' /></a>",
                    click: function (e) {  //add a click event listener on the delete button
                        e.preventDefault();
                        e.stopPropagation();
                        var title = '<span class="modalSmallCaption modalNotificationCaption">Edit Program</span>';
                        var grid = $("#grid").data("kendoGrid");
                        var idx = $(e.target).closest("tr").index();
                        var dataItem = grid.dataSource.view()[idx];
                        var data = { clientId: dataItem.clientId, programYearId: dataItem.programYearId };
                        var programName = dataItem.programName;
                        var programAbbr = dataItem.programAbbr;
                        var fiscalYear = dataItem.fy;
                        var active = dataItem.active;
                        //var storedProgramYear = localStorage.setItem('programYearID', programYear);

                        $.get("/Setup/ProgramWizard", data, function (data) {
                            p2rims.modalFramework.displayModalNoEvent(data, p2rims.modalFramework.customModalSizes.large, title);
                            $("a[id='closeModal'], button[id='closeDialogBtn']").click(function () { $('.ui-dialog-titlebar-close').click(); });
                            $('select[name="fyField"]').attr('disabled', 'disabled');
                            $('select[name="ClientId"]').attr('disabled', 'disabled');
                            $('.fyField').attr('disabled', 'disabled');
                            $('.k-combobox').addClass('unselectable');
                            $('.k-combobox').css('height', '25px');
                            $('.k-dropdown-wrap .k-select').css('display', 'none');
                            $('.k-dropdown-wrap').css('border', 'none');
                            $('.k-state-default, .k-input').css('background', 'none');
                            $('.kendoGridTable tr:nth-child(3) td').css('margin-top', '5px');
                            $('.k-dropdown-wrap.k-state-disabled').css('padding', '0px');
                     //       $('.k-combobox input.k-input').css({ 'webkit-appearance': 'none', 'height': '0px', 'padding-right': '0px' });
                      //      $('.k-widget.k-combobox.k-header.unselectable').text(programName);
                            $("#pnComboBox").data("kendoComboBox").text(programName);

                            $('#prgAbbrev').val(programAbbr);
                            $('.fyField').val(fiscalYear);
                            if (active === "No") {
                                $('#activate').attr('checked', false);
                            }

                            var el = $("#grid");
                            var row = el.find("tbody>tr");
                            if ($(row).hasClass("gridHighlight")) {
                                $(row).removeClass("gridHighlight");
                            };
                            var uniqueID = dataItem.uid;
                            var uniqueIDRow = el.find("tbody>tr[data-uid=" + uniqueID + "]");
                            $(uniqueIDRow).addClass('gridHighlight');
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
                        var idx = $(e.target).closest("tr").index();
                        var dataItem = grid.dataSource.view()[idx];
                        var inputData = { clientId: dataItem.clientId, programYearId: dataItem.programYearId };
                        var awards = dataItem.awards;
                        var meetings = dataItem.mtgs;
                        var program = dataItem.programName;
                        var year = dataItem.fy;
                        var clientAbbr = dataItem.clientAbbr;
                        var programAbbr = dataItem.programAbbr;

                        // Can only delete if no awards or meetings attached
                        if (awards === 0 && meetings === 0) {
                           
                                    var title = '<span class="modalSmallCaption modalWarningCaption">Warning</span>';
                                        $.get("/Setup/RemoveWarning", function (data) {
                                            p2rims.modalFramework.displayModalNoEvent(data, p2rims.modalFramework.customModalSizes.small, title);
                                            var made = "You have selected " + clientAbbr + "-" + year + "-" + program + " to be removed from the list. Please click CONFIRM to permanently remove it from the list.";
                                        var el = $("#grid");
                                        var row = el.find("tbody>tr");
                                        if ($(row).hasClass("gridHighlight")) {
                                            $(row).removeClass("gridHighlight");
                                        };
                                        $('.row-fluid p').append(made);

                                        $("a[id='closeModal'], button[id='closeDialogBtn']").click(function () { $('.ui-dialog-titlebar-close').click(); });
                                        $("button[id='saveDialogChanges']").click(function () {
                                            $('#saveDialogChanges').prop("disabled", true);
                                            $.ajax({
                                                cache: false,
                                                type: "POST",
                                                url: '/Setup/DeleteProgram',
                                                data: { clientProgramId: dataItem.clientProgramId, programYearId: dataItem.programYearId },
                                                success: function (data) {
                                                    if (!data.flag) {
                                                        for (var i = 0; i < data.messages.length; i++) {
                                                            $("#warningAlert").append($("<div/>").html(data.messages[i]));
                                                            $('#saveDialogChanges').prop("disabled", true);
                                                        }
                                                        $("#successMessage").html("").hide();
                                                        $("#failureMessage").html("Unable to remove program.").show();
                                                    } else {
                                                        $('.ui-dialog-titlebar-close').click();
                                                        findPrograms();
                                                        $("#successMessage").html("Program " + year + " - " + programAbbr + " deleted successfully "
                                                            + "for " + clientAbbr).show();
                                                        $("#failureMessage").html("").hide();
                                                    }
                                                },
                                                error: function (xhr, ajaxOptions, thrownError) {
                                                    $("#warningAlert").html("Failed to delete the program.");
                                                },
                                                complete: function (data) {
                                                    $('#saveDialogChanges').prop("disabled", true);
                                                }
                                            });
                                        });
                                    });
                        } else {
                            var title = '<span class="modalSmallCaption modalWarningCaption">Warning</span>';
                            $.get("/Setup/RemoveDisallowed", function (data) {
                                p2rims.modalFramework.displayModalNoEvent(data, p2rims.modalFramework.customModalSizes.small, title);
                                $("a[id='closeModal'], button[id='closeDialogBtn']").click(function () { $('.ui-dialog-titlebar-close').click(); });
                                var made = "You may not delete a program once awards and/or meetings have been assigned.";
                                $('.row-fluid p').append(made);
                            });
                        }
                    }
                }
                ], title: "Action", width: "85px"
            }
        ],
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
            responsive: false,
            pageSizes: true,
            pageSize: 20
        }
    });
    // Active program filter
    $('#ss-program-button').click(function () {
        findPrograms();
    });

    // This is where we added the same toolbar thats on the bottom of the grid to the top
    var grid = $("#grid").data("kendoGrid");
    var wrapper = $('<div class="k-pager-wrap k-grid-pager pagerTop"/>').insertBefore(grid.element.children(".k-grid-header"));
    wrapper.kendoPager($.extend({}, grid.options.pageable, { dataSource: grid.dataSource }));
    grid.element.height("").find(".pagerTop").css("border-width", "0 0 1px 0");

    // This is used to refresh the Kendo Grid
    function findPrograms() {
        $("#successMessage").html("").hide();
        $("#failureMessage").html("").hide();
        var clientValNext = $('#selectedClient'),
            yearValNext = $('#selectedFy'),
            activeValNext = $('#selectedActive');

        if ($('#ss-program-client option:selected').text().indexOf('Select') >= 0) {
            clientSelect.addClass('input-validation-error');
            $('#mainContentArea, .add-program').addClass('hidden');
            return false;
        } else {
            clientSelect.removeClass('input-validation-error');
            $('.ss-appliedBox_table select').removeClass('input-validation-error');
        }

        // Applied Filter Text Box
        $(clientValNext).html($('#ss-program-client option:selected').text());
        $(yearValNext).text($('#ss-program-fy option:selected').val());
        var isActive = $('#activeCheckbox').prop('checked');
        (isActive) ? activeValNext.text('Yes') : activeValNext.text('No');

        // Get programs
        $("#grid").data("kendoGrid").dataSource.data([]);
        $('#mainContentArea, .afterComment, .add-program').addClass('hidden');
        $.ajax({
            url: '/Setup/GetProgramsJsonString',
            data: {
                clientId: $('#ss-program-client option:selected').val(),
                fiscalYear: $('#ss-program-fy option:selected').val(),
                isActive: isActive
            }
        }).done(function (results) {
            if (results !== "" && JSON.parse(results) !== null &&
                    JSON.parse(results).length > 0) {                
                var grid = $("#grid").data("kendoGrid");
                grid.dataSource.filter([]);
                var ds = populateProgramDataSource(JSON.parse(results));
                ds.read();
                grid.dataSource.data(ds.data());
        //        grid.setDataSource(grid.dataSource);
                document.getElementById('grid').style.display = 'block';
                // Total data in Grid
                var sourcedata = ds.data();
                $('#totalCount span').text(sourcedata.length);
                $('#mainContentArea').removeClass('hidden');
            } else {
                $('.add-program').removeClass('hidden');
            }
        });
    }
    // Refresh button handler
    $('a.k-pager-refresh').on('click', function () {
        if ($("#ss-program-client").val() != "") {
            findPrograms();
        }
    });
});

// Awards data link
function awardLink(clientId, fy, programYearId) {
    var isChecked = $('#activeProgramFilter').is(':checked');
    sessionStorage.setItem("SS_ClientId", clientId);
    sessionStorage.setItem("SS_FiscalYear", fy);
    sessionStorage.setItem("SS_ProgramYearId", programYearId);
    sessionStorage.setItem("SS_ActiveProgram", isChecked);
    window.location = "/Setup/AwardSetup";
}

// Meeting data link
function meetingLink(clientId, fy, programYearId) {
    sessionStorage.setItem("SS_ClientId", clientId);
    sessionStorage.setItem("SS_FiscalYear", fy);
    sessionStorage.setItem("SS_ProgramYearId", programYearId);
    sessionStorage.setItem("SS_ActiveProgram", $('#activeCheckbox').prop('checked'));
    sessionStorage.setItem("SS_ActiveMeeting", false);
    window.location = "/Setup/MeetingSetup";
}

// Re-populate parent grid after adding/updating/deleting
var repopulateHighlightParentGrid = function () {
    // Refresh button handler
    $.ajax({
        url: '/Setup/GetProgramsJsonString',
        data: {
            clientId: $('#ss-program-client option:selected').val(),
            fiscalYear: $('#ss-program-fy option:selected').val()
        }
    }).done(function (results) {
        if (results !== "" && JSON.parse(results) !== null &&
                JSON.parse(results).length > 0) {
            var grid = $("#grid").data("kendoGrid");
            var gridTotal = $("#grid").data("kendoGrid").dataSource._data;
            var ds = populateProgramDataSource(JSON.parse(results));
            ds.read();
            grid.dataSource.data(ds.data());
            grid.setDataSource(ds);

            if ($('.ui-dialog-title:contains("Add Program")').length > 0) {
                // Gets total amount of data before added row
                var arrayTotal = [];
                for (i = 0; i < gridTotal.length; i++) {
                    arrayTotal.push(gridTotal[i].programYearId);
                }

                // Gets total amount of data after added row
                var gridTotalAfter = $("#grid").data("kendoGrid").dataSource._data;
                var newArrayTotal = [];
                for (i = 0; i < gridTotalAfter.length; i++) {
                    newArrayTotal.push(gridTotalAfter[i].programYearId);
                }

                // Grabs the newly added row's program year ID
                var difference = [];
                $.grep(newArrayTotal, function (el) {
                    if ($.inArray(el, arrayTotal) == -1) difference.push(el);
                });

                // Filters through list and if program year ID equals the object, then display the UID
                var obj = gridTotalAfter.filter(function (obj) {
                    if (difference == obj.programYearId) {
                        var rowID = obj.uid;
                        var el = $("#grid");
                        var row = el.find("tbody>tr[data-uid=" + rowID + "]");
                        $(row).addClass('gridHighlight');
                    }
                });
            } else {
                // Gets the reset data with the new UID's
                var gridTotalAfter = $("#grid").data("kendoGrid").dataSource._data;
                var getProgramYear = localStorage.getItem('programYearID');

                // Filters the program year ID to match the new data to highlight the row
                var obj = gridTotalAfter.filter(function (obj) {
                    var newVar = obj.programYearId;
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
    //TODO: re-populate parent grid and highlight the changed record
};

// Export link event handler
$("#btnExport").click(function (e) {
    var grid = $("#grid").data("kendoGrid");
    grid.saveAsExcel();
});