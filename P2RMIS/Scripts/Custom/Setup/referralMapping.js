//#region Upload Functionality
// Selectors
var failureMessage = $('#failureMessage'),
    successMessage = $('#successMessage'),
    validationMessage = $('#failureMessage'),
    preText = $('#preText'),
    uploadButton = $('#uploadButton');

var clientsJson, fiscalYearsJson, programYearsJson, cyclesJson;
var selectYearArray = [],
    clientSelectField = $(".ss-filterBox_award-table_clientSelect select"),
    yearSelectField = $(".ss-filterBox_award-table_yearSelect select"),
    programSelectField = $('.ss-filterBox_award-table_programSelect select'),
    cycleSelectField = $('.ss-filterBox_award-table_cycleSelect select'),
    mainButton = $('.ss-filterBox_award-table_buttonShell_buttonPrimary'),
    selectHeader = $(".afterComment"),
    mainContent = $("#mainContent");
    uploadSelectField = $("#uploadButtons, #referralMappingGrid"),
    resetButton = $("#reset-rm-button"),
    releaseButton = $("#release-rm-button"),
    referralMappingId = $("#referralMappingId");
    fileTemplateDownloadLink = $('#fileTemplateDownloadLink');

// Page Calls
var referralMappingCalls = {
    backButton:
        function () {
            // Back button
            $('#backButton').on('click', function () {
                window.history.back();
            });
        },
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
                var clientLength = $('.ss-filterBox_award-table_clientSelect option').length;
                programSelectField.attr('disabled', true);
                cycleSelectField.attr('disabled', true);
                if (clientLength === 2) {
                    $('.ss-filterBox_award-table_clientSelect option:nth-child(2)').attr('selected', true);
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
    setResetButtonStatus:
        function (referralResults) {
            var total = 0;
            for (var i = 0; i < referralResults.length; i++) {
                total += referralResults[i].assignTopanelTotal;
            }
            if (total > 0) {
                resetButton.prop("disabled", true);
            } else {
                resetButton.prop("disabled", false);
            }
        },
    onLoad:
        function () {
            referralMappingCalls.setClients();
            mainButton.prop("disabled", true);
            $(mainContent).add(uploadSelectField).add(successMessage).add(failureMessage).add(preText).addClass('hidden');

            // Client drop-down change event handler
            clientSelectField.on("change", function () {
                yearSelectField.find("option:gt(0)").remove();
                programSelectField.find("option:gt(0)").remove();
                programSelectField.attr('disabled', true);
                cycleSelectField.find("option:gt(0)").remove();
                cycleSelectField.attr('disabled', true);
                mainButton.prop("disabled", true);
                selectHeader.removeClass("hidden");
                $('.k-delete').click();
                $(mainContent).add(uploadSelectField).add(successMessage).add(failureMessage).add(preText).addClass('hidden');

                if (sessionStorage.getItem("RM_Client") !== clientSelectField.val()) {
                    sessionStorage.setItem("RM_Client", clientSelectField.val());
                    sessionStorage.removeItem("RM_Year");
                    sessionStorage.removeItem("RM_Program");
                    sessionStorage.removeItem("RM_Cycle");
                }
                var originalSelect = $('.ss-filterBox_award-table_clientSelect option:selected').text();
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
                        referralMappingCalls.setFiscalYears(fiscalYearsJson);
                    });
                }
            });
            // Fiscal Year drop-down change event handler
            yearSelectField.on("change", function () {
                var originalSelect = $('.ss-filterBox_award-table_yearSelect option:selected').text();
                programSelectField.find("option:gt(0)").remove();
                cycleSelectField.find("option:gt(0)").remove();
                cycleSelectField.attr('disabled', true);
                mainButton.prop("disabled", true);
                selectHeader.removeClass("hidden");
                $('.k-delete').click();
                $(mainContent).add(uploadSelectField).add(successMessage).add(failureMessage).add(preText).addClass('hidden');

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
                        referralMappingCalls.setProgramYears();
                    });
                }
            });
            // Program drop-down change event handler
            programSelectField.on("change", function () {
                // Set program years drop-down
                var originalSelect = $('.ss-filterBox_award-table_programSelect option:selected').text();
                cycleSelectField.find("option:gt(0)").remove();
                mainButton.prop("disabled", true);
                selectHeader.removeClass("hidden");
                $('.k-delete').click();
                $(mainContent).add(uploadSelectField).add(successMessage).add(failureMessage).add(preText).addClass('hidden');

                if (sessionStorage.getItem("RM_Program") !== programSelectField.val()) {
                    sessionStorage.setItem("RM_Program", programSelectField.val());
                    sessionStorage.removeItem("RM_Cycle");
                }
                if (originalSelect === 'Select Program') {
                    cycleSelectField.attr('disabled', true);
                } else {
                    cycleSelectField.attr('disabled', false);
                    $.ajax({
                        url: '/PanelManagement/GetCycles/',
                        data: {
                            programYearId: programSelectField.val()
                        }
                    }).done(function (results) {
                        cyclesJson = results;
                        referralMappingCalls.setCycles();
                    });
                }
            });
            // Cycle drop-down change event handler
            cycleSelectField.on("change", function () {
                var originalSelect = $('.ss-filterBox_award-table_cycleSelect option:selected').text();
                $('.k-delete').click();
                $(mainContent).add(uploadSelectField).add(successMessage).add(failureMessage).add(preText).addClass('hidden');

                sessionStorage.setItem("RM_Cycle", cycleSelectField.val());
                if (originalSelect === 'Select Cycle') {
                    mainButton.prop("disabled", true);
                    selectHeader.removeClass("hidden");
                } else {
                    mainButton.prop("disabled", false);
                    selectHeader.addClass("hidden");
                }
            });
            // Main button click event handler
            mainButton.on("click", function () {
                // Display referral mapping grid if found
                // or upload section if new
                $('#release-rm-button, #release-rm-button').attr('disabled', 'disabled');
                uploadButton.prop("disabled", true);
                selectAllCheckBox = 0;
                kendoLoad();
                $.ajax({
                    url: '/Setup/GetReferralMappingInitialLoadJson',
                    data: {
                        programYearId: programSelectField.val(),
                        receiptCycle: cycleSelectField.val()
                    }
                }).done(function (results) {
                    //TODO: if referral mapping data exist, but has panelApplications
                    // do not allow to reset, or release the applications associated with the panel(s)
                    if (results.flag) {
                        if (results.referralMapping != null && results.referralMapping.length > 0) {
                            uploadSelectField.removeClass("hidden");
                            var grid = $("#referralMappingGrid").data("kendoGrid");
                            var ds = populateRMDataSource(results.referralMapping);
                            ds.read();
                            grid.dataSource.data(ds.data());
                            grid.setDataSource(grid.dataSource);
                            grid.refresh();
                            $("#referralMappingGrid .k-grid-footer tbody tr td:nth-child(" + 5 + ")").addClass('align-right').text(ds.options.data[0].referredToPanelTotal);
                            $("#referralMappingGrid .k-grid-footer tbody tr td:nth-child(" + 6 + ")").addClass('align-right').text(ds.options.data[0].withdrawnTotal);
                            $("#referralMappingGrid .k-grid-footer tbody tr td:nth-child(" + 7 + ")").addClass('align-right').text(ds.options.data[0].nonCompliantTotal);
                            $("#referralMappingGrid .k-grid-footer tbody tr td:nth-child(" + 8 + ")").addClass('align-right').text(ds.options.data[0].assignTopanelTotal);

                            // Total data in Grid
                            var sourcedata = ds.data();
                            (selectAllCheckBox == 0) ? $('.selectAll').attr('disabled', 'disabled').addClass('no-pointer-events') : $('.selectAll').attr('disabled', false).removeClass('no-pointer-events');

                            $('#totalCount span').text(sourcedata.length);
                            // Set reset button status
                            referralMappingCalls.setResetButtonStatus(results.referralMapping);
                            $("#referralMappingId").val(results.referralMappingId);
                        }
                    } else {
                        mainContent.removeClass("hidden");
                    }
                    kendoUnload();
                });
            });
            // Reset button handler
            resetButton.on("click", function () {
                $.ajax({
                    url: '/Setup/ResetReferralMappingJson',
                    data: {
                        referralMappingId: referralMappingId.val()
                    }
                }).done(function (results) {
                    if (results.flag) {
                        $(".k-upload-files").remove();
                        $(".k-upload-status").remove();
                        $(".k-upload.k-header").addClass("k-upload-empty");
                        $(".k-upload-button").removeClass("k-state-focused");
                        uploadSelectField.addClass("hidden");
                        successMessage.hide();
                        mainButton.trigger("click");
                    } else {
                        // Unexpected error
                        var msgj = $("<div/>").html("There was an error resetting data. Please try again.");
                        failureMessage.append(msgj);
                    }
                });
            });
            // Release button handler
            releaseButton.on("click", function () {
                var sessionPanelIds = [];
                var releaseStats = [];
                var grid = $("#referralMappingGrid").data("kendoGrid");
                var data = grid.dataSource.data();
                for (var i = 0; i < $('.checkone').length; i++) {
                    if ($('.checkone').eq(i).is(":checked")) {
                        sessionPanelIds.push(data[i].SessionPanelId);
                        releaseStats.push({ count: data[i].ReferredToPanel, panel: data[i].PanelName });
                    }
                }
                $.ajax({
                    cache: false,
                    type: "POST",
                    url: '/Setup/ReleaseReferralMappingJson',
                    traditional: true,
                    data: {
                        referralMappingId: referralMappingId.val(),
                        sessionPanelIds: sessionPanelIds
                    },
                }).done(function (results) {
                    if (results.flag) {
                        successMessage.empty();
                        for (var i = 0; i < releaseStats.length; i++) {
                            var msgi = $("<div/>").html(releaseStats[i].count +
                                " applications assigned successfully to " + releaseStats[i].panel + ".");
                            successMessage.append(msgi);
                        }
                        failureMessage.hide();
                        successMessage.removeClass("hidden").show();
                        resetButton.prop("disabled", true);
                        setReferralMappingGrid();
                        releaseButton.attr('disabled', 'disabled');
                    } else {
                        // Unexpected error
                        var maxLen = results.errorResults.length <= 10 ? results.errorResults.length : 10;
                        for (var j = 0; j < maxLen; j++) {
                            var msgj = $("<div/>").html(results.errorResults[j]);
                            failureMessage.append(msgj);
                        }
                        failureMessage.removeClass("hidden").show();
                        successMessage.hide();
                    }
                });
            });
            // Checkbox handler
            $(document).on("click", ".checkone", function () {
                if ($.find('.checkone:checked').length > 0) {
                    releaseButton.prop("disabled", false);
                    $('.selectAll').attr({ 'disabled': false, "checked": false}).removeClass('no-pointer-events');
                } else {
                    releaseButton.prop("disabled", true);
                }
            });
            // This is for the Select All Checkbox
            $(document).on('click', '.selectAll', function () {
                $('#referralMappingGrid').find('.checkone').prop('checked', $(this).is(":checked"));
                var overallCheckbox = $(this).is(':checked');
                $('#referralMappingGrid tbody tr td:nth-child(2) input').each(function (i, value) {
                    var hasClass = $(value).hasClass('itemReleased');
                    var isChecked = $(value).attr('checked');
                    if (!hasClass) {
                        if (isChecked) {
                            releaseButton.prop("disabled", false)
                        } else {
                            releaseButton.prop("disabled", true);
                        }
                    } else {
                        $(this).attr('checked', false);
                    }
                });
            });
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
                select: referralMappingCalls.onSelect,
                upload: referralMappingCalls.onUpload,
                success: referralMappingCalls.onSuccess,
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
    onSelect:
        // Kendo Select
        function (e) {
            // Validation for files
            $(".field-validation-error").empty();
            var formData = e.files[0];
            var fileContentType = formData.extension.substring(1);
            var fileName = formData.name;
            sessionStorage.setItem('fileName', fileName);
            if (fileContentType.toLowerCase() === "xlsx") {
                $('.k-upload-files').removeClass('displayNone');
            } else {
                setTimeout(function () {
                    $('.k-button.k-upload-button').removeClass('k-state-focused');
                }, 200);
                $(".field-validation-error").append($("<div/>").html(INVALID_FILE_FORMAT).addClass("redColor"));
                $('.k-upload-files').addClass('displayNone');
                e.preventDefault();
            }
        },
    onUpload:
        // Kendo Upload
        function (e) {
            var fileBase = $('input#fileBase')[0].files[0];

            $.ajax({
                cache: false,
                type: "POST",
                data: fileBase,
                contentType: false,
                processData: false
            });
        },
    onSuccess:
        // Kendo Success
        function (e) {
            uploadButton.attr('disabled', false);
            $('.k-button.k-upload-button').removeClass('k-state-focused');
            $('.k-upload-status.k-upload-status-total, .k-upload-pct').addClass('displayNone');
            referralMappingCalls.failureMessageCall();
        },
    onRMUploadButton:
        function () {
            // Upload excel file
            uploadButton.on('click', function () {
                validationMessage.empty();
                preText.text('');
                var programYearId = $('.ss-filterBox_award-table_programSelect__select').val();
                var receiptCycle = $('.ss-filterBox_award-table_cycleSelect__select').val();
                $.ajax({
                    type: 'POST',
                    url: '/Setup/UploadReferralMapping',
                    data: { programYearId: programYearId, receiptCycle: receiptCycle }
                }).done(function (results) {
                    var errorTotal = "";
                    var errorResults = results.errorResults.Key;
                    var referralMappingId = results.errorResults.Value;
                    if (errorResults && errorResults.length > 0) {
                        var maxLen = errorResults.length < 10 ? errorResults.length : 10;
                        for (var i = 0; i < maxLen; i++) {
                            var errorMade = errorResults[i];
                            var htmlCreated = "<span>" + errorMade + "</span></br>";
                            errorTotal += htmlCreated;
                        }
                        if (errorResults.length > 10) {
                            validationMessage.text('More than 10 errors were encountered while processing your file.');
                        } else {
                            preText.text('Please correct the following errors and try again:').removeClass('hidden');
                            validationMessage.html(errorTotal);
                        }
                        validationMessage.removeClass('hidden').show();
                        uploadButton.attr('disabled', true);
                    } else {
                        var fileName = sessionStorage.getItem('fileName');
                        successMessage.text(fileName + ' uploaded successfully.').removeClass('hidden').show();
                    }
                    if (results.referralResults != null && results.referralResults.length > 0) {
                        mainContent.addClass("hidden");
                        uploadSelectField.removeClass("hidden");
                        var grid = $("#referralMappingGrid").data("kendoGrid");
                        var ds = populateRMDataSource(results.referralResults);
                        ds.read();
                        grid.dataSource.data(ds.data());
                        grid.setDataSource(grid.dataSource);
                        grid.refresh();
                        // Footer totals
                        $("#referralMappingGrid .k-grid-footer tbody tr td:nth-child(" + 5 + ")").addClass('align-right').text(ds.options.data[0].referredToPanelTotal);
                        $("#referralMappingGrid .k-grid-footer tbody tr td:nth-child(" + 6 + ")").addClass('align-right').text(ds.options.data[0].withdrawnTotal);
                        $("#referralMappingGrid .k-grid-footer tbody tr td:nth-child(" + 7 + ")").addClass('align-right').text(ds.options.data[0].nonCompliantTotal);
                        $("#referralMappingGrid .k-grid-footer tbody tr td:nth-child(" + 8 + ")").addClass('align-right').text(ds.options.data[0].assignTopanelTotal);
                        // Total data in Grid
                        var sourcedata = ds.data();
                        $('#totalCount span').text(sourcedata.length);
                        // Set reset button status
                        referralMappingCalls.setResetButtonStatus(results.referralResults);
                        $("#referralMappingId").val(referralMappingId);
                    }
                });
            });
        },
    onTemplateFileDownload:
        function () {
            // Download file template
            fileTemplateDownloadLink.on('click', function () {
                showFileDownloadWarning(undefined, undefined, "/Content/Referral_Mapping_Template.xlsx");
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
    allRMFunctions:
        function () {
            referralMappingCalls.backButton();
            referralMappingCalls.kendoUpload();
            referralMappingCalls.onRMUploadButton();
            referralMappingCalls.onTemplateFileDownload();
            referralMappingCalls.onLoad();
        }
};
// Call all functions
$(document).ready(referralMappingCalls.allRMFunctions);
//#endregion

//#region Kendo Grid
// Popultate award data source for grid
function populateRMDataSource(dataJson) {
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
                    Checkbox: { type: "boolean" },
                    PanelName: { type: "string" },
                    SessionPanelId: { type: "number" },
                    ReferredToPanel: { type: "number" },
                    WithDrawn: { type: "number" },
                    NonCompliant: { type: "number" },
                    Partnered: { type: "number" },
                    AssignedToPanel: { type: "number" },
                    Status: { type: "string" },
                    refferedToPanelTotal: { type: "number"},
                    withdrawnTotal: { type: "number" },
                    nonCompliantTotal: { type: "number" },
                    partneredTotal: { type: "number" },
                    assignTopanelTotal: { type: "number" }
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
$("#referralMappingGrid").kendoGrid({
    height: 450,
    resizable: true,
    columns: [
        { field: "Index", hidden: true },
        {
            field: "Checkbox", filterable: false, title: "<div class='textCenter'><input type=\'checkbox\' class='\selectAll\' data-bind='checked:Checkbox' style='position: absolute; top: 10px; left: 12px;' /></div>", width: "35px",
            template: function (dataItem) {
                var disabled = (dataItem.Status == "Released") ? "disabled" : "";
                var itemReleased = (dataItem.Status == "Released") ? "itemReleased" : "";
                if (dataItem.Status != "Released") selectAllCheckBox++;
                return '<div class="textCenter"><input type="checkbox" class="checkone '+ itemReleased +'"' + disabled +' /></div>';
            }, sortable: false
        },
        { field: "PanelName", title: "Panel Name", width: "300px", footerTemplate: "<div class='align-left'>Total</div>" },
        { field: "SessionPanelId", hidden: true, footerTemplate: "" },
        { field: "ReferredToPanel", title: "Referred To Panel", template: "<div class='align-right'>${ReferredToPanel}</div>", footerTemplate: "" },
        { field: "WithDrawn", title: "Withdrawn", width: "110px", template: "<div class='align-right'>${WithDrawn}</div>", footerTemplate: "" },
        { field: "NonCompliant", title: "Non-Compliant", template: "<div class='align-right'>${NonCompliant}</div>", footerTemplate: "" },
        { field: "AssignedToPanel", title: "Assigned To Panel", template: "<div class='align-right'>${AssignedToPanel}</div>", footerTemplate: "" },
        { field: "Status", title: "Status", width: "200px", footerTemplate: "" },
     ],
    editable: false,
    scrollable: true,
    groupable: true,
    sortable: false,    
    filterable: false,
    pageable: false
});

// Set referral mapping grid data
function setReferralMappingGrid() {
    selectAllCheckBox = 0;
    $.ajax({
        url: '/Setup/GetReferralMappingPostUploadJson',
        data: {
            referralMappingId: referralMappingId.val()
        }
    }).done(function (results) {
        if (results && results.referralMapping !== "") {
            var grid = $("#referralMappingGrid").data("kendoGrid");
            var ds = populateRMDataSource(results.referralMapping);
            ds.read();
            grid.dataSource.data(ds.data());
            grid.setDataSource(grid.dataSource);
            grid.refresh();
            $("#referralMappingGrid .k-grid-footer tbody tr td:nth-child(" + 5 + ")").addClass('align-right').text(ds.options.data[0].referredToPanelTotal);
            $("#referralMappingGrid .k-grid-footer tbody tr td:nth-child(" + 6 + ")").addClass('align-right').text(ds.options.data[0].withdrawnTotal);
            $("#referralMappingGrid .k-grid-footer tbody tr td:nth-child(" + 7 + ")").addClass('align-right').text(ds.options.data[0].nonCompliantTotal);
            $("#referralMappingGrid .k-grid-footer tbody tr td:nth-child(" + 8 + ")").addClass('align-right').text(ds.options.data[0].assignTopanelTotal);

            // Set select all checkbox
            var sourcedata = ds.data();
            (selectAllCheckBox == 0) ? $('.selectAll').attr('disabled', 'disabled').addClass('no-pointer-events') : $('.selectAll').attr('disabled', false).removeClass('no-pointer-events');

            // Total data in Grid
            $('#totalCount span').text(sourcedata.length);
            releaseButton.prop("disabled", true);
        }
    });
}

// Refresh button that sets page to 1 after refresh
$('a.k-pager-refresh').on('click', function (e) {
    e.preventDefault();
    setReferralMappingGrid();
});

$(document).on('mouseenter', '.ss-rm-header img', function () {
    $('.ss-rm-header_title').show();
}).on('mouseleave', '.ss-rm-header img', function () {
    $('.ss-rm-header_title').hide();
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