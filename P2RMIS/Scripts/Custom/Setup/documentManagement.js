// Page Calls
var documentManagement = {
    backButton:
        function () {
            // Back button
            $('#backButton').on('click', function () {
                window.history.back();
            });
        },
    exportButton:
        function () {
            var grid = $("#ss-grid-dm").data("kendoGrid"),
                gridArchive = $('#ss-grid-dmArchive').data('kendoGrid'),
                gridVis = $('#ss-grid-dm').is(':visible');

            if (gridVis) {
                grid.saveAsExcel();
            } else {
                gridArchive.saveAsExcel();
            }
        },
    allFunctions:
        function () {
            documentManagement.backButton();
        }
}

// Call all functions
$(document).ready(documentManagement.backButton);

// Export link event handler
$(document).on('click', '#newExport', function (e) {
    showFileDownloadWarning(function (param) {
        documentManagement.exportButton();
    }, null);
})

// Populate program data source
function populateSessionDataSource(dataJson) {
    var dataSource = new kendo.data.DataSource({
        type: "json",
        data: dataJson,
        batch: true,
        pageSize: 20,
        sort: { field: "index", dir: "asc" },
        schema: {
            model: {
                fields: {
                    Heading: { type: "string" },
                    Description: { type: "string" },
                    DocType: { type: "string" },
                    FiscalYear: { type: "string" },
                    Program: { type: "string" },
                    Action: { type: "command" }
                }
            }
        }
    });
    return dataSource;
}

