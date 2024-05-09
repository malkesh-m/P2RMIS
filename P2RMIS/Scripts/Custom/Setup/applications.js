//#region Upload Functionality
$(function () {
    // Selectors
    var failureMessage = $('#failureMessage'),
        successMessage = $('#successMessage'),
        validationMessage = $('#failureMessage'),
        preText = $('#preText'),
        uploadButton = $('#uploadButton');

    var clientsJson, fiscalYearsJson, programYearsJson, cyclesJson, PanelsJson, AwardsJson;
    var selectYearArray = [],
        clientSelectField = $("#app-management-client"),
        yearSelectField = $("#app-management-fy"),
        programSelectField = $('#app-management-program'),
        cycleSelectField = $('#app-management-cycle'),
        panelSelectField = $('#app-management-panel'),
        awardSelectField = $('#app-management-award'),
        mainButton = $('#app-management-button'),
        instructions = $('#filter-instructions'),

        selectHeader = $(".afterComment"),
        mainContent = $("#mainContentArea"),
        uploadSelectField = $("#uploadButtons, #applicationGrid"),
        searchSelectField = $('#app-management-application'),
        grid = $("#applicationGrid");

    // Page Calls
    var applicationCalls = {
        setClients:
            function () {
                $.ajax({
                    url: '/Setup/GetClientsJson'
                }).done(function (results) {
                    clientsJson = results;
                    clientSelectField.find("option:gt(0)").remove();
                    $.each(results, function (i, item) {
                        clientSelectField.append($("<option/>")
                            .attr("value", item.ClientId).html(item.ClientName));
                    });
                    var clientLength = $('#app-management-client option').length;
                    programSelectField.attr('disabled', true);
                    cycleSelectField.attr('disabled', true);
                    panelSelectField.attr('disabled', true);
                    awardSelectField.attr('disabled', true);
                    if (clientLength === 2) {
                        $('#app-management-client option:nth-child(2)').attr('selected', true);
                        yearSelectField.attr('disabled', false);
                        clientSelectField.trigger("change");
                    } else {
                        yearSelectField.attr('disabled', true);
                        if (sessionStorage.getItem("RM_Client")) {
                            clientSelectField.val(sessionStorage.getItem("RM_Client"));
                            clientSelectField.trigger("change");
                        }
                    }
                });
            },
        setFiscalYears:
            function () {
                $.each(fiscalYearsJson, function (i, item) {
                    yearSelectField.append($("<option/>")
                        .attr("value", item.YearValue).html(item.YearText));
                });
                if (sessionStorage.getItem("RM_Year")) {
                    yearSelectField.val(sessionStorage.getItem("RM_Year"));
                    yearSelectField.trigger("change");
                }
            },
        setProgramYears:
            function () {
                $.each(programYearsJson, function (i, item) {
                    programSelectField.append($("<option/>")
                        .attr("value", item.ProgramYearId).html(item.ProgramAbbr)
                        .attr("data-name", item.ProgramName));
                });
                if (sessionStorage.getItem("RM_Program")) {
                    programSelectField.val(sessionStorage.getItem("RM_Program"));
                    programSelectField.trigger("change");
                }
            },
        setPanels:
            function () {
                $.each(panelsJson, function (i, item) {
                    panelSelectField.append($("<option/>")
                        .attr("value", item.PanelId).html(item.PanelAbbreviation)
                        .attr("data-name", item.PanelAbbreviation));
                });
                if (sessionStorage.getItem("RM_Panel")) {
                    panelSelectField.val(sessionStorage.getItem("RM_Panel"));
                    panelSelectField.trigger("change");
                    mainButton.trigger("click");
                }
            },
        setCycles:
            function () {
                $.each(cyclesJson, function (i, item) {
                    cycleSelectField.append($("<option/>")
                        .attr("value", item).html(item)
                        .attr("data-name", item));
                });
                if (sessionStorage.getItem("RM_Cycle")) {
                    cycleSelectField.val(sessionStorage.getItem("RM_Cycle"));
                    cycleSelectField.trigger("change");
                    mainButton.trigger("click");
                }
            },
        setAwards:
            function () {
                $.each(AwardsJson, function (i, item) {
                    awardSelectField.append($("<option/>")
                        .attr("value", item.AwardTypeId).html(item.AwardAbbreviation)
                        .attr("data-name", item.AwardAbbreviation));
                });
                if (sessionStorage.getItem("RM_Award")) {
                    awardSelectField.val(sessionStorage.getItem("RM_Award"));
                    awardSelectField.trigger("change");
                    mainButton.trigger("click");
                }
            },
        setApplicationGrid:
            function () {
                var fiscalYear = yearSelectField.val();
                if (fiscalYear === "Select FY") {
                    fiscalYear = "";
                }
                kendoLoad();
                $.ajax({
                    url: '/Setup/GetApplicationsJson',
                    data: {
                        clientId: clientSelectField.val(),
                        fiscalYear: fiscalYear,
                        programYearId: programSelectField.val(),
                        receiptCycle: cycleSelectField.val(),
                        panelId: panelSelectField.val(),
                        awardId: awardSelectField.val(),
                        logNumber: searchSelectField.val()
                    }
                }).done(function (results) {
                    //TODO: if referral mapping data exist, but has panelApplications
                    // do not allow to reset, or release the applications associated with the panel(s)
                    if (results.applications.length > 0) {
                        if (results.applications != null && results.applications.length > 0) {
                            mainContent.removeClass('hidden');
                            $('#noResultsFound').addClass('hidden');
                            var grid = $("#applicationGrid").data("kendoGrid");
                            var ds = populateDataSource(results.applications);
                            ds.read();
                            grid.dataSource.data(ds.data());
                            grid.refresh();

                            var clientField = $('#app-management-client option:selected').text();
                            var fyField = $('#app-management-fy option:selected').text();
                            var programField = $('#app-management-program option:selected').text();
                            var cycleField = $('#app-management-cycle option:selected').text();
                            var panelField = $('#app-management-panel option:selected').text();
                            var awardField = $('#app-management-award option:selected').text();
                            var searchField = $('#app-management-application').val();

                            // Set Up Header
                            var client = (clientField == "Select Client") ? "" : clientField;
                            var fy = (fyField == "Select FY") ? "" : fyField;
                            var program = (programField == "Select Program") ? "" : programField;
                            var cycle = (cycleField == "Select Cycle") ? "" : cycleField;
                            var panel = (panelField == "Select Panel") ? "" : panelField;
                            var award = (awardField == "Select Award") ? "" : awardField;
                            var search = (searchField == "") ? "" : searchField;
                            var headerTitle = "";

                            if (client != "") headerTitle += client + " - ";
                            if (fy != "") headerTitle += fy + " - ";
                            if (program != "") headerTitle += program + " - ";
                            if (cycle != "") headerTitle += cycle + " - ";
                            if (panel != "") headerTitle += panel + " - ";
                            if (award != "") headerTitle += award + " - ";
                            if (search != "") { headerTitle += '"' + search + '"' } else {
                                ""
                            };

                            $('.result-header').text(headerTitle);
                            // Total data in Grid
                            var sourcedata = ds.data();
                            $('#totalCount').text(sourcedata.length);

                            // 508 Labels
                            setTimeout(function () {
                                // First Pager Input
                                var pageTextBox = $('#applicationGrid div:nth-child(2) input.k-textbox');
                                pageTextBox.attr('id', 'k-textBox');
                                pageTextBox.closest('.k-pager-input').append('<label for="k-textBox" class="sr-only">Page</label>');

                                var pageSelectBox = $('#applicationGrid div:nth-child(2) .k-pager-sizes select');
                                pageSelectBox.attr('id', 'k-textSelect');
                                pageSelectBox.closest('.k-widget').append('<label for="k-textSelect" class="sr-only">Items per Page</label>');

                                // Second Pager Input
                                var pageTextBox2 = $('#applicationGrid .k-grid-pager:last-child input.k-textbox');
                                pageTextBox2.attr('id', 'k-textBox-two');
                                pageTextBox2.closest('.k-pager-input').append('<label for="k-textBox-two" class="sr-only">Page</label>');

                                var pageSelectBox2 = $('#applicationGrid .k-grid-pager:last-child .k-pager-sizes select');
                                pageSelectBox2.attr('id', 'k-textSelect-two');
                                pageSelectBox2.closest('.k-widget').append('<label for="k-textSelect-two" class="sr-only">Items per Page</label>');
                            }, 500);
                        }
                    } else {
                        mainContent.addClass('hidden');
                        $('#noResultsFound').removeClass('hidden');
                    }
                    kendoUnload();
                });
                kendoUnload();
            },
        onLoad:
            function () {
                applicationCalls.setClients();
                mainButton.prop("disabled", true);

                // Client drop-down change event handler
                clientSelectField.on("change", function () {
                    yearSelectField.find("option:gt(0)").remove();
                    programSelectField.find("option:gt(0)").remove();
                    programSelectField.attr('disabled', true);
                    cycleSelectField.find("option:gt(0)").remove();
                    cycleSelectField.attr('disabled', true);
                    panelSelectField.find('option:gt(0)').remove();
                    awardSelectField.find('option:gt(0)').remove();
                    panelSelectField.attr('disabled', true);
                    awardSelectField.attr('disabled', true);
                    mainButton.prop("disabled", true);
                    selectHeader.removeClass("hidden");
                    $('.k-delete').click();

                    if (sessionStorage.getItem("RM_Client") !== clientSelectField.val()) {
                        sessionStorage.setItem("RM_Client", clientSelectField.val());
                        sessionStorage.removeItem("RM_Year");
                        sessionStorage.removeItem("RM_Program");
                        sessionStorage.removeItem("RM_Cycle");
                    }
                    var originalSelect = $('#app-management-client option:selected').text();
                    if (originalSelect === 'Select Client') {
                        yearSelectField.attr('disabled', true);
                    } else {
                        yearSelectField.attr('disabled', false);
                        $.ajax({
                            url: '/Setup/GetFiscalYearsJson',
                            data: { clientId: clientSelectField.val() }
                        }).done(function (results) {
                            var arrayTotal = [];
                            $.each(results, function (i, item) {
                                if (item.IsActive === true) {
                                    arrayTotal.push(item);
                                }
                            });
                            fiscalYearsJson = arrayTotal;
                            applicationCalls.setFiscalYears(fiscalYearsJson);
                        });
                    }
                    searchFieldSet();
                });
                // Fiscal Year drop-down change event handler
                yearSelectField.on("change", function () {
                    var originalSelect = $('#app-management-fy option:selected').text();
                    programSelectField.find("option:gt(0)").remove();
                    cycleSelectField.find("option:gt(0)").remove();
                    panelSelectField.find("option:gt(0)").remove();
                    awardSelectField.find("option:gt(0)").remove();
                    cycleSelectField.attr('disabled', true);
                    panelSelectField.attr('disabled', true);
                    awardSelectField.attr('disabled', true);
                    mainButton.prop("disabled", true);
                    selectHeader.removeClass("hidden");
                    $('.k-delete').click();

                    if (sessionStorage.getItem("RM_Year") !== yearSelectField.val()) {
                        sessionStorage.setItem("RM_Year", yearSelectField.val());
                        sessionStorage.removeItem("RM_Program");
                        sessionStorage.removeItem("RM_Cycle");
                    }
                    if (originalSelect === 'Select FY') {
                        programSelectField.attr('disabled', true);
                    } else {
                        programSelectField.attr('disabled', false);
                        $.ajax({
                            url: '/Setup/GetProgramYearsJson/',
                            data: {
                                clientId: clientSelectField.val(),
                                fiscalYear: yearSelectField.val()
                            }
                        }).done(function (results) {
                            programYearsJson = results;
                            applicationCalls.setProgramYears();
                        });
                    }
                    searchFieldSet();
                });
                // Program drop-down change event handler
                programSelectField.on("change", function () {
                    // Set program years drop-down
                    var originalSelect = $('#app-management-program option:selected').text();
                    cycleSelectField.find("option:gt(0)").remove();
                    panelSelectField.find("option:gt(0)").remove();
                    awardSelectField.find("option:gt(0)").remove();
                    mainButton.prop("disabled", false);
                    instructions.hide();
                    selectHeader.removeClass("hidden");
                    $('.k-delete').click();

                    if (sessionStorage.getItem("RM_Program") !== programSelectField.val()) {
                        sessionStorage.setItem("RM_Program", programSelectField.val());
                        sessionStorage.removeItem("RM_Cycle");
                        sessionStorage.removeItem("RM_Panel");
                    }
                    if (originalSelect === 'Select Program') {
                        cycleSelectField.attr('disabled', true);
                        panelSelectField.attr('disabled', true);
                        awardSelectField.attr('disabled', true);
                        mainButton.prop("disabled", true);
                        instructions.show();
                    } else {
                        cycleSelectField.attr('disabled', false);
                        panelSelectField.attr('disabled', false);
                        awardSelectField.attr('disabled', false);
                        mainButton.prop("disabled", false);
                        instructions.hide();
                        $.ajax({
                            url: '/PanelManagement/GetCycles/',
                            data: {
                                programYearId: programSelectField.val()
                            }
                        }).done(function (results) {
                            cyclesJson = results;
                            applicationCalls.setCycles();
                        });
                        $.ajax({
                            url: '/Setup/GetPanelsJson/',
                            data: {
                                programYearId: programSelectField.val()
                            }
                        }).done(function (results) {
                            panelsJson = results;
                            applicationCalls.setPanels();
                        });

                    }
                    searchFieldSet();
                });
                // Cycle drop-down change event handler
                cycleSelectField.on("change", function () {
                    var originalSelect = $('#app-management-cycle option:selected').text();
                    awardSelectField.find("option:gt(0)").remove();
                    $('.k-delete').click();

                    sessionStorage.setItem("RM_Cycle", cycleSelectField.val());
                    if (originalSelect === 'Select Cycle') {
                        awardSelectField.attr('disabled', true);
                        selectHeader.removeClass("hidden");
                    } else {
                        awardSelectField.attr('disabled', false);
                        selectHeader.addClass("hidden");
                        $.ajax({
                            url: '/SummaryStatement/GetAwardsJson/',
                            data: {
                                selectedFY: programSelectField.val(),
                                selectedCycle: cycleSelectField.val(),
                                selectedPanel: panelSelectField.val()
                            }
                        }).done(function (result) {
                            AwardsJson = result;
                            applicationCalls.setAwards();
                        });
                    }
                    searchFieldSet();
                });
                // Panel drop-down change event handler
                panelSelectField.on("change", function () {
                    var originalSelect = $('#app-management-panel option:selected').text();
                    awardSelectField.find("option:gt(0)").remove();
                    $('.k-delete').click();

                    sessionStorage.setItem("RM_Panel", panelSelectField.val());
                    if (originalSelect === 'Select Panel') {
                        selectHeader.removeClass("hidden");
                    } else {
                        awardSelectField.attr('disabled', false);
                        selectHeader.addClass("hidden");
                    }
                    $.ajax({
                        url: '/SummaryStatement/GetAwardsJson/',
                        data: {
                            selectedFY: programSelectField.val(),
                            selectedCycle: cycleSelectField.val(),
                            selectedPanel: panelSelectField.val()
                        }
                    }).done(function (result) {
                        AwardsJson = result;
                        applicationCalls.setAwards();
                    });
                    searchFieldSet();
                });
                // Main button click event handler
                mainButton.on("click", function () {
                    // Display referral mapping grid if found
                    // or upload section if new
                    applicationCalls.setApplicationGrid();
                });
                // Withdrawn modal
                $(document).on('click', '.withdrawApplication', function (e) {
                    var gettarget = e.target;
                    var getId = $(gettarget).closest('tr').find('.application-link').text();
                    var getAppId = $(gettarget).closest('tr').find('.application-link').attr('data-appId');
                    // load the data via ajax
                    var title = "Update Withdrawn Status - " + getId + "";
                    $.get('/Setup/GetWithdrawModal', { applicationId: getAppId },
                        function (responseText, textStatus, XMLHttpRequest) {
                            p2rims.modalFramework.displayModalNoEvent(responseText, p2rims.modalFramework.customModalSizes.medium, title);
                            $('.modal-footer').remove();
                            $('.ui-dialog').append('<div class="modal-footer"></div>');
                            $(".modal-footer").append(p2rims.modalFramework.defaultModalFooters.cancelSaveWithdrawFooter);
                            $('#applicationIdValue').val(getId);
                            $("a[id='closeModal'], button[id='closeDialogBtn']").click(function () { $('.ui-dialog-titlebar-close').click(); });
                        }
                    );
                });
                //get application info
                $(document).on('click', '.application-link', function (e) {
                    var gettarget = e.target;
                    var getId = $(gettarget).closest('tr').find('.application-link').text();
                    var getAppId = $(gettarget).closest('tr').find('.application-link').attr('data-appId');

                    // load the data via ajax
                    var title = 'View Application ' + getId;
                    $.get("/PanelManagement/PIInformation", { applicationId: getAppId },
                        function (responseText, textStatus, XMLHttpRequest) {
                            p2rims.modalFramework.displayModalNoEvent(responseText, p2rims.modalFramework.customModalSizes.large, title);
                            if ($('.modal-footer').is(':visible')) {
                                $('.modal-footer').remove();
                            }
                            $('.ui-dialog').append('<div class="modal-footer"></div>');
                            $(".modal-footer").append(p2rims.modalFramework.defaultModalFooters.closeFooter);
                            $("a[id='closeModal'], button[id='closeDialogBtn']").click(function () { $('.ui-dialog-titlebar-close').click(); });
                        });
                });
                // Reset modal
                $(document).on('click', '.resetApplication', function (e) {
                    var gettarget = e.target;
                    var getId = $(gettarget).closest('tr').find('.application-link').text();
                    var getAppId = $(gettarget).closest('tr').find('.application-link').attr('data-appId');
                    $('#ResetApplicationId').val(getAppId);
                    // load the data via ajax
                    var title = '<span class="modalSmallCaption modalWarningCaption">Reset Warning</span>';
                    $.get('/Setup/RemoveWarning',
                        {},
                        function (responseText, textStatus, XMLHttpRequest) {
                            p2rims.modalFramework.displayModalNoEvent(responseText,
                                p2rims.modalFramework.customModalSizes.medium,
                                title);
                            $('.modal-footer').remove();
                            $('.p2rmis-disallowed_p')
                                .text(
                                    'Please select CONFIRM to reset the Withdrawn status. Otherwise, select CANCEL to return to the previous page.');
                            $('.ui-dialog').append('<div class="modal-footer"></div>');
                            $(".modal-footer").append(p2rims.modalFramework.defaultModalFooters.confirmResetFooter);
                            $("a[id='closeModal'], button[id='closeDialogBtn']").click(function () {
                                $('.ui-dialog-titlebar-close').click();
                            });
                            // Reset
                            $(document).on('click', '#confirmResetWarning',
                                function (e) {
                                    e.stopImmediatePropagation();
                                    var resetId = $('#ResetApplicationId').val();
                                    $.get('/Setup/ConfirmResetWithdrawStatus', { applicationId: resetId },
                                        function () {

                                        }
                                    );
                                    $('.ui-dialog-titlebar-close').click();
                                    $('#app-management-button').click();
                                });
                        });
                }

                );

            },
        kendoUpload:
            function () {
                // File upload
                $("#fileBase").kendoUpload({
                    async: {
                        saveUrl: "ProcessReferralMappingExcelFile",
                        removeUrl: "remove",
                        autoUpload: true,
                    },
                    localization: {
                        select: "Browse"
                    },
                    multiple: false,
                    select: applicationCalls.onSelect,
                    upload: applicationCalls.onUpload,
                    success: applicationCalls.onSuccess,
                    remove: function (e) {
                        fileUidToRemove = e.files[0].uid;
                        e.preventDefault();
                        $(".k-upload-files li").each(function (i, item) {
                            var dataId = $(this).attr("data-uid");
                            if (fileUidToRemove == dataId) {
                                $(this).remove();
                            }
                        });
                        uploadButton.attr('disabled', true);
                        preText.addClass('displayNone');
                        validationMessage.empty();
                        $('.k-upload-status.k-upload-status-total').addClass('displayNone');
                        successMessage.hide();
                    }
                });
            },
        failureMessageCall:
            function () {
                var failureMade = failureMessage.length;
                if (failureMade > 0) {
                    if (failureMessage[0].innerHTML !== "") {
                        failureMessage.text('').hide();
                        successMessage.text('').hide();
                        validationMessage.text('');
                        preText.text('');
                        validationMessage.empty();
                    }
                }
            },
        allFunctions:
            function () {
                applicationCalls.kendoUpload();
                applicationCalls.onLoad();
            }
    };
    // Call all functions
    $(document).ready(applicationCalls.allFunctions);
    //#endregion

    //#region Kendo Grid
    // Popultate data source for grid
    function populateDataSource(dataJson) {
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
                        ApplicationId: { type: "number" },
                        Index: { type: "number" },
                        PanelName: { type: "string" },
                        SessionPanelId: { type: "number" },
                        Withdrawn: { type: "boolean" },
                        WithdrawnBy: { type: "string" },
                        Status: { type: "string" },
                        Title: { type: "string" },
                        PiOrganziation: { type: "string" },
                        PiName: { type: "string" },
                        Award: { type: "string" },
                        Panel: { type: "string" },
                        WithdrawDate: { type: "string" },
                        CanWithdraw: { type: "boolean" }
                    }
                },
                parse: function (response) {
                    return response;
                }
            }
        });
        return dataSource;
    }
    var selectAllCheckBox = 0;
    // Set up kendo grid
    $("#applicationGrid").kendoGrid({
        height: 450,
        resizable: true,
        columns: [
            { field: "LogNumber", title: "App ID", width: "100px", template: "<div class='application-link' data-appId='${ApplicationId}'><a href='\\#'>${LogNumber}</a></div>" },
            { field: "PiName", title: "PI Name", width: "130px" },
            { field: "Title", title: "Application Title", width: "250px" },
            { field: "PiOrganization", title: "PI Org" },
            { field: "Award", filterable: { multi: true }, title: "Award", width: "120px" },
            { field: "Panel", filterable: { multi: true }, title: "Panel", width: "100px" },
            { field: "WithdrawnBy", filterable: { multi: true }, title: "Withdrawn", width: "130px", template: "# if(!Withdrawn) { # <div></div> # } else if(WithdrawnBy == null) { # <div>${WithdrawnDate}</div> # } else if (WithdrawnBy == 0){ # <div class='align-left'>Admin<br/>${WithdrawnDate}</div> # } else { # <div class='align-left'>PI<br/>${WithdrawnDate}</div> # } # " },
            { field: "Action", filterable: false, title: "Action", width: "130px", template: "# if(CanWithdraw) { if(Withdrawn) { # <div><a href='\\#' class='withdrawApplication'>Update</a><br /><a href='\\#' class='resetApplication'>Reset</a></div> # } else { # <a href='\\#' class='withdrawApplication'>Withdraw</a> # } } #" }
        ],
        editable: false,
        scrollable: true,
        groupable: true,
        sortable: true,
        filterable: {
            extra: false,
            operators: {
                string: {
                    contains: "Contains",
                    startswith: "Starts with",
                    eq: "Is equal to",
                    neq: "Is not equal to",
                }
            }
        },
        filterMenuInit: onFilterMenuInit,        
        pageable: {
            input: true,
            numeric: false,
            refresh: true,
            pageSize: 20,
            responsive: false,
            pageSizes: true,
        }
    });

    function onFilterMenuInit(e) {
        if (e.field == "WithdrawnBy") {
            initCheckboxFilter.call(this, e);
        }
    }

    function initCheckboxFilter(e) {
        var popup = e.container.data("kendoPopup");
        var dataSource = this.dataSource;
        var field = e.field;
        var checkboxesDataSource = new kendo.data.DataSource({
            data: uniqueForField(dataSource.data(), field)
        });

        //var helpTextElement = e.container.children(":first").children(":first");
        //var findForm = helpTextElement.closest('form');
        //helpTextElement.closest("ul").remove();
        e.container.empty();
        e.container.append('<div><div class="k-filter-help-text" id="show-items-field">Show items with value that:</div><div><button type="submit" class="k-button k-primary">Filter</button><button type="reset" class="k-button">Clear</button></div></div>');
        //$('<div class="k-filter-help-text" id="show-items-field">Show items with value that:</div>').insertBefore('form.k-filter-menu button[type="submit"]');
        //e.container.find('button').wrapAll('<div />');
        //e.container.closest('form').find('div').wrapAll('<div />');
        var element = $("<div class='checkbox-container'></div>").insertAfter(e.container.find('.k-filter-help-text')).kendoListView({
            dataSource: checkboxesDataSource,
            template: "<label><input type='checkbox' value='#:" + field + "#'/>#:" + field + "#</label>"
        });
        element.empty();
        element.append('<label role="option" aria-selected="false"><input type="checkbox" value="Select All" class="selectAllFilter" />Select All</label><label role="option" aria-selected="false"><input type="checkbox" value="Admin" />Admin</label><label role="option" aria-selected="false"><input type="checkbox" value="PI" />PI</label><label role="option" aria-selected="false"><input type="checkbox" value="Not Withdrawn" />Not Withdrawn</label> ');

        e.container.find("[type='submit']").click(function (e) {
            e.preventDefault();
            e.stopPropagation();
            var filter = dataSource.filter() || { logic: "or", filters: [] };
            var fieldFilters = $.map(element.find(":checkbox:checked"), function (input) {
                var submitted = { "PI": 1, "Admin": 0, "Not Withdrawn": 2 };
                var filtervalue = "";
                if (input.value == "Admin") {
                    filtervalue = submitted.Admin;
                } else if (input.value == "PI") {
                    filtervalue = submitted.PI;
                } else {
                    filtervalue = null;
                }
                return {
                    field: field,
                    operator: "eq",
                    value: filtervalue
                };
            });
            if (fieldFilters.length) {
                removeFiltersForField(filter, field);
                filter.filters.push({
                    logic: "or",
                    filters: fieldFilters
                });
                dataSource.filter(filter);
            }
            popup.close();
        });
    }

    function removeFiltersForField(expression, field) {
        if (expression.filters) {
            expression.filters = $.grep(expression.filters, function (filter) {
                removeFiltersForField(filter, field);
                if (filter.filters) {
                    return filter.filters.length;
                } else {
                    return filter.field != field;
                }
            });
        }
    }
    function uniqueForField(data, field) {
        var map = {};
        var result = [];
        var item;
        for (var i = 0; i < data.length; i++) {
            item = data[i];
            if (!map[item[field]]) {
                result.push(item.toJSON());
                map[item[field]] = true;
            }
        }
        return result;
    }
    $(document).on('click', 'button[type="reset"]', function () {
        $(this).closest('form').find('.checkbox-container input').attr('checked', false);
        $("#applicationGrid").data("kendoGrid").dataSource.filter([]);
    });

    $(document).on('click', '.checkbox-container input', function () {
        var checked = $(this).is(':checked');
        var selectAll = $(this).is('.selectAllFilter');
        if (selectAll) {
            (checked) ? $(this).closest('.checkbox-container').find('label input').attr('checked', 'checked') : $(this).closest('.checkbox-container').find('label input').attr('checked', false);
        } else {
            $('.selectAllFilter').attr('checked', false);
        }
    })

    function uniqueForField(data, field) {
        var map = {};
        var result = [];
        var item;
        for (var i = 0; i < data.length; i++) {
            item = data[i];
            if (!map[item[field]]) {
                result.push(item.toJSON());
                map[item[field]] = true;
            }
        }
        return result;
    }

    // This is where we added the same toolbar thats on the bottom of the grid to the top
    var gridData = $("#applicationGrid").data("kendoGrid");
    var wrapper = $('<div class="k-pager-wrap k-grid-pager pagerTop"/>').insertBefore(gridData.element.children(".k-grid-header"));
    wrapper.kendoPager($.extend({}, gridData.options.pageable, { dataSource: gridData.dataSource }));
    gridData.element.height("").find(".pagerTop").css("border-width", "0 0 1px 0");

    // Search field
    $('input#app-management-application').keyup(function (e) {
        searchFieldSet();
    });

    function searchFieldSet() {
        var getLength = $('input#app-management-application').val().length;
        var originalSelect = $('#app-management-program option:selected').text();
        if (getLength == 0) {
            if (originalSelect === "Select Program") {
                $('#app-management-button').attr('disabled', true);
                instructions.show();
            } else {
                $('#app-management-button').attr('disabled', false);
                instructions.hide();
            }
        } else {
            if (getLength >= 4) {
                $('#app-management-button').attr('disabled', false);
                instructions.hide();
            } else {
                if (originalSelect === "Select Program") {
                    $('#app-management-button').attr('disabled', true);
                    instructions.show();
                } else {
                    $('#app-management-button').attr('disabled', false);
                    instructions.hide();
                }
            }
        }
    }

    // Refresh button that sets page to 1 after refresh
    $('a.k-pager-refresh').on('click', function (e) {
        e.preventDefault();
        applicationCalls.setApplicationGrid();
    });
    // Grid load/unload
    function kendoLoad() {
        var element = $('#loading');
        kendo.ui.progress(element, true);
    }

    // Grid load/unload
    function kendoUnLoad() {
        var element = $('#loading');
        kendo.ui.progress(element, false);
    }
});