// Program grid
$(function () {
    // Initialize
    var clientsJson, fiscalYearsJson, programYearsJson;
    var selectYearArray = [],
        clientSelectField = $(".ss-filterBox_dm-table_clientSelect select"),
        awardSelectField = $(".ss-filterBox_dm-table_yearSelect select"),
        programSelectField = $('.ss-filterBox_dm-table_programSelect select'),
        mainButton = $('.ss-filterBox_dm-table_buttonShell_buttonPrimary'),
        archived = $('#archived');

    // Get clients
    function getClients() {
        $.ajax({
            url: '/Setup/GetClientsJson'
        }).done(function (results) {
            clientsJson = results;
            setClients();
        });
    }
    // Set clients drop-down
    function setClients() {
        clientSelectField.find("option:gt(0)").remove();
        $.each(clientsJson, function (i, item) {
            clientSelectField.append($("<option/>")
            .attr("value", item.ClientId).html(item.ClientName));
        });
        var clientLength = $('.ss-filterBox_dm-table_clientSelect option').length;
        if (clientLength == 2) {
            $('.ss-filterBox_dm-table_clientSelect option:nth-child(2)').attr('selected', true);
        }
    }
    // Get clients on load
    getClients();
    // Set fiscal years drop-down
    function setFiscalYears(fiscalYearsJson) {
        awardSelectField.find("option:gt(0)").remove();
        $.each(fiscalYearsJson, function (i, item) {
            awardSelectField.append($("<option/>")
                .attr("value", item.YearValue).html(item.YearText));
        });
    }
    // Client drop-down change event handler
    clientSelectField.on("change", function () {
        var originalSelect = $('.ss-filterBox_dm-table_clientSelect option:selected').text();
        if (originalSelect == 'Select Client') {
            awardSelectField.attr('disabled', 'disabled');
            awardSelectField.find("option:gt(0)").remove();
            programSelectField.find("option:gt(0)").remove();
            programSelectField.attr('disabled', 'disabled');
            mainButton.attr('disabled', 'disabled');
        } else {
            awardSelectField.attr('disabled', false);
            programSelectField.attr('disabled', false);
            mainButton.attr('disabled', false);
        }
        if (clientSelectField.val() != "Select Client") {
            $.ajax({
                url: '/Setup/GetFiscalYearsJson',
                data: { clientId: clientSelectField.val() }
            }).done(function (results) {
                var arrayTotal = [];
                var addDocYearsSession = [];
                $.each(results, function (i, item) {
                    if (item.IsActive == true) {
                        arrayTotal.push(item);
                    }
                })
                fiscalYearsJson = arrayTotal;
                $.each(fiscalYearsJson, function (i, item) {
                    addDocYearsSession.push(item.YearText);
                })
                sessionStorage.setItem('addDocYears', JSON.stringify(addDocYearsSession));
                // Sets fiscal years for all
                setFiscalYears(fiscalYearsJson);
                // Sets program years for all
                setProgramYearsAll(fiscalYearsJson);
                var selectYearOption = $('.ss-filterBox_dm-table_yearSelect option');
                selectYearArray = [];
                $.each(selectYearOption, function (e, newItem) {
                    var selectYearOptionText = $(this).text();
                    selectYearArray.push(selectYearOptionText);
                });
            });
        }

    });
    // Fiscal Year drop-down change event handler
    awardSelectField.on("change", function () {
        var originalSelect = $('.ss-filterBox_dm-table_yearSelect option:selected').text();
        if (originalSelect == 'Select Fiscal Year') {
            $.ajax({
                url: '/Setup/GetFiscalYearsJson',
                data: { clientId: clientSelectField.val() }
            }).done(function (results) {
                var arrayTotal = [];
                $.each(results, function (i, item) {
                    if (item.IsActive == true) {
                        arrayTotal.push(item);
                    }
                })
                fiscalYearsJson = arrayTotal;
                setFiscalYears(fiscalYearsJson);
                setProgramYearsAll(fiscalYearsJson);
            });
        } else {
            $.ajax({
                url: '/Setup/GetProgramsJson/',
                data: {
                    clientId: clientSelectField.val(),
                    fiscalYear: awardSelectField.val()
                }
            }).done(function (results) {
                programYearsJson = results;
                setProgramYears();
            });
        }
    });
    // Program drop-down change event handler
    programSelectField.on("change", function () {
        // Set program years drop-down
        var thisText = $('.ss-filterBox_dm-table_programSelect option:selected').text();
        $.ajax({
            url: '/Setup/GetProgramsJson',
            data: {
                clientId: clientSelectField.val(),
            }
        }).done(function (results) {
            if (thisText == 'Select Program') {
                awardSelectField.find("option").remove();
                $.each(selectYearArray, function (i, item) {
                    awardSelectField.append($("<option/>")
                        .attr("value", item).html(item));
                });
                setProgramYearsAll(selectYearArray);
            } else {
                programYearsJson = results;
                // Sets active programs from the client
                var selectedYear = $('.ss-filterBox_dm-table_yearSelect option:selected').text();
                if (selectedYear == 'Select Fiscal Year') {
                    awardSelectField.find("option:gt(0)").remove();
                    $.each(programYearsJson, function (i, item) {
                        var fiscalYearsAbbr = programYearsJson[i].ProgramAbbr;
                        var fiscalYears = programYearsJson[i].FiscalYears;
                        if (thisText == fiscalYearsAbbr) {
                            $.each(fiscalYears, function (i, years) {
                                var newFiscal = fiscalYears[i];
                                $.each(selectYearArray, function (a, array) {
                                    if (newFiscal == array) {
                                        var lengthSelect = $('.ss-filterBox_dm-table_yearSelect select option').length;
                                        if (lengthSelect < 2) {
                                            awardSelectField.append($("<option/>").attr("value", array).html(array));
                                        } else {
                                            $("<option value='" + array + "'>" + array + "</option>").insertBefore(".ss-filterBox_dm-table_yearSelect select option:nth-child(2)");

                                        }
                                    }
                                })
                            })
                        }
                    });
                }
                var yearText = $('.ss-filterBox_dm-table_yearSelect option:selected').text();
                if (yearText == "Select Fiscal Year") {
                    awardSelectField.find("option:gt(0)").remove();
                    $.each(programYearsJson, function (i, item) {
                        var fiscalYearsAbbr = programYearsJson[i].ProgramAbbr;
                        var fiscalYears = programYearsJson[i].FiscalYears;
                        if (thisText == fiscalYearsAbbr) {
                            $.each(fiscalYears, function (i, years) {
                                var newFiscal = fiscalYears[i];
                                $.each(selectYearArray, function (a, array) {
                                    if (newFiscal == array) {
                                        var lengthSelect = $('.ss-filterBox_dm-table_yearSelect select option').length;
                                        if (lengthSelect < 2) {
                                            awardSelectField.append($("<option/>").attr("value", array).html(array));
                                        } else {
                                            $("<option value='" + array + "'>" + array + "</option>").insertBefore(".ss-filterBox_dm-table_yearSelect select option:nth-child(2)");
                                        }
                                    }
                                })
                            })
                        }
                    });
                }
            }
        });
    });
    // Set program years if its specific drop-down
    function setProgramYears(fiscalYearsJson) {
        programSelectField.find("option:gt(0)").remove();
        $.each(programYearsJson, function (i, item) {
            programSelectField.append($("<option/>")
                .attr("value", item.ClientProgramId).html(item.ProgramAbbr)
                .attr("data-name", item.ProgramName));
        });
    }
    // Set program years drop-down for all values
    function setProgramYearsAll(fiscalYearsJson) {
        programSelectField.find("option:gt(0)").remove();
        $.ajax({
            url: '/Setup/GetProgramsJson',
            data: {
                clientId: clientSelectField.val(),
            }
        }).done(function (results) {
            programYearsJson = results;
            var addDocProgram = [];
            // Sets active programs from the client
            $.each(programYearsJson, function (i, item) {
                addDocProgram.push(item.ProgramAbbr);
            })
            sessionStorage.setItem('addDocPrograms', JSON.stringify(addDocProgram));
            $.each(programYearsJson, function (i, item) {
                programSelectField.append($("<option/>")
                    .attr("value", programYearsJson[i].ClientProgramId).html(programYearsJson[i].ProgramAbbr)
                    .attr("data-name", programYearsJson[i].ProgramName));
            });
        });
    };
    // Refresh button handler
    $('body').on('click', '#ss-grid-dm a.k-pager-refresh', function (e) {
        e.preventDefault();
        setSessionGrid();
    });
    // Refresh button handler
    $('body').on('click', '#ss-grid-dmArchive a.k-pager-refresh', function (e) {
        e.preventDefault();
        setArchiveGrid();
        var bottomPager = $('#ss-grid-dmArchive .k-pager-wrap:last-child .k-pager-info.k-label').text();
        $('#ss-grid-dmArchive div:nth-child(3) .k-pager-info.k-label').text(bottomPager);
    });
    // In Use Grid
    $("#ss-grid-dm").kendoGrid({
        height: 550,
        resizable: true,
        toolbar: ["create"],
        columns: [
            { field: "Heading", filterable: true, title: "Heading", width: "300px", template: "<a href='\\#' data-link='${IsUrl}' class='linkPage' data-trainingId='${TrainingCategoryId}' data-docTypeId='${DocumentTypeId}' data-path='${Path}' data-id='${DocumentId}' data-video='${IsVideo}'>${Heading}</a>" },
            { field: "Description", filterable: false, width: "300px", title: "Description", template: "<span class='overflow'>${Description}</span>" },
            { field: "DocType", filterable: true, width: "200px", title: "Doc Type" },
            { field: "FiscalYear", filterable: { multi: true }, width: "100px", title: "FY" },
            { field: "Program", filterable: { multi: true }, width: "185px", title: "Program" },
            { field: "IsUrl", hidden: true },
            { field: "IsVideo", hidden: true },
            { field: "DocumentId", hidden: true },
            { field: "Path", hidden: true },
            { field: "TrainingCategoryId", hidden: true },
            { field: "DocumentTypeId", hidden: true },
            {
                command: [
                {
                    name: "editable",
                    text: "",
                    template: "<a class='k-button k-button-icontext k-grid-editable' href=''><img src='/Content/img/edit_20px_enabled.png' title='Edit Details' alt='Edit Details' /></a>",
                    click: function (e) {  //add a click event listener on the delete button
                        e.preventDefault();
                        e.stopPropagation();
                        var selectedHeading = $(e.target).closest('tr').find('td:nth-child(1) a').text();
                        var selectedDescription = $(e.target).closest('tr').find('td:nth-child(2) a').text();
                        var selectedText = $('.ss-filterBox_dm-table_clientSelect option:selected').text();
                        var selectedTextValue = $('.ss-filterBox_dm-table_clientSelect option:selected').val();
                        var selectedYears = $('.ss-filterBox_dm-table_yearSelect option:selected').text();
                        var selectedProgram = $('.ss-filterBox_dm-table_programSelect option:selected').text();
                        var clientProgramIdValue = $(".ss-filterBox_dm-table_programSelect option:selected").val();

                        if (selectedYears != 'Select Fiscal Year') {
                            sessionStorage.setItem('addDocYearsSelected', selectedYears);
                        } else {
                            sessionStorage.setItem('addDocYearsSelected', null);
                        }
                        if (selectedProgram != 'Select Program') {
                            sessionStorage.setItem('addDocProgramSelected', selectedProgram);
                        } else {
                            sessionStorage.setItem('addDocProgramSelected', null);
                        }

                        //// Stored Variables
                        sessionStorage.setItem('selectedHeading', selectedHeading);
                        sessionStorage.setItem('selectedDescription', selectedDescription);
                        sessionStorage.setItem('selectedText', selectedText);
                        sessionStorage.setItem('selectedTextValue', selectedTextValue);
                        sessionStorage.setItem('clientProgramIdValue', clientProgramIdValue);
                        sessionStorage.setItem('trainingTypeIdValue', 0);


                        var documentId = $(e.target).closest('tr').find('td:nth-child(1) a').attr('data-id');
                        var extension = $(e.target).closest('tr').find('td:nth-child(1) a').attr('data-path');
                        var docTypeId = $(e.target).closest('tr').find('td:nth-child(1) a').attr('data-trainingId');
                        var documentTypeIdSet = $(e.target).closest('tr').find('td:nth-child(1) a').attr('data-docTypeId');

                        var ext = extension.split('.').pop();
                        sessionStorage.setItem('documentId', documentId);
                        sessionStorage.setItem('ext', ext);
                        if (docTypeId == 0) {
                            sessionStorage.setItem('trainingTypeIdValue', 0);
                        } else {
                            sessionStorage.setItem('trainingTypeIdValue', docTypeId);
                        }
                        sessionStorage.setItem('documentTypeIdValue', documentTypeIdSet);

                        window.location.href = "/Setup/EditDocument";
                    }
                },
                {
                    name: "Destroy",
                    text: "",
                    template: "<a class='k-button k-button-icontext k-grid-Destroy' href=''><img src='/Content/img/icon_remove_16x16.png' title='Remove Document' alt='Remove Document' /></a>",
                    click: function (e) {  //add a click event listener on the delete button
                        e.preventDefault();
                        e.stopPropagation();
                        var documentTypeIdSet = $(e.target).closest('tr').find('td:nth-child(1) a').attr('data-id');
                        var documentHeading = $(e.target).closest('tr').find('td:nth-child(1) a').text();
                        var title = '<span class="modalSmallCaption modalWarningCaption">Warning</span>';
                        $.get("/Setup/RemoveWarning", function (data) {
                            p2rims.modalFramework.displayModalNoEvent(data, p2rims.modalFramework.customModalSizes.small, title);
                            var made = "You are removing a document from the list. Please select CONFIRM to remove the document or CANCEL to retain the document and return to the list.";
                            $('.row-fluid p').append(made);
                            $("a[id='closeModal'], button[id='closeDialogBtn']").click(function () { $('.ui-dialog-titlebar-close').click(); });
                            $("button[id='saveDialogChanges']").click(function () {
                                $('#saveDialogChanges').prop("disabled", true);
                                $.ajax({
                                    cache: false,
                                    type: "POST",
                                    url: '/Setup/DeleteDocument',
                                    data: { documentId: documentTypeIdSet },
                                    success: function (data) {
                                        if (!data.flag) {
                                            for (var i = 0; i < data.messages.length; i++) {
                                                $("#warningAlert").append($("<div/>").html(data.messages[i]));
                                                $('#saveDialogChanges').prop("disabled", true);
                                            }
                                        } else {
                                            $('.ui-dialog-titlebar-close').click();
                                            setTimeout(function () {
                                                mainButton.click();
                                                $('#successMessage').text('Document \'' + documentHeading + '\' was deleted successfully.').show();
                                            }, 200);
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
                },
                {
                    name: "Remove",
                    text: "",
                    template: "<a class='k-button k-button-icontext k-grid-Remove' href=''><img src='/Content/img/archive.png' title='Archive' alt='Archive Document' /></a>",
                    click: function (e) {  //add a click event listener on the delete button
                        e.preventDefault();
                        e.stopPropagation();
                        var documentTypeIdSet = $(e.target).closest('tr').find('td:nth-child(1) a').attr('data-id');
                        var title = '<span class="modalSmallCaption modalWarningCaption">Warning</span>';
                        $.get("/Setup/RemoveWarning", function (data) {
                            p2rims.modalFramework.displayModalNoEvent(data, p2rims.modalFramework.customModalSizes.small, title);
                            var made = "You are archiving a document from the list. Please select CONFIRM to archive the document or CANCEL to retain the document and return to the list.";
                            $('.row-fluid p').append(made);
                            $("a[id='closeModal'], button[id='closeDialogBtn']").click(function () { $('.ui-dialog-titlebar-close').click(); });
                            $("button[id='saveDialogChanges']").click(function () {
                                $('#saveDialogChanges').prop("disabled", true);
                                $.ajax({
                                    cache: false,
                                    type: "POST",
                                    url: '/Setup/ArchiveDocument',
                                    data: { documentId: documentTypeIdSet },
                                    success: function (data) {
                                        if (!data.flag) {
                                            for (var i = 0; i < data.messages.length; i++) {
                                                $("#warningAlert").append($("<div/>").html(data.messages[i]));
                                                $('#saveDialogChanges').prop("disabled", true);
                                            }
                                        } else {
                                            $('.ui-dialog-titlebar-close').click();
                                            setTimeout(function () {
                                                mainButton.click();
                                                $('#successMessage').text('Your document was archived successfully.').show();
                                            }, 200);
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
            pageSizes: true,
            responsive: false,
            pageSize: 20
        },
    });

    // This is where we added the same toolbar thats on the bottom of the grid to the top
    var grid = $("#ss-grid-dm").data("kendoGrid");
    var wrapper = $('<div class="k-pager-wrap pagerTop"/>').insertBefore(grid.element.children(".k-grid-header"));
    wrapper.kendoPager($.extend({}, grid.options.pageable, { dataSource: grid.dataSource }));
    grid.element.height("").find(".pagerTop").css("border-width", "0 0 1px 0");

    // Archive grid
    $("#ss-grid-dmArchive").kendoGrid({
        height: 550,
        resizable: true,
        toolbar: ["create"],
        columns: [
            { field: "Heading", filterable: true, title: "Heading", width: "300px", template: "<a href='\\#' data-link='${IsUrl}' class='linkPage' data-trainingId='${TrainingCategoryId}' data-docTypeId='${DocumentTypeId}' data-path='${Path}' data-id='${DocumentId}' data-video='${IsVideo}'>${Heading}</a>" },
            { field: "Description", filterable: false, width: "300px", title: "Description", template: "<div class='overflow'>${Description}</div>" },
            { field: "DocType", filterable: true, width: "200px", title: "Doc Type" },
            { field: "FiscalYear", filterable: { multi: true }, width: "100px", title: "FY" },
            { field: "Program", filterable: { multi: true }, width: "185px", title: "Program" },
            { field: "DocumentId", hidden: true },
            {
                command: [
                {
                    name: "Destroy",
                    text: "",
                    template: "<a class='k-button k-button-icontext k-grid-Destroy' href=''><img src='/Content/img/icon_remove_16x16.png' title='Remove Document' alt='Remove Document' /></a>",
                    click: function (e) {  //add a click event listener on the delete button
                        e.preventDefault();
                        e.stopPropagation();
                        var documentTypeIdSet = $(e.target).closest('tr').find('td:nth-child(1) a').attr('data-id');
                        var documentHeading = $(e.target).closest('tr').find('td:nth-child(1) a').text();
                        var title = '<span class="modalSmallCaption modalWarningCaption">Warning</span>';
                        $.get("/Setup/RemoveWarning", function (data) {
                            p2rims.modalFramework.displayModalNoEvent(data, p2rims.modalFramework.customModalSizes.small, title);
                            var made = "You are removing a document from the list. Please select CONFIRM to remove the document or CANCEL to retain the document and return to the list.";
                            $('.row-fluid p').append(made);
                            $("a[id='closeModal'], button[id='closeDialogBtn']").click(function () { $('.ui-dialog-titlebar-close').click(); });
                            $("button[id='saveDialogChanges']").click(function () {
                                $('#saveDialogChanges').prop("disabled", true);
                                $.ajax({
                                    cache: false,
                                    type: "POST",
                                    url: '/Setup/DeleteDocument',
                                    data: { documentId: documentTypeIdSet },
                                    success: function (data) {
                                        if (!data.flag) {
                                            for (var i = 0; i < data.messages.length; i++) {
                                                $("#warningAlert").append($("<div/>").html(data.messages[i]));
                                                $('#saveDialogChanges').prop("disabled", true);
                                            }
                                        } else {
                                            $('.ui-dialog-titlebar-close').click();
                                            setTimeout(function () {
                                                mainButton.click();
                                                $('#successMessage').text('Document \'' + documentHeading + '\' was deleted successfully.').show();
                                            }, 200);
                                        }
                                    },
                                    error: function (xhr, ajaxOptions, thrownError) {
                                        $("#warningAlert").html("Failed to delete the document.");
                                    },
                                    complete: function (data) {
                                        $('#submitDialog').prop("disabled", true);
                                    }
                                });
                            });
                        });
                    }
                },
                {
                    name: "Remove",
                    text: "",
                    template: "<a class='k-button k-button-icontext k-grid-Remove' href=''><img src='/Content/img/restore.png' title='Restore File' alt='Restore File' /></a>",
                    click: function (e) {  //add a click event listener on the delete button
                        e.preventDefault();
                        e.stopPropagation();
                        var documentTypeIdSet = $(e.target).closest('tr').find('td:nth-child(1) a').attr('data-id');
                        var title = '<span class="modalSmallCaption modalWarningCaption">Warning</span>';
                        $.get("/Setup/RemoveWarning", function (data) {
                            p2rims.modalFramework.displayModalNoEvent(data, p2rims.modalFramework.customModalSizes.small, title);
                            var made = "You are restoring a document from the list. Please select CONFIRM to restore the document or CANCEL to retain the document and return to the list.";
                            $('.row-fluid p').append(made);
                            $("a[id='closeModal'], button[id='closeDialogBtn']").click(function () { $('.ui-dialog-titlebar-close').click(); });
                            $("button[id='saveDialogChanges']").click(function () {
                                $('#saveDialogChanges').prop("disabled", true);
                                $.ajax({
                                    cache: false,
                                    type: "POST",
                                    url: '/Setup/UnarchiveDocument',
                                    data: { documentId: documentTypeIdSet },
                                    success: function (data) {
                                        if (!data.flag) {
                                            for (var i = 0; i < data.messages.length; i++) {
                                                $("#warningAlert").append($("<div/>").html(data.messages[i]));
                                                $('#saveDialogChanges').prop("disabled", true);
                                            }
                                        } else {
                                            $('.ui-dialog-titlebar-close').click();
                                            setTimeout(function () {
                                                mainButton.click();
                                                $('#successMessage').text('Your document was restored successfully.').show();
                                            }, 200);
                                        }
                                    },
                                    error: function (xhr, ajaxOptions, thrownError) {
                                        $("#warningAlert").html("Failed to restore document.");
                                    },
                                    complete: function (data) {
                                        $('#submitDialog').prop("disabled", true);
                                    }
                                });
                            });
                        });
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
    });
    // This is where we added the same toolbar thats on the bottom of the Archive grid to the top
    var gridArchive = $("#ss-grid-dmArchive").data("kendoGrid");
    var wrapper = $('<div class="k-pager-wrap pagerTop"/>').insertBefore(gridArchive.element.children(".k-grid-header"));
    wrapper.kendoPager($.extend({}, gridArchive.options.pageable, { dataSource: gridArchive.dataSource }));
    gridArchive.element.height("").find(".pagerTop").css("border-width", "0 0 1px 0");

    // Set session grid data
    function setSessionGrid() {
        var noResultsArchive = 0;
        var noResults = 0;
        //InUse
        $.ajax({
            url: '/Setup/GetActiveDocumentsJson',
            data: {
                clientId: $(".ss-filterBox_dm-table_clientSelect option:selected").val(), fiscalYear: $(".ss-filterBox_dm-table_yearSelect option:selected").val(), clientProgramId: $(".ss-filterBox_dm-table_programSelect option:selected").val()
            }
        }).done(function (results) {
            if (results !== "" && results !== "[]") {
                var ds = populateSessionDataSource(JSON.parse(results));
                var grid = $("#ss-grid-dm").data("kendoGrid");
                ds.read();
                grid.dataSource.data(ds.data());
         //       grid.setDataSource(ds);
                $('#totalRecords span').text(ds.total());
                var activeDocs = $('#ss-grid-dm tr td').length;
                sessionStorage.setItem('activeDocs', activeDocs);
                $('#ss-grid-dm').show();
                $('#ss-grid-dmArchive').hide();
                $('.ss-gridHeader').show();
                $('.noRecords').hide();
                $('.overflow').ellipsis({
                    lines: 4,             // force ellipsis after a certain number of lines. Default is 'auto'
                    ellipClass: 'ellip',  // class used for ellipsis wrapper and to namespace ellip line
                    responsive: true      // set to true if you want ellipsis to update on window resize. Default is false
                });
            } else {
                $('#ss-grid-dm').hide();
                $('#ss-grid-dmArchive').hide();
                $('.ss-gridHeader').hide();
                $('.noRecords').show();
                noResults = 1;
                $('.overflow').ellipsis({
                    lines: 4,             // force ellipsis after a certain number of lines. Default is 'auto'
                    ellipClass: 'ellip',  // class used for ellipsis wrapper and to namespace ellip line
                    responsive: true      // set to true if you want ellipsis to update on window resize. Default is false
                });
            }
        });
    }
    function setArchiveGrid() {
        var clientId = clientSelectField.val();
        var fiscalYear = $(".ss-filterBox_dm-table_yearSelect select").val();
        var clientProgramId = $(".ss-filterBox_dm-table_programSelect select").val();
        //Archive
        $.ajax({
            url: '/Setup/GetArchivedDocumentsJson',
            data: {
                clientId: clientId, fiscalYear: fiscalYear, clientProgramId: clientProgramId
            }
        }).done(function (results) {
            if (results != "" && results != "[]") {
                var ds = populateSessionDataSource(JSON.parse(results));
                var grid = $("#ss-grid-dmArchive").data("kendoGrid");
                ds.read();
                grid.dataSource.data(ds.data());
                // Total data in Grid
                $('#totalRecords span').text(ds.total());
         //       var gridTotal = grid.setDataSource(ds);
                var activeArchiveDocs = $('#ss-grid-dmArchive tr td').length;
                sessionStorage.setItem('activeArchiveDocs', activeArchiveDocs);
                $('#ss-grid-dm').hide();
                $('#ss-grid-dmArchive').show();
                $('.ss-gridHeader').show();
                $('.noRecords').hide();
                $('.overflow').ellipsis({
                    lines: 4,             // force ellipsis after a certain number of lines. Default is 'auto'
                    ellipClass: 'ellip',  // class used for ellipsis wrapper and to namespace ellip line
                    responsive: true      // set to true if you want ellipsis to update on window resize. Default is false
                });
            } else {
                $('#ss-grid-dm').hide();
                $('#ss-grid-dmArchive').hide();
                $('.ss-gridHeader').show();
                $('.noRecords').show();
                $('.overflow').ellipsis({
                    lines: 4,             // force ellipsis after a certain number of lines. Default is 'auto'
                    ellipClass: 'ellip',  // class used for ellipsis wrapper and to namespace ellip line
                    responsive: true      // set to true if you want ellipsis to update on window resize. Default is false
                });
            }
        });
    }
    // "Find" button event handler
    $(document).on("click", ".ss-filterBox_dm-table_buttonShell_buttonPrimary", function (e) {
        e.preventDefault();
        var inUse = $('#inUse').is(':checked');
        if (inUse) {
            setSessionGrid();
            $('#inUse').attr('checked', 'checked');
            archived.attr('checked', false);
        } else {
            setArchiveGrid();
            $('#inUse').attr('checked', false);
            archived.attr('checked', 'checked');
        }
        // Validation for select fields
        if ($('.ss-filterBox_dm-table_clientSelect select :selected').filter(':contains("Select")').length > 0) {
            clientSelectField.addClass('input-validation-error');
        } else {
            clientSelectField.removeClass('input-validation-error');
        }
        var originalClientSelect = $('.ss-filterBox_dm-table_clientSelect option:selected').val();
        var originalYearSelect = $('.ss-filterBox_dm-table_yearSelect option:selected').val();
        var originalProgramSelect = $('.ss-filterBox_dm-table_programSelect option:selected').val();
        sessionStorage.setItem('originalClientSelect', originalClientSelect);
        sessionStorage.setItem('originalYearSelect', originalYearSelect);
        sessionStorage.setItem('originalProgramSelect', originalProgramSelect);
    });
    // Add document to grid
    $('#addNewDoc, #addApplication').on('click', function () {
        var selectedText = $('.ss-filterBox_dm-table_clientSelect option:selected').text();
        var selectedTextValue = $('.ss-filterBox_dm-table_clientSelect option:selected').val();
        var selectedYears = $('.ss-filterBox_dm-table_yearSelect option:selected').text();
        var selectedProgram = $('.ss-filterBox_dm-table_programSelect option:selected').text();
        var clientProgramIdValue = $(".ss-filterBox_dm-table_programSelect option:selected").val();
        var newDocIdValue = $('#ss-grid-dm tr').find('td:nth-child(1) a').attr('data-id');
        sessionStorage.setItem('selectedText', selectedText);
        sessionStorage.setItem('selectedTextValue', selectedTextValue);
        sessionStorage.setItem('clientProgramIdValue', clientProgramIdValue);
        sessionStorage.setItem('newDocId', newDocIdValue);
        window.location.href = '/Setup/AddDocument';
        if (selectedYears != 'Select Fiscal Year') {
            sessionStorage.setItem('addDocYearsSelected', selectedYears);
        } else {
            sessionStorage.setItem('addDocYearsSelected', null);
        }
        if (selectedProgram != 'Select Program') {
            sessionStorage.setItem('addDocProgramSelected', selectedProgram);
        } else {
            sessionStorage.setItem('addDocProgramSelected', null);
        }
    })
    // Radio button click to show Archive grid
    archived.on('click', function () {
        setArchiveGrid();
        $('#successMessage, #failureMessage').hide();
    });
    // Radio button click to show Active grid
    $('#inUse').on('click', function () {
        setSessionGrid();
        $('#successMessage, #failureMessage').hide();
    });
    // This will refresh selected client/year/program grid session
    setTimeout(function () {
        setSelect();
    }, 1000);
    function setSelect() {
        var getClient = sessionStorage.getItem('originalClientSelect');
        var getYear = sessionStorage.getItem('originalYearSelect');
        var getProgram = sessionStorage.getItem('originalProgramSelect');
        if (getClient == 'Select Client') {
            awardSelectField.attr('disabled', 'disabled');
            awardSelectField.find("option:gt(0)").remove();
            programSelectField.find("option:gt(0)").remove();
            programSelectField.attr('disabled', 'disabled');
            mainButton.attr('disabled', 'disabled');
        } else {
            awardSelectField.attr('disabled', false);
            programSelectField.attr('disabled', false);
            mainButton.attr('disabled', false);
        }
        if (getClient != null) {
            $.ajax({
                url: '/Setup/GetFiscalYearsJson',
                data: { clientId: getClient }
            }).done(function (results) {
                var arrayTotal = [];
                var addDocYearsSession = [];
                $.each(results, function (i, item) {
                    if (item.IsActive == true) {
                        arrayTotal.push(item);
                    }
                })
                fiscalYearsJson = arrayTotal;
                $.each(fiscalYearsJson, function (i, item) {
                    addDocYearsSession.push(item.YearText);
                })
                sessionStorage.setItem('addDocYears', JSON.stringify(addDocYearsSession));
                // Sets fiscal years for all
                setFiscalYears(fiscalYearsJson);
                // Sets program years for all
                var selectYearOption = $('.ss-filterBox_dm-table_yearSelect option'),
                    selectClientOption = $('.ss-filterBox_dm-table_clientSelect option');
                selectYearArray = [];
                $.each(selectYearOption, function (e, newItem) {
                    var selectYearOptionText = $(this).text();
                    selectYearArray.push(selectYearOptionText);
                });
                selectClientOption.length;
                if (getClient != null) {
                    selectClientOption.each(function (i, item) {
                        if ($(this).val() == getClient) {
                            $(this).attr('selected', 'selected');
                        }
                    })
                    awardSelectField.attr('disabled', false);
                    programSelectField.attr('disabled', false);
                }
                setProgramYearsAllFixed(fiscalYearsJson);
                if (getYear != 'Select Fiscal Year') {
                    selectYearOption.each(function (i, item) {
                        if ($(this).val() == getYear) {
                            $(this).attr('selected', 'selected');
                        }
                    })
                    awardSelectField.attr('disabled', false);
                    programSelectField.attr('disabled', false);
                }
                setTimeout(function () {
                    if (getProgram != null) {
                        $('.ss-filterBox_dm-table_programSelect option').each(function (i, item) {
                            if ($(this).val() == getProgram) {
                                $(this).attr('selected', 'selected');
                            }
                        })
                        awardSelectField.attr('disabled', false);
                        programSelectField.attr('disabled', false);
                    }
                    mainButton.click();
                }, 500);
            });
        }
    }

    function setProgramYearsAllFixed(fiscalYearsJson) {
        $.ajax({
            url: '/Setup/GetProgramsJson',
            data: {
                clientId: clientSelectField.val(),
            }
        }).done(function (results) {
            programYearsJson = results;
            var addDocProgram = [];
            // Sets active programs from the client
            $.each(programYearsJson, function (i, item) {
                addDocProgram.push(item.ProgramAbbr);
            })
            sessionStorage.setItem('addDocPrograms', JSON.stringify(addDocProgram));
            $.each(programYearsJson, function (i, item) {
                programSelectField.append($("<option/>")
                    .attr("value", programYearsJson[i].ClientProgramId).html(programYearsJson[i].ProgramAbbr)
                    .attr("data-name", programYearsJson[i].ProgramName));
            });
        });
    };
    // Success message
    setTimeout(function () {
        window.onbeforeunload = function (e) {
            sessionStorage.setItem('successMessageDoc', '');
        };
        var getSessionDoc = sessionStorage.getItem('successMessageDoc');
        if (getSessionDoc == "" || getSessionDoc == null) {
            $('#successMessage').hide();
        } else {
            $('#successMessage').text(getSessionDoc).show();
        }
    }, 2000);
    // Unsets saved message
    $(window).bind('beforeunload', function () {
        sessionStorage.setItem('successMessageDoc', "");
    });
    // Adds title to any cell that has ellipsis
    $(document).on('hover', '.ellip', function (e) {
        var getText = $(e.target).text();
        $(e.target).attr('title', getText);
    })
});